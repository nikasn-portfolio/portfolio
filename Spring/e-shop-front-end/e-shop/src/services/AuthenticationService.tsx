import { JwtResponse } from "../domain/JwtResponse";
import { LoginDTO } from "../dto/LoginDTO";
import { RegisterDTO } from "../dto/RegisterDTO";
import { BaseService } from "./BaseService";

export class AuthenticationService extends BaseService {
    constructor() {
        super("/auth");
    }
    async login(data : LoginDTO): Promise<JwtResponse | undefined> {
        try {
            const response = await this.axios.post<JwtResponse>('/login', data);

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

    async register(data : RegisterDTO): Promise<JwtResponse | undefined> {
        try {
            const response = await this.axios.post<JwtResponse>('/register', data);

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