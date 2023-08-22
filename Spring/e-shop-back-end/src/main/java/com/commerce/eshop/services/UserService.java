package com.commerce.eshop.services;

import com.commerce.eshop.DTO.UpdatePasswordDTO;
import com.commerce.eshop.DTO.UpdatePhoneNumberDTO;
import com.commerce.eshop.models.user.ApplicationUser;
import com.commerce.eshop.models.user.Role;
import com.commerce.eshop.repository.UserRepository;
import jakarta.transaction.Transactional;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.core.userdetails.UserDetailsService;
import org.springframework.security.core.userdetails.UsernameNotFoundException;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.stereotype.Service;

import java.util.HashSet;
import java.util.Set;
import java.util.UUID;
@Service
@Transactional
public class UserService implements UserDetailsService {


    private PasswordEncoder encoder;

    private UserRepository userRepository;

    public UserService(PasswordEncoder encoder, UserRepository userRepository) {
        this.encoder = encoder;
        this.userRepository = userRepository;
    }
    @Override
    public UserDetails loadUserByUsername(String username) throws UsernameNotFoundException {

        System.out.println("In the user detail service");

        return userRepository.findByUsername(username).orElseThrow(() -> new UsernameNotFoundException("user not found"));
    }

    public ApplicationUser findUserByUsername(String username){
        return userRepository.findByUsername(username).orElseThrow(() -> new UsernameNotFoundException("user not found"));
    }

    public UpdatePasswordDTO updateUserPassword(String username, String oldPassword, String newPassword){
        ApplicationUser user = findUserByUsername(username);
        String encodedPassword = encoder.encode(oldPassword);
        boolean encodedPasswordMatches = encoder.matches(oldPassword, user.getPassword());
        if(!encodedPasswordMatches) throw new RuntimeException("Old password does not match");
        user.setPassword(encoder.encode(newPassword));
        userRepository.save(user);
        return new UpdatePasswordDTO(oldPassword,user.getPassword(),user.getUsername());
    }

    public UpdatePhoneNumberDTO updateUserPhone(String username, String newPhone){
        ApplicationUser user = findUserByUsername(username);
        user.setPhoneNumber(newPhone);
        userRepository.save(user);
        return new UpdatePhoneNumberDTO(user.getUsername(),user.getPhoneNumber());
    }
}
