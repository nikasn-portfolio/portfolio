package com.commerce.eshop.models;

import jakarta.persistence.*;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.util.UUID;

@Entity
@AllArgsConstructor
@NoArgsConstructor
@Data
@Table(name = "billing_address")
public class BillingAddress {

    @Id
    @GeneratedValue(strategy = GenerationType.UUID)
    private UUID id;

    private String firstName;

    private String lastName;

    public BillingAddress(String firstName, String lastName, String email, String addressLine1, String locality, String region, String country, String postalCode) {
        this.firstName = firstName;
        this.lastName = lastName;
        this.email = email;
        this.addressLine1 = addressLine1;
        this.locality = locality;
        this.region = region;
        this.country = country;
        this.postalCode = postalCode;
    }

    private String email;

    private String addressLine1;

    private String locality;

    private String region;

    private String country;

    private String postalCode;

    @OneToOne(mappedBy = "billingAddress")
    private Order order;

}
