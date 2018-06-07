#include "Ardunity.h"
#include "HCSR.h"

HCSR hcsr0(0, 2, 3);

void setup()
{
  ArdunityApp.attachController((ArdunityController*)&hcsr0);
  ArdunityApp.resolution(256, 1024);
  ArdunityApp.timeout(5000);
  ArdunityApp.begin(115200);
}

void loop()
{
  ArdunityApp.process();
}
