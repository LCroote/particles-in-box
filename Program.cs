using System;
using SplashKitSDK;

public class Program
{
    public static void Main()
    {

        // MessageServer server = new MessageServer();

        // while (server.IsRunnning)
        // {
        //     if ( server.HasIncomingRequests )
        //     {
        //         server.HandleNextRequest();
        //     }
        // }

        // server.StopServer();
        // Initialise Box (This inherits all Window Class Methods -> Isn't OOP amazing)
        Box box;
        int boxWidth = 500; 
        int boxHeight = 500; 

        // Draw Box
        box = new Box("Particles in a box", boxWidth, boxHeight);
        box.Refresh();

        // Create particles
        // Set number of particles, bounds of the box as bounds of particles as those of the Window, radius, mass 
        box.InitialiseParticles(number: 40, boundX: boxWidth, boundY: boxHeight, r: 10, m: 1);
        
        // Closing Conditions
        bool closeCondition2 = SplashKit.WindowCloseRequested("Particles in a box");
        bool closeCondition1 = false;

        // Drawing loop
        while(!closeCondition1 && !closeCondition2) 
        {
            closeCondition2 = SplashKit.WindowCloseRequested("Particles in a box");
            closeCondition1 = box.Quit;
            
            SplashKit.ProcessEvents();
            box.HandleInput();
            box.UpdateParticles(box);

        }
    }

}
