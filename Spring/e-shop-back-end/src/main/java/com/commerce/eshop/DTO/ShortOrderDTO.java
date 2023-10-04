package com.commerce.eshop.DTO;

import jakarta.persistence.Temporal;
import jakarta.persistence.TemporalType;

import java.math.BigDecimal;
import java.util.Date;
import java.util.List;
import java.util.UUID;

public class ShortOrderDTO {
    private UUID id;

    private String name;

    private String status;

    private BigDecimal price;

    @Temporal(TemporalType.TIMESTAMP) // Specifies that you want to store date and time
    private Date createdAt;

    private Boolean isDone;

    private String paymentUrl;

}
