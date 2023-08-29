import { PaymentMethod } from "./PaymentMethod";

export interface PaymentProcessor {
    processor: string;
    setup: Record<string, {
        supportedCurrencies: string[];
        paymentMethods: PaymentMethod[];
    }>;
}