#include <MCP4131.h>
#define isLow(pin) (digitalRead(pin) == LOW)

#define PRINT_INFO false
#define USE_LEDS true

//Pins
#if USE_LEDS
#define LED_PIN 8
#endif

#define MENU_BUTTON_PIN A0

#define INPUT_UP 7
#define INPUT_DOWN 6
#define INPUT_LEFT 5
#define INPUT_RIGHT 4

#define DPAD_UP 3
#define DPAD_RIGHT 2
#define DPAD_LEFT 1
#define DPAD_DOWN 0

#define LEFT_STICK_CS_X 10
#define LEFT_STICK_CS_Y 9

#define RIGHT_STICK_CS_X A2
#define RIGHT_STICK_CS_Y A3

//LEDs
#if USE_LEDS
  #include <Adafruit_NeoPixel.h>
  #define LED_COUNT 2
  int led_r = 250; int led_g = 0; int led_b = 170;
  Adafruit_NeoPixel led_strip(2, LED_PIN, NEO_GRB + NEO_KHZ800);
#endif

//Stick potentiometers
MCP4131 pX(LEFT_STICK_CS_X);
MCP4131 pY(LEFT_STICK_CS_Y);

MCP4131 prX(RIGHT_STICK_CS_X);
MCP4131 prY(RIGHT_STICK_CS_Y);

//Enums
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
  LEFT_STICK = (1 << 0),
  DPAD = (1 << 1),
  RIGHT_STICK = (1 << 2), 
  NONE = 0
};

//Output control mode state
stick_mode previous_control_mode = DPAD;
stick_mode current_control_mode = DPAD;
stick_mode stored_control_mode = DPAD;

//Joystick input state
stick_position current_joystick_input = CENTER;
stick_position previous_joystick_input = CENTER;

//Joystick functions
void set_control_output_mode(stick_mode new_mode) {  
  current_control_mode = new_mode;
}

void enable_control_output() { 
  set_control_output_mode(stored_control_mode);  
}

void disable_control_output() {
  stored_control_mode = current_control_mode;
  set_control_output_mode(NONE);
}

void read_joystick() {
  previous_joystick_input = current_joystick_input;
  stick_position sp = CENTER;
  if (isLow(INPUT_UP)) { sp = (stick_position)(sp | UP); }
  if (isLow(INPUT_DOWN)) { sp = (stick_position)(sp | DOWN); }
  if (isLow(INPUT_LEFT)) { sp = (stick_position)(sp | LEFT); }
  if (isLow(INPUT_RIGHT)) { sp = (stick_position)(sp | RIGHT); }
  current_joystick_input = sp;
}

bool stick_has_pos(stick_position pos) {
  return ((current_joystick_input & pos) == pos);
}

//mode selection state
bool menu_button_down = false;
bool returning_to_main = false;

enum select_state { MAIN, STICK_SELECT };
select_state current_state = MAIN;
select_state old_state = MAIN;

//MAIN
void setup() {
  #if PRINT_INFO
    Serial.begin(115200);
  #endif

  pinMode(MENU_BUTTON_PIN, INPUT_PULLUP);

  pinMode(INPUT_UP, INPUT_PULLUP);
  pinMode(INPUT_DOWN, INPUT_PULLUP);
  pinMode(INPUT_LEFT, INPUT_PULLUP);
  pinMode(INPUT_RIGHT, INPUT_PULLUP);

  pinMode(DPAD_UP,OUTPUT);
  pinMode(DPAD_DOWN,OUTPUT);
  pinMode(DPAD_LEFT,OUTPUT);
  pinMode(DPAD_RIGHT,OUTPUT);

  digitalWrite(DPAD_UP,HIGH);
  digitalWrite(DPAD_DOWN,HIGH);
  digitalWrite(DPAD_LEFT,HIGH);
  digitalWrite(DPAD_RIGHT,HIGH);

  #if USE_LEDS
    led_strip.begin();
    led_strip.setPixelColor(0, led_r, led_g, led_b);
    led_strip.setPixelColor(1, led_r, led_g, led_b);
    led_strip.show();
  #endif
}


