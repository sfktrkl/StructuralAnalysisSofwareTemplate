#pragma once

// When libraries are included, CLR is not building properly.
#include <osgViewer/Viewer>
#include <osg/Geode>
#include <osg/Group>
#include <osg/ShapeDrawable>

class MyOSG
{
public:

	void CreateCube(osg::Group* root);
	void CreateSphere(osg::Group* root);
	int TakeInput(int i);
};
