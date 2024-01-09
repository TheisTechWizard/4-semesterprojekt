import React, { useEffect, useState } from "react";
import Roombox from "../roombox/Roombox";
import Mapbox from "../mapbox/Mapbox";
import Addmapbox from "../addmapbox/Addmapbox";
import testdataroom from "../../components/room/testdata.json";
import sensorType from "./SensorType.json";

import "./main.css";
import axios from "axios";
import ReactjsAlert from "reactjs-alert";

//Dummy DATA
const roomdata = testdataroom;

const Main = () => {
  const [mappings, setMappings] = useState([]);
  const [ispending, setIsPending] = useState(true);
  const [error, setError] = useState(null);
  const [rooms, setRooms] = useState([]);

  //Alert states
  const [status, setStatus] = useState(false);
  const [type, setType] = useState("");
  const [title, setTitle] = useState("");

  const deleteMapping = (id) => {
    axios
      .delete("https://localhost:7138/Mapping/Delete/" + id)
      .then(() => {
        setStatus(true);
        setType("success");
        setTitle("Mapping has been successfully removed");
        setMappings(mappings.filter((mapping) => mapping.id !== id));
      })
      .catch((error) => {
        setStatus(true);
        setType("error");
        setTitle(error.message);
      });
  };

  useEffect(() => {
    axios
      .get("https://localhost:7138/Mapping/Get")
      .then((response) => {
        setMappings(response.data);
        setIsPending(false);
        setError(null);
      })
      .catch((error) => {
        setError(error.message + ": Try refreshing the page");
        setIsPending(false);
      });

    axios
      .get("https://localhost:7138/Room/GetMeetingRooms")
      .then((response) => {
        setRooms(response.data);
      });
  }, []);

  return (
    <div className="main-container overflow">
      <div className="main-roombox">
        {roomdata.length > 0 ? <Roombox rooms={roomdata} /> : "no rooms exist"}
      </div>
      <div className="main-mapcontainer">
        <h1>Create New Mapping</h1>
        <div>
          <Addmapbox sensorType={sensorType} rooms={rooms} />
        </div>
        <div className="main-mapbox">
          <h1>Existing Mappings</h1>
          {error && <div className="error-message">{error}</div>}
          {ispending && <div>Loading...</div>}
          <Mapbox
            mappings={mappings}
            rooms={rooms}
            sensorType={sensorType}
            onDelete={deleteMapping}
          />
        </div>
      </div>
      <ReactjsAlert
        status={status}
        type={type}
        title={title}
        Close={() => setStatus(false)}
      />
    </div>
  );
};

export default Main;
