using RepairRequestDB.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairRequestDB.Controller
{
    public class RequestRepairController
    {
        private RequestRepair[] _requestsRepairIsBegin;
        private RequestRepair[] _requestsRepairIsNotBegin;
        private DBContext _dbContext;

        public RequestRepairController(DBContext context)
        {
            _requestsRepairIsBegin = new RequestRepair[0];
            _requestsRepairIsNotBegin = new RequestRepair[0];
            _dbContext = context;
            var requstsRepair = context.ProcGetData<RequestRepair>();
            SplitRequestAsync(requstsRepair);
        }

        private void SplitRequest(RequestRepair[] requestsRepair)
        {
            List<RequestRepair> requestsRepairIsBegin = new List<RequestRepair>();
            List<RequestRepair> requestsRepairIsNotBegin = new List<RequestRepair>();
            foreach(var request in requestsRepair)
            {
                if(request.Status == 3)
                {
                    requestsRepairIsNotBegin.Add(request);
                }
                else
                    requestsRepairIsBegin.Add(request);
            }

            _requestsRepairIsBegin = requestsRepairIsBegin.ToArray();
            _requestsRepairIsNotBegin = requestsRepairIsNotBegin.ToArray();
        }

        private async Task SplitRequestAsync(RequestRepair[] requestRepair)
        {
            Task.Run(() => SplitRequest(requestRepair));
        }
    }
}
