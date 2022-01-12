using System;
using System.Threading;

namespace App_UI.Models
{
    public class Person : ICloneable
    {
        static int nextId;

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FullName { get => $"{LastName}, {FirstName}"; }

        public string City { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public DateTime BirthDay { get; set; }
        public int Id { get; private set; }

        public override string ToString() => FullName;


        public Person()
        {
            Id = Interlocked.Increment(ref nextId);
        }

        public object Clone()
        {
            var clone = (Person)MemberwiseClone();
            //clone.Id = Interlocked.Increment(ref nextId);
            return clone;
        }
    }
}
