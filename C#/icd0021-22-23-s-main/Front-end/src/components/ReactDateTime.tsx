import { HtmlAttributes } from "csstype";
import { setHours, setMinutes } from "date-fns";
import { type } from "os";
import { forwardRef, MutableRefObject, useEffect, useImperativeHandle, useState } from "react";
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import TimeSelect from "./test";

interface Props  {
    onChange: (date: Date) => void,
    label : string,
    value? : Date,
}

const ReactDateTime = ({onChange, label, value} : Props)  => {
    // useImperativeHandle allows us to expose functions or properties on the child component that the parent can access via the ref object
    
  
    
    const [startDate, setStartDate] = useState(value != null 
        ? value 
        : new Date());
    const [time, setTime] = useState(value != null ? value : new Date());
    useEffect(() => {
        console.log("time", time.getMinutes())
        console.log("startTime", startDate)
        let newDate = new Date(startDate);
        newDate.setHours(time.getHours());
        newDate.setMinutes(time.getMinutes());
        console.log(newDate);
        setStartDate(newDate);
        onChange(newDate);
    }, [time]);

    useEffect(() => { 
        onChange(startDate)
    }, [startDate]);

    

    
    return (
        <>
        <label className="control-label" htmlFor="Date">{label}</label>
        <div className="time-select-container">
        <TimeSelect value={time} onChange={(newTime) => setTime(newTime)} />
        <DatePicker id="Date" className="form-control" dateFormat="dd.MM.yyyy" selected={startDate} onChange={(date) => setStartDate(date!)} />
        </div>
        </>
        
    );
  };

  export default ReactDateTime;