namespace DataLayer
{
    public interface IStrategy<Type>
    {
        public Type Save(Type type);
        public bool Delete(string id);
        public IEnumerable<Type> GetAll();
        public void Update(Type type);
        public Type GetById(string id);
        public IEnumerable<Type> GetByRoomName(string roomName);
    }
}
