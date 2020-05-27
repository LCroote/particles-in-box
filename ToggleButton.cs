using SplashKitSDK;

class ToggleButton : Control
{
    Color ballColor {get; set;}
     public Color Value
    {
        get 
        {
            return ballColor;
        }
    }
    public ToggleButton(float x, float y, string label, Color labelColor) : base(x, y, label, labelColor)
    {
        ballColor = labelColor;
    }

    public override void Draw(Box box)
    {
        base.Draw(box);
        box.FillRectangle(labelColor, rectangle);
        box.DrawText(label, Color.Black, SplashKit.FontNamed("fontBold"), 16, x + 5, y - 20);
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