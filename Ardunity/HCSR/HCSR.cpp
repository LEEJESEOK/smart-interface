/*
  DigitalInput.cpp - Ardunity Arduino library
  Copyright (C) 2015 ojh6t3k.  All rights reserved.
*/

//******************************************************************************
//* Includes
//******************************************************************************
#include "Ardunity.h"
#include "HCSR.h"


//******************************************************************************
//* Constructors
//******************************************************************************
//trigPin : 2, echoPin : 3
HCSR::HCSR(int id, int trigPin, int echoPin) : ArdunityController(id)
{
	_trigPin = trigPin;
	_echoPin = echoPin;
    canFlush = true;
}

//******************************************************************************
//* Override Methods
//******************************************************************************
void HCSR::OnSetup()
{
	pinMode(_trigPin, OUTPUT);
  pinMode(_echoPin, INPUT);
}

void HCSR::OnStart()
{
}

void HCSR::OnStop()
{
}

void HCSR::OnProcess()
{
  //measuring distance
  if(started)
    {
      
        digitalWrite(_trigPin, LOW);
        delayMicroseconds(2);
        digitalWrite(_trigPin, HIGH);
        delayMicroseconds(10);
        digitalWrite(_trigPin, LOW);
        FLOAT32 newValue = (FLOAT32)(pulseIn(_echoPin, HIGH) * 0.034 / 2);
        if(_value != newValue){
          _value = newValue;
          dirty = true;
        }
    }
}

void HCSR::OnUpdate()
{
}

void HCSR::OnExecute()
{
}

void HCSR::OnFlush()
{
	ArdunityApp.push(_value);
}

//******************************************************************************
//* Private Methods
//******************************************************************************

