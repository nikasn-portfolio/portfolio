package com.commerce.eshop.specifications;

import com.commerce.eshop.models.user.RefreshToken;
import jakarta.persistence.criteria.Predicate;
import org.springframework.data.jpa.domain.Specification;
import jakarta.persistence.criteria.CriteriaBuilder;
import jakarta.persistence.criteria.CriteriaQuery;
import jakarta.persistence.criteria.Root;

import java.util.UUID;


public class TokenSpecification {
    public static Specification<RefreshToken> refreshTokenIsEqualToRefreshToken(String providedRefreshToken) {
        return (root, query, criteriaBuilder) -> criteriaBuilder.equal(root.get("refreshToken"), providedRefreshToken);
    }

    public static Specification<RefreshToken> refreshTokenBelongsToUser(UUID userId) {
        return (root, query, criteriaBuilder) -> criteriaBuilder.equal(root.get("applicationUser").get("id"), userId);
    }

    public static Specification<RefreshToken> refreshTokenIsNotExpired() {
        return (root, query, criteriaBuilder) -> criteriaBuilder.greaterThan(root.get("expirationTs"), java.time.Instant.now());
    }

    public static Specification<RefreshToken> refreshTokenIsEqualToPreviousRefreshToken(String providedRefreshToken) {
        return (root, query, criteriaBuilder) -> criteriaBuilder.equal(root.get("previousRefreshToken"), providedRefreshToken);
    }

    public static Specification<RefreshToken> previousRefreshTokenIsNotExpired() {
        return (root, query, criteriaBuilder) -> criteriaBuilder.greaterThan(root.get("previousExpirationTs"), java.time.Instant.now());
    }
}
