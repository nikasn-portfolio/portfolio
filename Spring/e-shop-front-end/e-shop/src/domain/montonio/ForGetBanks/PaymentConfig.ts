import { PaymentMethods } from "./PaymentMethods";

export interface PaymentConfig {
    uuid: string;
    name: string;
    paymentMethods: PaymentMethods;
}