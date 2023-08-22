import React, { useContext, useEffect, useMemo, useState } from 'react'
import { Calendar, View, DateLocalizer, Views, ResourceHeaderProps, CalendarProps, luxonLocalizer, EventProps, Event, EventPropGetter } from 'react-big-calendar'
import "react-big-calendar/lib/css/react-big-calendar.css";
import "../site.css";
import { Link, useNavigate, useParams } from 'react-router-dom';
import { MyEvent } from '../components/MyEvent';
import { PopUpRecordHandler } from '../components/PopUpUpdateEvent';
import { IRecord } from '../domain/IRecord';
import { IService } from '../domain/IService';
import { IUserResource } from '../domain/IUserResource';
import { IEvent } from '../dto/IEvent';
import { IdentityService } from '../services/IdentityService';
import { RecordService } from '../services/RecordService';
import { ServiceService } from '../services/ServiceService';
import { JwtContext } from './Root';
import { DateTime, Settings } from 'luxon'
import TimezoneSelect from '../components/TimeZoneSelect';
import { ClientService } from '../services/ClientService';
import { IClient } from '../domain/IClient';
import { event } from 'jquery';
import moment from 'moment';

const allViews: View[] = ['day', 'work_week'];



interface Props {
  localizer: DateLocalizer;
}

export interface IResource {
  resourceId: string;
  resourceTitle: string;
}




