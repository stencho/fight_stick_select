#include <Adafruit_NeoPixel.h>

#define LED_PIN 8
#define DISABLE_PIN A0

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

static bool disable_output = false;

MCP4131 pX(10);
MCP4131 pY(9);

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

enum output_mode {
  DPAD,
  JOYSTICK,
  RIGHT_JOYSTICK
};

static output_mode current_output_mode = DPAD;

static stick_position stick_input = CENTER;
static stick_position previous_stick_input = CENTER;


void setup() {
  Serial.begin(115200);
  // put your setup code here, to run once:
  strip.begin();
  //pinMode(5,OUTPUT);
  //pinMode(6,OUTPUT);

  pinMode(DISABLE_PIN, INPUT_PULLUP);

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

void loop() {
  strip.setPixelColor(0, 250,0,170);
  strip.setPixelColor(1, 250,0,170);
  strip.show();

  read_stick();
 
  disable_output = digitalRead(DISABLE_PIN) == LOW;

  if (disable_output) {
    set_stick(CENTER);
    set_dpad(CENTER);
    
  } else if (stick_input != previous_stick_input) {
    set_stick(stick_input);
    set_dpad(stick_input);

    stick_position pressed = (stick_position)(stick_input & ~previous_stick_input);
    stick_position released = (stick_position)(~stick_input & previous_stick_input);

    print_stick_pos(stick_input);
    Serial.print(" -> ");
    print_stick_pos(previous_stick_input);
    
    Serial.print(" [state]");
    printBinShort(stick_input);
    if (pressed != 0) {
      Serial.print(" [pressed]");
      printBinShort(pressed);
    }
    if (released != 0) {
      Serial.print(" [released]");
      printBinShort(released);
    }
    
    Serial.print("\n");    
  }      

  delay(3);
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

void printBinShort(byte aByte) {
  for (int8_t aBit = 3; aBit >= 0; aBit--)
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
void select_x() {
  digitalWrite(9,HIGH);
  digitalWrite(10,LOW);
}
void select_y() {
  digitalWrite(9,LOW);
  digitalWrite(10,HIGH);
}

static void set_stick(stick_position sp) {  
  switch (sp) {
    case LEFT:  
      select_x(); pX.writeWiper(0); 
      select_y(); pY.writeWiper(64);
      break;
    case UP_LEFT:
      select_x(); pX.writeWiper(0); 
      select_y(); pY.writeWiper(128); 
      break;
    case UP:
      select_x(); pX.writeWiper(64); 
      select_y(); pY.writeWiper(128);
      break;
    case UP_RIGHT:
      select_x(); pX.writeWiper(128); 
      select_y(); pY.writeWiper(128); 
      break;
    case RIGHT:
      select_x(); pX.writeWiper(128); 
      select_y(); pY.writeWiper(64);
      break;
    case DOWN_RIGHT:
      select_x(); pX.writeWiper(128); 
      select_y(); pY.writeWiper(0); 
      break;
    case DOWN:
       select_x(); pX.writeWiper(64); 
       select_y(); pY.writeWiper(0);
      break;
    case DOWN_LEFT:
      select_x(); pX.writeWiper(0); 
      select_y(); pY.writeWiper(0);
      break;
    case CENTER:
      select_x(); pX.writeWiper(64); 
      select_y(); pY.writeWiper(64); 
      break;
  }  
}
