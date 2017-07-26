using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chutzpah.Models.JS
{
    public class JsSnapshot : JsTestCase
    {
        public Dictionary<string, Dictionary<string, string>> Snapshots { get; set; }
    }
}
