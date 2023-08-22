import { ICategory } from "./ICategory";
import { IImage } from "./IImage";
import { IProductSize } from "./IProductSize";

export interface IProduct{
    id: string,
    name: string,
    description : string,
    category : ICategory,
    images : IImage[],
    productSizes : IProductSize[]
}