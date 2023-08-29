import { ICandy } from "../domain/ICandy";
import { BaseService } from "./BaseService";

export class CandyService extends BaseService {
    constructor() {
        super("/candy");
    }

    async getAllCandies(): Promise<ICandy[] | undefined> {
        try {
            const response = await this.axios.get<ICandy[]>('/getAllCandies');

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