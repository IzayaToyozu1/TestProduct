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
        private StatusRequest _statusRequestDone;

        public event EventHandler UpdateRequestsRepair;
        public event EventHandler EditRequestsRepair;

        public RequestRepairController(DBContext context, SettingsModel model)
        {
            _requestsRepairIsBegin = new RequestRepair[0];
            _requestsRepairIsNotBegin = new RequestRepair[0];
            _dbContext = context;
            _statusRequestDone = context.ProcGetData<StatusRequest>().First(s => s.Id == model.StatusRequestIsDone);
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

        public void CheckRequestsRepair()
        {
            
        }

        private void CheckDataRequests(RequestRepair[] requests)
        {
            var result = _dbContext.ProcGetData<RequestRepair>();
            result = SplitExceptRequestRepairs(result, _statusRequestDone);

            if (result.Equals(_requestsRepairIsBegin))
                return;

            for (int i = 0; i < requests.Length; i++)
            {
                var request = _requestsRepairIsBegin.FirstOrDefault(req => req.Id == requests[i].Id);
                if(request == null)
                    continue;
                request.Equals(requests[i]);
            }
        }

        private RequestRepair[] CopyDataArray(RequestRepair[] dataCopy)
        {
            RequestRepair[] result = new RequestRepair[0];
            result.CopyTo(dataCopy, 0);
            return result;
        }

        private RequestRepair[] SplitRequestsRepair(RequestRepair[] requestRepairs, StatusRequest requestStatus)
        {
            List<RequestRepair> requestRepairResult = new List<RequestRepair>();

            foreach (var repair in requestRepairs)
            {
                if (repair.Status == requestStatus.Id)
                {
                    requestRepairResult.Add(repair);
                }
            }

            return requestRepairs.ToArray();
        }

        private RequestRepair[] SplitExceptRequestRepairs(RequestRepair[] requestRepairs, StatusRequest requestStatus)
        {
            List<RequestRepair> requestRepairResult = new List<RequestRepair>();
            foreach (var request in requestRepairs)        
            {
                if (request.Status != requestStatus.Id)
                {
                    requestRepairResult.Add(request);
                }    
            }

            return requestRepairResult.ToArray();
        }

        private void SplitRequest(RequestRepair[] requestsRepair)
        {
            _requestsRepairIsBegin = 
                SplitRequestsRepair(_dbContext.ProcGetData<RequestRepair>(), _statusRequestDone);
            _requestsRepairIsNotBegin =
                SplitExceptRequestRepairs(_dbContext.ProcGetData<RequestRepair>(), _statusRequestDone);
        }

        private async Task SplitRequestAsync(RequestRepair[] requestRepair)
        {
            Task.Run(() => SplitRequest(requestRepair));
        }
    }
}
