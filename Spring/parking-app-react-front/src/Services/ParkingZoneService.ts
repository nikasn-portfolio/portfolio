import { IParkingZone } from "../Domain/IParkingZone";
import { BaseService } from "./BaseService";

export class ParkingZoneService extends BaseService {
    constructor(){
        super('v1/parking-zone');
    }

    async getParkingZones(): Promise<IParkingZone[] | undefined> {
        try {
            const response = await this.axios.get<IParkingZone[]>("");

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