export default function SelectableCalendar({ localizer }: Props) {
  const { jwtResponse, setJwtResponse, userRole, userId } = useContext(JwtContext);
  const [resource, setResources] = useState([] as IResource[])
  const [selectedEvent, setSelectedEvent] = useState(null as IEvent | null);
  const [eventData, setEventData] = useState([] as IRecord[]);
  const recordService = new RecordService();
  const identityService = new IdentityService();
  const servicesObj = new ServiceService();
  const [workerResource, setWorkerResource] = useState([] as IUserResource[]);
  const [services, setServices] = useState([] as IService[]);
  const { date } = useParams();
  const [clientData, setClientData] = useState([] as IClient[]);
  const navigate = useNavigate();

  useEffect(() => {
    console.log(date);
  }, [date]);
  useEffect(() => {
    if(jwtResponse === null){
      navigate("/login");
      console.log("redirect to login")
    }
  }, [jwtResponse]);

  useEffect(() => {
    console.log("popup closed")
  }, [selectedEvent])

  useEffect(() => {
    console.log("useEffect" + jwtResponse);
    if (jwtResponse) {
      recordService.getAll(jwtResponse.jwt).then(
        response => {
          console.log(response);
          if (response) {
            setEventData(response);
          } else {
            setEventData([]);
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
      servicesObj.getAll(jwtResponse.jwt).then(response => {
        console.log(response);
        if (response) {
          setServices(response);
        } else {
          setServices([]);
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
  }, [selectedEvent]);

  let event = useMemo(() => {
    return eventData.map((record) => {
      return {
        id: record.id,
        title: record.title,
        start: new Date(Date.UTC(new Date(record.startTime).getUTCFullYear(), new Date(record.startTime).getUTCMonth(),
        new Date(record.startTime).getUTCDate(), new Date(record.startTime).getUTCHours(),
        new Date(record.startTime).getUTCMinutes(), new Date(record.startTime).getUTCSeconds())),

        end: new Date(Date.UTC(new Date(record.endTime).getUTCFullYear(), new Date(record.endTime).getUTCMonth(),
        new Date(record.endTime).getUTCDate(), new Date(record.endTime).getUTCHours(),
        new Date(record.endTime).getUTCMinutes(), new Date(record.endTime).getUTCSeconds())),
        allDay: false,
        desc: record.comment.toString(),
        resourceId: record.appUserId,
        clientId: record.clientId,
        serviceId: record.serviceId,
        service: services.filter((service) => record.recordServices!.some((recordService) => recordService.serviceId === service.id)),
        isHidden: record.isHidden.toLowerCase(),
      } as IEvent;
    }) as IEvent[];
  }, [eventData]);

  useEffect(() => {
    var resourceList = [] as IResource[];
    workerResource.forEach((resource) => resourceList.push({ resourceId: resource.id, resourceTitle: resource.firstName + " " + resource.lastName }));
    if(userRole == "Worker"){
      resourceList = resourceList.filter(resource => resource.resourceId == userId)
    }
    setResources(resourceList);
  }, [workerResource]);


  //console.log(events);

  // class="rbc-event rbc-selected"

  //useEffect(() => {
  //   const handleDocumentClick = (event: MouseEvent) => {
  //     // Get the clicked element
  //     const clickedElement = event.target as HTMLElement;

  //     // Check if the clicked element is inside the calendar events
  //     const isCalendarEvent = clickedElement.classList.contains('rbc-event-content') || clickedElement.classList.contains('rbc-event') 
  //     || clickedElement.classList.contains('popup');

  //     console.log(clickedElement);
  //     console.log(isCalendarEvent);

  //     // Remove the selection if the click was outside of the calendar events
  //     if (!isCalendarEvent) {
  //       setSelectedId('');
  //     }
  //   };

  //   document.addEventListener('click', handleDocumentClick);

  //   return () => {
  //     document.removeEventListener('click', handleDocumentClick);
  //   };
  // }, [setSelectedId]);

  /*const defaultTZ = DateTime.local().zoneName

  
    const [timezone, setTimezone] = useState(defaultTZ)
  
    const {getNow, myEvents, scrollToTime } =
      useMemo(() => {
        Settings.defaultZone = timezone as string
        return {
          getNow: () => DateTime.local().toJSDate(),
          myEvents: [...events],
          scrollToTime: DateTime.local().toJSDate(),
        }
      }, [timezone])
  
    useEffect(() => {
      return () => {
        Settings.defaultZone = defaultTZ as string // reset to browser TZ on unmount
      }
    }, [])
    
    <TimezoneSelect
        defaultTZ={defaultTZ as string | undefined}
        setTimezone={setTimezone}
        timezone={timezone}
        title={`This calendar uses the 'luxonLocalizer'`}
      />
    */


function eventPropGetter(event: IEvent, start: Date, end: Date, isSelected: boolean): { className?: string, style?: React.CSSProperties } {
  if(event.isHidden === "true"){
    console.log("hidden")
    return { style: { backgroundColor: '#e6e6e6', color : 'black', borderColor : '#a6a6a6', alignItems : 'center' } };
  }
  if(event){
    start = new Date();
    return { style: { backgroundColor: '#e8fbe8', color : 'black', borderColor : '#90ee90', alignItems : 'center' } };
  }
  return {};
}

  return (
    <>

      {eventData.length > 0 && event.length > 0 && workerResource.length > 0 && clientData.length > 0 ? (
        console.log(event),
        <>
          <div className='.rbc-toolbar button'>
            <Link to='createRecord' className=''> <button className='button1'>Create record</button></Link>
            <strong>
              {userRole + " : " + eventData.length}
            </strong>
          </div>

          <Calendar
            defaultDate={date != null ? date : new Date()}
            defaultView={allViews[0]}
            eventPropGetter={eventPropGetter}
            min={new Date(0, 0, 0, 8, 0, 0)}
            max={new Date(0, 0, 0, 20, 0, 0)}
            events={event}
            localizer={localizer}
            resourceIdAccessor="resourceId"
            resources={resource}
            resourceTitleAccessor="resourceTitle"
            step={15}
            style={{ fontSize: '16px'}}
            views={allViews}
            startAccessor='start'
            endAccessor='end'
            titleAccessor='title'
            components={{
              event: (event: EventProps<IEvent>) => (
                <MyEvent event={event.event} clientData={clientData} resource={workerResource} />
              ),
            }}
            onSelectEvent={(currentEvent) => setSelectedEvent(currentEvent)}
          />
        </>
      ) : <div>loading... {eventData.length}</div>}
      {selectedEvent && selectedEvent.isHidden === "false" ? (<PopUpRecordHandler event={selectedEvent} setSelectedEvent={(event: IEvent | null) => setSelectedEvent(event)} resource={resource.filter((resource) => selectedEvent.resourceId === resource.resourceId)[0]}></PopUpRecordHandler>) : null}
    </>
  );
}

function useCallback(arg0: (event: any, start: any, end: any, isSelected: any) => any, arg1: never[]) {
    throw new Error('Function not implemented.');
  }
