#pragma once
#include "MyOSG.h"

void MyOSG::CreateCube(osg::Group* root)
{
	// cube centered at origin
	osg::Box* cube = new osg::Box(osg::Vec3(0, 0, 0), 1.0f);
	// associate shape with drawable
	osg::ShapeDrawable* cubeDrawable = new osg::ShapeDrawable(cube);
	// creage geode and add cubedrawable
	osg::Geode* cubeGeode = new osg::Geode();
	cubeGeode->addDrawable(cubeDrawable);

	// add cube geode to root
	root->addChild(cubeGeode);
	// turn on light (normals ???)
	root->getOrCreateStateSet()->setMode(GL_LIGHTING, osg::StateAttribute::ON);
}

void MyOSG::CreateSphere(osg::Group* root)
{
	osg::Sphere* sphere = new osg::Sphere(osg::Vec3(0, 0, 0), 10.0f);

	osg::ShapeDrawable* sphereDrawable = new osg::ShapeDrawable(sphere);

	osg::Geode* sphereGeode = new osg::Geode();

	sphereGeode->addDrawable(sphereDrawable);

	root->addChild(sphereGeode);

	root->getOrCreateStateSet()->setMode(GL_LIGHTING, osg::StateAttribute::ON);
}

int MyOSG::TakeInput(int i)
{
	// root node in scene graph
	osg::Group* root = new osg::Group();
	// create viewer
	osgViewer::Viewer viewer;

	if (i == 1)
	{
		MyOSG::CreateCube(root);
	}
	if (i == 2)
	{
		MyOSG::CreateSphere(root);
	}

	//
	viewer.setSceneData(root);
	// show viewer in second screen
	viewer.setUpViewOnSingleScreen(1);

	return viewer.run();

	/*
	osg::ref_ptr<osg::Vec3Array> vertices = new osg::Vec3Array;
	// Front
	vertices->push_back(osg::Vec3(0.0f, 0.0f, 0.0f));
	vertices->push_back(osg::Vec3(1.0f, 0.0f, 0.0f));
	vertices->push_back(osg::Vec3(1.0f, 0.0f, 1.0f));
	vertices->push_back(osg::Vec3(0.0f, 0.0f, 1.0f));
	// Back
	vertices->push_back(osg::Vec3(0.0f, 1.0f, 0.0f));
	vertices->push_back(osg::Vec3(1.0f, 1.0f, 0.0f));
	vertices->push_back(osg::Vec3(1.0f, 1.0f, 1.0f));
	vertices->push_back(osg::Vec3(0.0f, 1.0f, 1.0f));
	// Left

	// Right

	// Bottom

	// Top

	//osg::ref_ptr<osg::Vec3Array> normals = new osg::Vec3Array;
	//normals->push_back(osg::Vec3(0.0f, -1.0f, 0.0f));

	//osg::ref_ptr<osg::Vec4Array> colors = new osg::Vec4Array;
	//colors->push_back(osg::Vec4(1.0f, 0.0f, 0.0f, 1.0f));
	//colors->push_back(osg::Vec4(0.0f, 1.0f, 0.0f, 1.0f));
	//colors->push_back(osg::Vec4(0.0f, 0.0f, 1.0f, 1.0f));
	//colors->push_back(osg::Vec4(1.0f, 1.0f, 1.0f, 1.0f));

	osg::ref_ptr<osg::Geometry> quad = new osg::Geometry;
	quad->setVertexArray(vertices.get());

	//quad->setNormalArray(normals.get());
	//quad->setNormalBinding(osg::Geometry::BIND_OVERALL);
	//quad->setColorArray(colors.get());
	//quad->setColorBinding(osg::Geometry::BIND_PER_VERTEX);

	quad->addPrimitiveSet(new osg::DrawArrays(GL_QUADS, 0, 4));

	osg::ref_ptr<osg::Geode> root = new osg::Geode;
	root->addDrawable(quad.get());
	osgViewer::Viewer viewer;
	viewer.setSceneData(root.get());
	viewer.setUpViewOnSingleScreen(1);
	return viewer.run();
	*/
}