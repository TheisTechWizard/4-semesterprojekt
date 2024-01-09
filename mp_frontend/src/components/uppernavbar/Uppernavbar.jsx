import React from "react";
import "./uppernavbar.css";

const Navbar = () => {
  return (
    <div className="navbar">
      <div className="navbar-links">
        <div className="navbar-sitename">
          <h1>MapsPeople CMS</h1>
        </div>
        <div className="navbar-links-container">
          <p>
            <a href="#home">Map</a>
          </p>
        </div>
      </div>
    </div>
  );
};

export default Navbar;
