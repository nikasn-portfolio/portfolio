package com.commerce.eshop.DTO.identity;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@AllArgsConstructor
@NoArgsConstructor
public class RefreshTokenModel {
    private String jwt;

    private String refreshToken;

}
