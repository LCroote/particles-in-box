using System;
using SplashKitSDK;
using MathNet.Numerics.LinearAlgebra; // Linear Algebra for Collisions
using MathNet.Numerics.LinearAlgebra.Double; // Linear Algebra for Collisions
// Define a particle class
class Particle
{
    // assign variables
    // Postion
    private double _x;
    private double _y;
    // Velocity
    private double _vx;
    private double _vy;
    // radius
    private double _r;
    // Mass 
    private double _m;

    // Constructor
    public Particle(double x, double y, double vx, double vy, double r, double m)
    {
        _x = x;
        _y = y;
        _vx = vx;
        _vy = vy;
        _r = r;
        _m = m;

    }

    // get set
    // Used to set new velocity and position in inside ParticleCollision
    public double vx { get { return _vx; } set { _vx = value; }  }
    public double vy { get { return _vy; } set { _vy = value; }  }
    public double x { get { return _x; } set { _x = value; }  }
    public double y { get { return _y; } set { _y = value; }  }
    // Method
    // Movement
    public void Move(double inc)
    {
        _x = _x + _vx * inc;
        _y = _y + _vy * inc;
    }

    public void Draw(Box box)
    {
        box.FillCircle(Color.Black, _x, _y, _r);
    }

    public void WallCollsion(int boundX, int boundY)
    {
        // Collisions occur when the particles is within a radius distance from the wall
        if (_x > (boundX - _r))
        {
            _x = boundX - _r;
            _vx = _vx * (-1); // Reverse trajectory
        }
        if (_x < (0 + _r))
        {
            _x = (0 + _r);
            _vx = _vx * (-1); // Reverse trajectory
        }
        if (_y > (boundY - _r))
        {
            _y = (boundY - _r);
            _vy = _vy * (-1);
        }
        if (_y < (0 + _r))
        {
            _y = (0 + _r);
            _vy = _vy * (-1);
        }
    }

    public void ParticleCollision (Particle p2)
    {
        
        double[] v1PrimeArr = new double[2]; 
        double[] v2PrimeArr = new double[2]; 
         
        // Convert using Linear Algebra, simplifies calculations
        // Velocity Vector
        var v1 = Vector<double>.Build.Dense(new[] { _vx, _vy });
        var v2 = Vector<double>.Build.Dense(new[] { p2._vx, p2._vy });
        // Position Vector
        var x1 = Vector<double>.Build.Dense(new[] { _x, _y });
        var x2 = Vector<double>.Build.Dense(new[] { p2._x, p2._y });
        // Mass
        double m1 = _m; double m2 = p2._m;

        // Initialise new Vectors
        // Vectors are velocity vectors after collision
        var v1Prime = Vector<double>.Build.Dense(2);
        var v2Prime = Vector<double>.Build.Dense(2);

        // Check for collisions
        // If radial distance between center of two particles is less than the sum of their radii then this is a collision
        if (Math.Sqrt(Math.Pow(p2._x - _x, 2) + Math.Pow(p2._y - _y, 2)) < (_r + p2._r))
        {    
            
            // Find the euclidean distance between two points
            double overlapDistance = Math.Sqrt(Math.Pow(p2._x - _x, 2) + Math.Pow(p2._y - _y, 2));
            // Adjustment is the amount the particles need to be separated in order not overlap
            double adjustment = (_r + p2._r - overlapDistance) / 1.99; // Give small buffer so < 2
            // Calcualte the unit vector along the plane of collision 
            Vector<double> unitVector;   
            unitVector = (x1 - x2).Divide((x1 - x2).L2Norm());

            // Adjust particles to remove overlap
            x1 = x1 + (unitVector * adjustment);
            x2 = x2 - (unitVector * adjustment);
            // Adjust particles 
            // Set THIS particles position
            _x = x1.At(0); _y = x1.At(1);
            // Set OTHER particle position
            p2._x = x2.At(0); p2._y = x2.At(1);

            // Collision Calc
            // Some vector calculations
            // This can also be done formulaicly but I find vector to be simpler to type (let the library Math.Net to the work for you)
            v1Prime = v1 - (( (2 * m2) / (m1 + m2) ) * ( (v1 - v2).DotProduct( (x1 - x2) ) ) * ( x1 - x2 ) / Math.Pow( (x1 - x2).Norm(2), 2 ));
            v2Prime = v2 - (( (2 * m1) / (m1 + m2) ) * ( (v2 - v1).DotProduct( (x2 - x1) ) ) * ( x2 - x1 ) / Math.Pow( (x2 - x1).Norm(2), 2 ));

            // Update velocities
            // Set THIS particles velocities
            _vx = v1Prime.At(0); _vy = v1Prime.At(1);
            // Set OTHER particle velocities
            p2.vx = v2Prime.At(0); p2.vy = v2Prime.At(1);

            // Finally need to unattach particles which collide but do not separate
            // Particles can get stuck since a collision is only registered after they overlap. This results in a 
            // constant collison being triggerted every loop and the particles "Stick" together.
            // ie so for purposes of this program I just adjust them back the way they cam by some fudge factor
            // This fudge factor is proportional to the overlapping distance.

            // _x = _x + _vx * 5; _y = _y + _vy * 5;
            p2.x = p2.x + p2.vx * 1; p2.y = p2.y + p2.vy * 1;
        }
    }
}