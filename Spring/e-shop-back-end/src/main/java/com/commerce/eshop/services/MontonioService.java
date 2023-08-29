package com.commerce.eshop.services;

import com.commerce.eshop.DTO.montonio.TransactionPayload;
import com.fasterxml.jackson.core.type.TypeReference;
import com.fasterxml.jackson.databind.ObjectMapper;
import io.jsonwebtoken.Jwts;
import io.jsonwebtoken.SignatureAlgorithm;
import io.jsonwebtoken.io.Decoders;
import io.jsonwebtoken.security.Keys;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Service;

import java.security.Key;
import java.util.Date;
import java.util.Map;
import java.util.UUID;

@Service
public class MontonioService {
    @Value("${montonio.accessKey}")
    private String accessKey;
    @Value("${montonio.secretKey}")
    private String secretKey;
    public String getTokenForTransaction() {

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

    public String postTransaction(TransactionPayload payload) {
        String accessKey = "3883a70c-e539-47a7-9c74-45fd9b3ac3ae";
        String secretKey = "IMxt0/r2Ib2iUnc96xQVrxXYSNTBec5NmanBS5Q7ybmd";
        long currentTimeMillis = System.currentTimeMillis();
        long expirationMillis = currentTimeMillis + 600000;;
        payload.setAccessKey(accessKey);
        payload.setMerchantReference(UUID.randomUUID().toString());
        payload.getPayment().getMethodOptions().setPaymentDescription("Payment for order " + payload.getMerchantReference());
        ObjectMapper objectMapper = new ObjectMapper();
        Map<String, Object> payloadMap = objectMapper.convertValue(payload, new TypeReference<Map<String, Object>>() {});
        Key key = Keys.hmacShaKeyFor(secretKey.getBytes());
        String token = Jwts.builder()
                .setHeaderParam("typ", "JWT")
                .setClaims(payloadMap)
                .setIssuedAt(new Date(currentTimeMillis))
                .setExpiration(new Date(expirationMillis))
                .signWith(key, SignatureAlgorithm.HS256)
                .compact();

        return token;
    }
}
