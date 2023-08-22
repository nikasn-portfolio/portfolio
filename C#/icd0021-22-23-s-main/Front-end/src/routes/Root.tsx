import { createContext, useState } from "react";
import { Outlet } from "react-router-dom";
import {momentLocalizer} from "react-big-calendar";
import moment from 'moment';

import Footer from "../components/Footer";
import Header from "../components/Header";
import { IJWTResponse } from "../dto/IJWTResponse";
import SelectableCalendar from "./MyCalendar";


export const JwtContext = createContext<{
    jwtResponse: IJWTResponse | null,
    setJwtResponse: ((data: IJWTResponse | null) => void) | null,
    userRole: string | null,
    setUserRole: ((data: string | null) => void) | null,
    userId: string | null,
    setUserId: ((data: string | null) => void) | null,
}>({ jwtResponse: null, setJwtResponse: null, userRole: null, setUserRole: null, userId: null, setUserId: null });

const Root = () => {

    const [jwtResponse, setJwtResponse] = useState(null as IJWTResponse | null);

    const [userRole, setUserRole] = useState(null as string | null);

    const [userId, setUserId] = useState(null as string | null);
    return (
        <JwtContext.Provider value={{ jwtResponse, setJwtResponse, userRole, setUserRole, userId, setUserId }}>
            <Header />

            <div className="container">
                <main role="main" className="pb-3">
                    <Outlet />
                </main>
            </div>

            <Footer />
        </JwtContext.Provider>
    );
}

export default Root;