package com.commerce.eshop.controller;

import com.commerce.eshop.DTO.CategoryDTO;
import com.commerce.eshop.Helpers.HelperMethods;
import com.commerce.eshop.models.Candy;
import com.commerce.eshop.models.Category;
import com.commerce.eshop.services.CandyService;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import java.util.List;
import java.util.stream.Collectors;

@RestController
@RequestMapping("/api/candy")
public class CandyController {
    private CandyService candyService;

    public CandyController(CandyService candyService) {
        this.candyService = candyService;
    }

    @GetMapping("/getAllCandies")
    public List<Candy> getAllCandies(){
        return candyService.getAllCandies();
    }
}
