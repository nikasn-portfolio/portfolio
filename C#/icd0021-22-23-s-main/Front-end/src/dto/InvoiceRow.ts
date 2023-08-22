export interface IInvoiceRow {
    
    serviceId : string;

    quantity : number;
    
    priceOverride : number | null;
    
    total : number;
}