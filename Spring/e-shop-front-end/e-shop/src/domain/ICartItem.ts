import { IItem } from "./IItem";
import { IItemCard } from "./IItemCard";
import { IProduct } from "./IProduct";


export interface ICartItem{
    item : IItem,
    quantity: number,
    totalItemPrice: number
}