import { IClient } from "../domain/IClient";
import { IClientPost } from "../dto/IClientPost";
import { BaseEntityService } from "./BaseEntityService";

export class ClientService extends BaseEntityService<IClient> {
    constructor(){
        super('v1/Clients/');
    }

    async postClient(data : IClientPost,jwt: string): Promise<IClientPost | undefined> {
        try {
            const response = await this.axios.post<IClientPost>('', data,
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