using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairRequestDB.Model
{
    [NameProc("getStatus", "")]
    [NameScheme("Repair")]
    public class StatusRequest
    {
        public int Id { get; set; }

        [NameFieldDB("cName")] public string Name { get; set; }
    }
}
