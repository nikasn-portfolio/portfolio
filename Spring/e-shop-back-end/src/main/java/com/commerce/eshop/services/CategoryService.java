package com.commerce.eshop.services;

import com.commerce.eshop.models.Category;
import com.commerce.eshop.repository.CategoryRepository;
import jakarta.transaction.Transactional;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.UUID;

@Service
@Transactional
public class CategoryService {
    private CategoryRepository categoryRepository;

    public CategoryService(CategoryRepository categoryRepository) {
        this.categoryRepository = categoryRepository;
    }

    public List<Category> getAllCategories(){
        return categoryRepository.findAll();
    }

    public Category getCategoryById(UUID id){
        return categoryRepository.findById(id).get();
    }

    public Category saveCategory(Category category){
        return categoryRepository.save(category);
    }

}
