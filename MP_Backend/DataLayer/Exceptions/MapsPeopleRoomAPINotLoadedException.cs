using System.Runtime.Serialization;

namespace DataLayer
{

    public class MapsPeopleRoomAPINotLoadedException : Exception
    {
        public MapsPeopleRoomAPINotLoadedException(string message) : base(message)
        {
        }

       
    }
}