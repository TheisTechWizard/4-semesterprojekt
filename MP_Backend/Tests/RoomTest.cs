using DataLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelLayer;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    [TestClass]
    public class RoomTest
    {
        private string jschemaPath = @"..\..\..\..\DataLayer\Assets\JsonMeetingRoomSchema.json";
        [TestMethod]
        public void GetRoomById_RoomExists_ReturnCorrectRoom()
        {
            RoomData data = new(jschemaPath, new FlurlDataContext());
            Room room = data.GetRoomById("2cad5c6e3a25448484a2e721");

            Assert.AreEqual(room.Name, "Tokyo");
        }

        [TestMethod]
        public void GetMeetingRooms_RoomsExists_ReturnsAllExistingRooms()
        {
            RoomData data = new(jschemaPath, new FlurlDataContext());
            IEnumerable<Room> rooms = data.GetMeetingRooms();


            Assert.AreEqual(rooms.Count(), 10);
        }

        [TestMethod]
        public void GetRoomByName_RoomExists_ReturnCorrectRoom()
        {
            RoomData data = new RoomData(jschemaPath, new FlurlDataContext());
            Room room = data.GetRoomByName("Tokyo");

            Assert.AreEqual(room.Id, "2cad5c6e3a25448484a2e721");
        }
    }
}
