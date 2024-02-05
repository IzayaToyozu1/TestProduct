using System.ComponentModel;
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
        
        private RequestRepair[] CheckDataRequests(RequestRepair[] requests)
        {
            List<RequestRepair> requestsList = new List<RequestRepair>();
            var result = _dbContext.ProcGetData<RequestRepair>();

            for (int i = 0; i < result.Length; i++)
            {
                var req = _requestsRepairIsNotBegin.FirstOrDefault(r => r.Id == result[i].Id);
                bool reqIsDone = req == null && result[i].Status == _statusRequestDone.Id;
                bool reqIsEquals = req.Equals(result[i]);
                if (reqIsDone || reqIsEquals) 
                    continue;
                
                requestsList.Add(req);
            }
            
            return requestsList.ToArray();
        }

        private async void UpdateDataAsync(RequestRepair[] editorData)
        {
            Task.Run(() =>
            {
                for (int i = 0; i < editorData.Length; i++)
                {
                    if (editorData[i].Status == _statusRequestDone.Id)
                    {
                        var request = _requestsRepairIsNotBegin.FirstOrDefault(r => editorData[i].Id == r.Id);
                        RequestDelete(_requestsRepairIsNotBegin, request);
                        RequestInsert(_requestsRepairIsBegin, request);
                    }
                    else
                    {

                    }
                }
            });
        }

        private void RequestDelete(RequestRepair[] editorData, RequestRepair repair)
        {
            var data = new RequestRepair[editorData.Length];
            data = editorData.Where(r => r.Id != repair.Id).ToArray();
            editorData = data;
        }

        private void RequestInsert(RequestRepair[] editorData, RequestRepair repair)
        {

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
