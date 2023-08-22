import React from "react";

interface IButton {
  onClick: () => void;
  text: string;
}

const ButtonTemplate = (props: IButton) => {
  return (
    <button className="addToCartButton" onClick={props.onClick}>
      <div className="addToCartButtonContent">
        <span className="addToCartButtonText">{props.text}</span>
      </div>
    </button>
  );
};

export default ButtonTemplate;
