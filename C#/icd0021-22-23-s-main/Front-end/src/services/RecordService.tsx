import { useContext } from "react";
import { IRecord } from "../domain/IRecord";
import { JwtContext } from "../routes/Root";
import { BaseEntityService } from "./BaseEntityService";



export class RecordService extends BaseEntityService<IRecord> {
    
    constructor(){
        super(`v1/Records`);
    }

    async postOrder(data : IRecord,jwt: string): Promise<IRecord | undefined> {
        try {
            const response = await this.axios.post<IRecord>('', data,
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

    async getRecordById(jwt: string, id : string): Promise<IRecord | undefined> {
        try {
            const response = await this.axios.get<IRecord>(id,
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

    async deleteRecordById(jwt: string, id : string): Promise<IRecord | undefined> {
        try {
            const response = await this.axios.delete<IRecord>(id,
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

    async updateRecord(jwt: string, id : string, data : IRecord): Promise<IRecord | undefined> {
        try {
            const response = await this.axios.put<IRecord>(id, data,
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