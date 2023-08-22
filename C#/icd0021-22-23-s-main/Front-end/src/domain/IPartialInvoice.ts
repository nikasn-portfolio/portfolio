import { IAppUser } from "./IAppUser"
import { IClient } from "./IClient"
import { IPaymentMethod } from "./IPaymentMethod"
import { IRecord } from "./IRecord";

export interface IPartialInvoice {
    id: string;
    invoiceDate: Date;
    paymentDate: Date | null;
    paymentMethod: IPaymentMethod;
    client: IClient;
    appUser: IAppUser;
    serviceNames: string[];
    record: IRecord;
}