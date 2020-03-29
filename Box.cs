using System;
using SplashKitSDK;

class Box:Window
{
    Particle[] _particles;
    int _w; int _h;
    // Contructor
    public Box (string caption, int width, int height) : base(caption, width, height) {_w = width; _h = height;}
    
    // Methods
    // Fill box with particles
    public void InitialiseParticles(int number, int boundX, int boundY, double r, double m)
    {
        _particles = new Particle[number];

        Random rnd = new Random();
        for (int i = 0; i < number; i++)
        {   
            _particles[i] = new Particle(rnd.Next(0, boundX), rnd.Next(0, boundY), rnd.Next(-10, 10) / 20.0, rnd.Next(-10, 10) / 20.0, r, m);
        }
    }

    public void DrawParticles (Box box)
    {
        for (int i = 0; i < _particles.Length; i++)
        {   
            _particles[i].Draw(box);
        }
    }
    public void MoveParticles ()
    {
        for (int i = 0; i < _particles.Length; i++)
        {   
            _particles[i].Move(1.0);
        }
    }

    public void CheckCollisions ()
    {
        for (int i = 0; i < _particles.Length; i++)
        {   
            _particles[i].WallCollsion(_w, _h);
            // First particle check against the rest. The second check against all but the first ie (#particles - 1) and so on
            for (int j = i + 1; j < _particles.Length; j++)
            {
                _particles[i].ParticleCollision(_particles[j]);
            }
        }
    }

    public void UpdateParticles (Box box)
    {
        box.Clear(Color.White);     // Clear all particles
        box.CheckCollisions();      // Check for collisions between particles and walls
        box.MoveParticles();        // Move Particles to new positions
        box.DrawParticles (box);    // Redraw particles in new positions
        box.Refresh();              // Refresh Window
    }
}
