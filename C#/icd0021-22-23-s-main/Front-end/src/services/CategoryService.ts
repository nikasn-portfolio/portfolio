import { ICategory } from "../domain/ICategory";

import { BaseEntityService } from "./BaseEntityService";

export class CategoryService extends BaseEntityService<ICategory> {
    constructor(){
        super('v1/Categories');
    }
}