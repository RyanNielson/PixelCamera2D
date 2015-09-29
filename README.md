# PixelCamera2D
A Pixel Perfect Camera for 2D Games in Unity.

##### This is an early work in progress. Please report any issues you find. Also, feel free to contribute fixes or additions.

## Why Should I Use PixelCamera2D?

Unity is a great engine, but it isn't perfect for 2D games that use pixel art. This can become evident when you try to rotate sprites or move sprites. Rotating sprites often results in perfectly diagonal edges that leave the pixel grid. Moving sprites can have a similar problem where pixels don't line up as expected. Check out the [GIFs](#demo) below in the [Demo](#demo) section to see examples with and without PixelCamera2D.

## How It Works

The setup used in `PixelCamera2D` uses two camera's, a quad, and a render texture to display your game. The first camera renders an image to a render texture which is sized according to the `Base Width` and `Base Height` values. The second camera renders its child quad whose texture is the render texture mentioned above. This child quad is resized and scaled account to the screen width and height. 

This method makes pixel art games more consistant by removing perfectly straight diagonal lines and providing better scaling and letterboxing options.

## Installation

Copy the `PixelCamera2D` folder into your `Assets` folder.

## Usage

1. Drag the `PixelCamera2D` prefab from the `PixelCamera2D/Prefabs` folder into your scene. This prefab contains the pixel camera, render camera, and rendered quad.
2. Set the Camera component's `Size` value if necessary.
3. Set the `Base Width` and `Base Height` to a value that your pixel art was designed for. This is used to determine how many pixels wide and high your game should be. This is used when determining best fit and scaling sizes.
4. Click the `Create RenderTexture Asset` button and choose a location to save the new render texture. This is created using the given `Base Width` and `Base Height`, so anytime those change you should use this button. If you have an existing render texture that is the correct size from another scene, you can simply drop that into the camera's `Base Texture` value in the inspector.

## Inspector Options

- Base Height: The height of your game view.
- Base Width: The width of your game view.
- Behaviour: The type of view scaling behaviour to use. There are two options currently.
  - Best Pixel Perfect Fit: Scales your view by pixel perfect increments like 1x, 2x, 3x, etc. This ensures equal pixel sizes. For example for a base size of 200x200, view size will be fixed to sizes like 200x200, 400x400, etc depending on screen size.
  - Scale To Fit: Scales the view to fill the screen as much as possible while maintaining aspect ratio. This fills most of the screen, but may results in some rendering artifacts because of uneven pixel sizes.
- Quad: The quad the camera will render to. This shouldn't have to be changed.

## Notes

- To change your letterbox color just change the background color on the `PixelCamera2DRenderer` object.

## Demo

This project contains a demo in the `PixelCamera2DDemo` folder. This includes an example of static sprites, moving sprites, and a line renderer to show how each behaves. It also includes a button in play mode to enable and disable `PixelCamera2D` so that you can compare what your game would like with or without it.

<a name="gifs"></a>

#### Without PixelCamera2D

![Without PixelCamera2D](http://i.imgur.com/I6kZml0.gif)

Notice the differently sized pixels and smooth diagonal edges on the sprites and line.

#### With PixelCamera2D

![With PixelCamera2D](http://i.imgur.com/aTETXV0.gif)

The pixels are now of equal size and the diagonal lines have been pixelated.


