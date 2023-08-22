package Repository;

import Entities.ParkingUnit;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.CrudRepository;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;

import java.util.Optional;
import java.util.UUID;
@Repository
public interface IParkingUnitRepository extends BaseRepository<ParkingUnit, UUID> {

}
