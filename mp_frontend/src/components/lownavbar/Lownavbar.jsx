import React from "react";
import "./lownavbar.css";

const Lownavbar = () => {
  return (
    <div className="lownavbar">
      <div className="lownavbar-links">
        <div className="lownavbar-links-container">
          <p>
            <a href="#home">Venue</a>
          </p>
          <p>
            <a href="#home">Network</a>
          </p>
          <p>
            <a href="#home">App Config</a>
          </p>
          <p>
            <a href="#home">Import/Export</a>
          </p>
          <p>
            <a href="#home">Solutions</a>
          </p>
          <p>
            <a href="#home">Customers & Partners</a>
          </p>
          <p>
            <a href="#home">Auto Route</a>
          </p>
        </div>
      </div>
    </div>
  );
};

export default Lownavbar;
