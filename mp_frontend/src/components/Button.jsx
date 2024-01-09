import React from "react";

const Button = ({ text, classn, clickFunction }) => {
  return (
    <div className={classn}>
      <button onClick={clickFunction}>{text}</button>
    </div>
  );
};

export default Button;
