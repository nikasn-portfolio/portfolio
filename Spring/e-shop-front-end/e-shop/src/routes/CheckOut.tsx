import React, { useContext, useEffect, useState } from "react";
import { PaymentConfig } from "../domain/montonio/ForGetBanks/PaymentConfig";
import { MontonioPaymentService } from "../services/MontonioService";
import jwtToken from 'jsonwebtoken';
import { TransactionPayload } from "../domain/montonio/forPostTransaction/TransactionPayload";
import { AppContext } from "./Root";
import { PaymentMethod } from "../domain/montonio/ForGetBanks/PaymentMethod";
import { TransactionTokenObject } from "../domain/montonio/TransactionTokenObject";
import { LineItem } from "../domain/montonio/forPostTransaction/LineItem";
import { t } from "i18next";


const CheckOut = () => {
    const { cartList, setCartList, isScreenSmall } = useContext(AppContext)
    const [bankCountry, setBankCountry] = useState("EE");
    const [billingErrors, setBillingErrors] = useState<{ [key: string]: boolean }>({});
    const initialTransactionPayload: TransactionPayload = {
        accessKey: "",
        merchantReference: "",
        returnUrl: "https://localhost:3000/payment/return",
        notificationUrl: "",
        currency: "EUR",
        grandTotal: cartList.reduce((total, item) => total + item.totalItemPrice, 0),
        locale: "et",
        billingAddress: {
            firstName: "",
            lastName: "",
            email: "",
            addressLine1: "",
            locality: "",
            region: "",
            country: "EE",
            postalCode: "",
        },
        shippingAddress: {
            firstName: "",
            lastName: "",
            email: "",
            addressLine1: "",
            locality: "",
            region: "",
            country: "EE",
            postalCode: "",
        },
        lineItems: cartList.map(cartItem => { const lineItem : LineItem = {
                    name : cartItem.item.name,
                    quantity : cartItem.quantity,
                    finalPrice : cartItem.totalItemPrice
                }
             return lineItem;
            }
    ),
        payment: {
            method: "paymentInitiation",
            methodDisplay: "Pay with your bank",
            methodOptions: {
                paymentDescription: "",
                preferredCountry: bankCountry,
                preferredProvider: "",
            },
            amount: cartList.reduce((total, item) => total + item.totalItemPrice, 0),
            currency: "EUR",
        },
    };

    const [montonioData, setMontonioData] = useState(null as PaymentConfig | undefined | null);
    const montonioPaymentService = new MontonioPaymentService();
    const [transactionPayload, setTransactionPayload] = useState(initialTransactionPayload);

    const token = jwtToken.sign(
        transactionPayload, 
        'IMxt0/r2Ib2iUnc96xQVrxXYSNTBec5NmanBS5Q7ybmd',
        { algorithm: 'HS256', expiresIn: '10m' }
    );

    useEffect(() => {
        console.log(token);
    },[transactionPayload])

    useEffect(() => {
        const fetchMontonioData = async () => {
            const token = await new MontonioPaymentService().getToken()
            if (token !== undefined) {
                const data = await montonioPaymentService.getPaymentMethods(token);
                if (data !== undefined) {
                    setMontonioData(data);
                }
            }
        };

        fetchMontonioData(); // Call the async function to fetch data
    }, []);

    useEffect(() => {
        console.log(transactionPayload)
    }, [transactionPayload])

    

    const handleBillingAndShippingAddress = (event : React.ChangeEvent<HTMLInputElement>) => {
        if(event.target.name === "email" && (event.target.value.length === 0 || !event.target.value.includes("@"))){
            setTransactionPayload(prevPayload => ({...prevPayload, 
                billingAddress: {
                ...prevPayload.billingAddress, [event.target.name] : event.target.value
                },
                shippingAddress:{
                    ...prevPayload.shippingAddress, [event.target.name] : event.target.value
                }
            }))
            return;
        }
        setBillingErrors(prevErrors => ({...prevErrors, [event.target.name] : false}));
        setTransactionPayload(prevPayload => ({...prevPayload, 
            billingAddress: {
            ...prevPayload.billingAddress, [event.target.name] : event.target.value
            },
            shippingAddress:{
                ...prevPayload.shippingAddress, [event.target.name] : event.target.value
            }
        }))
    }

    const handlePaymentOptionClick = (event: React.MouseEvent<HTMLDivElement>, paymentMethod : PaymentMethod) => {
        const bankHeaderText = document.querySelector(".bankHeaderError");
            if(bankHeaderText !== null){
                bankHeaderText.classList.remove("bankHeaderError");
            }
        
        const selectedButton = document.querySelector(".checkout-payment-grid-item-selected");

        if (selectedButton !== null) {
            selectedButton.classList.remove("checkout-payment-grid-item-selected");
        }

        const clickedButton = event.currentTarget;
        clickedButton.classList.add("checkout-payment-grid-item-selected");
        
        setTransactionPayload(prevPayload => ({
            ...prevPayload, 
            payment : {
                ...prevPayload.payment,
                methodOptions: {
                    ...prevPayload.payment.methodOptions,
                    preferredProvider: paymentMethod.code
                }
            }
        }))

    }

    const handleCountryBankChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
        setBankCountry(event.target.value);
    }

    if (montonioData === null) {
        // Data is still being fetched
        return <div>Loading...</div>;
    }

    const handlePaymentTransactionSubmit = async () => {
        if(transactionPayload.billingAddress.firstName.length === 0){
            setBillingErrors(prevErrors => ({
                ...prevErrors,
                firstName: true
            }));
        }
        if(transactionPayload.billingAddress.lastName.length === 0){
            setBillingErrors(prevErrors => ({
                ...prevErrors,
                lastName: true,
              }));
        }
        if(transactionPayload.billingAddress.email.length === 0 || !transactionPayload.billingAddress.email.includes("@")){
            setBillingErrors(prevErrors => ({
                ...prevErrors,
                email: true,
              })); 
        }
        if(transactionPayload.billingAddress.addressLine1.length === 0){
            setBillingErrors(prevErrors => ({
                ...prevErrors,
                addressLine1: true,
              })); 
        }
        if(transactionPayload.billingAddress.locality.length === 0){
            setBillingErrors(prevErrors => ({
                ...prevErrors,
                locality: true,
              })); 
        }
        if(transactionPayload.billingAddress.region.length === 0){
            setBillingErrors(prevErrors => ({
                ...prevErrors,
                region: true,
              })); 
        }
        if(transactionPayload.billingAddress.postalCode.length === 0){
            setBillingErrors(prevErrors => ({
                ...prevErrors,
                postalCode: true,
              })); 
        }
        if(transactionPayload.payment.methodOptions.preferredProvider === ""){
            const bankHeaderText = document.querySelector(".bankChoiceHeader");
            if(bankHeaderText !== null){
                bankHeaderText.classList.add("bankHeaderError");
            }
        }
        if(Object.keys(billingErrors).length > 0){
            return;
        }
        const token = await new MontonioPaymentService().getTransactionToken(transactionPayload);
        if(token !== undefined){
            const transactionToken : TransactionTokenObject = {data : token}
        }
        
    }

    

    

    return (
        <div className="checkout-container">
            <div className="checkout-billing-form">
                <div className="checkout-billing-form-header">Billing form:</div>
                <div className="input-group-checkout">
                    <input type="text" name="firstName" className={`${billingErrors["firstName"] === true ? "input-error" : ""}`} onChange={e => handleBillingAndShippingAddress(e)}/>
                    <label htmlFor="">Firstname</label>
                </div>
                <div className="input-group-checkout">
                    <input type="text" name="lastName" className={`${billingErrors["lastName"] === true ? "input-error" : ""}`} onChange={e => handleBillingAndShippingAddress(e)} />
                    <label htmlFor="">Lastname</label>
                </div>
                <div className="input-group-checkout">
                    <input type="text" name="email" className={`${billingErrors["email"] === true ? "input-error" : ""}`} onChange={e => handleBillingAndShippingAddress(e)} />
                    <label htmlFor="">Email</label>
                </div>
                <div className="input-group-checkout">
                    <input type="text" placeholder="Kai 1-12" name="addressLine1" className={`${billingErrors["addressLine1"] === true ? "input-error" : ""}`} onChange={e => handleBillingAndShippingAddress(e)}/>
                    <label htmlFor="">Address</label>
                </div>
                <div className="input-group-checkout">
                    <input type="text" placeholder="Tallinn" name="locality" className={`${billingErrors["locality"] === true ? "input-error" : ""}`} onChange={e => handleBillingAndShippingAddress(e)} />
                    <label htmlFor="">City</label>
                </div>
                <div className="input-group-checkout">
                    <input type="text" placeholder="Harjumaa" name="region" className={`${billingErrors["region"] === true ? "input-error" : ""}`} onChange={e => handleBillingAndShippingAddress(e)} />
                    <label htmlFor="">Region</label>
                </div>
                <div className="input-group-checkout">
                    <input type="text" placeholder="13515" name="postalCode" className={`${billingErrors["postalCode"] === true ? "input-error" : ""}`} onChange={e => handleBillingAndShippingAddress(e)} />
                    <label htmlFor="">Postal code</label>
                </div>
                <div className="checkout-button-submit" onClick={handlePaymentTransactionSubmit}>Submit</div>
            </div>
            <div className="checkout-payment-grid-container">
                <div>
                    <div className="bankChoiceHeader">Choose your bank</div>
                    <select onChange={e => handleCountryBankChange(e)}>
                        <option value="EE">Estonia</option>
                        <option value="LV">Latvia</option>
                        <option value="LT">Lithuania</option>
                        <option value="DE">Germany</option>
                        <option value="FI">Finland</option>
                        <option value="PL">Poland</option>
                    </select>
                </div>
                {montonioData!.paymentMethods.paymentInitiation.setup[bankCountry].paymentMethods.map(method =>
                    <div className="checkout-payment-grid-item" onClick={e => handlePaymentOptionClick(e, method)}>
                        <img src={method.logoUrl} alt="" className="checkout-payment-img" />
                    </div>
                )}

            </div>
        </div>

    );



}

export default CheckOut;