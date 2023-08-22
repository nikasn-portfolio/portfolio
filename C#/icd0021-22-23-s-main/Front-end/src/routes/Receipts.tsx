import React, { useContext, useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { useNavigate } from "react-router-dom";
import { IPartialInvoice } from "../domain/IPartialInvoice";
import { IInvoice } from "../dto/IInvoice";
import { InvoiceService } from "../services/InvoiceService";
import { JwtContext } from "./Root";

const Receipts = () => {

    const { jwtResponse, userRole, userId } = useContext(JwtContext);
    const [invoiceData, setInvoiceData] = useState([] as IPartialInvoice[]);
    const [invoiceFooterData, setInvoiceFooterData] = useState();
    const [invoiceRowData, setInvoiceRowData] = useState();
    const invoiceService = new InvoiceService();
    const navigate = useNavigate();
    useEffect(() => {
        console.log("useEffect" + jwtResponse);
        if (jwtResponse) {
            invoiceService.getAll(jwtResponse.jwt).then(
                response => {
                    console.log(response);
                    if (response) {
                        setInvoiceData(response);
                    } else {
                        setInvoiceData([]);
                    }
                }
            );
        }
    }, []);

    async function handleDelete(event : any) {
        console.log("delete");
        const res = await invoiceService.deleteInvoice(event.target.id, jwtResponse!.jwt)
        if(res === undefined){
            alert("Error deleting invoice");
            return;
        }
        setInvoiceData(invoiceData.filter((invoice) => invoice.id !== event.target.id));
        navigate("/calendar");

    }

    useEffect(() => {
        console.log("invoice" + invoiceData);
    }, [invoiceData]);
    // .filter((invoice) => invoice.id === "6946d7d1-e840-423d-ae6d-bb17b1c9dd99")
    return (
        <div className="receipt-container">
            {invoiceData.map((invoice) => { return (<div className="receipt-item">
                <div className="receipt-block">Date : {new Date(invoice.invoiceDate).toISOString().substring(0,10).split("-").reverse().join("-") + " " + new Date(invoice.invoiceDate).getHours() + ":" + (new Date(invoice.invoiceDate).getMinutes() == 0 ? "00" : new Date(invoice.invoiceDate).getMinutes())}</div>
                <div className="vertical-line"></div>
                <div className="receipt-block">Payment method: {invoice.paymentMethod.paymentMethodName}</div>
                <div className="vertical-line"></div>
                <div className="receipt-block">Master : {invoice.appUser.fullName}</div>
                <div className="vertical-line"></div>
                <div className="receipt-block">Client : {invoice.client.fullName}</div>
                <div className="vertical-line"></div>
                <div className="receipt-block">{ invoice.serviceNames.map((name,index) => 
                    {if(index === invoice.serviceNames.length - 1) return `Service ${index + 1}: ${name}`;
                        return `Service ${index + 1}: ${name}, `}) }
                </div>
                <div className="vertical-line"></div>
                <div className="receipt-block">Record : {invoice.record.title}</div>
                <div className="button-container">
                    <Link to={`/receiptView/${invoice.id}`}><button className="button-receipt" >View</button></Link>
                    <button className="button-receipt" onClick={event => handleDelete(event)} id={invoice.id}>Delete</button>
                </div>
            </div>) })}
        </div>
    );
}

export default Receipts;