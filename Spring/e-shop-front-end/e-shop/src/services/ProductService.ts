import { IProduct } from "../domain/IProduct";
import { BaseService } from "./BaseService";

export class ProductService extends BaseService {
    constructor() {
        super("/products");
    }
    async getAllProductsBelongsToCategory(data : string): Promise<IProduct[] | undefined>{
        try {
            const response = await this.axios.get<IProduct[]>('/' + data);

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