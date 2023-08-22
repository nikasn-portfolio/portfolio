import { useContext, useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { IParkingZone } from "./Domain/IParkingZone";
import { ParkingZoneContext } from "./Root";
import { ParkingZoneService } from "./Services/ParkingZoneService";

export const LocationChoice = () => {
    const context = useContext(ParkingZoneContext);
    const [parkingZones, setParkingZones] = useState([] as IParkingZone[]);
    const navigate = useNavigate();
    useEffect(() => {
        new ParkingZoneService().getParkingZones().then(response => {
            console.log(response);
            if (response) {
              setParkingZones(response);
            } else {
              setParkingZones([]);
            }
          }
        );
    }, []);
    const handleLocationChange = (event: any) => {
        const locationId = event.target.value;
        context.setParkingZoneId(locationId);
    };
    const handleSubmit = (event: any) => {
        if(context.parkingZoneId !== "") {
            navigate("/parking");
        }
    };
    return (
        <div className="location-select">
            <label htmlFor="location">Choose parking location</label>
            <select className="form-select" id="location" name="locationId" onChange={e => handleLocationChange(e)}>
                <option id="">Select location</option>
                {parkingZones.map((parkingZone) => (<option value={parkingZone.parkingZoneId}>{parkingZone.name}</option>))}
            </select>
            <button onClick={e => handleSubmit(e)}>Submit</button>
        </div>
    );
};