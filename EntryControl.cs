using System;
using SplashKitSDK;
class EntryControl : Control
{
    public string defualtValue {get; private set;}
    public int value {get; private set;}
    public int previousValue {get; private set;}
    public bool IsReading {get; private set;}
    public int Value
    {
        get 
        {
            return value;
        }
    }

    public EntryControl(float x, float y, string label, Color labelColor, string defaultValue) : base(x, y, label, labelColor) 
    {
        defualtValue = defaultValue;
        value = Convert.ToInt32(defaultValue);
        previousValue = Convert.ToInt32(defaultValue);
    }

    public override void Draw(Box box)
    {
        base.Draw(box);

        box.DrawText(label, Color.Black, SplashKit.FontNamed("fontBold"), 16, x + 5, y - 20);

        if ( IsReading)
        {
            SplashKit.DrawCollectedText(Color.Black, SplashKit.FontNamed("fontThin"), 18, SplashKit.OptionDefaults());
        } else 
        {
            SplashKit.DrawText(Convert.ToString(value), Color.Black, SplashKit.FontNamed("fontThin"), 18, x, y);
            
        }
    }

    public override void onClick()
    {
            SplashKit.StartReadingText(rectangle);
            IsReading = true;
            previousValue = value;
    }
    public override void HandleInput()
    {
        base.HandleInput();

        if (IsReading)
        {
            if( SplashKit.KeyTyped(KeyCode.EscapeKey) )
            {
                value = previousValue;
                IsReading = false;
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

                IsReading = false;
            } 
        }

    }

}