import { Event } from "react-big-calendar";
import { IService } from "../domain/IService";

export interface IEvent{
    id : string;
    title: string;
    allDay: boolean;
    start: Date;
    end: Date;
    desc: string;
    user? : string;
    resourceId: string;
    serviceId? : string;
    service? : IService[];
    clientId: string;
    isHidden: string;

};