#include <SoftwareSerial.h>

SoftwareSerial mySerial(9, 10);

void setup()
{
  mySerial.begin(9600);   // Setting the baud rate of GSM Module  
  Serial.begin(9600);    // Setting the baud rate of Serial Monitor (Arduino)
  delay(100);
}


void loop()
{
    if(Serial.available() > 0){
        mySerial.println(Serial.read());
    }

    delay(100);

    if(mySerial.available() > 0){
        Serial.println(mySerial.read());
    }
    
 
}


