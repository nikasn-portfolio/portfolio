package Helpers;

import Entities.ParkingZone;
import org.springframework.data.jpa.domain.Specification;

import java.util.UUID;

public class Specifications {
    public static Specification<ParkingZone> hasEqualIds(UUID parkingZoneId) {
        return (parkingZone, cq, cb) -> cb.equal(parkingZone.get("ParkingZoneId"), parkingZoneId);
    }
}
