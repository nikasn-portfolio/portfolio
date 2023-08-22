import { event } from "jquery";
import moment from "moment";
import { useContext, useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { ICompany } from "../domain/ICompany";
import { IInvoiceFooterCreate } from "../domain/IInvoiceFooterCreate";
import { IPaymentMethod } from "../domain/IPaymentMethod";
import { IService } from "../domain/IService";
import { IEvent } from "../dto/IEvent";
import { IInvoice } from "../dto/IInvoice";
import { IInvoiceRow } from "../dto/InvoiceRow";
import { IResource } from "../routes/MyCalendar";
import { JwtContext } from "../routes/Root";
import { CompanyService } from "../services/CompanyService";
import { InvoiceFooterService } from "../services/InvoiceFooter";
import { InvoiceService } from "../services/InvoiceService";
import { PaymentMethodService } from "../services/PaymentMethodService";
import { RecordService } from "../services/RecordService";
import { InvoiceRow } from "./InvoiceRow";
import { PopUpCompanyHandler } from "./PopUpCompanyHandler";

interface Props {
    onClose: () => void;
    onCloseInvoice: (data: boolean) => void;
    event: IEvent;
    resource: IResource
}
export const PopUpInvoiceHandler = (props: Props) => {

    const [values, setValues] = useState({
        companyId: null,
        invoiceFooterId: null,
        clientId: props.event.clientId,
        appUserId: props.resource.resourceId,
        recordId: props.event.id,
        paymentMethodId: null,
        paymentDate: moment(new Date(Date.UTC(new Date().getUTCFullYear(), new Date().getUTCMonth(),
        new Date().getUTCDate(), new Date().getUTCHours(),
        new Date().getUTCMinutes(), new Date().getUTCSeconds()))).utcOffset(0, true).format(),
        isCompany: false,
        comment: "",
        invoiceRows: [] as IInvoiceRow[]
    } as IInvoice);

    const [listOfInvoiceRows, setListOfInvoiceRows] = useState([] as IInvoiceRow[]);
    const [paymentMethods, setPaymentMethods] = useState([] as IPaymentMethod[]);
    const { jwtResponse, setJwtResponse, userRole, userId } = useContext(JwtContext);
    const [companies, setCompanies] = useState([] as ICompany[]);
    const [invoiceFooter, setInvoiceFooter] = useState([] as IInvoiceFooterCreate[]);
    const [errors, setErrors] = useState([] as string[]);
    const [companyAddToggle, setCompanyAddToggle] = useState(false as boolean);
    const navigate = useNavigate();
    const [searchBar, setSearchBar] = useState("");
    const [companiesList, setCompaniesList] = useState([] as ICompany[]);



    useEffect(() => {
        if (jwtResponse == null) return;
        new PaymentMethodService().getAll(jwtResponse.jwt).then(
            response => {
                console.log(response);
                if (response) {
                    setPaymentMethods(response);
                } else {
                    setPaymentMethods([]);
                }
            }
        );
        new InvoiceFooterService().getAll(jwtResponse.jwt).then(
            response => {
                console.log(response);
                if (response) {
                    setInvoiceFooter(response);
                } else {
                    setInvoiceFooter([]);
                }
            }
        );

    }, [jwtResponse]);

    useEffect(() => {
        if (invoiceFooter.length > 0) {
            console.log(invoiceFooter[0])
            setValues((prevValues) => ({ ...prevValues, invoiceFooterId: invoiceFooter[0].id! }));
        }
    }, [invoiceFooter]);

    useEffect(() => {
        if (jwtResponse == null) return;
        new CompanyService().getAll(jwtResponse.jwt).then(
            response => {
                console.log(response);
                if (response) {
                    setCompanies(response);
                } else {
                    setCompanies([]);
                }
            }
        );
    }, [values.isCompany, companyAddToggle]);

    useEffect(() => {
        if (values.invoiceRows.length > 0) return;
      
        const listOfRows = props.event.service?.map(ev => ({
          serviceId: ev.id,
          quantity: 1,
          priceOverride: null,
          total: parseInt(ev.servicePrice),
        })) || [];
      
        setListOfInvoiceRows(listOfRows);
        setValues(prevValues => ({
          ...prevValues,
          invoiceRows: listOfRows,
        }));
      });
      

    function handleChange(event: any) {
        if(event === undefined) return;
        const target = event.target;
        const value = target.value;
        const name = target.name;
        const id = target.id;
        console.log('handleChange', event.target.value)

        switch (id || name) {
            case 'quantity':
                setValues((prevValues) => ({
                    ...prevValues,
                    invoiceRows: prevValues.invoiceRows.map((row) =>
                        row.serviceId === name
                            ? { ...row, quantity: parseInt(value) }
                            : row
                    ),
                }));
                setValues((prevValues) => ({
                    ...prevValues,
                    invoiceRows: prevValues.invoiceRows.map((row) =>
                        row.serviceId === name
                            ? { ...row, total: parseInt(props.event.service?.filter(e => e.id === name)[0].servicePrice as string) * value }
                            : row
                    ),
                }));
                break;
            case 'priceOverride':
                setValues((prevValues) => ({
                    ...prevValues,
                    invoiceRows: prevValues.invoiceRows.map((row) =>
                        row.serviceId === name
                            ? { ...row, priceOverride: parseInt(value) || null }
                            : row
                    ),
                }));
                break;
            case 'total':
                setValues((prevValues) => ({
                    ...prevValues,
                    invoiceRows: prevValues.invoiceRows.map((row) =>
                        row.serviceId === name ? { ...row, total: parseInt(value) } : row
                    ),
                }));
                break;
            case 'isCompany':
                if (values.isCompany) {
                    setValues((prevValues) => ({
                        ...prevValues,
                        companyId: null
                    }));
                }
                setValues((prevValues) => ({ ...prevValues, isCompany: !prevValues.isCompany }));
                break;
            default:
                setValues({ ...values, [name]: value });
                break;
        }
    }

    useEffect(() => {
        console.log('values', values);
        console.log(props.resource)
    }, [values]);


    useEffect(() => {
        if (searchBar.length == 0) {
            console.log("searchbar empty")
            setCompaniesList([]);
            setValues({ ...values, companyId: null });
            return;
        }
        let list = companies.filter((company) => company.companyName!.toLowerCase().includes(searchBar.toLowerCase()) || company.address!.toLowerCase().includes(searchBar.toLowerCase()) 
        || company.identificationCode!.toLowerCase().includes(searchBar.toLowerCase()) || company.vatNumber!.toLowerCase().includes(searchBar.toLowerCase()));
        setCompaniesList(list);
        if (list.length > 0) {
            console.log("companylist not empty");
            setValues({ ...values, companyId : list[0].id! });
            return;
        }

    }, [searchBar]);


    function closePopUp() {
        props.onCloseInvoice(false);
    }

    const onSubmit = async (event: any) => {
        console.log('onSubmit', event);
        event.preventDefault();
        let numOfErrors = 0;
        setErrors([]);
        if (values.isCompany && values.companyId === null) {
            setErrors((errors) => [...errors,"Bad company is not setted!"]);
            numOfErrors++;
        }
        if (values.paymentMethodId === null) {
            setErrors((errors) => [...errors,"Bad payment method is not setted!"]);
            numOfErrors++;
        }
        if (values.invoiceRows.length > 0) {
            values.invoiceRows.map((row) => {
                if(Number.isNaN(row.quantity)){
                    setErrors((errors) => [...errors, `${props.event.service?.filter((service) => service.id === row.serviceId)[0].serviceName} bad quantity is not setted!`]);
                    numOfErrors++;
                } 
                if(row.total === 0){
                    setErrors((errors) => [...errors,`${props.event.service?.filter((service) => service.id === row.serviceId)[0].serviceName} bad total is not setted!`]);
                    numOfErrors++;
                }
            });
        }
        if(numOfErrors > 0) return;
        

        const res = await new InvoiceService().postInvoice(values, jwtResponse!.jwt)
        console.log(res);
        if (res !== undefined) {
            const recordSercice = new RecordService();
            const record = await recordSercice.getRecordById(jwtResponse!.jwt, props.event.id);
            if (record === undefined) return DOMException;
            props.event.service?.forEach((ev) => record!.recordServices!.push({ serviceId: ev.id, recordId: record!.id }));
            record!.isHidden = "true";
            console.log(record);
            const update = await recordSercice.updateRecord(jwtResponse?.jwt!, props.event.id, record);
            console.log("update", update);
            if (update === undefined) return DOMException;
            console.log("update", update);
            props.onClose();
        }

    }

    function handleTogleClientAdd(event : any) {
        event.preventDefault();
        setCompanyAddToggle(!companyAddToggle);
    }
    return (
        console.log(errors),console.log(values.invoiceRows.length, "inv row"),console.log(props.event.service?.length, "service"),console.log(values.invoiceRows.length, "inv row"),
        <>
            {companyAddToggle && <PopUpCompanyHandler onClose={setCompanyAddToggle}></PopUpCompanyHandler>}
            {(values.invoiceRows.length == 0 && props.event.service?.length == 0) || values.invoiceRows.length > 0 ? (
                <>
                    <div className="popup">
                        <div className="popup-window2">
                            <div b-wrkb45jn1q className="container">
                                <div b-wrkb45jn1q role="main" className="pb-3">


                                    <h1>Create</h1>

                                    <h4>Invoice</h4>
                                    <hr />
                                    <div>
                                        <ul style={{ 'display': errors.length == 0 ? 'none' : '' }}>
                                            {errors.map((error) => { return <li>{error}</li> })}
                                        </ul>
                                    </div>
                                    <div className="row">
                                        <div className="col-md-4">
                                            <form action="/Invoices/Create" method="post">

                                                <div className="form-group">
                                                    <label className="control-label" htmlFor="InvoiceFooterId">InvoiceFooter</label>
                                                    <select className="form-control" data-val="true" data-val-required="The InvoiceFooterId field is required." id="InvoiceFooterId" name="InvoiceFooterId">
                                                        {invoiceFooter.map((footer) => (<option value={footer.id}>{footer.companyName}</option>))}
                                                    </select>
                                                </div>
                                                <div className="form-group">
                                                    <label className="control-label" htmlFor="AppUserId">Worker</label>
                                                    <select className="form-control" data-val="true" data-val-required="The AppUserId field is required." id="AppUserId" name="AppUserId">
                                                        <option value={props.resource.resourceId}>{props.resource.resourceTitle}</option>
                                                    </select>
                                                </div>
                                                <div className="form-group">
                                                    <label className="control-label" htmlFor="RecordId">Record</label>
                                                    <select className="form-control" data-val="true" data-val-required="The RecordId field is required." id="RecordId" name="recordId">
                                                        <option value={props.event.id}>{props.event.title}</option>
                                                    </select>
                                                </div>
                                                <div className="form-group">
                                                    <label className="control-label" htmlFor="PaymentMethodId">PaymentMethod</label>
                                                    <select className="form-control" data-val="true" data-val-required="The PaymentMethodId field is required." id="PaymentMethodId" name="paymentMethodId" onChange={(event) => handleChange(event)}>
                                                        <option value="">Select payment option</option>
                                                        {paymentMethods.map((paymentMethod) => { return <option value={paymentMethod.id}>{paymentMethod.paymentMethodName}</option> })}
                                                    </select>
                                                </div>
                                                <div className="form-group form-check">
                                                    <label className="form-check-label">
                                                        <input className="form-check-input" type="checkbox" data-val="true" data-val-required="The IsCompany field is required." id="isCompany" name="isCompany"
                                                            onChange={(event) => handleChange(event)} /> IsCompany
                                                    </label>
                                                </div>
                                                {values.isCompany ? 
                                                    (<div className="form-group">
                                                    <label className="control-label" htmlFor="Client">Companies search:</label>
                                                    <div className="form-floating mb-3">
                                                        <input
                                                            onChange={(e) => setSearchBar(e.target.value)}
                                                            value={searchBar}
                                                            className="form-control" autoComplete="searchBar" aria-required="true" placeholder="" type="text"
                                                            id="Input_searchBar" maxLength={128} name="searchBar" />
                                                        <label htmlFor="Input_searchBar">Search</label>
                                                    </div>
                                                    {searchBar.length > 0 ? (<div className="form-group">
                                                        <label className="control-label" htmlFor="Client">Companies</label>
                                                        <select className="form-control" data-val="true" data-val-required="The Client field is required." id="companyId" name="companyId" onChange={(e) => handleChange(e)} value={values.companyId as string}>
                                                            {companiesList!.map((company) => (
                                                                <option value={company.id}>{company.companyName}</option>
                                                            ))}
                                                        </select>
                                                    </div>) :
                                                    (<>
                                                    <label className="control-label" htmlFor="Client">Companies</label>
                                                        <select className="form-control" data-val="true" data-val-required="The Client field is required." id="companyId" name="companyId" onChange={(e) => handleChange(e)}>
                                                            <option value="">Select company</option>
                                                            {companies.map((company) => (
                                                                <option value={company.id}>{company.companyName}</option>
                                                            ))}
                                                        </select>
                                                    </>)}
                                                    <button className="button1" style={{margin : "5px 0px 0px 0px"}} onClick={e => handleTogleClientAdd(e)}>Add company</button>
                                                </div>) : null}
                                                <div className="form-group">
                                                    <label className="control-label" htmlFor="Comment">Comment</label>
                                                    <input className="form-control" type="text" data-val="true" data-val-maxlength="The field Comment must be a string or array type with a maximum length of &#x27;200&#x27;."
                                                        data-val-maxlength-max="200" id="comment" maxLength={200} name="comment" value={values.comment} onChange={(event) => handleChange(event)} />
                                                </div>
                                                <div className='button-container'>
                                                    <div className='form-group'>
                                                        <p className='button1' onClick={closePopUp}>Close </p>
                                                    </div>
                                                    <div className='form-group'>
                                                        <p className='button1' onClick={onSubmit} >Save</p>
                                                    </div>
                                                </div>
                                                <input name="__RequestVerificationToken" type="hidden" value="CfDJ8EHBI2yIA7RGjJPWM4qjsmsvsX2aJXSCvD2SFBssg7nggPxVs5TZuXvNmOP3oHo4FsMdIZbLsiNVz6RgIiSKIXur037JFzDSN8UgIu1DaljAc9cMpKxRNEnEA9vK4MnhwIQkbOUFjdJI2ZMRzKzVQuY" /><input name="IsCompany" type="hidden" value="false" /></form>
                                        </div>
                                        <div className="rows-container">
                                            <InvoiceRow invoice={values} services={props.event.service} onChange={handleChange} />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </>

            ) : <div>Loading...</div>}

        </>


    );
}