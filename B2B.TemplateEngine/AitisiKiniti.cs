using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2B.TemplateEngine
{

  public class NeaSyndesi
  {
    public string ServiceId { get; set; }
    public string Armodios { get; set; }
    public string TyposYphresias { get; set; }
  }
  public class AitisiKiniti
  {
    public string FullName { get; set; }
    public string Mobile { get; set; }
    public bool IsExisting { get; set; }
    public List<NeaSyndesi> NeaSyndesi { get; internal set; } = new List<NeaSyndesi>();
  }
}
