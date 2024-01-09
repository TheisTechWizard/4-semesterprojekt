import { faFloppyDisk, faTrash } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import axios from "axios";
import React, { useState } from "react";
import ReactjsAlert from "reactjs-alert";

import "./mapping.css";

const Mapping = ({ mapping, onDelete, rooms, sensorType }) => {
  const [roomName, setRoomName] = useState(mapping.roomName);
  const [origin, setOrigin] = useState(mapping.origin);
  const [originExternalId, setOriginExternalId] = useState(
    mapping.originExternalId
  );

  //Change color when updating mapping
  const [color, setColor] = useState(false);
  const changeColor = () => {
    setColor(true);
  };

  //Alert states
  const [status, setStatus] = useState(false);
  const [type, setType] = useState("");
  const [title, setTitle] = useState("");

  const handleSubmit = () => {
    const id = mapping.id;
    const internalIdList = mapping.internalIdList;
    const saveMapping = {
      id,
      origin,
      originExternalId,
      internalIdList,
      roomName,
    };
    if (
      saveMapping.origin === "" ||
      saveMapping.originExternalId === "" ||
      saveMapping.internalIdList === ""
    ) {
      setStatus(true);
      setType("warning");
      setTitle("All fields must be filled");
    } else {
      axios
        .put("https://localhost:7138/Mapping/Put", saveMapping)
        .then(() => {
          setStatus(true);
          setType("success");
          setTitle("Mapping has been saved");
        })
        .catch((error) => {
          setStatus(true);
          setType("error");
          setTitle(error.message);
        });
    }
  };

  return (
    <div className="mapping">
      <form
        onSubmit={handleSubmit}
        className={
          color
            ? "mapping-internal-map-container-db"
            : "mapping-internal-map-container"
        }
      >
        <div className="mapping-internalidlist">
          <select
            value={roomName}
            onChange={(e) =>
              setRoomName(e.target.value) &
              window.addEventListener("change", changeColor)
            }
          >
            {rooms.map((room) => (
              <option key={room.id} value={room.name}>
                {room.name}
              </option>
            ))}
          </select>
        </div>
        <div className="mapping-origin">
          <select
            value={origin}
            onChange={(e) =>
              setOrigin(e.target.value) &
              window.addEventListener("change", changeColor)
            }
          >
            {sensorType.map((sensor) => (
              <option key={sensor.id} value={sensor.Sensor}>
                {sensor.Sensor}
              </option>
            ))}
          </select>
        </div>
        <div className="mapping-originexternalid">
          <input
            type="text"
            placeholder={mapping.originExternalId}
            value={originExternalId}
            onChange={(e) =>
              setOriginExternalId(e.target.value) &
              window.addEventListener("change", changeColor)
            }
          />
        </div>
        <div className="mapping-deleteicon">
          <FontAwesomeIcon
            icon={faTrash}
            onClick={() => {
              if (window.confirm("Are you sure you wish to delete this?"))
                onDelete(mapping.id);
            }}
          />
        </div>
        <div className="mapping-saveicon">
          <FontAwesomeIcon icon={faFloppyDisk} onClick={() => handleSubmit()} />
        </div>
      </form>
      <ReactjsAlert
        status={status}
        type={type}
        title={title}
        Close={() => setStatus(false)}
      />
    </div>
  );
};

export default Mapping;
