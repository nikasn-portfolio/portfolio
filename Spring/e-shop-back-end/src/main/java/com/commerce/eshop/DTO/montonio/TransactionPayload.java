package com.commerce.eshop.DTO.montonio;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.math.BigDecimal;
import java.util.List;

@Data
@NoArgsConstructor
@AllArgsConstructor
public class TransactionPayload {
    private String accessKey;
    private String merchantReference;
    private String returnUrl;
    private String notificationUrl;
    private String currency;
    private BigDecimal grandTotal;
    private String locale;
    private BillingAddress billingAddress;
    private ShippingAddress shippingAddress;
    private List<LineItem> lineItems;
    private Payment payment;

    // Getters and setters

    // You can generate getters and setters using your IDE or manually
}
