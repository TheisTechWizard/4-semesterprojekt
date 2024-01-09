import axios from "axios";
import React, { useState } from "react";
import { Addmapping, Button } from "../../components";
import ReactJsAlert from "reactjs-alert";

import "./addmapbox.css";

const Addmapbox = ({ rooms, sensorType }) => {
  //Mapping states
  const [roomName, setRoomName] = useState("");
  const [origin, setOrigin] = useState("");
  const [originExternalId, setOriginExternalId] = useState("");

  //Alert states
  const [status, setStatus] = useState(false);
  const [type, setType] = useState("");
  const [title, setTitle] = useState("");

  const childToParent = (childData) => {
    setRoomName(childData.roomName);
    setOrigin(childData.origin);
    setOriginExternalId(childData.originExternalId);
  };

  const addMapping = () => {
    const id = "";
    const internalIdList = "";
    const setOfData = {
      id,
      origin,
      originExternalId,
      internalIdList,
      roomName,
    };
    if (
      setOfData.origin === "" ||
      setOfData.originExternalId === "" ||
      setOfData.roomName === ""
    ) {
      setStatus(true);
      setType("warning");
      setTitle("All fields must be filled");
    } else {
      axios
        .post("https://localhost:7138/Mapping/Post", setOfData)
        .then(() => {
          setStatus(true);
          setType("success");
          setTitle("The post request worked");
        })
        .then(() => {
          setTimeout(() => window.location.reload(), 2000);
        })
        .catch((error) => {
          setStatus(true);
          setType("error");
          setTitle(error.message);
        });
    }
  };

  return (
    <div className="addmappingbox">
      <Addmapping
        rooms={rooms}
        childToParent={childToParent}
        sensorType={sensorType}
      />
      <Button classn="addbutt" clickFunction={addMapping} text="Add Mapping" />
      <ReactJsAlert
        status={status}
        type={type}
        title={title}
        Close={() => setStatus(false)}
      />
    </div>
  );
};

export default Addmapbox;
