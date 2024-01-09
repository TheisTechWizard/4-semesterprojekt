import React from "react";

const Searchbar = ({ classn }) => {
  return (
    <div className={classn}>
      <input type="text" placeholder="Search for room" />
    </div>
  );
};
export default Searchbar;
