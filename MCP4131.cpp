/*
 Library written for use with Common-Anode 7-Segment Display. For more information on how to use this library,
 please refer to the github README.
 Written by: Derek Duncan
 Data: 4/7/2018
*/

#include "Arduino.h"
#include "MCP4131.h"
#include <SPI.h>

int slaveSelectPin;

MCP4131::MCP4131(int slavePin) {
    SPI.begin();
    slaveSelectPin = slavePin;
	pinMode(slavePin, OUTPUT);
}

byte MCP4131::readWiper() {
    return sendCommand(ADDRESS_WIPER0, COMMAND_READ, 255);
}

void MCP4131::writeWiper(unsigned int wiperValue) {  
    sendCommand(ADDRESS_WIPER0, COMMAND_WRITE, wiperValue);
}

void MCP4131::sendCommand(byte address, char command) {
    // take the SS pin low to select the chip:
    //enableChip();

    // Adjust SPI settings to fit MCP4131
    SPI.beginTransaction(SPISettings(200000, MSBFIRST, SPI_MODE0));

    
    byte msb = (address << 4) | ((command << 2) | COMMAND_MASK);

    SPI.transfer(msb);

    // Stop using the SPI bus to allow other chips to use SPI
    SPI.endTransaction();
    // take the SS pin high to de-select the chip:
    //disableChip();

}

byte MCP4131::sendCommand(byte address, char command, unsigned int data) {
    // take the SS pin low to select the chip:
    //enableChip();

    // Adjust SPI settings to fit MCP4131
    SPI.beginTransaction(SPISettings(200000, MSBFIRST, SPI_MODE0));

    byte transferByte = (address << 4) | ((command << 2) | COMMAND_MASK);

    SPI.transfer(transferByte);
    byte result = SPI.transfer(data);

    // Stop using the SPI bus to allow other chips to use SPI
    SPI.endTransaction();

    // take the SS pin high to de-select the chip:
    //disableChip();


    return result;
    
}

void MCP4131::decrementWiper() {
    sendCommand(ADDRESS_WIPER0, COMMAND_DECREMENT);
    }

void MCP4131::incrementWiper() {
    sendCommand(ADDRESS_WIPER0, COMMAND_INCREMENT);
}

void MCP4131::enableChip() {
    digitalWrite(slaveSelectPin, LOW);
}

void MCP4131::disableChip() {
    digitalWrite(slaveSelectPin, HIGH);
}