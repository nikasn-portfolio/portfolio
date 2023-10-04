import { ICandy } from "./ICandy";
import { IProduct } from "./IProduct";
import { ISize } from "./ISize";

export interface IOrderRow {
    id: string;
    product: IProduct;
    size: ISize;
    candy: ICandy;
    quantity: number;
}