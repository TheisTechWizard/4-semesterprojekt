using Flurl.Http;
using ModelLayer;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace DataLayer
{
    public class RoomData
    {
        IDataContext<IFlurlClient> dataContext;
        private string filepath;

        public RoomData(IDataContext<IFlurlClient> dataContext)
        {

            this.filepath = @"..\DataLayer\Assets\JsonMeetingRoomSchema.json";
            this.dataContext = dataContext;
        }

        //Constructor used for testing with different filepath
        public RoomData(string json_filepath, IDataContext<IFlurlClient> dataContext)
        {
            this.dataContext = dataContext;
            this.filepath = json_filepath;
        }

        public RoomData(IDataContext<IFlurlClient> dataContext, string filepath)
        {
            this.filepath = filepath;
            this.dataContext = dataContext;
        }

        public Room GetRoomById(string id)
        {
            Room res = null;
            JSchema schema;
            JToken json;

            try
            {
                schema = JSchema.Parse(File.ReadAllText(filepath));
                json = dataContext.Open().Request($"locations/details/{id}?v=1").GetJsonAsync<JToken>().Result;
            }
            catch (FileNotFoundException ex)
            {
                throw new JscemaNotLoadedExeption(ex.Message);
            }
            catch (AggregateException)
            {
                throw new MapsPeopleRoomAPINotLoadedException("Could not load data from MapsPeople");
            }

            //ValidationEventsList contain all bad json 
            IList<string> validationEvents = new List<string>();
            if (json.IsValid(schema, out validationEvents))
            {
                Room room = new Room();
                room.Id = json["id"].ToString();
                room.Name = json["properties"]["name"].ToString();
                room.Building = json["properties"]["building"].ToString();
                room.Floor = json["properties"]["floorName"].ToString();
                room.RoomId = json["properties"]["roomId"].ToString();
                res = room;
            }
            else
            {
                //Examine following list to find all bad data recieved from API
                //validationEvents.FirstOrDefault();
            }

            return res;
        }

        public Room GetRoomByName(string name)
        {
            Room res = null;
            JSchema schema;
            IEnumerable<JToken> json;

            try
            {
                schema = JSchema.Parse(File.ReadAllText(filepath));
                json = dataContext.Open().Request($"locations/Stigsborgvej%2FStigsborgvej%2F10?locationtype=poi&includeOutsidePOI=false&types=MeetingRoom&q={name}&orderby=name%2Cdesc&v=1&qFields=name").GetJsonAsync<JToken>().Result;
            }
            catch (FileNotFoundException ex)
            {
                throw new JscemaNotLoadedExeption(ex.Message);
            }
            catch (AggregateException)
            {
                throw new MapsPeopleRoomAPINotLoadedException("Could not load data from MapsPeople API");
            }

            //ValidationEventsList contain all bad json data
            IList<string> validationEvents = new List<string>();

            foreach (JToken token in json)
            {
                if (token.IsValid(schema, out validationEvents))
                {
                    Room room = new Room();
                    room.Id = token["id"].ToString();
                    room.Name = token["properties"]["name"].ToString();
                    room.Building = token["properties"]["building"].ToString();
                    room.Floor = token["properties"]["floorName"].ToString();
                    room.RoomId = token["properties"]["roomId"].ToString();
                    res = room;
                }
                else
                {
                    //Examine following list to find all bad data recieved from API
                    //validationEvents.FirstOrDefault();
                }
            }
            return res;
        }

        public IEnumerable<Room> GetMeetingRooms()
        {
            IEnumerable<JToken> json;
            List<Room> res = new List<Room>();
            JSchema schema;

            try
            {
                json = dataContext.Open().Request("locations/Stigsborgvej%2FStigsborgvej%2F10?locationtype=poi&includeOutsidePOI=false&types=MeetingRoom&q=&orderby=name%2Cdesc&v=1&qFields=name").GetJsonAsync<IEnumerable<JToken>>().Result;
                schema = JSchema.Parse(File.ReadAllText(filepath));
            }
            catch (AggregateException)
            {
                throw new MapsPeopleRoomAPINotLoadedException("Could not load data from Mapspeople API");

            }
            catch (FileNotFoundException ex)
            {
                throw new JscemaNotLoadedExeption(ex.Message);
            }


            IList<string> validationEvents = new List<string>();

            foreach (JToken item in json)
            {
                if (item.IsValid(schema, out validationEvents))
                {

                    Room room = new Room();
                    room.Id = item["id"].ToString();
                    room.Name = item["properties"]["name"].ToString();
                    room.Building = item["properties"]["building"].ToString();
                    room.Floor = item["properties"]["floorName"].ToString();
                    room.RoomId = item["properties"]["roomId"].ToString();

                    res.Add(room);
                }
                else
                {
                    //Examine following list to find all bad data recieved from API
                    //validationEvents.FirstOrDefault();
                }

            }
            return res;

        }
    }
}