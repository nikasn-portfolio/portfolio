import { ICandy } from "./ICandy";

export interface IItem{
    id : string,
    name : string,
    sizeId : string,
    sizeName : string,
    candies : ICandy[],
    image : string,
    price : number
}