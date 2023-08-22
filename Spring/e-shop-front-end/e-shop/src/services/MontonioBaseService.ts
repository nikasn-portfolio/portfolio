import Axios, { AxiosInstance } from 'axios';
export abstract class MontonioBaseService {
    private static hostBaseURL = "https://sandbox-stargate.montonio.com/api";

    protected axios: AxiosInstance;

    constructor(baseUrl: string){

        this.axios = Axios.create(
            {
                baseURL: MontonioBaseService.hostBaseURL + baseUrl,
                headers: {
                common: {
                    'Content-Type': 'application/json'
                    }
                }
            }
        )    

    }
}