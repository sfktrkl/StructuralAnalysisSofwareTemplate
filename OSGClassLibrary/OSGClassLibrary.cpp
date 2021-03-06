#include "stdafx.h"

#include "OSGClassLibrary.h"

using namespace OSGClassLibrary;

OSGClassWrapper::OSGClassWrapper()
{
	myOSG = new MyOSG();
}

void OSGClassWrapper::CreateCube() {
	myOSG->CreateCube();
}

void OSGClassWrapper::CreateSphere() {
    myOSG->CreateSphere();
}

void OSGClassWrapper::Render(IntPtr hwnd)
{
	// Get the pointer as window handler
	HWND nativeHWND = (HWND)hwnd.ToPointer();

	// Call the native Render method
	myOSG->Render(nativeHWND);
}

void OSGClassWrapper::Destroy()
{
	// Call the native Destroy method
	myOSG->Destroy();
}