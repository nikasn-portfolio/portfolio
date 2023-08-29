package com.commerce.eshop.DTO.montonio;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@NoArgsConstructor
@AllArgsConstructor
public class PaymentOptions {
    private String paymentDescription;
    private String preferredCountry;
    private String preferredProvider;

    // Getters and setters
}
