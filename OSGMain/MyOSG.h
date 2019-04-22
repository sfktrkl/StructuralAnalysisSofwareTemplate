#pragma once

#include <Windows.h>
#include <process.h>

#include <osgViewer/Viewer>
#include <osg/Geode>
#include <osg/Group>
#include <osg/ShapeDrawable>
#include <osgGA\TrackballManipulator>
#include <osgViewer\api\Win32\GraphicsWindowWin32>
#include <osg\Camera>

class __declspec(dllexport) MyOSG
{
public:
	void Render(HWND hwnd);
	void Destroy();

	void CreateCube(osg::Group* root);
	void CreateSphere(osg::Group* root);
	void TakeInput(int i);
};
