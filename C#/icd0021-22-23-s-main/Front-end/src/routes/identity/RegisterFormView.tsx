import { MouseEvent, useContext, useEffect, useState } from "react";
import { IRegisterData } from "../../dto/IRgeisterData";
import { IdentityService } from "../../services/IdentityService";
import { JwtContext } from "../Root";

interface IProps {
    values: IRegisterData;

    validationErrors: string[];

    handleChange: (target: EventTarget & HTMLInputElement) => void;

    onSubmit: (event: MouseEvent) => void;

    jwt: any;

}

const RegisterFormView = (props: IProps) => {
    const [searchBar, setSearchBar] = useState("");
    const [notification, setNotification] = useState([] as string[]);
    const [workerList, setWorkerList] = useState([] as any[] | undefined);
    const [selectedWorker, setSelectedWorker] = useState("" as string | undefined);
    const identityService = new IdentityService();
    useEffect(() => {
        let timer: string | number | NodeJS.Timeout | undefined;

        const getWorkersBySearchBarAsync = async () => {
            try {
                const workers = await identityService.getWorkersBySearch(searchBar, props.jwt);
                console.log(workers);
                setWorkerList(workers);
                if(workers !== undefined && workers.length > 0) setSelectedWorker(workers[0].id);
            } catch (error) {
                // Handle errors
            }
        };

        const delayedSearch = () => {
            // Clear previous timer if exists
            clearTimeout(timer);

            // Set a new timer for the delay
            timer = setTimeout(() => {
                getWorkersBySearchBarAsync();
            }, 500); // Adjust the delay time (in milliseconds) as needed
        };

        delayedSearch();

        // Cleanup function (if needed)
        return () => {
            clearTimeout(timer);
        };
    }, [searchBar]);

    async function handleWorkerRemove(id: string | undefined) {
        setNotification([] as string[]);
        let res = await identityService.removeWorker(id as string, props.jwt);
        if(res !== undefined){
            setNotification((prevErrors) => [...prevErrors, "Worker removed!"]);
        }
    }

    return (
        console.log(props.validationErrors.length),
        <>
            <form>
                <h2>Create a new account for worker.</h2>
                <hr />

                <ul style={{ 'display': props.validationErrors.length == 0 ? 'none' : '' }}>
                    {props.validationErrors.map((error) => { return <li>{error}</li> })}
                </ul>

                <div className="form-floating mb-3">
                    <input
                        onChange={(e) => props.handleChange(e.target)}
                        value={props.values.email}
                        className="form-control" autoComplete="username" aria-required="true" placeholder="name@example.com" type="email"
                        id="Input_Email" name="email" />
                    <label htmlFor="Input_Email">Email</label>
                </div>
                <div className="form-floating mb-3">
                    <input
                        onChange={(e) => props.handleChange(e.target)}
                        value={props.values.password}
                        className="form-control" autoComplete="new-password" aria-required="true" placeholder="password" type="password"
                        id="Input_Password" maxLength={100} name="password" />
                    <label htmlFor="Input_Password">Password</label>
                </div>
                <div className="form-floating mb-3">
                    <input
                        onChange={(e) => props.handleChange(e.target)}
                        value={props.values.confirmPassword}
                        className="form-control" autoComplete="new-password" aria-required="true" placeholder="password" type="password"
                        id="Input_ConfirmPassword" name="confirmPassword" />
                    <label htmlFor="Input_ConfirmPassword">Confirm Password</label>
                </div>

                <div className="form-floating mb-3">
                    <input
                        onChange={(e) => props.handleChange(e.target)}
                        value={props.values.firstName}
                        className="form-control" autoComplete="firstname" aria-required="true" placeholder="FirstName" type="text"
                        id="Input_FirstName" maxLength={128} name="firstName" />
                    <label htmlFor="Input_FirstName">First name</label>
                </div>


                <div className="form-floating mb-3">
                    <input
                        onChange={(e) => props.handleChange(e.target)}
                        value={props.values.lastName}
                        className="form-control" autoComplete="lastname" aria-required="true" placeholder="LastName" type="text"
                        id="Input_LastName" maxLength={128} name="lastName" />
                    <label htmlFor="Input_LastName">Last name</label>
                </div>

                <button
                    onClick={(e) => props.onSubmit(e)}
                    id="registerSubmit" className="button1">Register</button>

            </form>
            <hr />
            <h2>Delete worker</h2>
            <hr />
            <div>{notification[0]}</div>
            <div className="form-floating mb-3">
                <input
                    onChange={(e) => setSearchBar(e.target.value)}
                    value={searchBar}
                    className="form-control" autoComplete="searchBar" aria-required="true" placeholder="" type="text"
                    id="Input_searchBar" maxLength={128} name="searchBar" />
                <label htmlFor="Input_searchBar">Search</label>
            </div>
            {workerList !== undefined ? (<div className="form-group">
                <label className="control-label" htmlFor="Client">Workers</label>
                <select className="form-control" data-val="true" data-val-required="The Client field is required." id="Client" name="clientId" onChange={(e) => setSelectedWorker(e.target.value)} value={selectedWorker}>
                    {workerList.map((client) => (
                        <option value={client.id}>{client.firstName + " " + client.lastName}</option>
                    ))}
                </select>
                <button className="button1" style={{ margin: "5px 0px 0px 0px" }} onClick={e => handleWorkerRemove(selectedWorker)}>Remove selected worker</button>
            </div>) : <div>Workers not found!</div>}
        </>

    );
}

export default RegisterFormView;