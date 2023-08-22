package Helpers;

import Entities.ParkingUnit;
import Entities.ParkingZone;
import Public.DTO.ParkingUnitDTO;
import Repository.IParkingZoneRepository;
import com.fasterxml.jackson.databind.ObjectMapper;
import org.apache.tomcat.util.bcel.Const;

import java.sql.Timestamp;
import java.time.Instant;
import java.time.temporal.ChronoUnit;
import java.util.Optional;
import java.util.UUID;

import static Helpers.Specifications.hasEqualIds;


public class ParkingZoneValidator {
    // Current time with Estonian timezone +3
    public static final Instant currentTimeWithEstonianTimeZone = Instant.now().plus(3, ChronoUnit.HOURS);
    public static ParkingUnit ValidateParkingZone(UUID parkingZoneId, IParkingZoneRepository parkingZoneRepository, ParkingUnitDTO parkingUnitDTO, Integer timeSelected){
        Optional<ParkingZone> parkingZone = parkingZoneRepository.findOne(hasEqualIds(parkingZoneId));
        if(parkingZone.isPresent()){
            ParkingUnit result = new ParkingUnit();
            result.setLicensePlate(parkingUnitDTO.getName());
            result.setStartTime(Timestamp.from(currentTimeWithEstonianTimeZone));
            switch (timeSelected){
                case (1):
                    result.setEndTime(Timestamp.from(currentTimeWithEstonianTimeZone.plus(1, ChronoUnit.HOURS) ));
                    break;
                case (2):
                    result.setEndTime(Timestamp.from(currentTimeWithEstonianTimeZone.plus(2, ChronoUnit.HOURS) ));
                    break;
                case (3):
                    result.setEndTime(Timestamp.from(currentTimeWithEstonianTimeZone.plus(3, ChronoUnit.HOURS) ));
                    break;
                case (4):
                    result.setEndTime(Timestamp.from(currentTimeWithEstonianTimeZone.plus(4, ChronoUnit.HOURS) ));
                    break;
            }
            result.setParkingZone(parkingZone.get());
            return result;
        }else{
            throw new RuntimeException("ParkingZone does not exist.");
        }
    }
}
