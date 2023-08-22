package com.commerce.eshop.models.user;

import jakarta.persistence.*;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;
import org.hibernate.sql.results.graph.Fetch;

import java.sql.Timestamp;
import java.time.Instant;
import java.time.temporal.ChronoField;
import java.util.UUID;

@Entity
@Data
@AllArgsConstructor
@NoArgsConstructor
public class RefreshToken {
    @Id
    @GeneratedValue(strategy = GenerationType.UUID)
    private UUID id;

    @Column(name = "refresh_token")
    private String refreshToken = String.valueOf(UUID.randomUUID());
    @Column(columnDefinition = "TIMESTAMP")
    private Instant expirationTs = Instant.now().plusSeconds(604800);

    @Column(name = "previous_refresh_token")

    private String previousRefreshToken;
    @Column(columnDefinition = "TIMESTAMP")
    private Instant previousExpirationTs = Instant.now().plusSeconds(604800);

    @ManyToOne(fetch = FetchType.EAGER)
    @JoinColumn(name = "application_user_id")
    private ApplicationUser applicationUser;


}
