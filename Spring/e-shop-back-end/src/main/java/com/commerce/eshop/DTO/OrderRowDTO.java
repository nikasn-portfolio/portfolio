package com.commerce.eshop.DTO;

import com.commerce.eshop.models.Candy;
import com.commerce.eshop.models.Order;
import com.commerce.eshop.models.Product;
import com.commerce.eshop.models.Size;
import jakarta.persistence.*;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.util.UUID;
@Data
@AllArgsConstructor
@NoArgsConstructor
public class OrderRowDTO {

    private UUID id;

    private ProductDTO product;

    private SizeDTO size;


    private CandyDTO candy;

    private int quantity;
}
