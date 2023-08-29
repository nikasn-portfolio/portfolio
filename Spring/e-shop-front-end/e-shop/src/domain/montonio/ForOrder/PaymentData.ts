import { OrderAddress } from "./OrderAddress";
import { PaymentIntent } from "./PaymentIntent";
import { LineItem } from "../forPostTransaction/LineItem";

export interface PaymentData {
    uuid: string;
    paymentStatus: string;
    locale: string;
    merchantReference: string;
    merchantReferenceDisplay: string;
    merchantReturnUrl: string;
    merchantNotificationUrl: string;
    grandTotal: string;
    currency: string;
    paymentMethodType: string;
    paymentIntents: PaymentIntent[];
    refunds: any[]; // Assuming it's an array of refund data
    availableForRefund: number;
    lineItems: LineItem[];
    billingAddress: OrderAddress;
    shippingAddress: OrderAddress;
    expiresAt: string | null;
    createdAt: string;
    storeName: string;
    businessName: string;
    paymentUrl: string;
  }
  
  
  
  
  
  