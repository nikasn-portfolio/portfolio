package com.commerce.eshop.services;

import io.jsonwebtoken.Jwts;
import io.jsonwebtoken.SignatureAlgorithm;
import io.jsonwebtoken.io.Decoders;
import io.jsonwebtoken.security.Keys;
import org.springframework.stereotype.Service;

import java.security.Key;
import java.util.Date;

@Service
public class MontonioService {
    public static String getTokenForTransaction() {
        String accessKey = "b278ef3d-dcca-417f-a0d3-02cd8f73acd2";
        String secretKey = "7SBxaZN7mngCyCIpz7hgUHVP8amQ9+CfexO8Ck3LsTJL";
        long currentTimeMillis = System.currentTimeMillis();
        long expirationMillis = currentTimeMillis + 3600000;

        
        Key key = Keys.hmacShaKeyFor(secretKey.getBytes());


        String token = Jwts.builder()
                .setHeaderParam("typ", "JWT")
                .claim("accessKey", accessKey)
                .setIssuedAt(new Date(currentTimeMillis))
                .setExpiration(new Date(expirationMillis))
                .signWith(key, SignatureAlgorithm.HS256)
                .compact();

        return token;
    }
}
