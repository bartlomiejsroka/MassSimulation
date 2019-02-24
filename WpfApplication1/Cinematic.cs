using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    class Cinematic
    {
        private double mass, friction;
        private double velocity;
        public Cinematic(double mass, double friction)
        {
            this.mass = mass;
            this.friction = friction;  
        }
        public double CalculatePos(double time)
        {
            var pos =(time / friction )+ (mass/(friction*2))*(Math.Exp(-( friction * time) / mass)) - mass/(2*friction);
            return pos;
           
        }
        public double NoforcePosision(double time)
        {
            var vel = (1 / friction) - (1 / friction) * Math.Exp((-friction * time) / mass);
            return vel;
        }
    }
}
