import { IPaymentMethod } from "../domain/IPaymentMethod";
import { BaseEntityService } from "./BaseEntityService";

export class PaymentMethodService extends BaseEntityService<IPaymentMethod>{
        
        constructor(){
            super(`v1/PaymentMethods`);
        }
        
}