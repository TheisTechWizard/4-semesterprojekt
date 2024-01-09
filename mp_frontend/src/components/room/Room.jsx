import React from "react";
import "./room.css";

const Room = ({ room }) => {
  return (
    <div className="roombox">
      <p>{room.location}</p>
    </div>
  );
};

export default Room;
