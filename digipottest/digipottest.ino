#include <Adafruit_NeoPixel.h>

#define LED_PIN 8

Adafruit_NeoPixel strip(2, LED_PIN, NEO_GRB + NEO_KHZ800);

#include <MCP4131.h>

#define INPUT_UP 7
#define INPUT_DOWN 6
#define INPUT_LEFT 5
#define INPUT_RIGHT 4

#define DPAD_UP 3
#define DPAD_RIGHT 2
#define DPAD_LEFT 1
#define DPAD_DOWN 0

MCP4131 pX(10);
MCP4131 pY(9);


enum stick_position {
  TOP = 1, 
  BOTTOM = 2, 
  LEFT = 4, 
  RIGHT = 8,
  TOP_LEFT = TOP | LEFT,
  TOP_RIGHT = TOP | RIGHT,
  BOTTOM_LEFT = BOTTOM | LEFT,
  BOTTOM_RIGHT = BOTTOM | RIGHT,
  CENTER = 0 
};

enum output_mode {
  DPAD,
  JOYSTICK,
  RIGHT_JOYSTICK
};

static output_mode current_output_mode = DPAD;

static stick_position stick_input = CENTER;
static stick_position previous_stick_input = CENTER;

static void read_stick() {
  previous_stick_input = stick_input;
  stick_position sp = CENTER;
  if (digitalRead(INPUT_UP) == LOW) { sp = (stick_position)(sp | TOP); }
  if (digitalRead(INPUT_DOWN) == LOW) { sp = (stick_position)(sp | BOTTOM); }
  if (digitalRead(INPUT_LEFT) == LOW) { sp = (stick_position)(sp | LEFT); }
  if (digitalRead(INPUT_RIGHT) == LOW) { sp = (stick_position)(sp | RIGHT); }

  stick_input = sp;
}

void printBin(byte aByte) {
  for (int8_t aBit = 3; aBit >= 0; aBit--)
    Serial.write(bitRead(aByte, aBit) ? '1' : '0');
}

void setup() {
  Serial.begin(115200);
  // put your setup code here, to run once:
  strip.begin();
  //pinMode(5,OUTPUT);
  //pinMode(6,OUTPUT);

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
    
  //pX.writeWiper(i);
}


static void set_dpad(stick_position sp) {
  
  digitalWrite(DPAD_UP,HIGH);
  digitalWrite(DPAD_DOWN,HIGH);
  digitalWrite(DPAD_LEFT,HIGH);
  digitalWrite(DPAD_RIGHT,HIGH);

  switch (sp) {
    case LEFT: //Serial.println("left");         
      digitalWrite(DPAD_LEFT,LOW);
      break;
    case TOP_LEFT: //Serial.println("top_left");
      digitalWrite(DPAD_LEFT,LOW);
      digitalWrite(DPAD_UP,LOW);
      break;
    case TOP: //Serial.println("top");
      digitalWrite(DPAD_UP,LOW);
      break;
    case TOP_RIGHT: //Serial.println("top_right");
      digitalWrite(DPAD_UP,LOW);
      digitalWrite(DPAD_RIGHT,LOW);
      break;
    case RIGHT: //Serial.println("right");
      digitalWrite(DPAD_RIGHT,LOW);
      break;
    case BOTTOM_RIGHT: //Serial.println("bottom_right");
      digitalWrite(DPAD_DOWN,LOW);
      digitalWrite(DPAD_RIGHT,LOW);
      break;
    case BOTTOM: //Serial.println("bottom");
      digitalWrite(DPAD_DOWN,LOW);
      break;
    case BOTTOM_LEFT: //Serial.println("bottom_left");
      digitalWrite(DPAD_DOWN,LOW);
      digitalWrite(DPAD_LEFT,LOW);
      break;
    default: break;
  }  
}

void loop() {
  strip.setPixelColor(0, 250,0,170);
  strip.setPixelColor(1, 250,0,170);
  strip.show();

  read_stick();
 
  if (stick_input != previous_stick_input) {
    set_stick(stick_input);
    set_dpad(stick_input);

    stick_position pressed = (stick_position)(stick_input & ~previous_stick_input);
    stick_position released = (stick_position)(~stick_input & previous_stick_input);

    print_stick_pos(stick_input);
    Serial.print(" -> ");
    print_stick_pos(previous_stick_input);
    
    Serial.print(" [state]");
    printBin(stick_input);
    if (pressed != 0) {
      Serial.print(" [pressed]");
      printBin(pressed);
    }
    if (released != 0) {
      Serial.print(" [released]");
      printBin(released);
    }
    
    Serial.print("\n");    
  }      

  delay(3);
}

static void print_stick_pos(stick_position sp) {
  switch (sp) {
    case LEFT: Serial.print("left"); return;      
    case TOP_LEFT: Serial.print("top_left"); return;
    case TOP: Serial.print("top"); return;
    case TOP_RIGHT: Serial.print("top_right"); return;
    case RIGHT: Serial.print("right"); return;
    case BOTTOM_RIGHT: Serial.print("bottom_right"); return;
    case BOTTOM: Serial.print("bottom"); return;
    case BOTTOM_LEFT: Serial.print("bottom_left"); return;
    case CENTER: Serial.print("center"); return;
  }  
}

static void set_stick(stick_position sp) {  
  switch (sp) {
    case LEFT: //Serial.println("left");      
      mj_left();
      break;
    case TOP_LEFT: //Serial.println("top_left");
      mj_top_left();
      break;
    case TOP: //Serial.println("top");
      mj_top();
      break;
    case TOP_RIGHT: //Serial.println("top_right");
      mj_top_right();
      break;
    case RIGHT: //Serial.println("right");
      mj_right();
      break;
    case BOTTOM_RIGHT: //Serial.println("bottom_right");
      mj_bottom_right();
      break;
    case BOTTOM: //Serial.println("bottom");
      mj_bottom();
      break;
    case BOTTOM_LEFT: //Serial.println("bottom_left");
      mj_bottom_left();
      break;
    case CENTER: //Serial.println("center");
      mj_center();
      break;
  }  
}

//SELECT LEFT JOYSTICK POTENTIOMETER
void select_x() {
  digitalWrite(9,HIGH);
  digitalWrite(10,LOW);
}

void select_y() {
  digitalWrite(9,LOW);
  digitalWrite(10,HIGH);
}


//MOVE LEFT JOYSTICK
void mj_left() { select_x(); pX.writeWiper(0); select_y(); pY.writeWiper(64); }
void mj_top_left() { select_x(); pX.writeWiper(0); select_y(); pY.writeWiper(128); }
void mj_top() { select_x(); pX.writeWiper(64); select_y(); pY.writeWiper(128); }
void mj_top_right() { select_x(); pX.writeWiper(128); select_y(); pY.writeWiper(128); }
void mj_right() { select_x(); pX.writeWiper(128); select_y(); pY.writeWiper(64);}
void mj_bottom_right() { select_x(); pX.writeWiper(128); select_y(); pY.writeWiper(0); }
void mj_bottom() { select_x(); pX.writeWiper(64); select_y(); pY.writeWiper(0); } 
void mj_bottom_left() { select_x(); pX.writeWiper(0); select_y(); pY.writeWiper(0); }
void mj_center() { select_x(); pX.writeWiper(64); select_y(); pY.writeWiper(64); }
