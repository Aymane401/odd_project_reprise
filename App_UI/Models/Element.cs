using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App_UI.Models
{
    /// <summary>
    /// Element of the periodic table
    /// Atomic Number, Name, Symbol, Atomic Weight, Phase, Type, Melting Point (°C), Boiling Point (°C), Density (g/cm3), Discoverer, Discovery (Year)
    /// </summary>
    public class Element : ICloneable
    {
        static Random rnd = new Random();
        static int nextId;
        public int Id { get; private set; }

        public int AtomicNumber { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public double AtomicWeight { get; set; }
        public string Phase { get; set; }
        public string Type { get; set; }
        public double MeltingPoint { get; set; }
        public double BoilingPoint { get; set; }
        public double Density { get; set; }
        public string Discoverer { get; set; }
        public int Discovery { get; set; }

        public override string ToString() => $"{Name} ({Symbol})";

        public Element()
        {
            Id = Interlocked.Increment(ref nextId);
        }

        public object Clone()
        {
            var clone = (Element)MemberwiseClone();
            //clone.Id = Interlocked.Increment(ref nextId);
            return clone;
        }
    }
}
