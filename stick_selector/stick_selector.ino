#include <Adafruit_NeoPixel.h>

#define MODE_PIN_1 A0
#define MODE_PIN_2 A1

#include <MCP4131.h>

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
//#define RIGHT_STICK_CS_Y A3

MCP4131 pX(LEFT_STICK_CS_X);
MCP4131 pY(LEFT_STICK_CS_Y);

MCP4131 prX(RIGHT_STICK_CS_X);
//MCP4131 prY(RIGHT_STICK_CS_Y);

static bool disable_control = false;
const bool print_info = true;

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
static stick_position stick_input = CENTER;
static stick_position previous_stick_input = CENTER;

enum stick_mode : byte {
  LEFT_STICK = (1 << 0) | (1 << 1),
  DPAD = (1 << 0),
  RIGHT_STICK = (1 << 1), 
  NONE = 0
};

static stick_mode current_control_mode = DPAD;

stick_mode read_control_mode() {
  stick_mode om = NONE;  
  bool b1 = digitalRead(MODE_PIN_1) == LOW;
  bool b2 = digitalRead(MODE_PIN_2) == LOW;

  if (b1) om = (stick_mode)(om | (1 << 0));
  if (b2) om = (stick_mode)(om | (1 << 1));
  
  return om;
}

void setup() {
  Serial.begin(115200);
  
  pinMode(MODE_PIN_1, INPUT_PULLUP);
  pinMode(MODE_PIN_2, INPUT_PULLUP);

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
}

stick_mode previous_control_mode;

void loop() {
  read_stick();

  previous_control_mode = current_control_mode;
  current_control_mode = read_control_mode();
  
  if (print_info) {
    if (previous_control_mode != current_control_mode) {
      Serial.print("[MODE CHANGE] ");
      print_mode(previous_control_mode);
      Serial.print("->");
      print_mode(current_control_mode);
      Serial.println();
    }  
  }

  if (disable_control) {
    ls_set(CENTER);
    rs_set(CENTER);
    set_dpad(CENTER);
    
  } else if (stick_input != previous_stick_input) {
    ls_set(stick_input);
    rs_set(stick_input);
    set_dpad(stick_input);

    stick_position pressed = (stick_position)(stick_input & ~previous_stick_input);
    stick_position released = (stick_position)(~stick_input & previous_stick_input);

    if (print_info) {
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
    }
  }      

  delay(3);
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

static void read_stick() {
  previous_stick_input = stick_input;
  stick_position sp = CENTER;
  if (digitalRead(INPUT_UP) == LOW) { sp = (stick_position)(sp | UP); }
  if (digitalRead(INPUT_DOWN) == LOW) { sp = (stick_position)(sp | DOWN); }
  if (digitalRead(INPUT_LEFT) == LOW) { sp = (stick_position)(sp | LEFT); }
  if (digitalRead(INPUT_RIGHT) == LOW) { sp = (stick_position)(sp | RIGHT); }
  stick_input = sp;
}

void printBin(byte aByte, byte n) {
  for (int8_t aBit = n-1; aBit >= 0; aBit--)
    Serial.write(bitRead(aByte, aBit) ? '1' : '0');
}

bool stick_has_pos(stick_position pos) {
  return ((stick_input & pos) == pos);
}

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

//SELECT LEFT JOYSTICK POTENTIOMETER
void ls_select_x() {
  digitalWrite(RIGHT_STICK_CS_X,HIGH);
  //digitalWrite(RIGHT_STICK_CS_Y,HIGH);

  digitalWrite(LEFT_STICK_CS_X,LOW);
  digitalWrite(LEFT_STICK_CS_Y,HIGH);
}
void ls_select_y() {
  digitalWrite(RIGHT_STICK_CS_X,HIGH);
  //digitalWrite(RIGHT_STICK_CS_Y,HIGH);

  digitalWrite(LEFT_STICK_CS_X,HIGH);
  digitalWrite(LEFT_STICK_CS_Y,LOW);
}

//RIGHT JOYSTICK POTENTIOMETER
void rs_select_x() {
  digitalWrite(LEFT_STICK_CS_X,HIGH);
  digitalWrite(LEFT_STICK_CS_Y,HIGH);

  digitalWrite(RIGHT_STICK_CS_X,LOW);
  //digitalWrite(RIGHT_STICK_CS_Y,HIGH);  
}
void rs_select_y() {
  digitalWrite(LEFT_STICK_CS_X,HIGH);
  digitalWrite(LEFT_STICK_CS_Y,HIGH);

  digitalWrite(RIGHT_STICK_CS_X,HIGH);
  //digitalWrite(RIGHT_STICK_CS_Y,LOW);  
}

void rs_set(stick_position sp) {
  switch (sp) {
    case LEFT:  
      rs_select_x(); prX.writeWiper(0); 
      //rs_select_y(); prY.writeWiper(64);
      break;
    case UP_LEFT:
      rs_select_x(); prX.writeWiper(0); 
      //rs_select_y(); prY.writeWiper(128); 
      break;
    case UP:
      rs_select_x(); prX.writeWiper(64); 
      //rs_select_y(); prY.writeWiper(128);
      break;
    case UP_RIGHT:
      rs_select_x(); prX.writeWiper(128); 
      //rs_select_y(); prY.writeWiper(128); 
      break;
    case RIGHT:
      rs_select_x(); prX.writeWiper(128); 
      //rs_select_y(); prY.writeWiper(64);
      break;
    case DOWN_RIGHT:
      rs_select_x(); prX.writeWiper(128); 
      //rs_select_y(); prY.writeWiper(0); 
      break;
    case DOWN:
       rs_select_x(); prX.writeWiper(64); 
       //rs_select_y(); prY.writeWiper(0);
      break;
    case DOWN_LEFT:
      rs_select_x(); prX.writeWiper(0); 
      //rs_select_y(); prY.writeWiper(0);
      break;
    case CENTER:
      rs_select_x(); prX.writeWiper(64); 
      //rs_select_y(); prY.writeWiper(64); 
      break;
  }
}

void ls_set(stick_position sp) {  
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
}
