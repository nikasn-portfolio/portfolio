import { Address } from "../domain/montonio/forPostTransaction/Address";
import { IApplicationUser } from "./IApplicationUser";
import { IOrderRow } from "./IOrderRow";
export interface IOrder {
    id: string;
    name: string;
    status: string;
    bankName: string;
    price: number;
    createdAt: string;
    isDone: boolean;
    paymentUrl: string;
    billingAddress: Address;
    applicationUser: IApplicationUser;
    orderRows: IOrderRow[];
}