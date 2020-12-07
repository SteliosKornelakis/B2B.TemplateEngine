using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2B.TemplateEngine
{
  public class ListMapper
  {
    public ListMapper(string name)
    {
      Name = name;
    }

    public string Name { get; }

    public List<Dictionary<string, Func<string>>> List { get; } = new List<Dictionary<string, Func<string>>>();
  }
}
