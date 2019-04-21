#pragma once
#include "../OSGMain/MyOSG.h"
#include "../OSGMain/MyOSG.cpp"
using namespace System;

namespace OSGClassLibrary {
	public ref class OSGClassWrapper
	{
	public:
		OSGClassWrapper();
		int TakeInputWrapper(int i);

	private:
		MyOSG *myOSG; // an instance of class in C++

		// TODO: Add your methods for this class here.
	};
}
