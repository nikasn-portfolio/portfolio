import { useContext } from "react";
import { useNavigate } from "react-router-dom";
import { IParkingUnitDTO } from "./DTO/IParkingUnitDTO";
import { ParkingZoneContext } from "./Root";
import { ParkingUnitService } from "./Services/ParkingUnitService";

export const TimeValidation = () => {
    const context = useContext(ParkingZoneContext);
    const navigate = useNavigate();
    const handleTimeChange = async (event: any) => {
        const timeId = event.target.id;
        console.log(timeId);
        var x = await new ParkingUnitService().postParkingUnit(context.parkingZoneId, timeId, { name : context.licencePlate} as IParkingUnitDTO);
        context.setLicencePlate("");
        if(x !== undefined) {
            navigate("/parking");
        }
    }
    return (
        <div className="time-validation-wrapper">
            <div className="time-number">
                <div className="time-box" id="1" onClick={e => handleTimeChange(e)}>1h</div>
            </div>
            <div className="time-number">
                <div className="time-box" id="2" onClick={e => handleTimeChange(e)}>2h</div>
            </div>
            <div className="time-number">
                <div className="time-box" id="3" onClick={e => handleTimeChange(e)}>3h</div>
            </div>
            <div className="time-number">
                <div className="time-box" id="4" onClick={e => handleTimeChange(e)}>4h</div>
            </div>
        </div>
    );
};