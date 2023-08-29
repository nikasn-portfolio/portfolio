import { PaymentMethodOption } from "./PaymentMethodOption";

export interface PaymentMethodTransaction {
    method: string;
    methodDisplay: string;
    methodOptions: PaymentMethodOption;
    amount: number;
    currency: string;
}