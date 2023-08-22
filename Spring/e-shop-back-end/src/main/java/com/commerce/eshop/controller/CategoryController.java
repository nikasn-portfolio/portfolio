package com.commerce.eshop.controller;

import com.commerce.eshop.DTO.CategoryDTO;
import com.commerce.eshop.Helpers.HelperMethods;
import com.commerce.eshop.models.Category;
import com.commerce.eshop.models.Product;
import com.commerce.eshop.repository.CategoryRepository;
import com.commerce.eshop.services.CategoryService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import java.util.List;
import java.util.UUID;
import java.util.stream.Collectors;

@RestController
@RequestMapping("/api/category")
public class CategoryController {

    private CategoryService categoryService;

    public CategoryController(CategoryService categoryService) {
        this.categoryService = categoryService;
    }

    @GetMapping("/")
    public List<CategoryDTO> getAllCategories(){
        List<Category> categories = categoryService.getAllCategories();

        // Map Category entities to CategoryDTO using Java 8 Stream API and Collectors
        List<CategoryDTO> categoryDTOs = categories.stream()
                .map(HelperMethods::convertToCategoryDTO)
                .collect(Collectors.toList());

        return categoryDTOs;
    }

    @GetMapping("/{id}")
    public List<Product> getListOfProductsBelongsToCategory(@PathVariable UUID id){
        Category category = categoryService.getCategoryById(id);
        return category.getProducts();
    }



}
