using System;
using SplashKitSDK;
using System.Collections.Generic;

class Box:Window
{
    // Particle[] _particles;
    List<Particle> _particles = new List<Particle>();
    // public Controls controlPanel;
    int _w; int _h;
    // int viewWidth; int viewHeigth;
    private int _number = 40;
    private int _fps = 60;
    public bool Quit {get; set;}

    public Controls controlPanel {get; private set;}

    // ToggleButton test;
    // EntryControl test;

    public int fps 
    {
        get {
            return _fps;
        }
    }

    // Contructor
    public Box (string caption, int width, int height) : base(caption, width, height) 
    {
        _w = width; 
        _h = height;
        controlPanel = new Controls(width - 175, 25, "label", "default");

        // test = new ToggleButton(400, 300, "hell", Color.Red);
        // test = new EntryControl(400, 300, "hell", Color.Red, "40");
    }
    // Methods
    // Fill box with particles
    public void InitialiseParticles(int number, int boundX, int boundY, double r, double m)
    {
        // _particles = new Particle[_number];

        Random rnd = new Random();
        for (int i = 0; i < _number; i++)
        {   
            // _particles[i] = new Particle(rnd.Next(0, boundX), rnd.Next(0, boundY), rnd.Next(-10, 10) / 20.0, rnd.Next(-10, 10) / 20.0, r, m, boundX - 200, boundY);
            _particles.Add(new Particle(rnd.Next(0, boundX), rnd.Next(0, boundY), rnd.Next(-10, 10) / 20.0, rnd.Next(-10, 10) / 20.0, r, m, Color.Black, boundX - 200, boundY));
            
        }
    }

    public void DrawParticles ()
    {
        // for (int i = 0; i < _particles.Length; i++)
        // for (int i = 0; i < _particles.Count; i++)
        // {   
        //     _particles[i].Draw(this);
        // }
        foreach (var particle in _particles)
        {
            particle.Draw(this);
        }
    }
    
    public void MoveParticles ()
    {
        // for (int i = 0; i < _particles.Length; i++)
        // {   
        //     _particles[i].Move(1.0);
        // }
        foreach (var particle in _particles)
        {
            particle.Move(1.0);
        }
    }

    public void CheckCollisions ()
    {
        for (int i = 0; i < _particles.Count; i++)
        {   
            _particles[i].WallCollision();
            // First particle check against the rest. The second check against all but the first ie (#particles - 1) and so on
            for (int j = i + 1; j < _particles.Count; j++)
            {
                if(_particles[i].HasCollided(_particles[j])) // Check if collision occurs before calculating
                {
                    _particles[i].ParticleCollision(_particles[j]);
                }
            }
        }
    }

    public void UpdateBox ()
    {
        Clear(Color.White);
        controlPanel.Draw(this);
        // test.Draw(this);
        CheckCollisions();      // Check for collisions between particles and walls
        MoveParticles();        // Move Particles to new positions
        DrawParticles();    // Redraw particles in new positions

        SplashKit.RefreshScreen();
    }

    // Handle input from user
    public void HandleInput()
    {

        controlPanel.HandleInput();
        UpdateFromControls();
        
        // test.HandleInput();

        if (SplashKit.KeyTyped(KeyCode.EscapeKey))
        {
            Quit = true;
        }
    }

    public void UpdateFromControls()
    {
        List<Particle> adjustParticles = new List<Particle>();

        // Check Number Control
        // controlPanel.GetControl["number"] as 
        // ((EntryControl)controlPanel.GetControl["number"]).Value;
        // if(controlPanel.GetControl["number"].Value > _number)
        if(((EntryControl)controlPanel.GetControl["number"]).Value > _number)
        {
            Random rnd = new Random();
            // for (int i = 0; i < (controlPanel.GetControl["number"].Value - _number); i++)
            for (int i = 0; i < (((EntryControl)controlPanel.GetControl["number"]).Value - _number); i++)
            {
                _particles.Add(new Particle(rnd.Next(0, _w), rnd.Next(0, _h), rnd.Next(-10, 10) / 20.0, rnd.Next(-10, 10) / 20.0, 20, 1, Color.Black, _w - 200, _h));
            }

            // _number = controlPanel.GetControl["number"].Value;
            _number = ((EntryControl)controlPanel.GetControl["number"]).Value;
            
        } 
        // else if (controlPanel.GetControl["number"].Value < _number)
        else if (((EntryControl)controlPanel.GetControl["number"]).Value < _number)
        {
            // _particles.RemoveRange(0, _number - controlPanel.GetControl["number"].Value);
            _particles.RemoveRange(0, _number - ((EntryControl)controlPanel.GetControl["number"]).Value);

            // _number = controlPanel.GetControl["number"].Value;
            _number = ((EntryControl)controlPanel.GetControl["number"]).Value;
        }

        // Ball Colour Control
        foreach (var particle in _particles)
        {
            particle.color = ((ToggleButton)controlPanel.GetControl["color"]).Value;
        }
        // ((EntryControl)controlPanel.GetControl["color"])
        // if(((EntryControl)controlPanel.GetControl["color"])) 
        // {
        //     _fps = controlPanel.GetControl["fps"].Value;
        // }
    }
}
