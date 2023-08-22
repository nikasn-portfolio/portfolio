import { IInvoiceFooterCreate } from "../domain/IInvoiceFooterCreate";
import { BaseEntityService } from "./BaseEntityService";

export class InvoiceFooterService extends BaseEntityService<IInvoiceFooterCreate> {
    
    constructor(){
        super(`v1/InvoiceFooters`);
    }

    
    
}