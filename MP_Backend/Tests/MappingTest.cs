using DataLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelLayer;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Tests
{
    [TestClass]
    public class MappingTest
    {
        public static List<string> files = new List<string>();

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            foreach (var file in files)
            {
                DeleteCSV(file);
            }
        }

        public static void DeleteCSV(string fileName)
        {
            File.Delete(@$"..\..\..\..\DataLayer\Assets\{fileName}.csv");
        }

        public CSVStrategy FillData(string fileName)
        {
            FlurlDataContext dataContext = new FlurlDataContext();
            CSVStrategy strategy = new CSVStrategy(dataContext, @$"..\..\..\..\DataLayer\Assets\{fileName}.csv");
            files.Add(fileName);

            Mapping mapping1 = new();
            mapping1.Id = "1";
            mapping1.Origin = "Temp";
            mapping1.OriginExternalId = "11";
            mapping1.RoomName = "Manaus";

            Mapping mapping2 = new();
            mapping2.Id = "2";
            mapping2.Origin = "Temp";
            mapping2.OriginExternalId = "22";
            mapping2.RoomName = "Tokyo";

            Mapping mapping3 = new();
            mapping3.Id = "3";
            mapping3.Origin = "Humi";
            mapping3.OriginExternalId = "33";
            mapping3.RoomName = "Copenhagen";

            strategy.Save(mapping1);
            strategy.Save(mapping2);
            strategy.Save(mapping3);

            return strategy;
        }

        [TestMethod]
        public void GetByMappingId_MappingExists_ReturnCorrectMapping()
        {
            CSVStrategy s = FillData("GetByMappingId");

            Mapping getMapping = s.GetById("1");

            Assert.AreEqual(getMapping.RoomName, "Manaus");
            Assert.AreEqual(getMapping.InternalIdList, "24a8239713984ead8546b3cc");
        }

        [TestMethod]
        public void SaveToCSV_ValidMapping_CorrectMappingSavedToCSVFile()
        {
            //Intergrationstest
            CSVStrategy s = FillData("SaveToCSV");
            Mapping mapping = new Mapping();
            mapping.Id = "4";
            mapping.Origin = "Humi";
            mapping.OriginExternalId = "44";
            mapping.InternalIdList = "";
            mapping.RoomName = "Tokyo";
            s.Save(mapping);

            Mapping res = s.GetById("4");
            Assert.AreEqual(res.Id, mapping.Id);
            Assert.AreEqual(res.Origin, mapping.Origin);
            Assert.AreEqual(res.OriginExternalId, mapping.OriginExternalId);
            Assert.AreEqual(res.InternalIdList, mapping.InternalIdList);
        }

        [TestMethod]
        public void DeleteMappingFromCSV_MappingExists_CorrectMappingIsDeletedFromCSV()
        {
            CSVStrategy s = FillData("DeleteMappingFromCSV");
            bool deleted = s.Delete("2");
            Mapping res = s.GetById("2");

            Assert.IsNull(res);
            Assert.IsTrue(deleted);
        }

        [TestMethod]
        public void UpdateMapping_ValidChangeToExistingMapping_CorrectMappingUpdatedWithValidData()
        {
            CSVStrategy s = FillData("UpdateMapping");
            Mapping res = s.GetById("2");
            res.Origin = "Humi";

            s.Update(res);

            Assert.AreEqual(s.GetById("2").Origin, "Humi");
            Assert.AreEqual(s.GetAll().Count(), 3);
        }
    }
}