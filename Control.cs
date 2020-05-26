using System;
using SplashKitSDK;

abstract class Control
{
    protected float x;
    protected float y;
    protected string label;
    protected Color labelColor = Color.Black;
    protected Color color = Color.Black;
    public Rectangle rectangle;

    public string GetLabel
    {
        get 
        {
            return label;
        }
    }

    public void LoadResources()
    {
        SplashKit.LoadFont("fontBold", "Roboto-Bold.ttf");
        SplashKit.LoadFont("fontThin", "Roboto-Thin.ttf");
    }

    public Control(float xPosition, float yPosition, string labelString, Color labelColor)
    {
        x = xPosition;
        y = yPosition;
        label = labelString;
        rectangle = SplashKit.RectangleFrom(x, y, 140, 30);
        
        LoadResources();
    }

    public virtual void Draw(Box box)
    {
        box.DrawRectangle(labelColor, rectangle);

    }

    public abstract void onClick();
    public bool Clicked()
    {
        if( SplashKit.MouseClicked(MouseButton.LeftButton) )
        {
            if( SplashKit.MouseX() > x && SplashKit.MouseX() < (x + 140) && SplashKit.MouseY() > y && SplashKit.MouseY() < (y + 20)) 
            {
                return true;
            }

        } 
        return false;
    }

    // public delegate void ClickAction(Control c);

    public virtual void HandleInput()
    {
        if( Clicked() )
        {
           onClick();
        }

    }
}