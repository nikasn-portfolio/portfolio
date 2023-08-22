import { Outlet, useNavigate } from "react-router-dom";
import React, { createContext, useEffect, useState } from "react";

export const ParkingZoneContext = createContext<{
    parkingZoneId: string;
    setParkingZoneId: (parkingZoneId: string) => void;
    licencePlate: string;
    setLicencePlate: (licencePlate: string) => void;
}>({
    parkingZoneId: "",
    setParkingZoneId: () => { },
    licencePlate: "",
    setLicencePlate: () => { }
});

const Root = () => {
    const [parkingZoneId, setParkingZoneId] = useState("");
    const [licencePlate, setLicencePlate] = useState("");
    const navigate = useNavigate();
    useEffect(() => {
        navigate("/location/");
    },[]);

    useEffect(() => {
        console.log(parkingZoneId);
    }, [parkingZoneId]);

    return (
        <ParkingZoneContext.Provider value={{ parkingZoneId, setParkingZoneId, licencePlate, setLicencePlate }}>
            <div className="main-container">
                <Outlet />
            </div>
        </ParkingZoneContext.Provider>
    );
};

export default Root;





