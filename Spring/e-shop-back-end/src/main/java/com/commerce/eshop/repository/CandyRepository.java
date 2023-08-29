package com.commerce.eshop.repository;

import com.commerce.eshop.models.Candy;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import java.util.UUID;

@Repository
public interface CandyRepository extends JpaRepository<Candy, UUID> {

}
