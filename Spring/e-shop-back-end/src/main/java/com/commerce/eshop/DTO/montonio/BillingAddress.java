package com.commerce.eshop.DTO.montonio;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@AllArgsConstructor
@NoArgsConstructor
public class BillingAddress {
    private String firstName;
    private String lastName;
    private String email;
    private String addressLine1;
    private String locality;
    private String region;
    private String country;
    private String postalCode;

    // Getters and setters
}
