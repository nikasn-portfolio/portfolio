import { timers } from "jquery";
import { timeout } from "q";
import { useContext, useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { ILoginData } from "../../dto/ILoginData";
import { IdentityService } from "../../services/IdentityService";
import { JwtContext } from "../Root";
import { LoginFormView} from "./LoginFormView"

const Login = () => {

    const [values, setInput] = useState({
        password: "",
        email: ""
    } as ILoginData);

    const [validationErrors, setValidationErrors] = useState([] as string[]);

    const handleChange = (target: EventTarget & HTMLInputElement) => {
        // debugger;
        console.log(target.name, target.value, target.type)

        setInput({ ...values, [target.name]: target.value });
    }


    const {jwtResponse, setJwtResponse} = useContext(JwtContext);

    useEffect(() => {
        console.log("jwtResponse", jwtResponse);
    }, [jwtResponse]);

    const identityService = new IdentityService();

    const navigate = useNavigate()

    const onSubmit = async (event: MouseEvent) => {
        console.log('onSubmit', event);
        event.preventDefault();
        let numOfErrors = 0;

        
        // remove errors
        setValidationErrors([]);
        if (values.email.length == 0) {
            setValidationErrors((prevErrors) => [...prevErrors, "Email should not be empty!"]);
            numOfErrors++;
        }
        if (values.password.length == 0) {
            setValidationErrors((prevErrors) => [...prevErrors, "Password should not be empty!"]);
            numOfErrors++;
        }
        if(numOfErrors > 0) return;

        console.log(values);

        var jwtData = await identityService.login(values);

        if (jwtData == undefined) {
            // TODO: get error info
            setValidationErrors((prevErrors) => [...prevErrors, "Invalid email or password!"]);
            return;
        } 

        if (setJwtResponse){
            setJwtResponse(jwtData)
        };
        
        

    }

    useEffect(() => {
        if (jwtResponse !== null) {
          console.log(jwtResponse);
          navigate('/calendar', { replace: true });
        }
      }, [jwtResponse]);

    return (
        <LoginFormView values={values} handleChange={handleChange} validationErrors={validationErrors} onSubmit={onSubmit}></LoginFormView>
    );
}

export default Login;