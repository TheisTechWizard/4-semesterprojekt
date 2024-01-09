using ModelLayer;

namespace DataLayer
{
    public class MappingData
    {
        IStrategy<Mapping> strategy;

        public MappingData(IStrategy<Mapping> strategy)
        {
            this.strategy = strategy;
        }
        public bool DeleteMapping(string id)
        {
            return strategy.Delete(id);
        }

        public IEnumerable<Mapping> GetAllMappings()
        {
            return strategy.GetAll();
        }

        public Mapping GetMappingById(string id)
        {
            return strategy.GetById(id);
        }

        public Mapping SaveMapping(Mapping type)
        {
            return strategy.Save(type);
        }

        public void UpdateMapping(Mapping type)
        {
            strategy.Update(type);
        }

        public IEnumerable<Mapping> GetMappingByRoomName(string roomName)
        {
            return strategy.GetByRoomName(roomName);
        }
    }
}
