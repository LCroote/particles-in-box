using System;
using SplashKitSDK;
using MathNet.Numerics.LinearAlgebra; // Linear Algebra for Collisions
// Define a particle class
class Particle
{
    public Color color = Color.Black; 
    public double vx { get; private set; }
    public double vy { get; private set; }
    public double x { get; private set; }
    public double y { get; private set; }
    public double r { get; private set; }
    public double m { get; private set; }
    public int boundX { get; private set; }
    public int boundY { get; private set; }

    public Particle(double xPosition, double yPosition, double xVelocity, double yVelocity, double radius, double mass, Color pColor, int boundx, int boundy)
    {
        x = xPosition;
        y = yPosition;
        vx = xVelocity;
        vy = yVelocity;
        r = radius;
        m = mass;
        color = pColor;
        boundX = boundx;
        boundY = boundy;

    }

    public void Move(double inc)
    {
        x = x + vx * inc;
        y = y + vy * inc;
    }

    public void Draw(Box box)
    {
        box.FillCircle(color, x, y, r);
    }

    public void WallCollision()
    {
        if (x > (boundX - r))
        {
            x = boundX - r;
            vx = vx * (-1); 
        }
        if (x < (0 + r))
        {
            x = (0 + r);
            vx = vx * (-1); 
        }
        if (y > (boundY - r))
        {
            y = (boundY - r);
            vy = vy * (-1);
        }
        if (y < (0 + r))
        {
            y = (0 + r);
            vy = vy * (-1);
        }
    }

    public bool HasCollided(Particle p2)
    {
        return Math.Sqrt(Math.Pow(p2.x - x, 2) + Math.Pow(p2.y - y, 2)) < (r + p2.r);
    }
    public void ParticleCollision (Particle p2)
    {
        
        double[] v1PrimeArr = new double[2]; 
        double[] v2PrimeArr = new double[2]; 
         
        // Convert using Linear Algebra, simplifies calculations
        // Velocity Vector
        var v1 = Vector<double>.Build.Dense(new[] { vx, vy });
        var v2 = Vector<double>.Build.Dense(new[] { p2.vx, p2.vy });
        // Position Vector
        var x1 = Vector<double>.Build.Dense(new[] { x, y });
        var x2 = Vector<double>.Build.Dense(new[] { p2.x, p2.y });
        // Mass
        double m1 = m; double m2 = p2.m;

        // Initialise new Vectors
        // Vectors are velocity vectors after collision
        var v1Prime = Vector<double>.Build.Dense(2);
        var v2Prime = Vector<double>.Build.Dense(2);

        // Calculation    
        // Find the euclidean distance between two points
        double overlapDistance = Math.Sqrt(Math.Pow(p2.x - x, 2) + Math.Pow(p2.y - y, 2));
        // Adjustment is the amount the particles need to be separated in order not overlap
        double adjustment = (r + p2.r - overlapDistance) / 1.99; // Give small buffer so < 2
        // Calcualte the unit vector along the plane of collision 
        Vector<double> unitVector;   
        unitVector = (x1 - x2).Divide((x1 - x2).L2Norm());

        // Adjust particles to remove overlap
        // Particles can get stuck since a collision is only registered after they overlap. This results in a 
        // constant collison being triggered every loop and the particles "Sticking" together.
        // For purposes of this program I just adjust them back the way they came by some fudge factor
        // This fudge factor is proportional to the overlapping distance.
        x1 = x1 + (unitVector * adjustment);
        x2 = x2 - (unitVector * adjustment);
        // Adjust particles 
        // Set THIS particles position
        x = x1.At(0); y = x1.At(1);
        // Set OTHER particle position
        p2.x = x2.At(0); p2.y = x2.At(1);

        // Collision Calculation
        // Some vector calculations
        // This can also be done formulaicly but I find vector to be simpler to type (let the library Math.Net to the work)
        v1Prime = v1 - (( (2 * m2) / (m1 + m2) ) * ( (v1 - v2).DotProduct( (x1 - x2) ) ) * ( x1 - x2 ) / Math.Pow( (x1 - x2).Norm(2), 2 ));
        v2Prime = v2 - (( (2 * m1) / (m1 + m2) ) * ( (v2 - v1).DotProduct( (x2 - x1) ) ) * ( x2 - x1 ) / Math.Pow( (x2 - x1).Norm(2), 2 ));

        // Update velocities
        // Set THIS particles velocities
        vx = v1Prime.At(0); vy = v1Prime.At(1);
        // Set OTHER particle velocities
        p2.vx = v2Prime.At(0); p2.vy = v2Prime.At(1);

        // Move Particles once before exiting
        p2.x = p2.x + p2.vx * 1; p2.y = p2.y + p2.vy * 1;
    }
}