export interface PaymentIntent {
    uuid: string;
    paymentMethodType: string;
    paymentMethodMetadata: {
      preferredCountry: string;
      preferredProvider: string;
      paymentDescription: string;
    };
    amount: string;
    currency: string;
    status: string;
    serviceFee: string;
    serviceFeeCurrency: string;
    createdAt: string;
  }