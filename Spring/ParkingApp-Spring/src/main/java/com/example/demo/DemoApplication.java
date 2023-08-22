package com.example.demo;

import Configuration.Config;
import Configuration.CorsConfig;
import Entities.ParkingZone;
import Repository.IParkingUnitRepository;
import Repository.IParkingZoneRepository;
import org.springframework.boot.CommandLineRunner;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.ComponentScan;
import org.springframework.context.annotation.Import;
import org.springframework.data.jpa.repository.config.EnableJpaRepositories;

import java.util.List;
import java.util.Optional;

import static Helpers.Specifications.hasEqualIds;


@SpringBootApplication
@Import({Config.class, CorsConfig.class})
@EnableJpaRepositories(basePackageClasses= {IParkingUnitRepository.class, IParkingZoneRepository.class})

public class DemoApplication {

	public static void main(String[] args) {
		SpringApplication.run(DemoApplication.class, args);

	}

	@Bean
	public CommandLineRunner demo(IParkingZoneRepository parkingZoneRepository) {
		return (args) -> {
			// save a Initial parking zones
			parkingZoneRepository.save(new ParkingZone("Õismäe Maxima XX"));
			parkingZoneRepository.save(new ParkingZone("Õismäe Maxima X"));
			parkingZoneRepository.save(new ParkingZone("Õismäe Rimi"));
			parkingZoneRepository.save(new ParkingZone("Rocca Al Mare"));
			List<ParkingZone> parkingZones = parkingZoneRepository.findAll();
			Optional<ParkingZone> currentParkingZone = parkingZoneRepository.findOne(hasEqualIds(parkingZones.get(0).getParkingZoneId()));
			System.out.println(currentParkingZone.get());

		};
	}
}
