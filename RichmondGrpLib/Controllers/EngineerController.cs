using RichmondGrpLib.Entities;
using RichmondGrpLib.Models;
using RichmondGrpLib.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RichmondGrpLib.Controllers
{
    public class EngineerController
    {
        /**
         * Get All Engineers
         * 
         * */
        public List<EngineerModel> GetAllEngineers()
        {
            EngineerService service = new EngineerService();
            return service.GetAllEngineers();
        }
    }
}
