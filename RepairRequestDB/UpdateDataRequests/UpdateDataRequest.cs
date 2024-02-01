using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepairRequestDB.Controller;
using WorkerSQL;

namespace RepairRequestDB.UpdateDataRequests
{
    public class UpdateDataRequest
    {
        private readonly DBContext _context;

        public UpdateDataRequest(DBContext context, RequestRepairController requestController)
        {
            _context = context;
        }

        public bool CompareArray<T>(ref T[] array1, T[] array2) where T : class
        {
            if (array1.Equals(array2))
                return true;
            else
                return false;
        }
    }
}
