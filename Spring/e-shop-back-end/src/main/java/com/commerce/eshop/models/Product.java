package com.commerce.eshop.models;

import jakarta.persistence.*;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;
import org.hibernate.annotations.Fetch;

import java.util.List;
import java.util.UUID;

@Entity
@NoArgsConstructor
@AllArgsConstructor
@Data
@Table(name = "product")
public class Product {

    @Id
    @GeneratedValue(strategy = GenerationType.UUID)
    private UUID id;
    @Column(name = "name", nullable = false)
    private String name;

    @Column(name = "description")
    private String description;

    @ManyToOne(fetch = FetchType.EAGER)
    @JoinColumn(name = "category_id", nullable = false)
    private Category category;

    @OneToMany(fetch = FetchType.EAGER, mappedBy = "product")
    private List<Image> images;

    @OneToMany(fetch = FetchType.EAGER, mappedBy = "product")
    private List<ProductSize> productSizes;


    public Product(String name, String description, Category category) {
        this.name = name;
        this.description = description;
        this.category = category;
    }




}
