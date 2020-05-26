using System;
using SplashKitSDK;
using System.Collections.Generic;

class Box : Window
{
    List<Particle> particles = new List<Particle>();
    public int numberOfParticles {get; private set;}
    public bool Quit {get; set;}
    public Controls controlPanel {get; private set;}

    public Box (string caption, int width, int height) : base(caption, width, height) 
    {
        controlPanel = new Controls(width - 175, 25, "label", "default");
    }
    public void InitialiseParticles(int number, int boundX, int boundY, double r, double m)
    {
        Random rnd = new Random();
        for (int i = 0; i < numberOfParticles; i++)
        {   
            particles.Add(new Particle(rnd.Next(0, boundX), rnd.Next(0, boundY), rnd.Next(-10, 10) / 20.0, rnd.Next(-10, 10) / 20.0, r, m, Color.Black, boundX - 200, boundY));
            
        }
    }

    public void DrawParticles ()
    {
        foreach (var particle in particles)
        {
            particle.Draw(this);
        }
    }
    
    public void MoveParticles ()
    {
        foreach (var particle in particles)
        {
            particle.Move(1.0);
        }
    }

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

    public void UpdateBox ()
    {
        Clear(Color.White);
        controlPanel.Draw(this); // Draw Control Panel

        CheckCollisions();       // Check for collisions between particles and walls
        MoveParticles();         // Move Particles to new positions
        DrawParticles();         // Redraw particles in new positions

        // DrawText(CalculateEnergy().ToString(), Color.Black, Width - 50, Height - 50);

        SplashKit.RefreshScreen();
    }

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

        if(((EntryControl)controlPanel.GetControl["number"]).Value > numberOfParticles)
        {
            Random rnd = new Random();
            for (int i = 0; i < (((EntryControl)controlPanel.GetControl["number"]).Value - numberOfParticles); i++)
            {
                particles.Add(new Particle(rnd.Next(0, Width), rnd.Next(0, Height), rnd.Next(-10, 10) / 20.0, rnd.Next(-10, 10) / 20.0, 20, 1, Color.Black, Width - 200, Height));
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

    public double CalculateEnergy()
    {
        double energy = 0;
        
        foreach (var particle in particles)
        {
            energy += (0.5 * particle.m * (particle.vx * particle.vx + particle.vy + particle.vy) );
            // Console.WriteLine($"{1/2 * particle.m}");
            // Console.WriteLine($"{(particle.vx * particle.vx + particle.vy + particle.vy)}");
        }

        return energy;

    }
}
