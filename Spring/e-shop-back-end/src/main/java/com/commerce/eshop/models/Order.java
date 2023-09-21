package com.commerce.eshop.models;

import jakarta.persistence.*;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.math.BigDecimal;
import java.sql.Timestamp;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.UUID;

@Entity
@NoArgsConstructor
@AllArgsConstructor
@Data
@Table(name = "orders")
public class Order {
    @Id
    private UUID id;

    public Order(UUID id, String status, String bankName, BigDecimal price, Date createdAt, String paymentUrl) {
        this.id = id;
        this.status = status;
        this.bankName = bankName;
        this.price = price;
        this.createdAt = createdAt;
        this.paymentUrl = paymentUrl;
    }

    private String name = UUID.randomUUID().toString();

    private String status;

    private String bankName;

    private BigDecimal price;

    @Temporal(TemporalType.TIMESTAMP) // Specifies that you want to store date and time
    private Date createdAt;

    private Boolean isDone = false;

    private String paymentUrl;

    @OneToMany(mappedBy = "order", fetch = FetchType.EAGER, cascade = CascadeType.ALL)
    private List<OrderRow> orderRows = new ArrayList<>();
}
