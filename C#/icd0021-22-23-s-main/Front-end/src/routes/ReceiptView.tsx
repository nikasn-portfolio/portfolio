import moment, { utc } from "moment";
import React, { useContext, useEffect, useRef, useState } from "react";
import { Link, useParams } from "react-router-dom";
import { useReactToPrint } from "react-to-print";
import { IInvoice } from "../domain/IInvoice";
import { InvoiceService } from "../services/InvoiceService";
import { JwtContext } from "./Root";

export const ReceiptView = () => {
    const [invoiceData, setInvoiceData] = useState(null as IInvoice | null);
    const { jwtResponse, userRole, userId } = useContext(JwtContext);
    const { id } = useParams();
    const componentRef = useRef(null);
    const handlePrint = useReactToPrint({ content: () => componentRef.current })
    useEffect(() => {
        if (jwtResponse) {
            new InvoiceService().getInvoiceById(jwtResponse.jwt, id as string).then(
                response => {
                    console.log(response);
                    if (response) {
                        setInvoiceData(response);
                    } else {
                        setInvoiceData(null);
                    }
                }
            );
        }
    }, []);
    return invoiceData !== null && invoiceData.invoiceRows !== undefined ?
        (
            console.log(invoiceData.record),console.log(invoiceData),
            <>
                <h1>{new Date(moment(invoiceData.paymentDate).utc(true).format()).toISOString()}</h1>

                <div className="receipt-body" ref={componentRef}>
                    <div className="receipt-body-data">
                        <div className="main-header-receipt">Receipt</div>
                    </div>
                    <hr />
                    <div className="receipt-body-data">
                        <div className="header-receipt">Record title: </div>
                        <div className="value-receipt">{invoiceData.record.title}</div>
                    </div>
                    <div className="receipt-body-data">
                        <div className="header-receipt">Record start time: </div>
                        <div className="value-receipt">{new Date(invoiceData.record.startTime).toISOString().substring(0, 10).split("-").reverse().join("-") + " " 
                        + new Date(invoiceData.record.startTime).getHours() + ":" + (new Date(invoiceData.record.startTime).getMinutes() == 0 ? "00" : new Date(invoiceData.record.startTime).getMinutes())}</div>
                    </div>
                    <div className="receipt-body-data">
                        <div className="header-receipt">Record end time: </div>
                        <div className="value-receipt">{new Date(invoiceData.record.endTime).toISOString().substring(0, 10).split("-").reverse().join("-") + " " + new Date(invoiceData.record.endTime).getHours() + ":" + (new Date(invoiceData.record.endTime).getMinutes() == 0 ? "00" : new Date(invoiceData.record.endTime).getMinutes())}</div>
                    </div>
                    <div className="receipt-body-data">
                        <div className="header-receipt">Master: </div>
                        <div className="value-receipt">{invoiceData.appUser.fullName}</div>
                    </div>
                    <div className="receipt-body-data">
                        <div className="header-receipt">Client: </div>
                        <div className="value-receipt">{invoiceData.client.fullName}</div>
                    </div>
                    {invoiceData.invoiceRows.map((row, index) => {
                        return (
                            <div className="receipt-body-data">
                                <div className="header-receipt">Service {index + 1}: </div>
                                <div className="value-receipt">{row.service.serviceName}</div>
                            </div>
                        )
                    })}
                    <div className="receipt-body-data-closure-line" />
                    <hr className="receipt-body-data-separator" />
                    <div className="receipt-body-data">
                        <div className="header-receipt">Invoice establishing date:</div>
                        <div className="value-receipt">{new Date(invoiceData.invoiceDate).toISOString().substring(0, 10).split("-").reverse().join("-") + " " + new Date(invoiceData.invoiceDate).getHours() + ":" + (new Date(invoiceData.invoiceDate).getMinutes() == 0 ? "00" : new Date(invoiceData.invoiceDate).getMinutes())}</div>
                    </div>
                    <div className="receipt-body-data">
                        <div className="header-receipt">Invoice payment date: </div>
                        <div className="value-receipt">{new Date(invoiceData.paymentDate).toISOString().substring(0, 10).split("-").reverse().join("-") + " " + new Date(invoiceData.paymentDate).getHours() + ":" + (new Date(invoiceData.paymentDate).getMinutes() == 0 ? "00" : new Date(invoiceData.paymentDate).getMinutes())}</div>
                    </div>
                    <div className="receipt-body-data">
                        <div className="header-receipt">Invoice payment date: </div>
                        <div className="value-receipt">{invoiceData.paymentMethod.paymentMethodName}</div>
                    </div>
                    <div className="receipt-body-data-closure-line" />
                    <hr className="receipt-body-data-separator" />
                    {invoiceData.invoiceRows.length > 0 ? invoiceData.invoiceRows.map((row, index) => {
                        return (
                            <>
                                <div className="receipt-body-data">
                                    <div className="header-receipt">Service {index + 1}: </div>
                                    <div className="value-receipt">{row.service.serviceName}</div>
                                </div>
                                <div className="receipt-body-data">
                                    <div className="header-receipt">Quantity: </div>
                                    <div className="value-receipt">{row.quantity}</div>
                                </div>
                                {row.priceOverride !== null && row.priceOverride !== undefined ? (
                                    <>
                                        <div className="receipt-body-data">
                                            <div className="header-receipt">Service changed price: </div>
                                            <div className="value-receipt">{row.priceOverride / row.quantity}</div>
                                        </div>
                                    </>
                                ) :
                                    <>
                                        <div className="receipt-body-data">
                                            <div className="header-receipt">Service price: </div>
                                            <div className="value-receipt">{row.total / row.quantity}</div>
                                        </div>
                                    </>
                                }
                                {index !== invoiceData.invoiceRows.length - 1 ? (
                                    <>
                                        <div className="receipt-body-data-closure-line" />
                                        <hr className="receipt-body-data-separator" />
                                    </>
                                ) : null}

                            </>
                        )
                    }) : null}
                    {invoiceData.invoiceRows.length > 0 ? (
                        <>
                            <div className="receipt-body-data-closure-line" />
                            <div className="receipt-body-total-price-rows">Total price:
                                {invoiceData.invoiceRows.reduce((totalPrice, row) => { if (row.priceOverride !== null && row.priceOverride !== undefined) { return totalPrice + row.priceOverride } return totalPrice + row.total }, 0)}
                            </div>
                        </>
                    ) : null}
                    <hr className="receipt-body-data-separator" />
                    <div className="receipt-body-data">
                        <div className="header-receipt">Company: </div>
                        <div className="value-receipt">{invoiceData.invoiceFooter.companyName}</div>
                    </div>
                    <div className="receipt-body-data">
                        <div className="header-receipt">Address: </div>
                        <div className="value-receipt">{invoiceData.invoiceFooter.address}</div>
                    </div>
                    <div className="receipt-body-data">
                        <div className="header-receipt">Email: </div>
                        <div className="value-receipt">{invoiceData.invoiceFooter.email}</div>
                    </div>
                    <div className="receipt-body-data">
                        <div className="header-receipt">Iban: </div>
                        <div className="value-receipt">{invoiceData.invoiceFooter.iban}</div>
                    </div>
                    <div className="receipt-body-data">
                        <div className="header-receipt">Phone number: </div>
                        <div className="value-receipt">{invoiceData.invoiceFooter.phone}</div>
                    </div>
                    <div className="receipt-body-data-closure-line" />
                </div>

                <button className="button-receipt" style={{ marginTop: 15, marginBottom: 15, color: "green" }} onClick={handlePrint}>Print</button>

                <Link to="/receipts"><button className="button-receipt" style={{ marginTop: 15 }}>Back</button></Link>
            </>

        ) : <div>Loading...</div>;
}