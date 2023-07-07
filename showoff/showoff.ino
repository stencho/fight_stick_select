#define USE_LCD false

#include <Adafruit_NeoPixel.h>
#include <gfxfont.h>
#include <Adafruit_SSD1306.h>

//PINS
#define INPUT_UP 13
#define INPUT_DOWN 12
#define INPUT_LEFT 11
#define INPUT_RIGHT 10

#define MODE_PIN_1 6
#define MODE_PIN_2 9

#define MENU_BUTTON_PIN A5

#define LED_PIN 5

//ENUMS
enum stick_position {
  UP = 1 << 0, 
  DOWN = 1 << 1, 
  LEFT = 1 << 2, 
  RIGHT = 1 << 3,
  UP_LEFT = UP | LEFT,
  UP_RIGHT = UP | RIGHT,
  DOWN_LEFT = DOWN | LEFT,
  DOWN_RIGHT = DOWN | RIGHT,
  CENTER = 0 
};

enum stick_mode : byte {
  LEFT_STICK = (1 << 0) | (1 << 1),
  DPAD = (1 << 0),
  RIGHT_STICK = (1 << 1), 
  NONE = 0
};


//JOYSTICK STATE AND FUNCTIONS
static stick_position stick_input = CENTER;
static stick_position previous_stick_input = CENTER;

static stick_position pressed;
static stick_position released;

/// @brief print n individual bits of byte b
void print_bits(byte b, int n) {
  for (int8_t i = n-1; i >= 0; i--) 
    Serial.write(bitRead(b, i) ? '1' : '0');
}

/// @brief read joystick input pins and update stick_input
static void read_stick() {
  previous_stick_input = stick_input;
  stick_position sp = CENTER;
  if (digitalRead(INPUT_UP) == LOW) { sp = (stick_position)(sp | UP); }
  if (digitalRead(INPUT_DOWN) == LOW) { sp = (stick_position)(sp | DOWN); }
  if (digitalRead(INPUT_LEFT) == LOW) { sp = (stick_position)(sp | LEFT); }
  if (digitalRead(INPUT_RIGHT) == LOW) { sp = (stick_position)(sp | RIGHT); }
  stick_input = sp;
}

/// @brief returns true if stick_input contains pos, i.e. UP in UP, UP_LEFT or UP_RIGHT
bool stick_has_pos(stick_position pos) { return ((stick_input & pos) == pos); }

//MODE STATE AND FUNCTIONS
static stick_mode current_control_mode = DPAD;
static stick_mode stored_control_mode = DPAD;

/// @brief updates the control mode output pins and current_control_mode
void change_stick_mode(stick_mode new_mode) {
  bool b1 = bitRead(new_mode, 0) == 1;
  bool b2 = bitRead(new_mode, 1) == 1;
  
  if (b1) {
    digitalWrite(MODE_PIN_1, LOW);
  } else {
    digitalWrite(MODE_PIN_1, HIGH);
  }
  
  if (b2) {
    digitalWrite(MODE_PIN_2, LOW);
  } else {
    digitalWrite(MODE_PIN_2, HIGH);
  }

  current_control_mode = new_mode;
}

void enable_controller_input() { 
  change_stick_mode(stored_control_mode);  
}

void disable_controller_input() {
  stored_control_mode = current_control_mode;
  change_stick_mode(NONE);
}

//LED VARS
Adafruit_NeoPixel strip(2, LED_PIN, NEO_GRB + NEO_KHZ800);
int led_r = 250; int led_g = 0; int led_b = 170;

//DISPLAY VARS
#define SCREEN_WIDTH 128 // OLED display width, in pixels
#define SCREEN_HEIGHT 32 // OLED display height, in pixels

#if USE_LCD
  Adafruit_SSD1306 display(SCREEN_WIDTH, SCREEN_HEIGHT, &Wire, -1);
#endif

enum LCD_menu_state { MAIN, STICK_SELECT };
LCD_menu_state current_state = MAIN;
LCD_menu_state old_state = MAIN;

bool returning_to_main = false;
bool menu_button_down = false;

