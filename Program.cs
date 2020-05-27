using System;
using SplashKitSDK;

public class Program
{
    public static void Main()
    {

        Box box;
        int boxWidth = 900; 
        int boxHeight = 700; 

        box = new Box("Particles in a box", boxWidth, boxHeight);
        box.Refresh();

        // box.InitialiseParticles(number: 10, boundX: boxWidth, boundY: boxHeight, r: 30, m: 1);
        
        bool closeCondition2 = SplashKit.WindowCloseRequested("Particles in a box");
        bool closeCondition1 = false;

        while(!closeCondition1 && !closeCondition2) 
        {
            closeCondition2 = SplashKit.WindowCloseRequested("Particles in a box");

            SplashKit.ProcessEvents();

            box.HandleInput();
            
            box.UpdateBox();

        }
    }

}
