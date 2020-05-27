using System;
using SplashKitSDK;
using System.Collections.Generic;

class Box : Window
{
    List<Particle> particles = new List<Particle>();
    public int numberOfParticles {get; private set;}
    public bool Quit {get; set;}
    public Controls controlPanel {get; private set;}
    private int borderWidth = 5;

    public Box (string caption, int width, int height) : base(caption, width, height) 
    {
        controlPanel = new Controls(width - 175, 25, "label", "default");
    }
    // This method is only a placeholder since controls have been implemented
    // The body can be removed without consequence
    public void InitialiseParticles(int boundX, int boundY)
    {
        // Adjust bounds for Border
        boundX -= borderWidth;
        boundY -= borderWidth;

        Random rnd = new Random();
        // Set random radius and derive mass acroding to: mass is proportional to the square of radius
        int r = rnd.Next(10, 40);
        int m = r * r;
        for (int i = 0; i < numberOfParticles; i++)
        {   
            particles.Add(new Particle(
                rnd.Next(0 + borderWidth, boundX)
                , rnd.Next(0 + borderWidth, boundY)
                , rnd.Next(-10, 10) / 20.0
                , rnd.Next(-10, 10) / 20.0
                , r
                , m
                , Color.Black
                , borderWidth
                , borderWidth
                , Width - borderWidth - 200
                , Height - borderWidth
                )
                );
            
        }
    }
    
    /*--------------------------------------------------------------------------------------------------*/
    /*----------------------------------- Calculations -------------------------------------------------*/
    /*--------------------------------------------------------------------------------------------------*/
    public void MoveParticles ()
    {
        foreach (var particle in particles)
        {
            particle.Move(1.0);
        }
    }

    // Check each particle collision with Walls and other particles.
    public void CheckCollisions ()
    {
        for (int i = 0; i < particles.Count; i++)
        {   
            particles[i].WallCollision();
            for (int j = i + 1; j < particles.Count; j++)
            {
                if(particles[i].HasCollided(particles[j])) 
                {
                    particles[i].ParticleCollision(particles[j]);
                }
            }
        }
    }
    // Energy calculated relatively in order to check that the collisions in the box are infact linear
    public double CalculateEnergy()
    {
        double energy = 0;
        
        foreach (var particle in particles)
        {
            energy += (0.5 * particle.m * (particle.vx * particle.vx + particle.vy * particle.vy) );
        }

        return energy;

    }
    /*--------------------------------------------------------------------------------------------------*/
    /*---------------------------------------- Drawing -------------------------------------------------*/
    /*--------------------------------------------------------------------------------------------------*/
    public void UpdateBox ()
    {
        Clear(Color.White);

        CheckCollisions();       // Check for collisions between particles and walls
        MoveParticles();         // Move Particles to new positions
        
        controlPanel.Draw(this); // Draw Control Panel
        DrawBox();               // Draw Box with Border
        DrawParticles();         // Redraw particles in new positions
        DrawText($"Relative Energy: {CalculateEnergy().ToString("0.##")}", Color.Black, Width - 190, Height - 50);

        SplashKit.RefreshScreen();
    }

    public void DrawBox()
    {
        FillRectangle(Color.Black, 0, 0, Width - 200, Height);
        FillRectangle(Color.White, borderWidth, borderWidth, Width - (200 + 2 * borderWidth), Height - 2 * borderWidth);
    }
    public void DrawParticles ()
    {
        foreach (var particle in particles)
        {
            particle.Draw(this);
        }
    }
    /*--------------------------------------------------------------------------------------------------*/
    /*----------------------------------- Interaction and Events----------------------------------------*/
    /*--------------------------------------------------------------------------------------------------*/
    public void HandleInput()
    {
        controlPanel.HandleInput();
        UpdateFromControls();
        
        if (SplashKit.KeyTyped(KeyCode.EscapeKey))
        {
            Quit = true;
        }
    }
    public void UpdateFromControls()
    {
        List<Particle> adjustParticles = new List<Particle>();

        // Since Controls are stored as a Control Object they must be cast to their object in order to access 
        // the Value Property this is done this way since covariant return types in C# are not supported
        if(((EntryControl)controlPanel.GetControl["number"]).Value > numberOfParticles)
        {
            Random rnd = new Random();
            for (int i = 0; i < (((EntryControl)controlPanel.GetControl["number"]).Value - numberOfParticles); i++)
            {
                int r = rnd.Next(5, 50);
                int m = r * r;
                
                particles.Add(new Particle(
                    rnd.Next(0 + borderWidth, Width - borderWidth)
                    , rnd.Next(0 + borderWidth, Height - borderWidth)
                    , rnd.Next(-10, 10) / 40.0
                    , rnd.Next(-10, 10) / 40.0
                    
                    , r
                    , m
                    
                    , Color.Gray
                    , borderWidth
                    , borderWidth
                    , Width - borderWidth - 200
                    , Height - borderWidth
                    )
                    );
            }

            numberOfParticles = ((EntryControl)controlPanel.GetControl["number"]).Value;
            
        } 
        else if (((EntryControl)controlPanel.GetControl["number"]).Value < numberOfParticles)
        {
            particles.RemoveRange(0, numberOfParticles - ((EntryControl)controlPanel.GetControl["number"]).Value);

            numberOfParticles = ((EntryControl)controlPanel.GetControl["number"]).Value;
        }

        foreach (var particle in particles)
        {
            particle.color = ((ToggleButton)controlPanel.GetControl["color"]).Value;
        }
    }


}
