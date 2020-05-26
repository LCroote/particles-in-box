using System;
using SplashKitSDK;
using System.Collections.Generic;
class Controls
{
    int _x;
    int _y;

    // private Dictionary<string, NumericEntryControl> controls = new Dictionary<string, NumericEntryControl>();
    private Dictionary<string, Control> controls = new Dictionary<string, Control>();

    public Dictionary<string, Control> GetControl 
    {
        get 
        {
            return controls;
        }
    }
    public Controls(int x, int y, string label, string defaultValue)
    {
        _x = x;
        _y = y;
        CreateControls();
    }

    public void DrawPanel(Box box)
    {
        box.FillRectangle(Color.Gray, _x, _y, 150, 150);
        box.DrawText("Controls", Color.Black, _x, _y - 10);
    }

    public void CreateControls()
    {
        // controls.Add("number", new NumericEntryControl( _x + 5, _y + (20 * 0) + 20, "# Particles", "40"));
        controls.Add("number", new EntryControl( _x + 5, _y + (20 * 0) + 20, "No. Particles", Color.Black, "40"));
        controls.Add("color", new ToggleButton( _x + 5, _y + (50 * 1) + 20, "Toggle Colour", Color.Black));
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