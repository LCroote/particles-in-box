using System;
using SplashKitSDK;
class EntryControl : Control
{
    string _defaultValue;
    int value;
    int previousValue;
    bool reading = false;
    
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

    public EntryControl(float x, float y, string label, Color labelColor, string defaultValue) : base(x, y, label, labelColor) 
    {
        _defaultValue = defaultValue;
        value = Convert.ToInt32(defaultValue);
        previousValue = Convert.ToInt32(defaultValue);
    }

    public override void Draw(Box box)
    {
        base.Draw(box);

        box.DrawText(_label, Color.Black, SplashKit.FontNamed("fontBold"), 16, _x + 5, _y - 20);

        if ( IsReading)
        {
            SplashKit.DrawCollectedText(Color.Black, SplashKit.FontNamed("fontThin"), 18, SplashKit.OptionDefaults());
        } else 
        {
            SplashKit.DrawText(Convert.ToString(value), Color.Black, SplashKit.FontNamed("fontThin"), 18, _x, _y);
            
        }
    }

    public override void onClick()
    {
            // Start reading input
            SplashKit.StartReadingText(rectangle);
            reading = true;
            previousValue = value;
    }
    public override void HandleInput()
    {
        // Check if there is a click in control box
        base.HandleInput();

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