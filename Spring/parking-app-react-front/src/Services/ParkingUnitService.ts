import { IParkingUnitDTO } from "../DTO/IParkingUnitDTO";
import { BaseService } from "./BaseService";

export class ParkingUnitService extends BaseService {
    constructor(){
        super('v1/parking-unit');
    }
    // http://localhost:8080/api/v1/parking-unit/parkingZone/2e60dac0-ee7f-44fa-8fab-0f316f641594/timeAmount/2
    async postParkingUnit(parkingZoneId : string, timeAmount : number, data : IParkingUnitDTO): Promise<IParkingUnitDTO[] | undefined> {
        try {
            const response = await this.axios.post<IParkingUnitDTO[]>("/parkingZone/" + parkingZoneId + "/timeAmount/" + timeAmount, data);

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