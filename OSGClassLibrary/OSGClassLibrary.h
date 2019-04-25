#pragma once
#include <Windows.h>
#include "../OSGMain/MyOSG.h"
#include "../OSGMain/MyOSG.cpp"
using namespace System;

namespace OSGClassLibrary {
	public ref class OSGClassWrapper
	{
	public:
		OSGClassWrapper();
		void TakeInput(int i);
		void Render(IntPtr hwnd);
		void Destroy();

	private:
		MyOSG* myOSG; // an instance of class in C++

		// TODO: Add your methods for this class here.
	};
}
