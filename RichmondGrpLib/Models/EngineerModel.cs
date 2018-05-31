using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RichmondGrpLib.Models
{
    public class EngineerModel
    {
        public int EngineerId { get; set; }
        public string EngineerName { get; set; }
        public System.DateTime CreateDate { get; set; }
        public string CreateUserId { get; set; }
        public System.DateTime UpdateDate { get; set; }
        public string UpdateUserId { get; set; }
    }
}
