package com.commerce.eshop.DTO;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.util.UUID;

@Data
@AllArgsConstructor
@NoArgsConstructor
public class BillingAddressDTO {
    private UUID id;

    private String firstName;

    private String lastName;

    private String email;

    private String addressLine1;

    private String locality;

    private String region;

    private String country;

    private String postalCode;
}
