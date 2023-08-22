package com.commerce.eshop.services;

import com.commerce.eshop.models.ProductSize;
import com.commerce.eshop.repository.ProductRepository;
import com.commerce.eshop.repository.ProductSizeRepository;
import jakarta.transaction.Transactional;
import org.springframework.stereotype.Service;

@Service
@Transactional
public class ProductSizeService {

    private ProductSizeRepository productSizeRepository;

    public ProductSizeService(ProductSizeRepository productSizeRepository) {
        this.productSizeRepository = productSizeRepository;
    }

    public ProductSize saveProductSize(ProductSize productSize){
        return productSizeRepository.save(productSize);
    }
}
