package com.commerce.eshop.DTO;

import com.commerce.eshop.models.OrderRow;
import jakarta.persistence.Id;
import jakarta.persistence.Temporal;
import jakarta.persistence.TemporalType;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.math.BigDecimal;
import java.util.Date;
import java.util.List;
import java.util.UUID;

@Data
@NoArgsConstructor
@AllArgsConstructor
public class OrderDTO {
    private UUID id;

    private String name;

    private String status;

    private String bankName;

    private BigDecimal price;

    @Temporal(TemporalType.TIMESTAMP) // Specifies that you want to store date and time
    private Date createdAt;

    private Boolean isDone;

    private String paymentUrl;

    List<OrderRowDTO> orderRows;
}
