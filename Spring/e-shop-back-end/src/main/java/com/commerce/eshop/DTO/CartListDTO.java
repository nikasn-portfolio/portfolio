package com.commerce.eshop.DTO;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.math.BigDecimal;

@NoArgsConstructor
@AllArgsConstructor
@Data
public class CartListDTO {
    private CartItemDTO item;
    private Integer quantity;
    private BigDecimal totalItemPrice;
}
