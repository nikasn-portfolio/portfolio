package com.commerce.eshop.DTO;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@AllArgsConstructor
@NoArgsConstructor
public class ApplicationUserDTO {
    private String username;

    private String firstName;

    private String lastName;

    private String phoneNumber;
}
