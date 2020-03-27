using System;
using System.Text;

using Struct.Core;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            LocalStorage ls = new LocalStorage();

            ls.Set("PlayerInfo",
                new SampleObject
                {
                    name = "Jim Roton",
                    phone = "214.735.0934",
                    address = new Address()
                    {
                        address = "2316 Elm Valley Dr",
                        city = "Little Elm",
                        state = "TX",
                        zip = "75068"
                    }
                }
            );

            SampleObject so = ls.Get<SampleObject>("PlayerInfo");

            Console.Write(so);
        }

        public class SampleObject
        {
            public string name { get; set; }

            public string phone { get; set; }

            public Address address { get; set; }

            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(this.name);
                sb.AppendLine(this.phone);
                sb.AppendLine(this.address.address);
                sb.AppendLine(this.address.city);
                sb.AppendLine(this.address.state);
                sb.AppendLine(this.address.zip);

                return sb.ToString();
            }
        }

        public class Address
        {
            public string address { get; set; }

            public string city { get; set; }

            public string state { get; set; }

            public string zip { get; set; }
        }
    }
}
