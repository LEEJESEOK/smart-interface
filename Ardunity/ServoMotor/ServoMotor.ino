#include <Servo.h>
#include "Ardunity.h"
#include "DigitalInput.h"
#include "GenericServo.h"

DigitalInput dInput1(1, 4, true);
GenericServo servo0(0, 2, false);
DigitalInput dInput2(2, 7, true);

void setup()
{
  ArdunityApp.attachController((ArdunityController*)&dInput1);
  ArdunityApp.attachController((ArdunityController*)&servo0);
  ArdunityApp.attachController((ArdunityController*)&dInput2);
  ArdunityApp.resolution(256, 1024);
  ArdunityApp.timeout(5000);
  ArdunityApp.begin(115200);
}

void loop()
{
  ArdunityApp.process();
}
