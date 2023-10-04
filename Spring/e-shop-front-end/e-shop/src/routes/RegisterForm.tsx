import envelopeIcon from '../images/loginForm/envelope-closed-svgrepo-com.svg'
import lockIcon from '../images/loginForm/lock-closed-svgrepo-com.svg'
import profileIcon from '../images/loginForm/profile-svgrepo-com.svg'
import phoneIcon from '../images/loginForm/phone-svgrepo-com.svg'
import { useContext, useEffect, useState } from 'react'
import { AppContext } from './Root'
import { RegisterDTO } from '../dto/RegisterDTO'
import { useNavigate } from 'react-router-dom'
import { AuthenticationService } from '../services/AuthenticationService'
import jwtWebToken from 'jsonwebtoken';


export const RegisterForm = () => {
    const { jwt, setJwt, setUserId } = useContext(AppContext);
    const [values, setValues] = useState({
        username: "",
        password: "",
        r_password: "",
        phoneNumber: "",
        firstName: "",
        lastName: ""
    } as RegisterDTO)
    const [error, setError] = useState([] as JSX.Element[]);
    const navigate = useNavigate();
    const authenticationService = new AuthenticationService();
    const handleChange = (target: EventTarget & HTMLInputElement) => {
        setValues({ ...values, [target.name]: target.value });
    }


    const handleSubmit = async () => {
        const errors: string[] = [];
        const emailField = document.getElementById("email");
        const passwordField = document.getElementById("password");
        const r_passwordField = document.getElementById("r_password");
        const phoneField = document.getElementById("phone");
        const firstNameField = document.getElementById("first_name");
        const lastNameField = document.getElementById("last_name");
        if (!values.username.includes("@")) {
            emailField!.style.border = "2px solid red";
            errors.push("Email is not valid.")
        }
        if (values.password.length < 5) {
            passwordField!.style.border = "2px solid red";
            errors.push("Password must be at least 5 characters length.")
        }
        if (values.r_password.localeCompare(values.password) != 0) {
            console.log(values.password)
            console.log(values.r_password)
            r_passwordField!.style.border = "2px solid red";
            errors.push("Password is not match with confirm password value.")
        }
        if (!values.phoneNumber.includes("+")) {
            phoneField!.style.border = "2px solid red";
            errors.push("Phone number should contain country code.")
        }
        if (values.firstName.length == 0) {
            firstNameField!.style.border = "2px solid red";
            errors.push("Firstname must not be empty.")
        }
        if (values.firstName.length > 50) {
            firstNameField!.style.border = "2px solid red";
            errors.push("Firstname length is too long.")
        }
        if (values.lastName.length == 0) {
            lastNameField!.style.border = "2px solid red";
            errors.push("Lastname must not be empty.")
        }
        if (values.lastName.length > 50) {
            lastNameField!.style.border = "2px solid red";
            errors.push("Lastname length is too long.")
        }
        if (errors.length > 0) {
            const errorMessage = errors.map((error, index) => {
                const errorNumber = index + 1;
                return (
                    <div key={index}>
                        {errorNumber}. {error}
                        <br />
                    </div>
                );
            });

            setError(errorMessage);
            return;
        }
        const response = await authenticationService.register(values);
        if (response != undefined) {
            if (response.errorMessage.length > 0) {
                emailField!.style.border = "2px solid red";
                errors.push(response.errorMessage);
                const errorMessage = errors.map((error, index) => {
                    const errorNumber = index + 1;
                    return (
                        <div key={index}>
                            {errorNumber}. {error}
                            <br />
                        </div>
                    );
                });

                setError(errorMessage);
                return;
            }
            if (setJwt) {
                console.log(response);
                setJwt(response);
                const decodedToken = jwtWebToken.decode(response.jwt) as { userId: string } | null;
                if (decodedToken !== null && setUserId !== null) {
                    setUserId(decodedToken.userId);
                }
            }
        }
    }

    useEffect(() => {
        //TODO: check if jwt is not null and redirect to home page
        if (jwt !== null) {
            navigate("/home");
        }
    }, [jwt]);

    useEffect(() => console.log(values.username), [values])

    return (
        <div className="registerFormContainer">
            <div className='loginFormErrorContainer'>
                {error != null ? (error.map((errorMessage, index) => (
                    <div key={index}>{errorMessage}</div>
                ))) : (<div></div>)}
            </div>
            <div className="loginAndRegisterForm">
                <div className="loginAndRegisterFormHeader">
                    Sign Up
                </div>
                <div className="loginFormBody">
                    <div id="email" className="loginFormBodyInput">
                        <label htmlFor="email">Email</label>
                        <div className="inputContainer">
                            <img src={envelopeIcon} alt="" className='loginFormIcon' />
                            <input type="email" name="username" onChange={e => handleChange(e.target)} className='loginFormField' placeholder="example@gmail.com" />
                        </div>
                    </div>
                    <div id="password" className="loginFormBodyInput">
                        <label htmlFor="password">Password</label>
                        <div className="inputContainer">
                            <img src={lockIcon} alt="" className='loginFormIcon' />
                            <input type="password" name="password" onChange={e => handleChange(e.target)} className='loginFormField' placeholder="*******" />
                        </div>
                    </div>
                    <div id="r_password" className="loginFormBodyInput">
                        <label htmlFor="password">Confirm password</label>
                        <div className="inputContainer">
                            <img src={lockIcon} alt="" className='loginFormIcon' />
                            <input type="password" name="r_password" className='loginFormField' placeholder="*******" onChange={e => handleChange(e.target)} />
                        </div>
                    </div>
                    <div id="phone" className="loginFormBodyInput">
                        <label htmlFor="email">Phone</label>
                        <div className="inputContainer">
                            <img src={phoneIcon} alt="" className='loginFormIcon' />
                            <input type="phone" name="phoneNumber" onChange={e => handleChange(e.target)} className='loginFormField' placeholder="+37255683467" />
                        </div>
                    </div>
                    <div id="first_name" className="loginFormBodyInput">
                        <label htmlFor="email">First name</label>
                        <div className="inputContainer">
                            <img src={profileIcon} alt="" className='loginFormIcon' />
                            <input type="first_name" name="firstName" onChange={e => handleChange(e.target)} className='loginFormField' placeholder="John" />
                        </div>
                    </div>
                    <div id="last_name" className="loginFormBodyInput">
                        <label htmlFor="email">Last name</label>
                        <div className="inputContainer">
                            <img src={profileIcon} alt="" className='loginFormIcon' />
                            <input type="last_name" name="lastName" onChange={e => handleChange(e.target)} className='loginFormField' placeholder="Smith" />
                        </div>
                    </div>
                    <button className="loginFormButton" onClick={handleSubmit}>Sign Up</button>
                </div>
            </div>
        </div>
    );
}