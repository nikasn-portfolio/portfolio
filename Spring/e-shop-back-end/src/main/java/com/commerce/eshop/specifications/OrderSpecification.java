package com.commerce.eshop.specifications;

import com.commerce.eshop.models.Order;
import com.commerce.eshop.models.Product;
import jakarta.persistence.criteria.Predicate;
import org.springframework.data.jpa.domain.Specification;

import java.util.UUID;

public class OrderSpecification {

    public static Specification<Order> orderBelongsToApplicationUserAndSortedByDate(UUID applicationUserId) {
        return (root, query, criteriaBuilder) -> {
            Predicate belongsToUserPredicate = criteriaBuilder.equal(root.get("applicationUser").get("id"), applicationUserId);

            query.orderBy(criteriaBuilder.asc(root.get("createdAt")));

            return criteriaBuilder.and(belongsToUserPredicate);
        };
    }

}
