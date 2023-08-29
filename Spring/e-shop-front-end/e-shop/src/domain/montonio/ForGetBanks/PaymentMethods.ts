import { PaymentProcessor } from "./PaymentProcessor";

export interface PaymentMethods {
    paymentInitiation: PaymentProcessor;
    cardPayments: {
        processor: string;
    };
    blik: {
        processor: string;
    };
}