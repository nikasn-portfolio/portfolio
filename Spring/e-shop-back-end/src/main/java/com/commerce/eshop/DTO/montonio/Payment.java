package com.commerce.eshop.DTO.montonio;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.math.BigDecimal;

@Data
@AllArgsConstructor
@NoArgsConstructor
public class Payment {
    private String method;
    private String methodDisplay;
    private PaymentOptions methodOptions;
    private BigDecimal amount;
    private String currency;

    // Getters and setters
}
