import { MouseEvent, useContext, useState } from "react";
import { useNavigate } from "react-router-dom";
import { IRegisterData } from "../../dto/IRgeisterData";
import { IdentityService } from "../../services/IdentityService";
import { JwtContext } from "../Root";
import RegisterFormView from "./RegisterFormView";

const Register = () => {

    const [values, setInput] = useState({
        password: "",
        confirmPassword: "",
        email: "",
        firstName: "",
        lastName: "",
    } as IRegisterData);

    const [validationErrors, setValidationErrors] = useState([] as string[]);

    const handleChange = (target: EventTarget & HTMLInputElement) => {
        // debugger;
        // console.log(target.name, target.value, target.type)

        setInput({ ...values, [target.name]: target.value });
    }

    const {jwtResponse, setJwtResponse} = useContext(JwtContext);
    const navigate = useNavigate();

    const identityService = new IdentityService();

    const onSubmit = async (event: MouseEvent) => {
        console.log('onSubmit', event);
        event.preventDefault();
        var numOfErrors = 0;

        /*if (values.firstName.length == 0 || values.lastName.length == 0 || values.email.length == 0 || values.password.length == 0 || values.password != values.confirmPassword) {
            setValidationErrors([...validationErrors, "Firstname should not be empty!"]);
            numOfErrors++;
        }*/
        // remove errors
        setValidationErrors([]);
        if (values.firstName.length === 0) {
            setValidationErrors((prevErrors) => [...prevErrors, "Firstname should not be empty!"]);
            numOfErrors++;
          }
          if (values.lastName.length === 0) {
            setValidationErrors((prevErrors) => [...prevErrors, "Lastname should not be empty!"]);
            numOfErrors++;
          }
          if (values.email.length === 0) {
            setValidationErrors((prevErrors) => [...prevErrors, "Email should not be empty!"]);
            numOfErrors++;
          }
          if (values.password.length === 0) {
            setValidationErrors((prevErrors) => [...prevErrors, "Password should not be empty!"]);
            numOfErrors++;
          }
          if (values.confirmPassword.length === 0) {
            setValidationErrors((prevErrors) => [...prevErrors, "Confirm password should not be empty!"]);
            numOfErrors++;
          }
          if (values.password !== values.confirmPassword) {
            setValidationErrors((prevErrors) => [...prevErrors, "Password and confirm password should be the same!"]);
            numOfErrors++;
          }
        if (numOfErrors > 0) {
            return;
        }

        var jwtData = await identityService.register(values);

        if (jwtData == undefined) {
            // TODO: get error info
            setValidationErrors(["no jwt"]);
            return;
        } 
        navigate("/calendar");
        

    }

    return (
        <RegisterFormView values={values} handleChange={handleChange} onSubmit={onSubmit} validationErrors={validationErrors} jwt={jwtResponse?.jwt} />
    );
}

export default Register;