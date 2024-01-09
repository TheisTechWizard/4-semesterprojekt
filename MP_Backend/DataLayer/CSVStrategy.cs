using CsvHelper;
using Flurl.Http;
using ModelLayer;
using System.Globalization;

namespace DataLayer
{
    public class CSVStrategy : IStrategy<Mapping>
    {
        private string filePath;
        private RoomData roomData;

        public CSVStrategy(IDataContext<IFlurlClient> context)
        {
            this.filePath = @"..\DataLayer\Assets\mapping.csv";
            this.roomData = new RoomData(context);
        }

        public CSVStrategy(IDataContext<IFlurlClient> context, string fileName)
        {
            this.filePath = fileName;
            this.roomData = new RoomData(context);
        }

        private void WriteToCSV(Mapping mapping)
        {
            try
            {
                using (var stream = File.Open(filePath, FileMode.Append))
                using (var writer = new StreamWriter(stream))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecord(mapping);
                    csv.NextRecord();
                }
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException("Could not find and load CSV file");
            }
        }

        public IEnumerable<Mapping> GetAll()
        {
            IEnumerable<string> strCSV = File.ReadLines(filePath);
            IEnumerable<Mapping> res = from str in strCSV
                                       let tmp = str.Split(',')
                                       select new Mapping
                                       {
                                           Id = tmp[0],
                                           Origin = tmp[1],
                                           OriginExternalId = tmp[2],
                                           InternalIdList = tmp[3],
                                           RoomName = tmp[4]
                                       };
            return res;
        }

        public Mapping Save(Mapping mapping)
        {
            if (mapping.Id == "" || mapping.Id == null)
            {
                mapping.Id = DateTime.UtcNow.ToFileTimeUtc().ToString();
            }

            if (mapping.InternalIdList == "" || mapping.InternalIdList == null)
            {
                mapping.InternalIdList = roomData.GetRoomByName(mapping.RoomName).Id;
            }

            WriteToCSV(mapping);
            return mapping;
        }

        public void Update(Mapping mapping)
        {
            try
            {
                string[] mappings = File.ReadAllLines(filePath);
                List<string> mappingList = mappings.ToList();
                foreach (string mappingItem in mappingList)
                {
                    string[] id = mappingItem.Split(',');

                    if (id[0].Contains(mapping.Id))
                    {
                        mappingList.Remove(mappingItem);
                        Delete(mapping.Id);
                        mapping.InternalIdList = roomData.GetRoomByName(mapping.RoomName).Id;
                        WriteToCSV(mapping);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public bool Delete(string mapping_id)
        {
            string[] mappings = File.ReadAllLines(filePath);
            List<string> mappingList = mappings.ToList();
            bool deleted = false;
            int i = 0;

            while (deleted != true)
            {
                string mappingItem = mappingList.ElementAt(i);
                string[] id = mappingItem.Split(',');

                if (id[0].Contains(mapping_id))
                {
                    mappingList.Remove(mappingItem);
                    deleted = true;
                }
                i++;
            }

            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                foreach (string m in mappingList)
                {
                    Mapping mapp = new Mapping();
                    string[] map = m.Split(',');
                    mapp.Id = map[0];
                    mapp.Origin = map[1];
                    mapp.OriginExternalId = map[2];
                    mapp.InternalIdList = map[3];
                    mapp.RoomName = map[4];

                    csv.WriteRecord(mapp);
                    csv.NextRecord();
                }
            }
            return deleted;
        }

        public Mapping GetById(string id)
        {
            IEnumerable<string> strCSV = File.ReadLines(filePath);
            IEnumerable<Mapping> res = from str in strCSV
                                       let tmp = str.Split(',')
                                       where tmp[0] == id
                                       select new Mapping
                                       {
                                           Id = tmp[0],
                                           Origin = tmp[1],
                                           OriginExternalId = tmp[2],
                                           InternalIdList = tmp[3],
                                           RoomName = tmp[4]
                                       };
            return res.FirstOrDefault();
        }

        public IEnumerable<Mapping> GetByRoomName(string roomName)
        {
            IEnumerable<string> strCSV = File.ReadLines(filePath);
            IEnumerable<Mapping> res = from str in strCSV
                                       let tmp = str.Split(',')
                                       where tmp[4] == roomName
                                       select new Mapping
                                       {
                                           Id = tmp[0],
                                           Origin = tmp[1],
                                           OriginExternalId = tmp[2],
                                           InternalIdList = tmp[3],
                                           RoomName = tmp[4]
                                       };
            return res;
        }
    }
}
