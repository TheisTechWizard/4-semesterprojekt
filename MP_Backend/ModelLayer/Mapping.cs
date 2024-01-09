using CsvHelper.Configuration.Attributes;

namespace ModelLayer
{
    public class Mapping
    {
        public string Id { get; set; }
        public string Origin { get; set; }
        public string OriginExternalId { get; set; }
        public string InternalIdList { get; set; }
        public string RoomName { get; set; }
    }
}
