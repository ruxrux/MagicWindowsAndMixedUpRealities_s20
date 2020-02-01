import codeanticode.syphon.*;

import deadpixel.keystone.*;

// syphon
PGraphics canvas;
SyphonClient client;

// keystone
Keystone ks;
CornerPinSurface surface;
PGraphics offscreen;

void settings() {
  size(1280, 720, P3D);
  PJOGL.profile = 1;
}

void setup() {
  // Create syhpon client to receive frames from the first available running server: 
  client = new SyphonClient(this);

  
  // keystone object and surface
  ks = new Keystone(this);
  surface = ks.createCornerPinSurface(1280, 720, 20);
  
  // offscreen buffer to draw the surface we want projected
  // note that we're matching the resolution of the CornerPinSurface.
  offscreen = createGraphics(1280, 720, P3D);

}

void draw() {
  background(0);
  

  
  
  if (client.newFrame()) {
    canvas = client.getGraphics(canvas);  
  }
  
  // Convert the mouse coordinate into surface coordinates
  PVector surfaceMouse = surface.getTransformedMouse();

  // Draw the scene, offscreen
  offscreen.beginDraw();
  offscreen.background(0);
  offscreen.fill(255);
 
  offscreen.ellipse(surfaceMouse.x, surfaceMouse.y, 10, 10);
    
    // only draw if we already passed an image onto canvas
   if(canvas != null){
     // draw the Syphone image in the offscreen buffer
    offscreen.image(canvas, 0, 0, 1280, 720);  
   }
  offscreen.endDraw();

  // most likely, you'll want a black background to minimize
  // bleeding around your projection area
  background(0);
 
  // render the scene, transformed using the corner pin surface
  surface.render(offscreen);
}

void keyPressed() {
    switch(key) {
  case 'c':
    // enter/leave calibration mode, where surfaces can be warped 
    // and moved
    ks.toggleCalibration();
    break;

  case 'l':
    // loads the saved layout
    ks.load();
    break;

  case 's':
    // saves the layout
    ks.save();
    break;
    
  //case ' ':
  //  client.stop();  
  //  break;
  //case 'd':
  //  println(client.getServerName());
  //  break;
  }
}