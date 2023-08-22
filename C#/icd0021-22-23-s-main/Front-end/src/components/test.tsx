import React, { useState } from 'react';

type TimeSelectProps = {
  value: Date;
  onChange: (newTime: Date) => void;
  minTime?: Date;
  maxTime?: Date;
  step?: number;
};

const TimeSelect = ({ value, onChange, minTime = new Date(0, 0, 0, 8, 0), maxTime = new Date(0, 0, 0, 20, 0), step = 15 }: TimeSelectProps) => {
  const [hours, setHours] = useState(value.getHours());
  const [minutes, setMinutes] = useState(value.getMinutes());

  const handleHoursChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
    const newHours = parseInt(event.target.value);
    const newTime = new Date(value);
    newTime.setHours(newHours);
    onChange(newTime);
    setHours(newHours);
  };

  const handleMinutesChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
    const newMinutes = parseInt(event.target.value);
    const newTime = new Date(value);
    newTime.setMinutes(newMinutes);
    onChange(newTime);
    setMinutes(newMinutes);
  };

  const hoursOptions = [];
  for (let i = minTime.getHours(); i <= maxTime.getHours(); i += 1) {
    hoursOptions.push(i);
  }

  const minutesOptions = [];
  for (let i = 0; i < 60; i += step) {
    minutesOptions.push(i);
  }

  return (
    <div className='time-select-item'>
      <select className='form-select' value={hours} onChange={handleHoursChange}>
        {value.getHours() === 1 &&(<option value={1}>
          Select hours
        </option>)}
        {hoursOptions.map((hour) => (
          <option key={hour} value={hour}>
            {hour.toString().padStart(2, '0')}
          </option>
        ))}
      </select>
      <div className='semicolon'>
        :
      </div>
      <select className='form-select' value={minutes} onChange={handleMinutesChange}>
        {value.getMinutes() === 1 && (<option value={1}>
          Select hours
        </option>)}
        {minutesOptions.map((minute) => (
          <option key={minute} value={minute}>
            {minute.toString().padStart(2, '0')}
          </option>
        ))}
      </select>
    </div>
  );
};

export default TimeSelect;
