using Flurl.Http;

namespace DataLayer
{
    public class FlurlDataContext : IDataContext<IFlurlClient>
    {
        static string APItoken = "";
        private string baseUri = $"https://api.mapsindoors.com/{APItoken}/api";


        public IFlurlClient Open()
        {
            return new FlurlClient(baseUri);
        }
    }
}
