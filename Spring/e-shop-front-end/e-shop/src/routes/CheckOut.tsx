import React, { useContext, useEffect, useState } from "react";
import { PaymentConfig } from "../domain/montonio/ForGetBanks/PaymentConfig";
import { MontonioPaymentService } from "../services/MontonioService";
import jwtToken from 'jsonwebtoken';
import { TransactionPayload } from "../domain/montonio/forPostTransaction/TransactionPayload";
import { AppContext } from "./Root";
import { PaymentMethod } from "../domain/montonio/ForGetBanks/PaymentMethod";
import { TransactionTokenObject } from "../domain/montonio/TransactionTokenObject";
import { LineItem } from "../domain/montonio/forPostTransaction/LineItem";
import { t, use } from "i18next";
import { OrderService } from "../services/OrderService";
import { useNavigate } from "react-router-dom";


const CheckOut = () => {
    const { cartList, setCartList, isScreenSmall, userId } = useContext(AppContext)
    const [bankCountry, setBankCountry] = useState("EE");
    const [bankName, setBankName] = useState(null as string | null);
    const navigate = useNavigate();
    const deliveryOptions: Record<string, string> = {
        Omniva: '10'
      };
    const [billingErrors, setBillingErrors] = useState<{ [key: string]: boolean }>({});
    const initialTransactionPayload: TransactionPayload = {
        accessKey: "",
        merchantReference: "18d8c770-06d8-4f4f-b9a5-18eb7808dc26",
        returnUrl: "https://localhost:3000/payment/return",
        notificationUrl: "https://mystore.com/payment/notify",
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
        lineItems: cartList.map(cartItem => {
            const lineItem: LineItem = {
                name: cartItem.item.name,
                quantity: cartItem.quantity,
                finalPrice: cartItem.totalItemPrice
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
        if(userId == null){
            navigate("/login")
        }
    },[userId])

    useEffect(() => {
        console.log(token);
    }, [transactionPayload])

    useEffect(() => {
        var checkbox = document.getElementById("Omniva") as HTMLInputElement | null;
        if(checkbox != null && checkbox.checked){
            setTransactionPayload(prevPayload => ({
                ...prevPayload,
                grandTotal: cartList.reduce((total, item) => total + item.totalItemPrice, 0) + parseInt(deliveryOptions.Omniva)
                }));
        }
    }, [transactionPayload.grandTotal])

    useEffect(() => {
        var checkbox = document.getElementById("Omniva") as HTMLInputElement | null;
        if(transactionPayload.billingAddress.locality == "Tallinn"){
            setTransactionPayload(prevPayload => ({
                ...prevPayload,
                grandTotal: cartList.reduce((total, item) => total + item.totalItemPrice, 0)
            }));
        }
    },[transactionPayload.billingAddress.locality])

    useEffect(() => {
        setTransactionPayload(prevPayload => ({
            ...prevPayload,
            grandTotal: cartList.reduce((total, item) => total + item.totalItemPrice, 0),
            lineItems: cartList.map(cartItem => {
                const lineItem: LineItem = {
                    name: cartItem.item.name,
                    quantity: cartItem.quantity,
                    finalPrice: cartItem.totalItemPrice
                }
                return lineItem;
            }
            ),
        }))
    }, [cartList])

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



    const handleBillingAndShippingAddress = (event: React.ChangeEvent<HTMLInputElement>) => {
        if (event.target.name === "email" && (event.target.value.length === 0 || !event.target.value.includes("@"))) {
            setTransactionPayload(prevPayload => ({
                ...prevPayload,
                billingAddress: {
                    ...prevPayload.billingAddress, [event.target.name]: event.target.value
                },
                shippingAddress: {
                    ...prevPayload.shippingAddress, [event.target.name]: event.target.value
                }
            }))
            return;
        }
        setBillingErrors(prevErrors => ({ ...prevErrors, [event.target.name]: false }));
        setTransactionPayload(prevPayload => ({
            ...prevPayload,
            billingAddress: {
                ...prevPayload.billingAddress, [event.target.name]: event.target.value
            },
            shippingAddress: {
                ...prevPayload.shippingAddress, [event.target.name]: event.target.value
            }
        }))
    }

    const handlePaymentOptionClick = (event: React.MouseEvent<HTMLDivElement>, paymentMethod: PaymentMethod) => {
        const bankHeaderText = document.querySelector(".bankHeaderError");
        if (bankHeaderText !== null) {
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
            payment: {
                ...prevPayload.payment,
                methodOptions: {
                    ...prevPayload.payment.methodOptions,
                    preferredProvider: paymentMethod.code
                }
            }
        }))

        setBankName(paymentMethod.name);

    }

    const handleCountryBankChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
        setBankCountry(event.target.value);
    }

    if (montonioData === null) {
        // Data is still being fetched
        return <div>Loading...</div>;
    }

    const handlePaymentTransactionSubmit = async () => {
        let errorsCounter = 0;
        if (transactionPayload.billingAddress.firstName.length === 0) {
            errorsCounter++;
            setBillingErrors(prevErrors => ({
                ...prevErrors,
                firstName: true
            }));
        }
        if (transactionPayload.billingAddress.lastName.length === 0) {
            errorsCounter++;
            setBillingErrors(prevErrors => ({
                ...prevErrors,
                lastName: true,
            }));
        }
        if (transactionPayload.billingAddress.email.length === 0 || !transactionPayload.billingAddress.email.includes("@")) {
            errorsCounter++;
            setBillingErrors(prevErrors => ({
                ...prevErrors,
                email: true,
            }));
        }
        if (transactionPayload.billingAddress.addressLine1.length === 0) {
            errorsCounter++;
            setBillingErrors(prevErrors => ({
                ...prevErrors,
                addressLine1: true,
            }));
        }
        if (transactionPayload.billingAddress.locality.length === 0) {
            errorsCounter++;
            setBillingErrors(prevErrors => ({
                ...prevErrors,
                locality: true,
            }));
        }
        if (transactionPayload.billingAddress.region.length === 0) {
            errorsCounter++;
            setBillingErrors(prevErrors => ({
                ...prevErrors,
                region: true,
            }));
        }
        if (transactionPayload.billingAddress.postalCode.length === 0) {
            errorsCounter++;
            setBillingErrors(prevErrors => ({
                ...prevErrors,
                postalCode: true,
            }));
        }
        if (transactionPayload.payment.methodOptions.preferredProvider === "") {
            errorsCounter++;
            const bankHeaderText = document.querySelector(".bankChoiceHeader");
            if (bankHeaderText !== null) {
                bankHeaderText.classList.add("bankHeaderError");
            }
        }
        if (errorsCounter > 0) {
            console.log(errorsCounter);
            console.log("return")
            return;
        }
        const token = await new MontonioPaymentService().getTransactionToken(transactionPayload);
        console.log(token);
        
        if (token !== undefined) {
            const transactionToken: TransactionTokenObject = { data: token };
            const orderData = await new MontonioPaymentService().getTransactionData(transactionToken);
            console.log(orderData);
            const updatedPayload = { ...orderData, bankName: bankName, orderRow: cartList, customerId : userId };
            console.log(updatedPayload);
            if(orderData !== undefined){
                const order = await new OrderService().saveOrder(updatedPayload);
            }

            // get order and save it somehow
            //const orderData = 
        }


    }

    const handleDeliveryOptionChange = (event : React.ChangeEvent<HTMLInputElement>) =>{
        const option = event.currentTarget.id;
        if(option == "Omniva" && event.currentTarget.checked){
            setTransactionPayload(prevPayload => ({
                ...prevPayload,
                grandTotal: cartList.reduce((total, item) => total + item.totalItemPrice, 0) + parseInt(deliveryOptions.Omniva),
                lineItems: cartList.map(cartItem => {
                    const lineItem: LineItem = {
                        name: cartItem.item.name,
                        quantity: cartItem.quantity,
                        finalPrice: cartItem.totalItemPrice
                    }
                    return lineItem;
                }
                ),
            }))
        }
        if(option == "Omniva" && event.currentTarget.checked == false){
            setTransactionPayload(prevPayload => ({
                ...prevPayload,
                grandTotal: prevPayload.grandTotal - parseInt(deliveryOptions.Omniva),
                lineItems: cartList.map(cartItem => {
                    const lineItem: LineItem = {
                        name: cartItem.item.name,
                        quantity: cartItem.quantity,
                        finalPrice: cartItem.totalItemPrice
                    }
                    return lineItem;
                }
                ),
            }))
        }
    }





    return (
        <div className="checkout-container">
            <div className="checkout-billing-form">
                <div className="checkout-billing-form-header">Billing form:</div>
                <div className="input-group-checkout">
                    <input type="text" name="firstName" className={`${billingErrors["firstName"] === true ? "input-error" : ""}`} onChange={e => handleBillingAndShippingAddress(e)} />
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
                    <input type="text" placeholder="Kai 1-12" name="addressLine1" className={`${billingErrors["addressLine1"] === true ? "input-error" : ""}`} onChange={e => handleBillingAndShippingAddress(e)} />
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
                {cartList.reduce((total, item) => total + item.totalItemPrice, 0) >= 50 && transactionPayload.billingAddress.locality === "Tallinn" &&
            
                (<div className="check-out-delivery-info">
                    <div className="delivery-info-header">Delivery info:</div>
                    <div className="delivery-info-text">Free delivery in Tallinn for orders over 50€ ! We will contact you as soon as product will be finished.</div>
                </div>)
                }
                {transactionPayload.billingAddress.locality != "Tallinn" &&
            
                (<div className="check-out-delivery-info">
                    <div className="delivery-info-header">Delivery info:</div>
                    <div className="delivery-info-text">Orders below than 50€ have no free shipping by us.</div>
                    <label htmlFor="">
                        <input type="checkbox" className="checkbox-checkout" onChange={e => handleDeliveryOptionChange(e)} id="Omniva"  />
                        Omniva: {deliveryOptions.Omniva}€
                    </label>
                    
                </div>)
                }
            </div>
            <div className="checkout-payment-grid-container">
                <div className="bankingHeader">
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
                <div className="checkoutButtonSubmiContainer">
                    <div className="checkout-button-submit" onClick={handlePaymentTransactionSubmit}>Submit</div>
                </div>
                


            </div>
            <div className="itemDescriptionContainer">
                
                    {cartList.map(cartItem => (
                        <>
                        <div className="itemDescriptionItem">
                        <div className="itemDescriptionImg"><img className="checkout-img" src={cartItem.item.image} alt="" /></div>
                        <div className="itemDescriptionHeader">Item : {cartItem.item.name}</div>
                        <div className="itemDescriptionHeader">Quantity : {cartItem.quantity}</div>
                        <div className="itemDescriptionHeader">Price : {cartItem.totalItemPrice}</div>
                        <div className="itemCandiesContainer">
                            <div className="itemDescriptionHeader">Candies :</div>
                            {cartItem.item.candies.length === 0 
                            ? 
                            <div className="candyItem">No candies</div> 
                            : 
                            cartItem.item.candies.map(candy =>(
                                <div className="candyItem">
                                    <div className="candyItemName">{candy.name}</div>
                                    <div className="candyItemPrice">{candy.quantity}g</div>
                                </div>
                            ))}
                            
                        </div>
                        

                        </div>
                        </>
                    ))}
                    <div className="totalPriceContainerCheckOut">
                            <span className="totalPriceTitle">Total price : </span>
                            <span className="totalPriceValue">{transactionPayload.grandTotal}€</span>
                    </div>
                    
            </div>
        </div>

    );



}

export default CheckOut;