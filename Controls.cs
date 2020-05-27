using SplashKitSDK;
using System.Collections.Generic;
class Controls
{
    public int x {get; set;}
    public int y {get; set;}
    private Dictionary<string, Control> controls = new Dictionary<string, Control>();

    public Dictionary<string, Control> GetControl 
    {
        get 
        {
            return controls;
        }
    }
    public Controls(int xPosition, int yPosition, string label, string defaultValue)
    {
        x = xPosition;
        y = yPosition;
        CreateControls();
    }

    public void LoadResources()
    {
        SplashKit.LoadFont("fontBold", "Roboto-Bold.ttf");
        SplashKit.LoadFont("fontThin", "Roboto-Thin.ttf");
    }
    public void DrawPanel(Box box)
    {
        box.FillRectangle(Color.Gray, x, y, 150, 150);
        box.FillRectangle(Color.White, x + 1, y + 1, 148, 148);
        box.DrawText("Controls", Color.Black, SplashKit.FontNamed("fontBold"), 20, x, y - 21);
    }

    public void CreateControls()
    {
        controls.Add("number", new EntryControl( x + 5, y + (20 * 0) + 20, "No. Particles", Color.Black, "40"));
        controls.Add("color", new ToggleButton( x + 5, y + (50 * 1) + 20, "Toggle Colour", Color.Black));
    }
    public void Draw(Box box)
    {
        DrawPanel(box);
        foreach (var control in controls)
        {
            control.Value.Draw(box);
        }
    }

    public void HandleInput()
    {
        foreach (var control in controls)
        {
            control.Value.HandleInput();
        }
    }
}