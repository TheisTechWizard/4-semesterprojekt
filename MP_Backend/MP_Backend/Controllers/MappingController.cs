using DataLayer;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;

namespace MP_Backend.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class MappingController : ControllerBase
    {
        MappingData mpData;
        public MappingController(IStrategy<Mapping> strategy)
        {
            mpData = new MappingData(strategy);
        }

        [HttpGet]
        public IEnumerable<Mapping> Get()
        {
            return mpData.GetAllMappings();
        }

        [HttpPost]
        public void Post(Mapping mapping)
        {
            mpData.SaveMapping(mapping);
        }

        [HttpPut]
        public void Put(Mapping mapping)
        {
            mpData.UpdateMapping(mapping);
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            mpData.DeleteMapping(id);
        }

        [HttpGet("{id}")]
        public Mapping GetMappingById(string id)
        {
            return mpData.GetMappingById(id);
        }

        [HttpGet("{room_name}")]
        public IEnumerable<Mapping> GetByName(string room_name)
        {
            return mpData.GetMappingByRoomName(room_name);
        }
    }
}
