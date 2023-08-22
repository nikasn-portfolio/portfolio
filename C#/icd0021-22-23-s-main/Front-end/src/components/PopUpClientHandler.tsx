import React, { Dispatch, SetStateAction, useContext, useState } from "react";
import { useNavigate } from "react-router-dom";
import { IClientPost } from "../dto/IClientPost";
import { JwtContext } from "../routes/Root";
import { ClientService } from "../services/ClientService";

interface IProps {
    onClose: Dispatch<SetStateAction<boolean>>;
}

export function PopUpClientHandler(props: IProps) {

    const [values, setValues] = useState({
        firstName: "",
        lastName: "",
        fullName: "",
        phoneNumber: "+372",
    }as IClientPost) 

    const [validationErrors, setValidationErrors] = useState([] as string[]);
    const clientService = new ClientService();
    const { jwtResponse, setJwtResponse, userRole, userId } = useContext(JwtContext);
    const navigate = useNavigate();


    const onSubmit = async (event: any) => {
        console.log('onSubmit', event);
        event.preventDefault();
        setValidationErrors([]);
        if (values.firstName.length == 0 || values.lastName.length == 0) {
            setValidationErrors(prev => [...prev, "Check credentials that you have inserted!"]);
            return;
        }
        // remove errors
        console.log(values)
        const result = await clientService.postClient(values, jwtResponse?.jwt as string);
        console.log(result);
        if(result !== undefined){
            props.onClose(false);
        }
    
    
      }

      const phoneNumberRegex = /^[0-9+]+$/;

    const handleChange = (target: any) => {
        // debugger;
        console.log(target.name, target.value, target.type)

        if(target.name == "phoneNumber"){
            if(!phoneNumberRegex.test(target.value)){
                return;
            }
            
        }

        setValues({ ...values, [target.name]: target.value });
    }

    function close(){
        props.onClose(false);
    }

    return (
        <>
            <div className="popup">
                <div className="popup-window3" style={{height : "400px"}}>
                    <h4>Create client</h4>
                    <hr />
                    <ul style={{ 'display': validationErrors.length == 0 ? 'none' : '' }}>
                        {validationErrors.map((error) => { return <li>{error}</li> })}
                    </ul>
                    <div className="row">
                        <div className="col-md-4">
                            <form action="/Records/Create" method="post">
                                <div className="form-group" style={{ width: values.firstName.length > 8 ? 380 : 100 }}>
                                    <label className="control-label" htmlFor="Title">FirstName</label>
                                    <textarea className="form-control" wrap="soft" style={{ resize: "none", height: values.firstName.length > 45 ? 76 : 38 }} data-val="true" data-val-maxlength="The field Title must be a string or array type with a maximum length of &#x27;100&#x27;." data-val-maxlength-max="100" data-val-required="The Title field is required." id="Title" maxLength={100} name="firstName" value={values.firstName} onChange={(e) => handleChange(e.target)} />
                                </div>
                                <div className="form-group" style={{ width: values.lastName.length > 8 ? 380 : 100 }}>
                                    <label className="control-label" htmlFor="Title">LastName</label>
                                    <textarea className="form-control" wrap="soft" style={{ resize: "none", height: values.lastName.length > 45 ? 76 : 38 }} data-val="true" data-val-maxlength="The field Title must be a string or array type with a maximum length of &#x27;100&#x27;." data-val-maxlength-max="100" data-val-required="The Title field is required." id="Title" maxLength={100} name="lastName" value={values.lastName} onChange={(e) => handleChange(e.target)} />
                                </div>
                                <div className="form-group" style={{ width: values.phoneNumber.length > 8 ? 380 : 100 }}>
                                    <label className="control-label" htmlFor="Title">PhoneNumber</label>
                                    <textarea className="form-control" wrap="soft" style={{ resize: "none", height: values.phoneNumber.length > 45 ? 76 : 38 }} data-val="true" data-val-maxlength="The field Title must be a string or array type with a maximum length of &#x27;100&#x27;." data-val-maxlength-max="100" data-val-required="The Title field is required." id="Title" maxLength={100} name="phoneNumber" value={values.phoneNumber} onChange={(e) => handleChange(e.target)} />
                                </div>
                            </form>
                        </div>

                        


                    </div>
                    <button className='button1' style={{margin : "10px 0px 0px 0px"}} onClick={close} >Back</button>
                    <button className='button1' style={{margin : "10px 0px 0px 0px"}} onClick={e => onSubmit(e)} >Save</button>
                </div>
            </div>
        </>

    )
}