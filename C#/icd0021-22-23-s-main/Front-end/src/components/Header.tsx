import { useContext, useEffect, useLayoutEffect, useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { JwtContext } from "../routes/Root";
import React from "react";
import { useJwt } from "react-jwt";
import { IJWTResponse } from "../dto/IJWTResponse";
import { IdentityService } from "../services/IdentityService";
import { ILogoutData } from "../dto/ILogout";


const Header = () => {
    const { jwtResponse, setJwtResponse, userRole, setUserRole, setUserId}  = useContext(JwtContext)
    const [tokenExpired, setTokenExpired] = useState(false as boolean | null);
    const [exp, setExp] = useState(null as number | null);
    let parsedJwtToken = null as any | null;
    const [emailAddress, setEmailAddress] = useState(null as string | null);
    // decode the token through the useJwt hook
    // parse the parsedJwtToken from the decoded token
    const { decodedToken } = useJwt(jwtResponse?.jwt as string);
    const navigate = useNavigate();
    

    

    const [values, setInput] = useState({
        jwt: "",
        refreshToken : ""
    } as IJWTResponse);

    useEffect(() => {
        if (decodedToken != null) {
            parsedJwtToken = JSON.parse(JSON.stringify(decodedToken));
            console.log(parsedJwtToken);
            setEmailAddress(parsedJwtToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"]);
            if(setUserRole) setUserRole(parsedJwtToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"]);
            if(setUserId) setUserId(parsedJwtToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"]);
            console.log(parsedJwtToken["exp"]);
            setExp(parsedJwtToken["exp"] as number);


        }
        console.log(emailAddress);
    }, [decodedToken]);

    useEffect(() => {
        if (jwtResponse) {
            const interval = setInterval(async () => {
                const currentTime = Math.floor(Date.now() / 1000);
                const expirationTime = exp || 0;
                setTokenExpired(currentTime >= expirationTime - 10);

                /*console.log(exp + " exp");
                console.log(currentTime);
                console.log(tokenExpired);
                console.log(jwtResponse);*/


                //if token is expired, call a new token from the server
                if (tokenExpired !== null && tokenExpired && jwtResponse !== null) {
                    const identityService = new IdentityService();
                    var jwtData = await identityService.refreshToken(jwtResponse);
                    if (jwtData == undefined) {
                        await userLogout(jwtResponse);
                        return;
                    } 
            
                    if (setJwtResponse) setJwtResponse(jwtData);
                }
            }, 1000);
            return () => clearInterval(interval);
        }

    }, [exp, tokenExpired, jwtResponse]);

    

    const userLogout = async (jwtResponse : IJWTResponse) => {
        const identityService = new IdentityService();
        const tokenDeletedCount =  await identityService.logout(jwtResponse);
        if(setJwtResponse) setJwtResponse(null);
        if(setUserRole) setUserRole(null);
        if(setUserId) setUserId(null);
        console.log(tokenDeletedCount?.TokenDeleteCount);
    }


    return (
        <header>
            <nav className="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3">
                <div className="container">
                    <Link className="navbar-brand" to="/">Webapp</Link>
                    <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target=".navbar-collapse" aria-controls="navbarSupportedContent"
                        aria-expanded="false" aria-label="Toggle navigation">
                        <span className="navbar-toggler-icon"></span>
                    </button>
                    <div className="navbar-collapse collapse d-sm-inline-flex justify-content-between">
                        <ul className="navbar-nav flex-grow-1">
                            <li className="nav-item">
                                <Link to="/" className="nav-link text-dark">Home</Link>
                            </li>

                            <li className="nav-item" style={{ 'display': jwtResponse == null ? 'none' : '' }}>
                                <Link to="calendar" className="nav-link text-dark">Calendar</Link>
                            </li>
                            <li className="nav-item" style={{ 'display': jwtResponse != null && userRole == "Admin" ? '' : 'none' }}>
                                <Link to="receipts" className="nav-link text-dark">Receipts</Link>
                            </li>
                            <li className="nav-item" style={{ 'display': jwtResponse != null && userRole == "Admin" ? '' : 'none' }}>
                                <Link to="register" className="nav-link text-dark" >Manage workers</Link>
                            </li>

                        </ul>

                        <ul className="navbar-nav">
                            
                            <li className="nav-item" style={{ 'display': jwtResponse == null ? 'none' : '' }}>
                                <div className="nav-link text-dark" >{emailAddress}</div>
                            </li>
                            <li className="nav-item" style={{ 'display': jwtResponse == null ? 'none' : '' }} onClick={() => {userLogout(jwtResponse as any)}}>
                                <Link to="/" className="nav-link text-dark" >Logout</Link>
                            </li>
                            
                            <li className="nav-item" style={{ 'display': jwtResponse == null ? '' : 'none' }}>
                                <Link to="login" className="nav-link text-dark" >Login</Link>
                            </li>
                        </ul>


                    </div>
                </div>
            </nav>
        </header>
    );
}

export default Header;