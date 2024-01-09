import React, { useState } from "react";

import "./addmapping.css";

const Addmapping = ({ childToParent, rooms, sensorType }) => {
  const [roomName, setRoomName] = useState("");
  const [origin, setOrigin] = useState("");
  const [originExternalId, setOriginExternalId] = useState("");

  childToParent({ roomName, origin, originExternalId });

  return (
    <div className="addmapping">
      <div className="addmapping-internalidlist">
        <select value={roomName} onChange={(e) => setRoomName(e.target.value)}>
          <option value="" disabled>
            Select a room
          </option>
          {rooms.map((room) => (
            <option key={room.id} value={room.name}>
              {room.name}
            </option>
          ))}
        </select>
      </div>
      <div className="addmapping-origin">
        <select value={origin} onChange={(e) => setOrigin(e.target.value)}>
          <option value="" disabled>
            Select a sensor type
          </option>
          {sensorType.map((sensor) => (
            <option key={sensor.id} value={sensor.Sensor}>
              {sensor.Sensor}
            </option>
          ))}
        </select>
      </div>
      <div className="addmapping-originexternalid">
        <input
          type="text"
          placeholder="Type in ID for sensor"
          value={originExternalId}
          onChange={(e) => setOriginExternalId(e.target.value)}
        />
      </div>
    </div>
  );
};

export default Addmapping;
