package com.commerce.eshop.repository;

import com.commerce.eshop.models.user.ApplicationUser;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.stereotype.Repository;

import java.util.Optional;
import java.util.UUID;
@Repository
public interface UserRepository extends JpaRepository<ApplicationUser, UUID> {

    Optional<ApplicationUser> findByUsername(String username);
}
