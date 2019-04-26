#pragma once

#include <Windows.h>
#include <process.h>

#include <osgViewer/Viewer>
#include <osg/Geode>
#include <osg/Group>
#include <osg/ShapeDrawable>
#include <osgGA\TrackballManipulator>
#include <osgViewer\api\Win32\GraphicsWindowWin32>
#include <osg/Camera>
#include <osg/LightSource>

class MyOSG
{
public:
	bool CreateViewer(HWND hwnd);
	void Render(HWND hwnd);
	void Destroy();

	void CreateCube();
	void CreateSphere();

private:
	osg::ref_ptr<osg::Group> root;
};
