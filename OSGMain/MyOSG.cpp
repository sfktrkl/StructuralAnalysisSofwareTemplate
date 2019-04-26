#pragma once
#include "MyOSG.h"

osg::ref_ptr<osgViewer::Viewer> viewer;
bool finished;

static void RenderingThread(void*)
{
	// Keep the rendering as long as the viewer's work isn't done
	while (!viewer->done())
	{
		viewer->frame();
	}

	// The rendering is done, set the status to Finished
	finished = true;
}

bool MyOSG::CreateViewer(HWND hwnd)
{
	// Get the dimensions of the window handle
	RECT rect;
	GetWindowRect(hwnd, &rect);

	// WindowData is used to pass in the Win32 window handle attached the GraphicsContext::Traits structure
	osg::ref_ptr<osg::Referenced> windata(new osgViewer::GraphicsWindowWin32::WindowData(hwnd));

	// Create osg's graphics context traits
	osg::ref_ptr<osg::GraphicsContext::Traits> traits = new osg::GraphicsContext::Traits;

	// Set location and size of the window
	traits->x = 0;
	traits->y = 0;
	traits->width = rect.right - rect.left;
	traits->height = rect.bottom - rect.top;
	traits->windowDecoration = false;
	traits->doubleBuffer = true;
	traits->sharedContext = 0;
	traits->inheritedWindowData = windata;
	traits->pbuffer = false;

	//// Create graphics context
	osg::ref_ptr<osg::GraphicsContext> gc = osg::GraphicsContext::createGraphicsContext(traits.get());
	osg::ref_ptr<osg::Camera> camera = new osg::Camera;
	camera->setGraphicsContext(gc.get());
	camera->setViewport(new osg::Viewport(0, 0, traits->width, traits->height));

	camera->setDrawBuffer(GL_BACK);
	camera->setReadBuffer(GL_BACK);

	root = new osg::Group();

	// Create the viewer and attach the camera to it
	viewer = new osgViewer::Viewer;
	viewer->addSlave(camera.get());

	viewer->setCamera(camera.get());
	viewer->setSceneData(root);
	viewer->setKeyEventSetsDone(0);
	viewer->setCameraManipulator(new osgGA::TrackballManipulator);

	// turn off light
	root->getOrCreateStateSet()->setMode(GL_LIGHTING, osg::StateAttribute::ON);

	// The viewer isn't rendering yet, set the status to False
	finished = false;

	return true;
}

void MyOSG::CreateCube()
{
	// cube centered at origin
	osg::ref_ptr<osg::Box> cube(new osg::Box(osg::Vec3(0, 0, 0), 1.0f));
	
    // associate shape with drawable
	osg::ref_ptr<osg::ShapeDrawable> cubeDrawable(new osg::ShapeDrawable(cube));
	
    // creage geode and add cubedrawable
	osg::ref_ptr<osg::Geode> cubeGeode(new osg::Geode());

	cubeGeode->addDrawable(cubeDrawable);

	// add cube geode to root
	root->addChild(cubeGeode);

    viewer->setSceneData(root);
}

void MyOSG::CreateSphere()
{
	//smart pointer ??
	osg::ref_ptr<osg::Sphere> sphere(new osg::Sphere(osg::Vec3(0, 0, 0), 1.0f));

	osg::ref_ptr<osg::ShapeDrawable> sphereDrawable(new osg::ShapeDrawable(sphere));

	osg::ref_ptr<osg::Geode> sphereGeode(new osg::Geode());

	sphereGeode->addDrawable(sphereDrawable);

	root->addChild(sphereGeode);

    viewer->setSceneData(root);
}

void MyOSG::Render(HWND hwnd)
{
	if (CreateViewer(hwnd))
	{
		// Create a rendering thread
		_beginthread(RenderingThread, 0, NULL);

		//viewer->realize();
		//	// Keep the rendering as long as the viewer's work isn't done
		//while (!viewer->done())
		//{
		//	viewer ->frame();
		//}
		//// The rendering is done, set the status to Finished
		//finished = true;
	}
}

void MyOSG::Destroy()
{
	// Set viewer's work to Done
	viewer->setDone(true);

	// Get the rendering status
	while (!finished) Sleep(10);

	// Release the memory
	viewer = NULL;
	finished = NULL;
	root = NULL;
}