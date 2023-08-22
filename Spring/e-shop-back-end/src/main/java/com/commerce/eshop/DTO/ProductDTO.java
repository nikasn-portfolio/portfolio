package com.commerce.eshop.DTO;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.util.List;
import java.util.UUID;
@Data
@AllArgsConstructor
@NoArgsConstructor
public class ProductDTO {
    private UUID id;
    private String name;
    private String description;
    private CategoryDTO category;
    private List<ImageDTO> images;
    private List<ProductSizeDTO> productSizes;
}
