package com.commerce.eshop.services;

import com.commerce.eshop.models.Product;
import com.commerce.eshop.repository.ProductRepository;
import jakarta.transaction.Transactional;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.UUID;

import static com.commerce.eshop.specifications.ProductSpecification.productBelongsToCategory;

@Service
@Transactional
public class ProductService {


    private ProductRepository productRepository;

    public ProductService(ProductRepository productRepository) {
        this.productRepository = productRepository;
    }

    public List<Product> getListOfProductsBelongsToCategory(UUID categoryId){
        return productRepository.findAll(productBelongsToCategory(categoryId));
    }

    public List<Product> getAllProducts(){
        return productRepository.findAll();
    }

    public Product getProductById(UUID id){
        return productRepository.findById(id).orElse(null);
    }

    public Product saveProduct(Product product){
        return productRepository.save(product);
    }

}
