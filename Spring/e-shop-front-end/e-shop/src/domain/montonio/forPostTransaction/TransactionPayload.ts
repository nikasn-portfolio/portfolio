import { ICartItem } from "../../ICartItem";
import { Address } from "./Address";
import { LineItem } from "./LineItem";
import { PaymentMethodTransaction } from "./PaymentMethodTransaction";

export interface TransactionPayload {
    accessKey: string;
    merchantReference: string;
    returnUrl: string;
    notificationUrl: string;
    currency: string;
    grandTotal: number;
    locale: string;
    billingAddress: Address;
    shippingAddress: Address;
    lineItems: LineItem[];
    payment: PaymentMethodTransaction;
}
