import { IService } from "../domain/IService";
import { BaseEntityService } from "./BaseEntityService";



export class ServiceService extends BaseEntityService<IService> {
    
    constructor(){
        super(`v1/Services`);
    }

    
}