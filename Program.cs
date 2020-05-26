using System;
using SplashKitSDK;

public class Program
{
    public static void Main()
    {

        Box box;
        int boxWidth = 900; 
        int boxHeight = 700; 

        // int number;
        // string input;
        // ToggleButton test = new ToggleButton(400, 500, "Hello", Color.Black);
        // Simple Control Panel
        // Draw Box
        box = new Box("Particles in a box", boxWidth, boxHeight);
        box.Refresh();

        // // Create particles
        // Set number of particles, bounds of the box as bounds of particles as those of the Window, radius, mass 
        box.InitialiseParticles(number: 40, boundX: boxWidth, boundY: boxHeight, r: 20, m: 1);
        
        // Closing Conditions
        bool closeCondition2 = SplashKit.WindowCloseRequested("Particles in a box");
        bool closeCondition1 = false;

        // Start Reading
        // Drawing loop
        while(!closeCondition1 && !closeCondition2) 
        {
            // Set number of particles, bounds of the box as bounds of particles as those of the Window, radius, mass 
            // box.InitialiseParticles(number: 40, boundX: boxWidth, boundY: boxHeight, r: 20, m: 1);
            
            closeCondition2 = SplashKit.WindowCloseRequested("Particles in a box");
            // closeCondition1 = box.Quit;

            SplashKit.ProcessEvents();

            // test.HandleInput();

            // test.Draw(box);

            box.HandleInput();
            
            box.UpdateBox();

            // SplashKit.RefreshScreen(Convert.ToUInt32(box.fps));              // Refresh Window with FPS
            // SplashKit.RefreshScreen();          // Refresh Window with FPS

        }
    }

}
