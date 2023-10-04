import { IOrder } from "../domain/IOrder";
import { PaymentData } from "../domain/montonio/ForOrder/PaymentData";
import { TransactionPayload } from "../domain/montonio/forPostTransaction/TransactionPayload";
import { BaseService } from "./BaseService";

export class OrderService extends BaseService {
    constructor() {
        super("/orders");
    }

    async saveOrder(order : any): Promise<string| undefined> {
        try {
            const response = await this.axios.post<string>('/save', order);

            console.log('response', response);
            if (response.status === 200) {
                return response.data;
            }
            return undefined;
        } catch (e) {
            console.log('error: ', (e as Error).message);
            return undefined;
        }
    }

    async getOrderById(id : string): Promise<IOrder| undefined> {
        try {
            const response = await this.axios.get<IOrder>('/getById/' + id);

            console.log('response', response);
            if (response.status === 200) {
                return response.data;
            }
            return undefined;
        } catch (e) {
            console.log('error: ', (e as Error).message);
            return undefined;
        }
    }

}