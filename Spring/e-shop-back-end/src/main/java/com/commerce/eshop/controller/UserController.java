package com.commerce.eshop.controller;

import com.commerce.eshop.DTO.UpdatePasswordDTO;
import com.commerce.eshop.DTO.UpdatePhoneNumberDTO;
import com.commerce.eshop.models.user.ApplicationUser;
import com.commerce.eshop.models.user.Role;
import com.commerce.eshop.repository.RoleRepository;
import com.commerce.eshop.repository.UserRepository;
import com.commerce.eshop.services.UserService;
import org.apache.coyote.http2.Http2Protocol;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("/api/profile")
public class UserController {

    private UserService userService;

    public UserController(UserService userService) {
        this.userService = userService;
    }
    @GetMapping("/")
    public String getLayer(){
        return "User layer access!";
    }

    @PostMapping ("/updatePassword")
    public UpdatePasswordDTO updateUserPassword(@RequestBody UpdatePasswordDTO updatePasswordDTO){
        return userService.updateUserPassword(updatePasswordDTO.getUsername(), updatePasswordDTO.getOldPassword(), updatePasswordDTO.getNewPassword());
    }

    @PostMapping("/updatePhone")
    public UpdatePhoneNumberDTO updateUserPhone(@RequestBody UpdatePhoneNumberDTO updatePhoneNumberDTO){
        return userService.updateUserPhone(updatePhoneNumberDTO.getUsername(), updatePhoneNumberDTO.getNewPhoneNumber());
    }

    /*@GetMapping("/roles")
    public Role getRole(){
        return roleRepository.findByAuthority("ADMIN").get();
    }*/
}
