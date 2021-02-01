using System;
using System.Collections.Generic;
using System.Text;

class Scene
{
    protected List<Renderable> renderables;
    protected Camera camera;

    public Scene()
    {
        renderables = new List<Renderable>();
        this.camera = null;
    }

    /// <summary>
    /// Update the scene: handle input and move/render all objects
    /// </summary>
    public virtual void Update()
    {
        // Handle Input
        renderables.ForEach(delegate (Renderable r)
        {
            r.HandleInput();
        });

        // Movement
        renderables.ForEach(delegate (Renderable r)
        {
            r.Move(camera);
        });

        // Render
        renderables.ForEach(delegate (Renderable r)
        {
            r.Render(camera);
        });
    }
}
