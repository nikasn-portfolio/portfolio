export interface OrderAddress {
    firstName: string;
    lastName: string;
    email: string;
    phoneNumber: string | null;
    phoneCountry: string | null;
    addressLine1: string;
    addressLine2: string | null;
    locality: string;
    region: string | null;
    country: string;
    postalCode: string;
    companyName: string | null;
    companyLegalName: string | null;
    companyRegCode: string | null;
    companyVatNumber: string | null;
  }