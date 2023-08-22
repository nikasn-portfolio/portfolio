package com.commerce.eshop.models.user;

import java.util.*;

import jakarta.persistence.*;
import lombok.Data;
import org.springframework.security.core.GrantedAuthority;
import org.springframework.security.core.userdetails.UserDetails;

@Entity
@Table(name="users")
@Data
public class ApplicationUser implements UserDetails{

    @Id
    @GeneratedValue(strategy=GenerationType.UUID)
    private UUID id;
    @Column(unique=true)
    private String username;
    private String password;

    private String phoneNumber;

    private String firstName;

    private String lastName;

    @ManyToMany(fetch=FetchType.EAGER)
    @JoinTable(
            name="user_role_junction",
            joinColumns = {@JoinColumn(name="user_id", referencedColumnName = "id")},
            inverseJoinColumns = {@JoinColumn(name="role_id", referencedColumnName = "id")}
    )
    private Set<Role> authorities;

    @OneToMany(fetch = FetchType.EAGER, mappedBy = "applicationUser")
    private List<RefreshToken> refreshTokenList;


    public ApplicationUser() {
        super();
        authorities = new HashSet<>();
    }


    public ApplicationUser(UUID userId, String username, String password, Set<Role> authorities, String firstName, String lastName, String phoneNumber) {
        super();
        this.id = userId;
        this.username = username;
        this.password = password;
        this.authorities = authorities;
        this.firstName = firstName;
        this.lastName = lastName;
        this.phoneNumber = phoneNumber;
    }



    @Override
    public Collection<? extends GrantedAuthority> getAuthorities() {
        // TODO Auto-generated method stub
        return this.authorities;
    }


    /* If you want account locking capabilities create variables and ways to set them for the methods below */
    @Override
    public boolean isAccountNonExpired() {
        // TODO Auto-generated method stub
        return true;
    }

    @Override
    public boolean isAccountNonLocked() {
        // TODO Auto-generated method stub
        return true;
    }

    @Override
    public boolean isCredentialsNonExpired() {
        // TODO Auto-generated method stub
        return true;
    }

    @Override
    public boolean isEnabled() {
        // TODO Auto-generated method stub
        return true;
    }

}