package Controllers;


import Entities.ParkingUnit;
import Helpers.ParkingZoneValidator;
import Public.DTO.ParkingUnitDTO;
import Repository.IParkingUnitRepository;
import Repository.IParkingZoneRepository;
import jdk.jshell.spi.ExecutionControl;
import org.springframework.web.bind.annotation.*;

import java.util.List;
import java.util.UUID;

@RestController
@RequestMapping("/api/v1/parking-unit")
public class ParkingUnitController {

    private IParkingUnitRepository parkingUnitRepository;
    private IParkingZoneRepository parkingZoneRepository;

    public ParkingUnitController(IParkingUnitRepository parkingUnitRepository, IParkingZoneRepository parkingZoneRepository) {
        this.parkingUnitRepository = parkingUnitRepository;
        this.parkingZoneRepository = parkingZoneRepository;
    }


    @PostMapping("/parkingZone/{parkingZoneId}/timeAmount/{time}")
    public ParkingUnit postParkingUnit(@PathVariable("parkingZoneId") UUID parkingZoneId,@PathVariable("time") Integer timeAmount ,@RequestBody ParkingUnitDTO parkingUnitDTO){
        ParkingUnit parkingUnit = ParkingZoneValidator.ValidateParkingZone(parkingZoneId, parkingZoneRepository, parkingUnitDTO, timeAmount);
        return parkingUnitRepository.save(parkingUnit);
    }

    // other controller methods
}
