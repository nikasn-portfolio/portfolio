import { useEffect } from "react";
import { IService } from "../domain/IService";
import { IInvoice } from "../dto/IInvoice";

interface Props {
    invoice: IInvoice;
    services: IService[] | undefined;
    onChange: (data : any) => void;
}

export const InvoiceRow = (props: Props) => {
  return(
    <>
        {props.services?.map((service) => (
            <>
                <div className="row-item">
                <div>{service.serviceName}</div>
                <label htmlFor="quantity">Quantity: </label>
                <input type="number" step="1" min="1" onChange={props.onChange} id="quantity" name={service.id} value={props.invoice.invoiceRows.filter(e => e.serviceId === service.id)[0].quantity} />
                <label htmlFor="priceOverride">New price: </label>
                <input type="number" step="1" min="0" onChange={props.onChange} id="priceOverride" name={service.id} value={props.invoice.invoiceRows.filter(e => e.serviceId === service.id)[0].priceOverride!} />
                <label htmlFor="total">Total price </label>
                <input type="number" step="1" min="0" onChange={props.onChange} id="total" name={service.id} value={parseInt(service.servicePrice) * props.invoice.invoiceRows.filter(s => s.serviceId === service.id)[0].quantity} />
            </div>
            <hr />
            </>
        ))}
    </>
  );  
};