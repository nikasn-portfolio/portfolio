package com.commerce.eshop.models;

import jakarta.persistence.*;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.util.List;
import java.util.UUID;

@Entity
@Data
@NoArgsConstructor
@AllArgsConstructor
@Table(name = "size")
public class Size {
    @Id
    @GeneratedValue(strategy = GenerationType.UUID)
    private UUID id;
    private String name;

    private Integer candyLimit;

    @OneToMany(mappedBy = "size")
    private List<ProductSize> productSizes;

    @OneToMany(mappedBy = "size")
    private List<OrderRow> orderRows;

    public Size(String name, Integer candyLimit) {
        this.name = name;
        this.candyLimit = candyLimit;
    }
}