void loop() {
  //set up returning_to_main as true if we're coming back from selecting a new control output method
  if (!returning_to_main) returning_to_main = (old_state != MAIN && current_state == MAIN);
  old_state = current_state; //this is the only place old_state matters, so do this here

  //reads the INPUT_<dir> pins and sets up current_joystick_input
  read_joystick();

  menu_button_down = isLow(MENU_BUTTON_PIN);

  //Handle 
  if (current_state == MAIN) {
    if (returning_to_main) {
      if (current_joystick_input == CENTER) {
        returning_to_main = false;
        enable_control_output();
      } 
    }

    if (menu_button_down) {
      current_state = STICK_SELECT;
      disable_control_output();
    }

  } else if (current_state == STICK_SELECT) {
    if (!menu_button_down) {   
      if (current_joystick_input == UP || current_joystick_input == DOWN) { stored_control_mode = DPAD; set_control_output_mode(DPAD); current_state = MAIN; } 
      else if (current_joystick_input == LEFT) { stored_control_mode = LEFT_STICK; set_control_output_mode(LEFT_STICK); current_state = MAIN; }
      else if (current_joystick_input == RIGHT) { stored_control_mode = RIGHT_STICK; set_control_output_mode(RIGHT_STICK); current_state = MAIN; }

      else { current_state = MAIN; }
    }    
  } 

  if (menu_button_down || returning_to_main || current_state != MAIN) return; 

  if (current_control_mode == NONE) {
    ls_set(CENTER);
    rs_set(CENTER);
    set_dpad(CENTER);
    
  } else if (current_joystick_input != previous_joystick_input) {
    if (current_control_mode == DPAD) {
      set_dpad(current_joystick_input);
      ls_set(CENTER);
      rs_set(CENTER);
    } else if (current_control_mode == LEFT_STICK) {
      ls_set(current_joystick_input);
      rs_set(CENTER);
      set_dpad(CENTER);
    } else if (current_control_mode == RIGHT_STICK) {
      ls_set(CENTER);
      rs_set(current_joystick_input);
      set_dpad(CENTER);
    } else {
      ls_set(CENTER);
      rs_set(CENTER);
      set_dpad(CENTER);
    }

    #if PRINT_INFO
      stick_position pressed = (stick_position)(stick_input & ~previous_stick_input);
      stick_position released = (stick_position)(~stick_input & previous_stick_input);

      Serial.print("[");
      print_mode(current_control_mode);
      Serial.print("] ");

      print_stick_pos(stick_input);
      Serial.print(" -> ");
      print_stick_pos(previous_stick_input);
      
      Serial.print(" [state]");
      printBin(stick_input, 4);

      if (pressed != 0) {
        Serial.print(" [pressed]");
        printBin(pressed, 4);
      }
      if (released != 0) {
        Serial.print(" [released]");
        printBin(released, 3);
      }
      
      Serial.print("\n");    
    #endif
  }

  previous_control_mode = current_control_mode;   
}


//DPAD CONTROL
static void set_dpad(stick_position sp) {  
  digitalWrite(DPAD_UP,HIGH);
  digitalWrite(DPAD_DOWN,HIGH);
  digitalWrite(DPAD_LEFT,HIGH);
  digitalWrite(DPAD_RIGHT,HIGH);

  switch (sp) {
    case LEFT:  
      digitalWrite(DPAD_LEFT,LOW);
      break;
    case UP_LEFT: 
      digitalWrite(DPAD_LEFT,LOW);
      digitalWrite(DPAD_UP,LOW);
      break;
    case UP:
      digitalWrite(DPAD_UP,LOW);
      break;
    case UP_RIGHT:
      digitalWrite(DPAD_UP,LOW);
      digitalWrite(DPAD_RIGHT,LOW);
      break;
    case RIGHT:
      digitalWrite(DPAD_RIGHT,LOW);
      break;
    case DOWN_RIGHT:
      digitalWrite(DPAD_DOWN,LOW);
      digitalWrite(DPAD_RIGHT,LOW);
      break;
    case DOWN: 
      digitalWrite(DPAD_DOWN,LOW);
      break;
    case DOWN_LEFT: 
      digitalWrite(DPAD_DOWN,LOW);
      digitalWrite(DPAD_LEFT,LOW);
      break;
    default: break;
  }  
}

//STICK POTENTIOMETERS
void ls_select_x() {
  digitalWrite(LEFT_STICK_CS_X,LOW);
  digitalWrite(LEFT_STICK_CS_Y,HIGH);
}
void ls_select_y() {
  digitalWrite(LEFT_STICK_CS_X,HIGH);
  digitalWrite(LEFT_STICK_CS_Y,LOW);
}

void rs_select_x() {
  digitalWrite(RIGHT_STICK_CS_X,LOW);
  digitalWrite(RIGHT_STICK_CS_Y,HIGH);  
}
void rs_select_y() {
  digitalWrite(RIGHT_STICK_CS_X,HIGH);
  digitalWrite(RIGHT_STICK_CS_Y,LOW);  
}

