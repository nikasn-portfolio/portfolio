import React, { useEffect, useState } from "react";
import { PaymentConfig } from "../domain/montonio/PaymentConfig";
import { MontonioPaymentService } from "../services/MontonioService";
import jwtToken from 'jsonwebtoken';

const CheckOut = () => {

    const [montonioData, setMontonioData] = useState(null as PaymentConfig | undefined | null);
    const montonioPaymentService = new MontonioPaymentService();
    const [bankCountry, setBankCountry] = useState("EE");

    const getTokenForTransaction = () => {
        const payload = {
            accessKey: 'b278ef3d-dcca-417f-a0d3-02cd8f73acd2'
        };

        const authHeader = jwtToken.sign(
            payload,
            '7SBxaZN7mngCyCIpz7hgUHVP8amQ9+CfexO8Ck3LsTJL',
            { algorithm: 'HS256', expiresIn: '1h' }
        );

        return authHeader;
    }

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

    const handlePaymentOptionClick = (event: React.MouseEvent<HTMLDivElement>) => {
        const selectedButton = document.querySelector(".checkout-payment-grid-item-selected");

        if (selectedButton !== null) {
            selectedButton.classList.remove("checkout-payment-grid-item-selected");
        }

        const clickedButton = event.currentTarget;
        clickedButton.classList.add("checkout-payment-grid-item-selected");
    }

    const handleCountryBankChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
        setBankCountry(event.target.value);
    }

    if (montonioData === null) {
        // Data is still being fetched
        return <div>Loading...</div>;
    }

    return (
        <div className="checkout-container">
            <div className="checkout-billing-form">
                <div className="checkout-billing-form-header">Billing form:</div>
                <div className="input-group-checkout">
                    <input type="text" />
                    <label htmlFor="">Firstname</label>
                </div>
                <div className="input-group-checkout">
                    <input type="text" />
                    <label htmlFor="">Lastname</label>
                </div>
                <div className="input-group-checkout">
                    <input type="text" />
                    <label htmlFor="">Email</label>
                </div>
                <div className="input-group-checkout">
                    <input type="text" placeholder="Kai 1-12" />
                    <label htmlFor="">Address</label>
                </div>
                <div className="input-group-checkout">
                    <input type="text" placeholder="Tallinn" />
                    <label htmlFor="">City</label>
                </div>
                <div className="input-group-checkout">
                    <input type="text" placeholder="Harjumaa" />
                    <label htmlFor="">Region</label>
                </div>
                <div className="input-group-checkout">
                    <input type="text" placeholder="13515" />
                    <label htmlFor="">Postal code</label>
                </div>
                <div className="checkout-button-submit">Submit</div>
            </div>
            <div className="checkout-payment-grid-container">
                <div>
                    Choose your bank
                    <select name="" id="" onChange={e => handleCountryBankChange(e)}>
                        <option value="EE">Estonia</option>
                        <option value="LV">Latvia</option>
                        <option value="LT">Lithuania</option>
                        <option value="DE">Germany</option>
                        <option value="FI">Finland</option>
                        <option value="PL">Poland</option>
                    </select>
                </div>
                {montonioData!.paymentMethods.paymentInitiation.setup[bankCountry].paymentMethods.map(method =>
                    <div className="checkout-payment-grid-item" onClick={e => handlePaymentOptionClick(e)}>
                        <img src={method.logoUrl} alt="" className="checkout-payment-img" />
                    </div>
                )}

            </div>
        </div>

    );



}

export default CheckOut;