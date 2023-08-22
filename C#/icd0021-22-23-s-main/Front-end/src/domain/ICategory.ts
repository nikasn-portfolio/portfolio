import { IService } from "./IService";

export interface ICategory{
        categoryName: string,
        categoryImageUrl: string,
        services: IService[],
        id: string
}