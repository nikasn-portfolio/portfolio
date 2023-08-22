package com.commerce.eshop.DTO;

import com.commerce.eshop.models.Product;
import com.commerce.eshop.models.ProductSizeId;
import com.commerce.eshop.models.Size;
import jakarta.persistence.EmbeddedId;
import jakarta.persistence.JoinColumn;
import jakarta.persistence.ManyToOne;
import jakarta.persistence.MapsId;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.math.BigDecimal;
import java.util.UUID;
@Data
@NoArgsConstructor
@AllArgsConstructor
public class ProductSizeDTO {
    private UUID productId;
    private SizeDTO size;
    private Integer quantity;
    private BigDecimal price;
}
