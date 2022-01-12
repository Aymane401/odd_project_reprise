using System;
using System.Threading;

namespace App_UI.Models
{
    /// <summary>
    /// Animal class
    /// Represents an animal with weight, height, name, latin name, and species
    /// </summary>
    public class Animal : ICloneable
    {
        static Random rnd = new Random();
        static int nextId;

        public int Id { get; private set; }

        public int Weight { get; set; }
        public int Height { get; set; }
        public string Name { get; set; }
        public string LatinName { get; set; }
        public string Group { get; set; }
        public string Enclosure { get; set; }

        public override string ToString() => $"{Name} ({LatinName})";

        public Animal()
        {
            Id = Interlocked.Increment(ref nextId);

            Weight = rnd.Next(1, 10);
            Height = rnd.Next(10, 100);
        }

        public object Clone()
        {
            var clone = (Animal)MemberwiseClone();
            //clone.Id = Interlocked.Increment(ref nextId);
            return clone;
        }
    }
}
