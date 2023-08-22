import { IPartialInvoice } from "../domain/IPartialInvoice";
import { IInvoice } from "../dto/IInvoice";
import { IInvoice as IInvoiceOnView } from "../domain/IInvoice";

import { BaseEntityService } from "./BaseEntityService";

export class InvoiceService extends BaseEntityService<IPartialInvoice> {
    
    constructor(){
        super(`v1/Invoices`);
    }

    async postInvoice(data : IInvoice,jwt: string): Promise<IInvoice | undefined> {
        try {
            const response = await this.axios.post<IInvoice>('', data,
                {
                    headers: {
                        'Authorization': 'Bearer ' + jwt
                    }
                }
            );

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

    async deleteInvoice(id : string,jwt: string): Promise<void> {
        try {
            const response = await this.axios.delete(id,
                {
                    headers: {
                        'Authorization': 'Bearer ' + jwt
                    }
                }
            );

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

    async getInvoiceById(jwt: string, id : string): Promise<IInvoiceOnView | undefined> {
        try {
            const response = await this.axios.get<IInvoiceOnView>(id,
                {
                    headers: {
                        'Authorization': 'Bearer ' + jwt
                    }
                }
            );

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