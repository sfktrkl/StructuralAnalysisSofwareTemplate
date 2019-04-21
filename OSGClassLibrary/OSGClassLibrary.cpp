#include "stdafx.h"

#include "OSGClassLibrary.h"

using namespace OSGClassLibrary;

OSGClassWrapper::OSGClassWrapper()
{
	myOSG = new MyOSG();
}

int OSGClassWrapper::TakeInputWrapper(int i) {
	return myOSG->TakeInput(i);
}