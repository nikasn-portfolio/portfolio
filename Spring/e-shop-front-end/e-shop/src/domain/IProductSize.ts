import { ISize } from "./ISize";

export interface IProductSize{
    productId: string;
    size: ISize;
    quantity: number;
    emptyPrice: number;
    fullPrice: number
}