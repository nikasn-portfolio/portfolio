package com.commerce.eshop.Helpers;

import com.commerce.eshop.DTO.*;
import com.commerce.eshop.models.*;

public class HelperMethods {
    public static CategoryDTO convertToCategoryDTO(Category category) {
        CategoryDTO categoryDTO = new CategoryDTO();
        categoryDTO.setId(category.getId());
        categoryDTO.setName(category.getName());
        categoryDTO.setImage(category.getImage());
        return categoryDTO;
    }

    public static ImageDTO convertToImageDTO(Image image) {
        ImageDTO imageDTO = new ImageDTO();
        imageDTO.setId(image.getId());
        imageDTO.setImage(image.getImage());
        imageDTO.setProductId(image.getProduct().getId());
        return imageDTO;
    }

    public static SizeDTO convertToSizeDTO(Size size) {
        SizeDTO sizeDTO = new SizeDTO();
        sizeDTO.setId(size.getId());
        sizeDTO.setName(size.getName());
        return sizeDTO;
    }

    public static ProductSizeDTO convertToProductSizeDTO(ProductSize productSize) {
        ProductSizeDTO productSizeDTO = new ProductSizeDTO();
        productSizeDTO.setSize(convertToSizeDTO(productSize.getSize()));
        productSizeDTO.setQuantity(productSize.getQuantity());
        productSizeDTO.setPrice(productSize.getPrice());
        productSizeDTO.setProductId(productSize.getProduct().getId());
        return productSizeDTO;
    }

    public static ProductDTO convertToProductDTO(Product product) {
        ProductDTO productDTO = new ProductDTO();
        productDTO.setId(product.getId());
        productDTO.setName(product.getName());
        productDTO.setDescription(product.getDescription());
        productDTO.setCategory(convertToCategoryDTO(product.getCategory()));
        productDTO.setImages(product.getImages().stream().map(HelperMethods::convertToImageDTO).toList());
        productDTO.setProductSizes(product.getProductSizes().stream().map(HelperMethods::convertToProductSizeDTO).toList());
        return productDTO;
    }
}
