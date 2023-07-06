namespace TestProduct.Service
{
    public class ShortTimeService: ITimeService
    {
        public string GetTime() => DateTime.Now.ToShortTimeString();
    }
}
