import { Dispatch, SetStateAction, useContext, useState } from "react";
import { useNavigate } from "react-router-dom";
import { ICompany } from "../domain/ICompany";
import { JwtContext } from "../routes/Root";
import { CompanyService } from "../services/CompanyService";

interface IProps {
    onClose: Dispatch<SetStateAction<boolean>>;
}

export function PopUpCompanyHandler(props: IProps) {

    const [values, setValues] = useState({
        companyName : "",
        identificationCode: "",
        address: "",
        vatNumber: ""
    }as ICompany) 

    const [validationErrors, setValidationErrors] = useState([] as string[]);
    const companyService = new CompanyService();
    const { jwtResponse, setJwtResponse, userRole, userId } = useContext(JwtContext);
    const navigate = useNavigate();


    const onSubmit = async (event: any) => {
        console.log('onSubmit', event);
        event.preventDefault();
        let errors = 0;
        setValidationErrors([]);
        if (values.companyName.length == 0) {
            setValidationErrors(prev => [...prev, "Company name should not be empty!"]);
            errors++;
        }
        if (values.identificationCode!.length == 0) {
            setValidationErrors(prev => [...prev, "Indentification code should not be empty!"]);
            errors++;
        }
        if (values.address!.length == 0) {
            setValidationErrors(prev => [...prev, "Address should not be empty!"]);
            errors++;
        }
        if(errors > 0){
            return;
        }
        console.log(values)
        const result = await companyService.postCompany(values, jwtResponse?.jwt as string);
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
                    <h4>Create company</h4>
                    <hr />
                    <ul style={{ 'display': validationErrors.length == 0 ? 'none' : '' }}>
                        {validationErrors.map((error) => { return <li>{error}</li> })}
                    </ul>
                    <div className="row">
                        <div className="col-md-4">
                            <form action="/Records/Create" method="post">
                                <div className="form-group" style={{ width: values.companyName.length > 8 ? 380 : 100 }}>
                                    <label className="control-label" htmlFor="Title">Company name</label>
                                    <textarea className="form-control" wrap="soft" style={{ resize: "none", height: values.companyName.length > 45 ? 76 : 38 }} data-val="true" data-val-maxlength="The field Title must be a string or array type with a maximum length of &#x27;100&#x27;." data-val-maxlength-max="100" data-val-required="The Title field is required." id="Title" maxLength={100} name="companyName" value={values.companyName} onChange={(e) => handleChange(e.target)} />
                                </div>
                                <div className="form-group" style={{ width: values.identificationCode!.length > 8 ? 380 : 100 }}>
                                    <label className="control-label" htmlFor="identificationCode">identificationCode</label>
                                    <textarea className="form-control" wrap="soft" style={{ resize: "none", height: values.identificationCode!.length > 45 ? 76 : 38 }} data-val="true" data-val-maxlength="The field Title must be a string or array type with a maximum length of &#x27;100&#x27;." data-val-maxlength-max="100" data-val-required="The Title field is required." id="identificationCode" maxLength={100} name="identificationCode" value={values.identificationCode} onChange={(e) => handleChange(e.target)} />
                                </div>
                                <div className="form-group" style={{ width: values.address!.length > 8 ? 380 : 100 }}>
                                    <label className="control-label" htmlFor="address">Address</label>
                                    <textarea className="form-control" wrap="soft" style={{ resize: "none", height: values.address!.length > 45 ? 76 : 38 }} data-val="true" data-val-maxlength="The field Title must be a string or array type with a maximum length of &#x27;100&#x27;." data-val-maxlength-max="100" data-val-required="The Title field is required." id="address" maxLength={100} name="address" value={values.address} onChange={(e) => handleChange(e.target)} />
                                </div>
                                <div className="form-group" style={{ width: values.vatNumber!.length > 8 ? 380 : 100 }}>
                                    <label className="control-label" htmlFor="vatNumber">VatNumber</label>
                                    <textarea className="form-control" wrap="soft" style={{ resize: "none", height: values.vatNumber!.length > 45 ? 76 : 38 }} data-val="true" data-val-maxlength="The field Title must be a string or array type with a maximum length of &#x27;100&#x27;." data-val-maxlength-max="100" data-val-required="The Title field is required." id="address" maxLength={100} name="vatNumber" value={values.vatNumber} onChange={(e) => handleChange(e.target)} />
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