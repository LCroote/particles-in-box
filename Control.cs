using System;
using SplashKitSDK;

abstract class Control
{
    protected float _x;
    protected float _y;
    protected string _label;
    
    protected Color labelColor = Color.Black;
    protected Color color = Color.Black;
    public Rectangle rectangle;

    public string GetLabel
    {
        get 
        {
            return _label;
        }
    }

    public void LoadResources()
    {
        SplashKit.LoadFont("fontBold", "Roboto-Bold.ttf");
        SplashKit.LoadFont("fontThin", "Roboto-Thin.ttf");
    }

    public Control(float x, float y, string label, Color labelColor)
    {
        _x = x;
        _y = y;
        _label = label;
        rectangle = SplashKit.RectangleFrom(x, y, 140, 30);
        
        LoadResources();
    }

    public virtual void Draw(Box box)
    {
        // box.DrawRectangle(Color.Black, rectangle);
        box.DrawRectangle(labelColor, rectangle);

    }

    public abstract void onClick();
    public bool Clicked()
    {
        if( SplashKit.MouseClicked(MouseButton.LeftButton) )
        {
            if( SplashKit.MouseX() > _x && SplashKit.MouseX() < (_x + 140) && SplashKit.MouseY() > _y && SplashKit.MouseY() < (_y + 20)) 
            {
                return true;
            }

        } 
        return false;
    }

    // public delegate void ClickAction(Control c);

    public virtual void HandleInput()
    {
        // Check if there is a click in control box
        if( Clicked() )
        {
           onClick();
        }

    }
}