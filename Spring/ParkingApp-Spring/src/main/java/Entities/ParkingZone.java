package Entities;

import jakarta.persistence.*;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.util.UUID;

@Entity
@Table(name = "ParkingZone")
@Data
@AllArgsConstructor
@NoArgsConstructor
public class ParkingZone {
    @Id
    @GeneratedValue(strategy = GenerationType.UUID)
    private UUID ParkingZoneId;

    private String Name;

    public ParkingZone(String name){
        this.Name = name;
    }

}
