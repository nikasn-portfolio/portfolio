import { event } from 'jquery';
import moment from 'moment';
import React, { useContext, useEffect, useRef, useState } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import { ICategory } from '../domain/ICategory';
import { IClient } from '../domain/IClient';
import { IRecord } from '../domain/IRecord';
import { IRecordService } from '../domain/IRecordService';
import { IService } from '../domain/IService';
import { IUserResource } from '../domain/IUserResource';
import { IEvent } from '../dto/IEvent';
import { IResource } from '../routes/MyCalendar';
import { JwtContext } from '../routes/Root';
import { CategoryService } from '../services/CategoryService';
import { ClientService } from '../services/ClientService';
import { IdentityService } from '../services/IdentityService';
import { RecordService } from '../services/RecordService';
import { ServiceService } from '../services/ServiceService';
import { PopUpClientHandler } from './PopUpClientHandler';
import { PopUpInvoiceHandler } from './PopUpInvoiceCreate';
import ReactDateTime from './ReactDateTime';


interface Props {
  event: IEvent;
  setSelectedEvent: (event: IEvent | null) => void
  resource: IResource;
}

export const PopUpRecordHandler = (props: Props) => {

  const [searchBar, setSearchBar] = useState("");
  const [values, setInput] = useState({
    id: props.event.id,
    recordServices: props.event.service?.map((service) => { return { serviceId: service.id, recordId: props.event.id } as IRecordService }) as IRecordService[],
    serviceId: "",
    title: props.event.title,
    startTime: moment(new Date(Date.UTC(props.event.start.getUTCFullYear(), props.event.start.getUTCMonth(),
    props.event.start.getUTCDate(), props.event.start.getUTCHours(),
    props.event.start.getUTCMinutes(), props.event.start.getUTCSeconds()))).utcOffset(0, true).format(),
    endTime: props.event.end,
    comment: props.event.desc,
    isHidden: props.event.isHidden.toString(),
    appUserId: props.event.resourceId,
    clientId: props.event.clientId,
  } as IRecord);
  const [localServiceList, setLocalServiceList] = useState(JSON.parse(JSON.stringify(props.event.service as IService[])) as IService[]);
  const { jwtResponse, setJwtResponse, userRole, userId } = useContext(JwtContext);
  const [categoryWithServices, setCategoryWithServices] = useState([] as ICategory[]);
  const [services, setServices] = useState([] as IService[]);
  const categoryWithServicesClass = new CategoryService();
  const recordService = new RecordService();
  const identityService = new IdentityService();
  const [workerResource, setWorkerResource] = useState([] as IUserResource[]);
  const navigate = useNavigate();
  const [clientData, setClientData] = useState([] as IClient[]);
  const [currentTarget, setCurrentTarget] = useState(null as EventTarget & HTMLSelectElement | null)
  const [closeRecord, setCloseRecord] = useState(false);
  const [validationErrors, setValidationErrors] = useState([] as string[]);
  const [clientAddToggle, setClientAddToggle] = useState(false as boolean);
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
    if (target.value == "" && target.tagName.toLocaleLowerCase() === "select") return;
    if (target.name == "serviceId") {
      let service = services.filter((service) => service.id == target.value)[0];
      console.log(service);
      setLocalServiceList([...localServiceList, service]);
      values.recordServices?.push({ serviceId: service.id, recordId: values.id });
    }
    console.log(localServiceList);
    setInput({ ...values, [target.name]: target.value });
    target.value = "";

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

    }
  }, []);

  useEffect(() => {
    if (jwtResponse) {
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
      setInput({ ...values, clientId: props.event.clientId });
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

    /*if (values.email.length == 0 || values.password.length == 0) {
        setValidationErrors(["Bad input values!"]);
        return;
    }*/
    // remove errors
    //setValidationErrors([]);
    console.log(values)


  }

  function handleTogleClientAdd(event: any) {
    event.preventDefault();
    setClientAddToggle(!clientAddToggle);
  }

  function closePopUp() {
    props.setSelectedEvent(null);
    console.log(props.event.service?.map((service) => { return { serviceId: service.id, recordId: props.event.id } as IRecordService }));
    //values.recordServices = props.event.service?.map((service) => {return {serviceId : service.id, recordId : props.event.id} as IRecordService});
  }

  async function DeleteFromPopUp() {
    console.log("delete")
    const deletedRecord = await recordService.deleteRecordById(jwtResponse?.jwt as string, props.event.id);
    console.log(deletedRecord);
    closePopUp();
  }

  async function UpdateFromPopUp() {
    console.log("update")
    if (values.title == "" || values.title == null) {
      setValidationErrors((error) => [...error, "Title is required!"]);
      return;
    }

    const updatedRecord = await recordService.updateRecord(jwtResponse?.jwt as string, props.event.id, values);
    console.log(updatedRecord);
    closePopUp();
  }

  async function CloseRecord() {
    //closePopUp();
    console.log("close");
    setCloseRecord(true);
  }

  const localServiceIds = localServiceList.map((service) => service.id);
  const filteredServices = services.filter((service) => !localServiceIds.includes(service.id));
  function PutInArrayValueFirstById(array: any, idToPlaceFirst: any) {
    // Find the index of the item with the specified id
    const index = array.findIndex((item: { id: any; }) => item.id === idToPlaceFirst);

    // If the item is found, move it to the beginning of the array
    if (index !== -1) {
      const item = array[index];
      array.splice(index, 1);
      array.unshift(item);
    }

    return array as IClient[];
  }

  return (
    console.log(values),console.log(new Date(moment(new Date(values.startTime)).utcOffset(6, true).format())),
    <>
      {clientAddToggle && <PopUpClientHandler onClose={setClientAddToggle}></PopUpClientHandler>}
      {closeRecord && (<PopUpInvoiceHandler onCloseInvoice={setCloseRecord} onClose={closePopUp} event={props.event} resource={props.resource}></PopUpInvoiceHandler>)}
      <div className="popup">
        <div className="popup-window">
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
                  <select className="form-select" data-val="true" data-val-required="The ServiceId field is required." id="Service" name="categoryId" onChange={(e) => handleChangeCategory(e.target)}>
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
                  <button className='button1' style={{ margin: "10px auto" }} onClick={(e) => handleTarget(e)}>Delete selected service</button>
                </div>
                <div className="form-group">
                  <label className="control-label" htmlFor="Worker">Worker</label>
                  <select className="form-control" data-val="true" data-val-required="The ServiceId field is required." id="Worker" name="appUserId" onChange={(e) => handleChange(e.target)} value={values.appUserId}>
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
                <>
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
                      <select className="form-control" data-val="true" data-val-required="The Client field is required." id="Client" name="clientId" onChange={(e) => handleChange(e.target)} value={values.clientId}>
                        {clientList!.map((client) => (
                          <option value={client.id}>{client.firstName + " " + client.lastName}</option>
                        ))}
                      </select>
                    </div>) :
                      (<>
                        <label className="control-label" htmlFor="Client">Clients</label>
                        <select className="form-control" data-val="true" data-val-required="The Client field is required." id="Client" name="clientId" onChange={(e) => handleChange(e.target)} value={values.clientId}>
                          {PutInArrayValueFirstById(clientData, props.event.clientId).map((client) => (
                            <option value={client.id}>{client.firstName + " " + client.lastName}</option>
                          ))}
                        </select>
                      </>)}
                    <button className="button1" style={{ margin: "5px 0px 0px 0px" }} onClick={e => handleTogleClientAdd(e)}>Add client</button>

                  </div>
                </>
                <div className="form-group" style={{ width: values.title.length > 8 ? 380 : 100 }}>
                  <label className="control-label" htmlFor="Title">Title</label>
                  <textarea className="form-control" wrap="soft" style={{ resize: "none", height: values.title.length > 45 ? 76 : 38 }} data-val="true" data-val-maxlength="The field Title must be a string or array type with a maximum length of &#x27;100&#x27;." data-val-maxlength-max="100" data-val-required="The Title field is required." id="Title" maxLength={100} name="title" value={values.title} onChange={(e) => handleChange(e.target)} />
                </div>

                <ReactDateTime onChange={(time: Date) => handleStartTimeChange(time)} label={"Start date"} value={new Date(moment(new Date(values.startTime)).utcOffset(6, true).format())} />
                <ReactDateTime onChange={(time: Date) => handleEndTimeChange(time)} label={"End date"} value={values.endTime as Date} />


                <div className={`form-group`} style={{ width: values.comment.length > 8 ? 380 : 100 }}>
                  <label className="control-label" htmlFor="Comment">Comment</label>
                  <textarea className="form-control" wrap="soft" style={{ resize: "none", height: values.comment.length > 45 ? 76 : 38 }} data-val="true" data-val-maxlength="The field Comment must be a string or array type with a maximum length of &#x27;200&#x27;." data-val-maxlength-max="200" id="Comment" maxLength={200} name="comment" value={values.comment} onChange={(e) => handleChange(e.target)} />
                </div>
              </form>
            </div>
          </div>
          <p className='button1' style={{ margin: "10px 0px 10px", display: jwtResponse != null && userRole == "Admin" ? '' : 'none' }} onClick={CloseRecord} >Close record</p>
          <div className='button-container'>
            <div className='form-group'>
              <p className='button1' onClick={closePopUp}>Close </p>
            </div>
            <div className='form-group'>
              <p className='button1' onClick={DeleteFromPopUp}>Delete </p>
            </div>
            <div className='form-group'>
              <p className='button1' onClick={UpdateFromPopUp} >Save</p>
            </div>
          </div>



        </div>
      </div>
    </>


  );
}
