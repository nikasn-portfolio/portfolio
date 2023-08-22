import React, { useState } from "react";
import PropTypes from "prop-types";

type CustomTimeInputProps = {
    onChange: (data: Date) => void;
  }

const CustomTimeInput = ({onChange} : CustomTimeInputProps) => {
  const [hours, setHours] = useState(0);
  const [minutes, setMinutes] = useState(0);

  const handleHoursChange = (event : any) => {
    console.log(parseInt(event.target.value));
    const newHours = parseInt(event.target.value);
    if (isNaN(newHours)) return;
    setHours(newHours);
    console.log(onChange);
    onChange(new Date());
  };

  const handleMinutesChange = (event : any) => {
    const newMinutes = parseInt(event.target.value, 10);
    if (isNaN(newMinutes)) return;
    setMinutes(newMinutes);
    //onChange(new Date(0, 0, 0, hours, newMinutes));
  };

  const hoursOptions = [];
  for (let i = CustomTimeInput.defaultProps.minTime.getHours(); i <= CustomTimeInput.defaultProps.maxTime.getHours(); i += CustomTimeInput.defaultProps.step) {
    hoursOptions.push(i);
  }

  const minutesOptions = [];
  for (let i = 0; i < 60; i += CustomTimeInput.defaultProps.step) {
    minutesOptions.push(i);
  }

  return (
    <div>
      <select value={hours} onChange={handleHoursChange}>
        {hoursOptions.map((hour) => (
          <option key={hour} value={hour}>
            {hour.toString().padStart(2, "0")}
          </option>
        ))}
      </select>
      :
      <select value={minutes} onChange={handleMinutesChange}>
        {minutesOptions.map((minute) => (
          <option key={minute} value={minute}>
            {minute.toString().padStart(2, "0")}
          </option>
        ))}
      </select>
    </div>
  );
};



CustomTimeInput.defaultProps = {
  value: new Date(),
  minTime: new Date(0, 0, 0, 8, 0),
  maxTime: new Date(0, 0, 0, 20, 0),
  step: 1,
};

export default CustomTimeInput;
