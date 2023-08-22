import { ICategory } from "../domain/ICategory";
import { BaseService } from "./BaseService";

export class CategoryService extends BaseService {
    constructor() {
        super("/category");
    }

    async getAllCategories(): Promise<ICategory[] | undefined> {
        try {
            const response = await this.axios.get<ICategory[]>('/');

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