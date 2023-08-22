package Controllers;

import Entities.ParkingZone;
import Repository.IParkingZoneRepository;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import java.util.List;

@RestController
@RequestMapping("/api/v1/parking-zone")
public class ParkingZoneController {
    private IParkingZoneRepository parkingZoneRepository;

    public ParkingZoneController(IParkingZoneRepository parkingZoneRepository){
        this.parkingZoneRepository = parkingZoneRepository;
    }

    @GetMapping()
    public List<ParkingZone> getListOfParkingZones(){
        return (List<ParkingZone>) parkingZoneRepository.findAll();
    }
}
