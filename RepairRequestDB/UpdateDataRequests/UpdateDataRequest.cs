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
        private readonly RequestRepairController _controller;

        public UpdateDataRequest(DBContext context, RequestRepairController requestController)
        {
            _context = context;
            _controller = requestController;
        }

        public void CheckDataRequestsRepair()
        {
            _controller.
        }
    }
}
