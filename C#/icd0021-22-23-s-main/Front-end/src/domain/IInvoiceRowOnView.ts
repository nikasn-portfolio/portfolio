import { IService } from "./IService";

export interface IInvoiceRowOnView {
    id: string,
    service : IService,
    serviceId : string;

    quantity : number;
    
    priceOverride : number | null;
    
    total : number;
};