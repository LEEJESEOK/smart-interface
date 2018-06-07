/*
  DigitalInput.h - Ardunity Arduino library
  Copyright (C) 2015 ojh6t3k.  All rights reserved.
*/

#ifndef HCSR_h
#define HCSR_h

#include "ArdunityController.h"

class HCSR : public ArdunityController
{
public:
	HCSR(int id, int trigPin, int echoPin);	

protected:
	void OnSetup();
	void OnStart();
	void OnStop();
	void OnProcess();
	void OnUpdate();
	void OnExecute();
	void OnFlush();

private:
    int _trigPin;
    int _echoPin;
  	FLOAT32 _value;
};

#endif

