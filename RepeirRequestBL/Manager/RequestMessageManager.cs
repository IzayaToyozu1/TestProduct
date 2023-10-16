using RepeirRequestBL.Model;

namespace RepeirRequestBL.Manager
{
    public class RequestMessageManager
    {
        public RequestMessageManager() 
        { 
            
        }

        public void Edit(RequestMessage request, string text)
        {
            request.Comment = text;
        }

        public async Task EditAsync(RequestMessage request, string text) 
            => await Task.Run(() => { Edit(request, text); });
    }
}
