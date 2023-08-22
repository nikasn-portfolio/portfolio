package com.commerce.eshop.models.user;

import jakarta.persistence.*;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;
import org.springframework.security.core.GrantedAuthority;

import java.util.HashSet;
import java.util.List;
import java.util.Set;
import java.util.UUID;
@Entity
@Table(name = "roles")
@Data
@NoArgsConstructor
public class Role implements GrantedAuthority {
    @Id
    @GeneratedValue(strategy = GenerationType.UUID)
    private UUID id;
    @Column(name = "authority")
    private String authority;

    public Role(UUID id, String authority) {
        this.id = id;
        this.authority = authority;
    }



    public Role(String authority){
        this.authority = authority;
    }
    @Override
    public String getAuthority() {
        return this.authority;
    }
}
