namespace RepeirRequestBL.DataBase
{
    public interface IApplicationDB
    {
        public string Connection { get; set; }

        public T ExecuteMethod<T>();

        public async Task<T> ExecuteMethodAsync<T>()
        {
            await Task.Delay(0);
            return ExecuteMethod<T>();
        }

        public T ExecuteMethod<T>(object obj);



    }
}
