import { ICompany } from "../domain/ICompany";
import { BaseEntityService } from "./BaseEntityService";

export class CompanyService extends BaseEntityService<ICompany> {
    constructor(){
        super('v1/Companies');
    }

    async postCompany(data : ICompany,jwt: string): Promise<ICompany | undefined> {
        try {
            const response = await this.axios.post<ICompany>('', data,
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