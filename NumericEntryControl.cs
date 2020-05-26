using System;
using SplashKitSDK;
class NumericEntryControl
{
    float _x;
    float _y;
    string _label;
    string _defaultValue;
    int value;
    int previousValue;
    bool reading = false;
    
    public Rectangle rectangle;



    public string GetLabel
    {
        get 
        {
            return _label;
        }
    }

    public bool IsReading
    {
        get 
        {
            return reading;
        }
    }
    public int Value
    {
        get 
        {
            return value;
        }
    }
    public void LoadResources()
    {
        SplashKit.LoadFont("fontBold", "Roboto-Bold.ttf");
        SplashKit.LoadFont("fontThin", "Roboto-Thin.ttf");
    }

    public NumericEntryControl(float x, float y, string label, string defaultValue)
    {
        _x = x;
        _y = y;
        _label = label;
        _defaultValue = defaultValue;
        rectangle = SplashKit.RectangleFrom(x, y, 140, 20);
        
        value = Convert.ToInt32(defaultValue);
        previousValue = Convert.ToInt32(defaultValue);


        LoadResources();
    }

    public void Draw(Box box)
    {
        box.DrawRectangle(Color.Black, rectangle);
        box.DrawText(_label, Color.Black, SplashKit.FontNamed("fontBold"), 18, _x + 5, _y - 20);

        if ( IsReading)
        {
            SplashKit.DrawCollectedText(Color.Black, SplashKit.FontNamed("fontThin"), 18, SplashKit.OptionDefaults());
        } else 
        {
            SplashKit.DrawText(Convert.ToString(value), Color.Black, SplashKit.FontNamed("fontThin"), 18, _x, _y);
            
        }
    }

    public void HandleInput()
    {
        // Check if there is a click in control box
        if( SplashKit.MouseClicked(MouseButton.LeftButton) )
        {
            if( SplashKit.MouseX() > _x && SplashKit.MouseX() < (_x + 140) && SplashKit.MouseY() > _y && SplashKit.MouseY() < (_y + 20)) 
            {
                // Start reading input
                SplashKit.StartReadingText(rectangle);
                reading = true;
                previousValue = value;
            }

        }

        if (reading)
        {
            if( SplashKit.KeyTyped(KeyCode.EscapeKey) )
            {
                value = previousValue;
                reading = false;
            } 
            else if (SplashKit.KeyTyped(KeyCode.ReturnKey))
            {
                previousValue = value;


                try
                {
                    value = Convert.ToInt32(SplashKit.TextInput());
                }
                catch (System.Exception)
                {
                    value = previousValue;
                }

                reading = false;
            } 
        }

    }

}