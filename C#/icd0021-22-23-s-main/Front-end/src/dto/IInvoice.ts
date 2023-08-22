import { IBaseEntity } from "../domain/IBaseEntity";
import { IInvoiceRow } from "./InvoiceRow";

export interface IInvoice extends IBaseEntity{
    companyId : string | null;
    invoiceFooterId : string | null;
    clientId : string | null;
    appUserId : string | null;
    recordId : string | null;
    paymentMethodId : string | null;
    paymentDate : Date | string | null;
    isCompany : boolean;
    comment : string
    invoiceRows : IInvoiceRow[];
}