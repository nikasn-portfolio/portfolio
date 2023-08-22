package com.commerce.eshop.controller;

import com.commerce.eshop.services.MontonioService;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("/api/montonio")
public class MontonioController {
    @GetMapping("/token")
    public String getToken(){
        return MontonioService.getTokenForTransaction();
    }
}
