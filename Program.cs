using System;
using SplashKitSDK;

public class Program
{
    public static void Main()
    {

        int boxWidth = 900; 
        int boxHeight = 700; 

        Box box = new Box("Particles in a box", boxWidth, boxHeight);

        box.InitialiseParticles(boundX: boxWidth, boundY: boxHeight);
        
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
