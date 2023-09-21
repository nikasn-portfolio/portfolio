package com.commerce.eshop.controller;

import com.commerce.eshop.DTO.ProductDTO;
import com.commerce.eshop.Helpers.HelperMethods;
import com.commerce.eshop.models.Product;
import com.commerce.eshop.services.ProductService;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import java.util.List;
import java.util.UUID;
import java.util.stream.Collectors;

@RestController
@RequestMapping("/api/products")
public class ProductController {

    private ProductService productService;

    public ProductController(ProductService productService) {
        this.productService = productService;
    }

    @GetMapping("/")
    public List<ProductDTO> getAllProducts(){
        List<Product> products = productService.getAllProducts();
        List<ProductDTO> productDTOs = products.stream()
                .map(HelperMethods::convertToProductDTO)
                .collect(Collectors.toList());
        return productDTOs;
    }

    @GetMapping("/product/{productId}")
    public ProductDTO getAllProducts(@PathVariable UUID productId){
        Product product = productService.getProductById(productId);
        ProductDTO productDTO = HelperMethods.convertToProductDTO(product);
        return productDTO;
    }

    @GetMapping("/{categoryId}")
    public List<ProductDTO> getListOfProductsBelongsToCategory(@PathVariable UUID categoryId){
        List<Product> listOfProductsBelongsToCategory = productService.getListOfProductsBelongsToCategory(categoryId);
        List<ProductDTO> listOfProductDTOsBelongsToCategory = listOfProductsBelongsToCategory.stream()
                .map(HelperMethods::convertToProductDTO)
                .collect(Collectors.toList());
        return listOfProductDTOsBelongsToCategory;
    }

}
