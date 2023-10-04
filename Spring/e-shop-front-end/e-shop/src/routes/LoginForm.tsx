import { use } from 'i18next';
import { useContext, useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { JwtResponse } from '../domain/JwtResponse';
import { LoginDTO } from '../dto/LoginDTO';
import envelopeIcon from '../images/loginForm/envelope-closed-svgrepo-com.svg'
import lockIcon from '../images/loginForm/lock-closed-svgrepo-com.svg'
import { AuthenticationService } from '../services/AuthenticationService';
import { AppContext } from './Root';
import jwtWebToken from 'jsonwebtoken';

export const LoginForm = () => {
    const { jwt, setJwt, setUserId } = useContext(AppContext);
    const [error, setError] = useState({
        error: ""
    })
    const authenticationService = new AuthenticationService();
    const navigate = useNavigate();
    const [values, setValues] = useState({
        username: '',
        password: ''
    } as LoginDTO);
    const handleChange = (target: EventTarget & HTMLInputElement) => {
        setValues({
            ...values,
            [target.name]: target.value
        });
    }

    const handleSubmit = async () => {
        const emailField = document.getElementById("emailField");
        const passwordField = document.getElementById("passwordField");
        emailField!.style.border = "2px solid black";
        passwordField!.style.border = "2px solid black";
        let errors = 0;
        if (values.username.length == 0) {
            setError({ ...error, error: 'Email length can not be 0' });
            emailField!.style.border = "2px solid red";
            errors++;
        }
        if (errors > 0) {
            return;
        }
        const jwtResponse = await authenticationService.login(values).then(
            response => {
                console.log(response);
                if (response) {
                    if (setJwt) {
                        setJwt(response);
                        const decodedToken = jwtWebToken.decode(response.jwt) as { userId: string } | null;
                        if (decodedToken !== null && setUserId !== null) {
                            setUserId(decodedToken.userId);
                        }

                    }
                }
            }
        );
        if (jwtResponse === undefined) {
            emailField!.style.border = "2px solid red";
            passwordField!.style.border = "2px solid red";
            setError({ ...error, error: 'Email or password are not valid' })
        }

    }

    useEffect(() => {
        //TODO: check if jwt is not null and redirect to home page
        if (jwt !== null) {
            navigate("/home");
        }
    }, [jwt]);

    return (
        <div className="loginFormContainer">
            <div className='loginFormErrorContainer'>
                <span>{error.error}</span>
            </div>
            <div className="loginAndRegisterForm">
                <div className="loginAndRegisterFormHeader">
                    Sign In
                </div>
                <div className="loginFormBody">
                    <div id='emailField' className="loginFormBodyInput">
                        <label htmlFor="username">Email</label>
                        <div className="inputContainer">
                            <img src={envelopeIcon} alt="" className='loginFormIcon' />
                            <input type="username" name="username" id="username" className='loginFormField' placeholder="example@gmail.com" onChange={e => handleChange(e.target)} />
                        </div>
                    </div>
                    <div id='passwordField' className="loginFormBodyInput">
                        <label htmlFor="password">Password</label>
                        <div className="inputContainer">
                            <img src={lockIcon} alt="" className='loginFormIcon' />
                            <input type="password" name="password" id="password" className='loginFormField' placeholder="*******" onChange={e => handleChange(e.target)} />
                        </div>
                    </div>
                    <button className="loginFormButton" onClick={handleSubmit}>Sign In</button>
                </div>
            </div>
        </div>
    );
}