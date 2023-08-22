package com.commerce.eshop.specifications;

import com.commerce.eshop.models.Product;
import com.commerce.eshop.models.user.RefreshToken;
import org.springframework.data.jpa.domain.Specification;

import java.util.UUID;

public class ProductSpecification {

    public static Specification<Product> productBelongsToCategory(UUID categoryId) {
        return (root, query, criteriaBuilder) -> criteriaBuilder.equal(root.get("category").get("id"), categoryId);
    }
}
