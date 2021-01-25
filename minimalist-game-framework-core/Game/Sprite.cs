using System;
using System.Collections.Generic;
using System.Text;

interface Sprite
{
    /// <summary>
    /// Update the Sprite from user input.
    /// </summary>
    void HandleInput();

    /// <summary>
    /// Update the position of the Sprite.
    /// </summary>
    void Move();

    /// <summary>
    /// Render the Sprite to the screen relative to the camera.
    /// <param name="camera">The camera object</param>
    /// </summary>
    void Render(Camera camera);
}
