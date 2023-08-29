package com.commerce.eshop.controller;

import com.commerce.eshop.DTO.montonio.TransactionPayload;
import com.commerce.eshop.services.MontonioService;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("/api/montonio")
public class MontonioController {

    private MontonioService MontonioService;

    public MontonioController(MontonioService MontonioService) {
        this.MontonioService = MontonioService;
    }
    @GetMapping("/token")
    public String getToken(){
        return MontonioService.getTokenForTransaction();
    }

    @PostMapping("/transactionToken")
    public String getTokenForTransaction(@RequestBody TransactionPayload payload){
        return MontonioService.postTransaction(payload);
    }
}
