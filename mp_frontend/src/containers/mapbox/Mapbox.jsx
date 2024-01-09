import React from "react";
import { Mapping } from "../../components";
import "./mapbox.css";

const Mapbox = ({ mappings, onDelete, rooms, sensorType }) => {
  return (
    <div className="mapbox-container">
      {mappings.map((map) => (
        <Mapping
          key={map.id}
          mapping={map}
          onDelete={onDelete}
          rooms={rooms}
          sensorType={sensorType}
        />
      ))}
    </div>
  );
};

export default Mapbox;
