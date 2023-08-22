package Repository;

import Entities.ParkingZone;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;

import java.util.Optional;
import java.util.UUID;
@Repository
public interface IParkingZoneRepository extends BaseRepository<ParkingZone, UUID>, SearchRepository<ParkingZone, UUID> {
    /*@Query("select t from ParkingZone t where t.ParkingZoneId = :id")
    Optional<ParkingZone> findOne(@Param("id")UUID uuid);*/
}
