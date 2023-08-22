import { IRecordService } from "./IRecordService";
import { IService } from "./IService";

export interface IRecord {
    id: string;
    serviceId: string;
    title: string;
    startTime : Date | string;
    endTime : Date | string;
    comment: string;
    isHidden: string;
    clientId: string;
    appUserId: string;
    recordServices? : IRecordService[];
}