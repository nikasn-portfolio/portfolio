import { PasswordUpdateDTO } from "../dto/PasswordUpdateDTO";
import { PhoneNumberUpdateDTO } from "../dto/PhoneNumberUpdateDTO";
import { BaseService } from "./BaseService";

export class UserService extends BaseService {
    constructor() {
        super("/profile");
    }
    async updateUserPassword(data : PasswordUpdateDTO): Promise<PasswordUpdateDTO | undefined> {
        try {
            const response = await this.axios.post<PasswordUpdateDTO>('/updatePassword', data);

            console.log('response', response);
            if (response.status === 200) {
                return response.data;
            }
            return undefined;
        } catch (e) {
            console.log('error: ', (e as Error).message);
            return undefined;
        }
    }
    async updateUserPhone(data : PhoneNumberUpdateDTO): Promise<PhoneNumberUpdateDTO | undefined> {
        try {
            const response = await this.axios.post<PhoneNumberUpdateDTO>('/updatePhone', data);

            console.log('response', response);
            if (response.status === 200) {
                return response.data;
            }
            return undefined;
        } catch (e) {
            console.log('error: ', (e as Error).message);
            return undefined;
        }
    }
}