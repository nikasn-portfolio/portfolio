package com.commerce.eshop.repository;

import com.commerce.eshop.models.user.RefreshToken;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.JpaSpecificationExecutor;

import java.util.List;
import java.util.UUID;

public interface RefreshTokenRepository extends JpaRepository<RefreshToken, UUID>, JpaSpecificationExecutor<RefreshToken> {
    List<RefreshToken> findAll();
}
