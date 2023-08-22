import { IUserResource } from "../domain/IUserResource";
import { IJWTResponse } from "../dto/IJWTResponse";
import { ILoginData } from "../dto/ILoginData";
import { ILogoutData } from "../dto/ILogout";
import { IRegisterData } from "../dto/IRgeisterData";
import { BaseService } from "./BaseService";

export class IdentityService extends BaseService {
    constructor(){
        super('v1/identity/account/');
    }

    async register(data: IRegisterData): Promise<IJWTResponse | undefined> {
        try {
            const response = await this.axios.post<IJWTResponse>('register', data);

            console.log('register response', response);
            if (response.status === 200) {
                return response.data;
            }
            return undefined;
        } catch (e){
            console.log('error: ', (e as Error).message);
            return undefined;
        }
    }   

    async login(data: ILoginData): Promise<IJWTResponse | undefined> {
        try {
            const response = await this.axios.post<IJWTResponse>('login', data);

            console.log('login response', response);
            if (response.status === 200) {
                return response.data;
            }
            return undefined;
        } catch (e){
            console.log('error: ', e);
            //console.log('error: ', (e as Error).message);
            return undefined;
        }
    }
    
    async refreshToken(data: IJWTResponse): Promise<IJWTResponse | undefined> {
        try {
            const response = await this.axios.post<IJWTResponse>('refreshtoken', data);

            console.log('refreshtoken response', response);
            if (response.status === 200) {
                return response.data;
            }
            return undefined;
        } catch (e){
            console.log('error: ', (e as Error).message);
            return undefined;
        }
    }
    
    async logout(data: IJWTResponse): Promise<ILogoutData | undefined> {
        try {
            const response = await this.axios.post<ILogoutData>('logout', {data, headers: {
                'Authorization': 'Bearer ' + data.jwt
            }} );

            console.log('logout response', response);
            if (response.status === 200) {
                return response.data;
            }
            return undefined;
        } catch (e){
            console.log('error: ', (e as Error).message);
            return undefined;
        }
    } 

    async getWorkers(data: IJWTResponse): Promise<IUserResource[] | undefined> {
        try {
            const response = await this.axios.get<IUserResource[]>('getworkers', {data, headers: {
                'Authorization': 'Bearer ' + data.jwt
            }} );

            console.log('getworkers response', response);
            if (response.status === 200) {
                return response.data;
            }
            return undefined;
        } catch (e){
            console.log('error: ', (e as Error).message);
            return undefined;
        }
    }

    async getWorkersBySearch(data:string, jwtResponse: IJWTResponse): Promise<IUserResource[] | undefined> {
        try {
            const response = await this.axios.get<IUserResource[]>('getworkerbynameorphone/' + data, {data, headers: {
                'Authorization': 'Bearer ' + jwtResponse.jwt
            }} );

            console.log('getworkers response', response);
            if (response.status === 200) {
                return response.data;
            }
            return undefined;
        } catch (e){
            console.log('error: ', (e as Error).message);
            return undefined;
        }
    }


    async removeWorker(data:string, jwtResponse: IJWTResponse): Promise<IUserResource[] | undefined> {
        try {
            const response = await this.axios.delete<IUserResource[]>('removeworker/' + data, {data, headers: {
                'Authorization': 'Bearer ' + jwtResponse.jwt
            }} );

            console.log('getworkers response', response);
            if (response.status === 200) {
                return response.data;
            }
            return undefined;
        } catch (e){
            console.log('error: ', (e as Error).message);
            return undefined;
        }
    }

}