//STICK POSITION SELECTION
void ls_set(stick_position sp) {  
  digitalWrite(RIGHT_STICK_CS_X,HIGH);
  digitalWrite(RIGHT_STICK_CS_Y,HIGH);

  switch (sp) {
    case LEFT:  
      ls_select_x(); pX.writeWiper(0); 
      ls_select_y(); pY.writeWiper(64);
      break;
    case UP_LEFT:
      ls_select_x(); pX.writeWiper(0); 
      ls_select_y(); pY.writeWiper(128); 
      break;
    case UP:
      ls_select_x(); pX.writeWiper(64); 
      ls_select_y(); pY.writeWiper(128);
      break;
    case UP_RIGHT:
      ls_select_x(); pX.writeWiper(128); 
      ls_select_y(); pY.writeWiper(128); 
      break;
    case RIGHT:
      ls_select_x(); pX.writeWiper(128); 
      ls_select_y(); pY.writeWiper(64);
      break;
    case DOWN_RIGHT:
      ls_select_x(); pX.writeWiper(128); 
      ls_select_y(); pY.writeWiper(0); 
      break;
    case DOWN:
       ls_select_x(); pX.writeWiper(64); 
       ls_select_y(); pY.writeWiper(0);
      break;
    case DOWN_LEFT:
      ls_select_x(); pX.writeWiper(0); 
      ls_select_y(); pY.writeWiper(0);
      break;
    case CENTER:
      ls_select_x(); pX.writeWiper(64); 
      ls_select_y(); pY.writeWiper(64); 
      break;
  }  

  digitalWrite(LEFT_STICK_CS_X,HIGH);
  digitalWrite(LEFT_STICK_CS_Y,HIGH);
}

void rs_set(stick_position sp) {
  digitalWrite(LEFT_STICK_CS_X,HIGH);
  digitalWrite(LEFT_STICK_CS_Y,HIGH);

  switch (sp) {
    case LEFT:  
      rs_select_x(); prX.writeWiper(0); 
      rs_select_y(); prY.writeWiper(64);
      break;
    case UP_LEFT:
      rs_select_x(); prX.writeWiper(0); 
      rs_select_y(); prY.writeWiper(128); 
      break;
    case UP:
      rs_select_x(); prX.writeWiper(64); 
      rs_select_y(); prY.writeWiper(128);
      break;
    case UP_RIGHT:
      rs_select_x(); prX.writeWiper(128); 
      rs_select_y(); prY.writeWiper(128); 
      break;
    case RIGHT:
      rs_select_x(); prX.writeWiper(128); 
      rs_select_y(); prY.writeWiper(64);
      break;
    case DOWN_RIGHT:
      rs_select_x(); prX.writeWiper(128); 
      rs_select_y(); prY.writeWiper(0); 
      break;
    case DOWN:
       rs_select_x(); prX.writeWiper(64); 
       rs_select_y(); prY.writeWiper(0);
      break;
    case DOWN_LEFT:
      rs_select_x(); prX.writeWiper(0); 
      rs_select_y(); prY.writeWiper(0);
      break;
    case CENTER:
      rs_select_x(); prX.writeWiper(64); 
      rs_select_y(); prY.writeWiper(64); 
      break;
  }

  digitalWrite(RIGHT_STICK_CS_X,HIGH);
  digitalWrite(RIGHT_STICK_CS_Y,HIGH); 
}


//PRINT FUNCTIONS
void printBin(byte aByte, byte n) {
  for (int8_t aBit = n-1; aBit >= 0; aBit--)
    Serial.write(bitRead(aByte, aBit) ? '1' : '0');
}

static void print_mode(stick_mode sm) {
  switch (sm) {
    case NONE: Serial.print("NONE"); return;
    case DPAD: Serial.print("DPAD"); return;
    case LEFT_STICK: Serial.print("LEFT_STICK"); return;
    case RIGHT_STICK: Serial.print("RIGHT_STICK"); return;
  }
}

static void print_stick_pos(stick_position sp) {
  switch (sp) {
    case LEFT: Serial.print("left"); return;      
    case UP_LEFT: Serial.print("top_left"); return;
    case UP: Serial.print("top"); return;
    case UP_RIGHT: Serial.print("top_right"); return;
    case RIGHT: Serial.print("right"); return;
    case DOWN_RIGHT: Serial.print("bottom_right"); return;
    case DOWN: Serial.print("bottom"); return;
    case DOWN_LEFT: Serial.print("bottom_left"); return;
    case CENTER: Serial.print("center"); return;
  }  
}