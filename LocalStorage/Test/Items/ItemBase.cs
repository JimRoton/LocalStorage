using System.Text;
using System.Reflection;

namespace Test
{
    public abstract class ItemBase
    {
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            PropertyInfo[] properties = this.GetType().GetProperties();

            sb.AppendLine($"--- {this.GetType().Name} ---");

            foreach (PropertyInfo pi in properties)
                sb.AppendLine($"{pi.Name}: {this.GetType().GetProperty(pi.Name).GetValue(this)}");

            return sb.ToString();
        }
    }
}
