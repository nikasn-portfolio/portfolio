package com.commerce.eshop.models;

import jakarta.persistence.*;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.math.BigDecimal;
import java.util.List;
import java.util.UUID;

@Entity
@Data
@NoArgsConstructor
@AllArgsConstructor
@Table(name = "candy")
public class Candy {
    @Id
    @GeneratedValue(strategy = GenerationType.UUID)
    private UUID id;

    private String name;

    private String image;

    private Integer quantity;

    public Candy(String name, String image, Integer quantity) {
        this.name = name;
        this.image = image;
        this.quantity = quantity;
    }
}
