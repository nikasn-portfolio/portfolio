export interface PaymentMethod {
    name: string;
    logoUrl: string;
    supportedCurrencies: string[];
    uiPosition: number;
    code: string;
}