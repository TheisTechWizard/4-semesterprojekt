import React from "react";
import { Button, Searchbar, Room } from "../../components";
import "./roombox.css";

const Roombox = ({ rooms }) => {
  return (
    <div className="roombox-container">
      <div className="roombox-searchbar">
        <Searchbar classn="roombox-searchbar-input" />
        <Button classn="roombox-searchbar-button" text="Search" />
      </div>
      <div className="roombox-list">
        {rooms.map((room) => (
          <Room key={room.id} room={room} />
        ))}
      </div>
    </div>
  );
};

export default Roombox;
