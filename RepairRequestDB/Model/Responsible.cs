using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairRequestDB.Model
{
    [NameProc("getAllResponsible", "insertOrUpdateResponsible")]
    [NameScheme("Repair")]
    public class Responsible
    {
        [NameFieldDB("id")] public int Id { get; set; }
        [NameFieldDB("cName")] public string Name { get; set; }
        [NameFieldDB("isActive")] public bool IsActive { get; set; }
        public int TypeService { get; set; }
        [NameFieldDB("obj")] public string NameObj { get; set; }
        [NameFieldDB("id_obj")] public int IdObj { get; set; }
        [NameFieldDB("kadr_id")] public int IdPersonal { get; set; }
        [NameFieldDB("kadr_name")] public string NamePersonal { get; set; }
        [NameFieldDB("isWorkNow")] public bool IsWork { get; set; }
    }
}