#if USE_LCD
  const int stick_select_axis_display_pos_x = 64;
  const int stick_select_axis_display_pos_y = 24;
  const int stick_select_axis_display_size = 12; //stick to even numbers

  const int main_axis_display_pos_x = 15;
  const int main_axis_display_pos_y = 16;
  const int main_axis_display_size = 15;

  const int main_mode_name_pos_x = 33;
  const int main_mode_name_pos_y = 13;

  static int last_activity = 0;
  const int blank_delay = 1000;
  static bool blanked = false;

  //DRAWING FUNCTIONS
  void draw_word(char* text, int X, int Y) {draw_word(text, X, Y, WHITE, CLEAR_FEATURE);}
  void draw_word(char* text, int X, int Y, int color_fg, int color_bg) {
    int length = strlen(text);

    for (int i = 0; i < length; i++) {
      display.drawChar(X + (i * 6), Y, *(text + i), color_fg, color_bg, 1);
    }
  }

  void draw_word_centered(char* text, int X, int Y) {draw_word_centered(text, X, Y, WHITE, CLEAR_FEATURE);}
  void draw_word_centered(char* text, int X, int Y, int color_fg, int color_bg) {
    int length = strlen(text);
    draw_word(text, X-((length * 6) / 2), Y, color_fg, color_bg);
  }

  void draw_word_right_align(char* text, int X, int Y) {draw_word_right_align(text, X, Y, WHITE, CLEAR_FEATURE);}
  void draw_word_right_align(char* text, int right_side, int Y, int color_fg, int color_bg) {
    int length = strlen(text);
    draw_word(text, (right_side - (length * 6)), Y, color_fg, color_bg);
  }
#endif

//MAIN
void setup() {
  pinMode(INPUT_UP, INPUT_PULLUP);
  pinMode(INPUT_DOWN, INPUT_PULLUP);  
  pinMode(INPUT_LEFT, INPUT_PULLUP);
  pinMode(INPUT_RIGHT, INPUT_PULLUP);
  
  pinMode(MODE_PIN_1, OUTPUT);
  pinMode(MODE_PIN_2, OUTPUT);

  pinMode(MENU_BUTTON_PIN, INPUT_PULLUP);  

  change_stick_mode(DPAD);

  Serial.begin(115200);

  #if USE_LCD
    display.begin();
    display.dim(true);

    display.clearDisplay();
    display.display();

    display.setTextSize(1);
    display.setTextColor(WHITE);
  #endif
  
  //RGB lighting
  strip.begin();
  strip.setPixelColor(0, led_r, led_g, led_b);
  strip.setPixelColor(1, led_r, led_g, led_b);
  strip.show();
}


void loop() {
  if (!returning_to_main) returning_to_main = (old_state != MAIN && current_state == MAIN);
  old_state = current_state;

  strip.show();
  read_stick();

  change_stick_mode(current_control_mode);

  pressed = (stick_position)(stick_input & ~previous_stick_input);
  released = (stick_position)(~stick_input & previous_stick_input);

  menu_button_down = digitalRead(A5) == LOW;

  #if USE_LCD
    if (stick_input == CENTER && !menu_button_down) {
      last_activity++;

    } else { 
      last_activity = 0;
      blanked = false;
    }

    //blank the LCD when idle
    if (last_activity > blank_delay) { 
      if (!blanked) {
          display.clearDisplay();
          display.display();

        blanked = true;
      }
      return;
    }
  
    display.clearDisplay();  
    display.setRotation(0);
  #endif

  if (current_state == MAIN) {
    #if USE_LCD
      draw_main_screen();
    #endif

    if (returning_to_main) {
      if (stick_input == CENTER) {
        returning_to_main = false;
        enable_controller_input();
      } 
    }

    if (menu_button_down) {
      current_state = STICK_SELECT;
      disable_controller_input();
    }

  } else if (current_state == STICK_SELECT) {
    #if USE_LCD
      draw_stick_select_screen();
    #endif
    
    if (!menu_button_down) {    
      
      if (stick_input == UP) { stored_control_mode = DPAD; change_stick_mode(DPAD); current_state = MAIN; } 
      else if (stick_input == LEFT) { stored_control_mode = LEFT_STICK; change_stick_mode(LEFT_STICK); current_state = MAIN; }
      else if (stick_input == RIGHT) { stored_control_mode = RIGHT_STICK; change_stick_mode(RIGHT_STICK); current_state = MAIN; }

      else { current_state = MAIN; }
    }
  } 
  
  #if USE_LCD
    display.display(); 
  #endif
}
#if USE_LCD
//DRAWING FUNCTIONS
void draw_axis_display(int pos_x, int pos_y, int size, bool draw_line = false, bool draw_border = true, int color = WHITE) {
  int crosshair_size = (size / 2);
  if (crosshair_size % 2 != 0)  crosshair_size-=1;

  if (draw_border) {
    display.drawRect(
      pos_x - (size/2), pos_y - (size/2), 
      size+1,  size+1,
      color);
  }

  if (stick_input == CENTER) {    
    display.drawLine(
      pos_x - (crosshair_size / 2), pos_y, 
      pos_x + (crosshair_size / 2), pos_y,
      color);
    display.drawLine(
      pos_x, pos_y - (crosshair_size / 2), 
      pos_x, pos_y + (crosshair_size / 2),
      color);
  } else {
    int render_circle_x = 0;
    int render_circle_y = 0;

    if (stick_has_pos(UP)) render_circle_y -= (size/2);
    if (stick_has_pos(DOWN)) render_circle_y += (size/2);
    if (stick_has_pos(LEFT)) render_circle_x -= (size/2);
    if (stick_has_pos(RIGHT)) render_circle_x += (size/2);
    
    render_circle_x += pos_x;
    render_circle_y += pos_y;

    display.drawLine(
      pos_x, pos_y, 
      render_circle_x, render_circle_y,
      color);
  }
}

