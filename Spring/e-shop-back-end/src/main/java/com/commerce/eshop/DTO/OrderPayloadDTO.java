package com.commerce.eshop.DTO;

import com.commerce.eshop.DTO.montonio.BillingAddress;
import com.commerce.eshop.models.Candy;
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
@AllArgsConstructor
@NoArgsConstructor
public class OrderPayloadDTO {
    private UUID uuid;

    private String paymentStatus;

    private String bankName;

    private BigDecimal grandTotal;

    @Temporal(TemporalType.TIMESTAMP) // Specifies that you want to store date and time
    private Date createdAt;

    private String paymentUrl;

    private UUID customerId;

    private BillingAddress billingAddress;

    private List<CartListDTO> orderRow;
}
