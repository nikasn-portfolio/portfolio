import { IAppUser } from "./IAppUser"
import { IClient } from "./IClient"
import { IInvoiceFooterOnView } from "./IInvoiceFooterOnView"
import { IInvoiceRowOnView } from "./IInvoiceRowOnView"
import { IPaymentMethod } from "./IPaymentMethod"
import { IRecord } from "./IRecord"

export interface IInvoice{
    id: string,
    invoiceDate: Date,
    paymentDate: Date,
    paymentMethod: IPaymentMethod,
    client: IClient,
    appUser: IAppUser,
    record: IRecord,
    invoiceRows: IInvoiceRowOnView[],
    invoiceFooter: IInvoiceFooterOnView
}