using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ZoomInside
{
    public class EsItem
    {
        public string Info { get; set; }
        public string DangerScale { get; set; }
    }
    public class Аllergens
    {
        public List<string> Allergens { get; set; }
        public string Message { get; set; }
    }
}
