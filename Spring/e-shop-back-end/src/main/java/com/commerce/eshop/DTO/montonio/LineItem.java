package com.commerce.eshop.DTO.montonio;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.math.BigDecimal;

@Data
@NoArgsConstructor
@AllArgsConstructor
public class LineItem {
    private String name;
    private int quantity;
    private BigDecimal finalPrice;

    // Getters and setters
}
