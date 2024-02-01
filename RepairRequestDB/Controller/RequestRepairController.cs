using System.Data;
using RepairRequestDB.Model;
using WorkerSQL;

namespace RepairRequestDB.Controller
{
    public class RequestRepairController
    {
        private RequestRepair[] _requestsRepairIsBegin;
        private RequestRepair[] _requestsRepairIsNotBegin;
        private DBContext _dbContext;

        public event EventHandler UpdateRequestsRepair;
        public event EventHandler EditRequestsRepair;

        public RequestRepairController(DBContext context)
        {
            _requestsRepairIsBegin = new RequestRepair[0];
            _requestsRepairIsNotBegin = new RequestRepair[0];
            _dbContext = context;
            var requstsRepair = context.ProcGetData<RequestRepair>();
            SplitRequestAsync(requstsRepair);
        }

        public RequestRepair[] GetRequestsRepairIsBegin()
        {
            return CopyDataArray(_requestsRepairIsBegin);
        }

        public RequestRepair[] GetRequestRepairIsNotBegin()
        {
            return CopyDataArray(_requestsRepairIsNotBegin);
        }

        private RequestRepair[] CopyDataArray(RequestRepair[] dataCopy)
        {
            RequestRepair[] result = new RequestRepair[0];
            result.CopyTo(dataCopy, 0);
            return result;
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
