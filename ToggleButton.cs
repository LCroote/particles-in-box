using System;
using SplashKitSDK;

class ToggleButton : Control
{
    Color ballColor {get; set;}
     public Color Value
    {
        get 
        {
            return labelColor;
        }
    }
    public ToggleButton(float x, float y, string label, Color labelColor) : base(x, y, label, labelColor)
    {
    }

    public override void Draw(Box box)
    {
        base.Draw(box);

        // box.DrawText(_label, labelColor, SplashKit.FontNamed("fontBold"), 16, _x + 20, _y - 2);
        box.FillRectangle(labelColor, rectangle);
        box.DrawText(_label, Color.Black, SplashKit.FontNamed("fontBold"), 16, _x + 5, _y - 20);


    }

    public override void onClick()
    {
        
        labelColor = SplashKit.RandomColor();
        ballColor = labelColor;
    }

    public override void HandleInput()
    {
        base.HandleInput();
    }
}