void draw_axis_display_circle(int pos_x, int pos_y, int radius, bool draw_line = false, bool draw_border = true, int color = WHITE) {
  int crosshair_size = radius;
  if (crosshair_size % 2 != 0)  crosshair_size-=1;

  if (draw_border) {
    display.drawCircle(
      pos_x, pos_y, 
      radius,color);
  }

  if (stick_input == CENTER || returning_to_main) {    
    display.drawLine(
      pos_x - (crosshair_size / 2), pos_y, 
      pos_x + (crosshair_size / 2), pos_y,
      color);
    display.drawLine(
      pos_x, pos_y - (crosshair_size / 2), 
      pos_x, pos_y + (crosshair_size / 2),
      color);
  } else {
    int render_circle_x = 0;
    int render_circle_y = 0;

    if (stick_has_pos(UP)) render_circle_y -= radius;
    if (stick_has_pos(DOWN)) render_circle_y += radius;
    if (stick_has_pos(LEFT)) render_circle_x -= radius;
    if (stick_has_pos(RIGHT)) render_circle_x += radius;

    if (stick_input == UP_LEFT 
     || stick_input == UP_RIGHT 
     || stick_input == DOWN_LEFT 
     || stick_input == DOWN_RIGHT) {
      render_circle_x /= (PI / 2);
      render_circle_y /= (PI / 2);
    }
    
    render_circle_x += pos_x;
    render_circle_y += pos_y;

    display.drawLine(
      pos_x, pos_y, 
      render_circle_x, render_circle_y,
      color);
  }
}


//MAIN/SELECT SCREEN DRAWING CODE
void draw_main_screen() {
  draw_axis_display_circle(main_axis_display_pos_x, main_axis_display_pos_y, main_axis_display_size, false, true);
  
  switch (current_control_mode) {
    case NONE: draw_word("DISABLED", main_mode_name_pos_x, main_mode_name_pos_y); break;
    case DPAD: draw_word("D-PAD", main_mode_name_pos_x, main_mode_name_pos_y); break;
    case LEFT_STICK: draw_word("LEFT STICK", main_mode_name_pos_x, main_mode_name_pos_y); break;
    case RIGHT_STICK: draw_word("RIGHT STICK", main_mode_name_pos_x, main_mode_name_pos_y); break;
  }
}

void draw_stick_select_screen() {
  draw_word_centered("D-PAD", 65, 10);
  draw_word_right_align("LEFT", 64 - (stick_select_axis_display_size / 2) - 1, 32-11);
  draw_word("RIGHT", 64 + (stick_select_axis_display_size / 2) + 3, 32-11);

  draw_axis_display(stick_select_axis_display_pos_x, stick_select_axis_display_pos_y, stick_select_axis_display_size, true, true, WHITE);

  if (stick_input == UP) 
    display.fillRect(64 - (3 * 6), 9, 6 * 6+1, 9, INVERSE);
  if (stick_input == LEFT) 
    display.fillRect(64 - (stick_select_axis_display_size / 2) - (4 * 6) - 2, 32-12, 4 * 6 + 2, 9, INVERSE);
  if (stick_input == RIGHT) 
    display.fillRect(64 + (stick_select_axis_display_size / 2)+1, 32-12, 5 * 6 + 2, 9, INVERSE);

}
#endif

