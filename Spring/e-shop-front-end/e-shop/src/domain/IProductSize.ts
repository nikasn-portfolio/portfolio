import { ISize } from "./ISize";

export interface IProductSize{
    productId: string;
    size: ISize;
    quantity: number;
    price: number;
    emptyPrice: number;
    fullPrice: number
}