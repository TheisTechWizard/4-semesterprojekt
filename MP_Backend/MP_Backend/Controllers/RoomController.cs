using DataLayer;
using Flurl.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;

namespace MP_Backend.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class RoomController : ControllerBase
    {
        RoomData roomData;
        public RoomController(IDataContext<IFlurlClient> dataContext)
        {
            roomData = new RoomData(dataContext);
        }

        [HttpGet("{room_name}")]
        public Room GetRoomByName(string room_name)
        {
            try
            {
                Room room = roomData.GetRoomByName(room_name);
                return room;
            }
            catch (JscemaNotLoadedExeption)
            {
                throw;
            }
            catch (MapsPeopleRoomAPINotLoadedException)
            {
                throw;
            }
        }

        [HttpGet]
        public IEnumerable<Room> GetMeetingRooms()
        {
            try
            {
                return roomData.GetMeetingRooms();
            }
            catch (JscemaNotLoadedExeption)
            {
                throw;
            }
            catch (MapsPeopleRoomAPINotLoadedException)
            {
                throw;
            }
        }

        [HttpGet("{room_id}")]
        public Room GetRoomById(string room_id)
        {
            try
            {
                return roomData.GetRoomById(room_id);  
            }
            catch (JscemaNotLoadedExeption)
            {
                throw;
            }
            catch (MapsPeopleRoomAPINotLoadedException)
            {
                throw;
            }
        }
    }
}
