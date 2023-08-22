package Entities;

import jakarta.persistence.*;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.sql.Timestamp;
import java.util.UUID;

@Entity
@Table(name = "ParkingUnit")
@Data
@AllArgsConstructor
@NoArgsConstructor
public class ParkingUnit {
    @Id
    @GeneratedValue(strategy = GenerationType.UUID)
    private UUID ParkingUnitId;

    private String LicensePlate;

    private Timestamp StartTime;

    private Timestamp EndTime;

    @ManyToOne
    @JoinColumn(name = "ParkingZoneId")
    private ParkingZone ParkingZone;
}
