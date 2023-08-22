package com.commerce.eshop.DTO;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@NoArgsConstructor
@AllArgsConstructor
public class UpdatePhoneNumberDTO {
    private String username;
    private String newPhoneNumber;
}
