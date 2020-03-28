using System.Text;
using System.Reflection;

namespace Test
{
    public class Address : ItemBase
    {
        public string Address1 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }

        public override string ToString()
        {
            return $"{this.Address1}, {this.City}, {this.State} {this.Zip}";
        }
    }
}
