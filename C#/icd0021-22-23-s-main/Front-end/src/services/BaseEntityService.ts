import { IBaseEntity } from "../domain/IBaseEntity";
import { BaseService } from "./BaseService";

export abstract class BaseEntityService<TEntity extends IBaseEntity> extends BaseService {
    constructor(baseUrl: string) {
        super(baseUrl);
    }

    async getAll(jwt: string): Promise<TEntity[] | undefined> {
        try {
            const response = await this.axios.get<TEntity[]>('',
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