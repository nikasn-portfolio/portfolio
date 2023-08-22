import { useContext, useEffect, useRef, useState } from "react";
import { IRecord } from "../domain/IRecord";
import { IService } from "../domain/IService";
import { ServiceService } from "../services/ServiceService";
import { JwtContext } from "./Root";
import { v4 as Guid } from 'uuid';
import { Link, useNavigate } from "react-router-dom";
import { IdentityService } from "../services/IdentityService";
import { IUserResource } from "../domain/IUserResource";
import { RecordService } from "../services/RecordService";
import { ClientService } from "../services/ClientService";
import { IClient } from "../domain/IClient";
import { setHours, setMinutes } from "date-fns";
import ReactDateTime from "../components/ReactDateTime";
import { IRecordService } from "../domain/IRecordService";
import { CategoryService } from "../services/CategoryService";
import { ICategory } from "../domain/ICategory";
import { PopUpClientHandler } from "../components/PopUpClientHandler";
import moment from "moment";


const CreateRecord = () => {
    const { jwtResponse, setJwtResponse, userRole, userId } = useContext(JwtContext);
    const [selectedClientId, setSelectedClientId] = useState("");
    const [values, setInput] = useState({
        id: "",
        recordServices: [] as IRecordService[],
        serviceId: "",
        title: "",
        startTime: new Date(new Date().setHours(1, 1)),
        endTime: new Date(new Date().setHours(1, 1)),
        comment: "",
        isHidden: "false",
        appUserId: userRole == "Worker" ? userId : "",
        clientId: "",
    } as IRecord);
    const [localServiceList, setLocalServiceList] = useState([] as IService[]);
    const [categoryWithServices, setCategoryWithServices] = useState([] as ICategory[]);
    const [services, setServices] = useState([] as IService[]);
    const categoryWithServicesClass = new CategoryService();
    const recordService = new RecordService();
    const identityService = new IdentityService();
    const [workerResource, setWorkerResource] = useState([] as IUserResource[]);
    const navigate = useNavigate();
    const [clientData, setClientData] = useState([] as IClient[]);
    const [currentTarget, setCurrentTarget] = useState(null as EventTarget & HTMLSelectElement | null)
    const [validationErrors, setValidationErrors] = useState([] as string[]);
    const [clientAddToggle, setClientAddToggle] = useState(false as boolean);
    const [searchBar, setSearchBar] = useState("");
    const [clientList, setClientList] = useState([] as any[]);



    function handleStartTimeChange(date: Date) {
        setInput({ ...values, startTime: moment(new Date(Date.UTC(date.getUTCFullYear(), date.getUTCMonth(),
            date.getUTCDate(), date.getUTCHours(),
            date.getUTCMinutes(), date.getUTCSeconds()))).utcOffset(0, true).format() });
    }

    function handleEndTimeChange(date: Date) {
        setInput({ ...values, endTime: moment(new Date(Date.UTC(date.getUTCFullYear(), date.getUTCMonth(),
            date.getUTCDate(), date.getUTCHours(),
            date.getUTCMinutes(), date.getUTCSeconds()))).utcOffset(0, true).format() });
    }


    function handleChange(target: EventTarget & HTMLInputElement | EventTarget & HTMLSelectElement | EventTarget & HTMLTextAreaElement) {
        console.log(target.name + " : " + target.value);
        if (target.value == "" && target.id === "Service") return;

        if (target.name == "serviceId") {
            let service = services.filter((service) => service.id == target.value)[0];
            console.log(service);
            setLocalServiceList([...localServiceList, service]);
            // put serviceID as default value to change later on server on mapping
            values.recordServices?.push({ serviceId: service.id, recordId: service.id });
        }
        console.log(localServiceList);
        setInput({ ...values, [target.name]: target.value });
        if (target.id === "Service") target.value = "";

    }

    function handleChangeCategory(target: EventTarget & HTMLSelectElement) {
        console.log(target.value);
        if (target.value == "") {
            setServices([]);
            return;
        }
        setServices(categoryWithServices.filter((category) => category.id == target.value)[0].services as IService[]);
    }

    function handleSelectedService(target: EventTarget & HTMLSelectElement) {
        setCurrentTarget(target);
    }


    function handleTarget(event: any) {
        event.preventDefault();
        if (currentTarget != null) {
            console.log(currentTarget.value + " HT")
            setLocalServiceList(localServiceList.filter((service) => service.id !== currentTarget.value));
            setInput({ ...values, recordServices: values.recordServices?.filter((recordService) => recordService.serviceId !== currentTarget.value) })
            currentTarget.value = "";
        }

    }

    useEffect(() => {
        console.log(values);
    }, [values]);



    useEffect(() => {
        if (jwtResponse) {
            categoryWithServicesClass.getAll(jwtResponse.jwt).then(response => {
                console.log(response);
                if (response) {
                    setCategoryWithServices(response);
                } else {
                    setCategoryWithServices([]);
                }
            }
            );
            identityService.getWorkers(jwtResponse).then(
                response => {
                    console.log(response);
                    if (response) {
                        setWorkerResource(response);
                    } else {
                        setWorkerResource([]);
                    }
                }
            );
            new ClientService().getAll(jwtResponse.jwt).then(
                response => {
                    console.log(response);
                    if (response) {
                        setClientData(response);
                    } else {
                        setClientData([]);
                    }
                }
            );
        }
    }, [clientAddToggle]);

    useEffect(() => {
        if (searchBar.length == 0) {
            console.log("searchbar empty")
            setClientList([]);
            setInput({ ...values, clientId: "" });
            return;
        }
        let list = clientData.filter((client) => client.firstName!.toLowerCase().includes(searchBar.toLowerCase()) || client.lastName!.toLowerCase().includes(searchBar.toLowerCase()) || client.phoneNumber!.toLowerCase().includes(searchBar.toLowerCase()));
        setClientList(list);
        if (list.length > 0) {
            setInput({ ...values, clientId: list[0].id });
            console.log("clientlist not empty");
            return;
        }

    }, [searchBar]);

    const onSubmit = async (event: any) => {
        console.log('onSubmit', event);
        event.preventDefault();
        let numOfErorrs = 0;

        /*if (values.email.length == 0 || values.password.length == 0) {
            setValidationErrors(["Bad input values!"]);
            return;
        }*/
        // remove errors
        setValidationErrors([]);
        if (values.appUserId == "") {
            setValidationErrors((prev) => [...prev, "Choose worker!"]);
            numOfErorrs++;
        }
        if (values.clientId == "") {
            setValidationErrors((prev) => [...prev, "Choose client!"]);
            numOfErorrs++;
        }
        if (values.title == "") {
            setValidationErrors((prev) => [...prev, "Write title!"]);
            numOfErorrs++;
        }
        /*if (values.startTime.getHours() === 1) {
            setValidationErrors((prev) => [...prev, "Set start time hours!"]);
            numOfErorrs++;
        }
        if (values.startTime.getMinutes() === 1) {
            setValidationErrors((prev) => [...prev, "Set start time minutes!"]);
            numOfErorrs++;
        }*/
        /*if (values.endTime.getHours() === 1) {
            setValidationErrors((prev) => [...prev, "Set end time hours!"]);
            numOfErorrs++;
        }
        if (values.endTime.getMinutes() === 1) {
            setValidationErrors((prev) => [...prev, "Set end time minutes!"]);
            numOfErorrs++;
        }*/
        if (numOfErorrs > 0) return;
        console.log(values)
        if (jwtResponse) {
            var response = await recordService.postOrder(values, jwtResponse.jwt);
        }

        if (response == undefined) {
            // TODO: get error info
            //setValidationErrors(["no jwt"]);
            console.log(response);
            return;
        }

        if (response) {
            navigate('/calendar')
        };

    }

    function handleTogleClientAdd(event: any) {
        event.preventDefault();
        setClientAddToggle(!clientAddToggle);
    }

    const localServiceIds = localServiceList.map((service) => service.id);
    const filteredServices = services.filter((service) => !localServiceIds.includes(service.id));


    if (userRole == "Worker") {

    }

    return (
        console.log(setHours(setMinutes(new Date(), 0), 17).toISOString().substring(0, setHours(setMinutes(new Date(), 0), 17).toISOString().length - 5)),
        console.log(values),
        <>
            <div b-wrkb45jn1q className="container">
                <main b-wrkb45jn1q role="main" className="pb-3">


                    <h1>Create</h1>

                    <h4>Record</h4>
                    <hr />
                    <ul style={{ 'display': validationErrors.length == 0 ? 'none' : '' }}>
                        {validationErrors.map((error) => { return <li>{error}</li> })}
                    </ul>
                    <div className="row">
                        <div className="col-md-4">
                            <form action="/Records/Create" method="post">

                                <div className="form-group">
                                    <label className="control-label" htmlFor="Service">Service category</label>
                                    <select className="form-select" data-val="true" data-val-required="The ServiceId field is required." id="" name="categoryId" onChange={(e) => handleChangeCategory(e.target)}>
                                        <option value="">Select service to update record</option> {/* Add this */}
                                        {categoryWithServices.map((category) => (
                                            <option value={category.id}>{category.categoryName}</option>
                                        ))}
                                    </select>
                                    <label className="control-label" htmlFor="Service">Service</label>
                                    <select className="form-select" data-val="true" data-val-required="The ServiceId field is required." id="Service" name="serviceId" onChange={(e) => handleChange(e.target)}>
                                        <option value="">Select service to update record</option> {/* Add this */}
                                        {filteredServices.map((service) => (
                                            <option value={service.id}>{service.serviceName}</option>
                                        ))}
                                    </select>
                                    <label className="control-label" htmlFor="Service">Added services</label>
                                    <select className="form-select" data-val="true" data-val-required="The ServiceId field is required." id="Service" name="services" onChange={(e) => handleSelectedService(e.target)}>
                                        <option value="">Select service to delete</option> {/* Add this */}
                                        {localServiceList.map((service) => (
                                            <option value={service.id}>{service.serviceName}</option>
                                        ))}
                                    </select>
                                    <button className="button1" style={{ margin: "5px 0px 0px 0px" }} onClick={(e) => handleTarget(e)}>Delete selected service</button>
                                </div>
                                <div className="form-group">
                                    <label className="control-label" htmlFor="Worker">Worker</label>
                                    <select className="form-control" data-val="true" data-val-required="The ServiceId field is required." id="Worker" name="appUserId" onChange={(e) => handleChange(e.target)}>

                                        {
                                            userRole === "Worker" ? (
                                                <>
                                                    {workerResource
                                                        .filter((resource) => resource.id === userId)
                                                        .map((worker) => (
                                                            <option key={worker.id} value={worker.id}>
                                                                {worker.firstName + " " + worker.lastName}
                                                            </option>
                                                        ))}
                                                </>
                                            ) : (
                                                <>
                                                    <option value="">Select master</option>
                                                    {workerResource.map((worker) => (
                                                        <option key={worker.id} value={worker.id}>
                                                            {worker.firstName + " " + worker.lastName}
                                                        </option>
                                                    ))}
                                                </>
                                            )
                                        }
                                    </select>
                                </div>
                                <div className="form-group">
                                    <label className="control-label" htmlFor="Client">Client search:</label>
                                    <div className="form-floating mb-3">
                                        <input
                                            onChange={(e) => setSearchBar(e.target.value)}
                                            value={searchBar}
                                            className="form-control" autoComplete="searchBar" aria-required="true" placeholder="" type="text"
                                            id="Input_searchBar" maxLength={128} name="searchBar" />
                                        <label htmlFor="Input_searchBar">Search</label>
                                    </div>
                                    {searchBar.length > 0 ? (<div className="form-group">
                                        <label className="control-label" htmlFor="Client">Clients</label>
                                        <select className="form-control" data-val="true" data-val-required="The Client field is required." id="Client" name="clientId" onChange={(e) => handleChange(e.target)}>
                                            {clientList!.map((client) => (
                                                <option value={client.id}>{client.firstName + " " + client.lastName}</option>
                                            ))}
                                        </select>
                                    </div>) :
                                    (<>
                                    <label className="control-label" htmlFor="Client">Clients</label>
                                        <select className="form-control" data-val="true" data-val-required="The Client field is required." id="Client" name="clientId" onChange={(e) => handleChange(e.target)}>
                                            <option value="">Select client</option>
                                            {clientData.map((client) => (
                                                <option value={client.id}>{client.firstName + " " + client.lastName}</option>
                                            ))}
                                        </select>
                                    </>)}
                                    <button className="button1" style={{ margin: "5px 0px 0px 0px" }} onClick={e => handleTogleClientAdd(e)}>Add client</button>
                                </div>
                                <div className="form-group">
                                    <label className="control-label" htmlFor="Title">Title</label>
                                    <input className="form-control" type="text" data-val="true" data-val-maxlength="The field Title must be a string or array type with a maximum length of &#x27;100&#x27;." data-val-maxlength-max="100" data-val-required="The Title field is required." id="Title" maxLength={100} name="title" value={values.title} onChange={(e) => handleChange(e.target)} />
                                </div>

                                <ReactDateTime onChange={(time: Date) => handleStartTimeChange(time)} label={"Start date"} value={values.startTime as Date} />
                                <ReactDateTime onChange={(time: Date) => handleEndTimeChange(time)} label={"End date"} value={values.endTime as Date} />


                                <div className="form-group">
                                    <label className="control-label" htmlFor="Comment">Comment</label>
                                    <input className="form-control" type="text" data-val="true" data-val-maxlength="The field Comment must be a string or array type with a maximum length of &#x27;200&#x27;." data-val-maxlength-max="200" id="Comment" maxLength={200} name="comment" value={values.comment} onChange={(e) => handleChange(e.target)} />
                                </div>
                                <div className="form-group">
                                    <input type="submit" value="Create" className="btn btn-primary" onClick={(e) => onSubmit(e)} />
                                </div>
                            </form>
                        </div>
                    </div>

                    <div>
                        <Link to={`/calendar?date=${values.startTime}`}>Back to List</Link>
                    </div>


                </main>
            </div>
            {clientAddToggle && <PopUpClientHandler onClose={setClientAddToggle}></PopUpClientHandler>}
        </>

    );
}

export default CreateRecord;