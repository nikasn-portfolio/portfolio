package com.commerce.eshop.services;

import com.commerce.eshop.DTO.identity.JwtResponse;
import com.commerce.eshop.models.user.ApplicationUser;
import com.commerce.eshop.models.user.RefreshToken;
import com.commerce.eshop.repository.RefreshTokenRepository;
import com.commerce.eshop.repository.UserRepository;
import com.fasterxml.jackson.databind.JsonSerializable;
import com.fasterxml.jackson.databind.util.JSONPObject;
import com.nimbusds.jose.*;
import com.nimbusds.jose.crypto.DirectEncrypter;
import io.jsonwebtoken.Claims;
import io.jsonwebtoken.SignatureAlgorithm;
import io.jsonwebtoken.io.Decoders;
import io.jsonwebtoken.security.Keys;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.security.core.Authentication;
import org.springframework.security.core.GrantedAuthority;
import org.springframework.security.oauth2.jwt.*;
import org.springframework.stereotype.Service;

import javax.crypto.spec.SecretKeySpec;
import java.security.Key;
import java.time.Instant;
import java.util.Date;
import java.util.List;
import java.util.Map;
import java.util.UUID;
import java.util.stream.Collectors;
import io.jsonwebtoken.Jwts;

import static com.commerce.eshop.specifications.TokenSpecification.*;

@Service
public class TokenService {


    private JwtEncoder jwtEncoder;


    private JwtDecoder jwtDecoder;


    private RefreshTokenRepository refreshTokenRepository;


    private UserService userService;

    @Value("${jwt.secret}")
    private String jwtSecret;



    public TokenService(JwtEncoder jwtEncoder, JwtDecoder jwtDecoder, RefreshTokenRepository refreshTokenRepository, UserService userService) {
        this.jwtEncoder = jwtEncoder;
        this.jwtDecoder = jwtDecoder;
        this.refreshTokenRepository = refreshTokenRepository;
        this.userService = userService;
    }

    public String generateJwt(Authentication auth){

        Instant now = Instant.now();
        String scope = auth.getAuthorities()
                .stream()
                .map(GrantedAuthority::getAuthority)
                .collect(Collectors.joining(" "));

        Instant currentTime = Instant.now();
        long currentTimeMillis = currentTime.toEpochMilli();
        Instant plusTwoMinutes = currentTime.plusSeconds(1000);
        long plusTwoMinutesMillis = plusTwoMinutes.toEpochMilli();

        JwtClaimsSet claims = JwtClaimsSet.builder()
                .issuer("e-shop")
                .subject(auth.getName())
                .claim("roles", scope)
                .claim("phone", ((ApplicationUser) auth.getPrincipal()).getPhoneNumber())
                .claim("fullName", ((ApplicationUser) auth.getPrincipal()).getFirstName() + " " + ((ApplicationUser) auth.getPrincipal()).getLastName())
                .build();

        Key key = Keys.hmacShaKeyFor(jwtSecret.getBytes());

        String token = Jwts.builder()
                .setHeaderParam("typ", "JWT")
                .setClaims(claims.getClaims())
                .setIssuedAt(new Date(currentTimeMillis))
                .setExpiration(new Date(plusTwoMinutesMillis))
                .signWith(key, SignatureAlgorithm.HS256)
                .compact();

        return token;
    }

    public Claims verifyToken(String token) {
        try {
            Key key = Keys.hmacShaKeyFor(jwtSecret.getBytes());

            return Jwts.parserBuilder()
                    .setSigningKey(key)
                    .build()
                    .parseClaimsJws(token)
                    .getBody();
        } catch (JwtException e) {
            // Handle verification failure
            return null;
        }
    }

    public List<RefreshToken> getUserListOfRefreshTokens(ApplicationUser principal){
        UUID applicationUserId = principal.getId();
        List<RefreshToken> refreshTokensBelongsToCurrentUser = refreshTokenRepository.findAll(refreshTokenBelongsToUser(applicationUserId));
        return refreshTokensBelongsToCurrentUser;
    }

    public void deleteRefreshToken(RefreshToken token){
        refreshTokenRepository.delete(token);
    }

    public RefreshToken saveRefreshToken(RefreshToken token){
        return refreshTokenRepository.save(token);
    }

    public JwtResponse updateJwtTokenByRefreshToken(String jwt, String refreshToken){
        Claims claims = verifyToken(jwt);
        if(claims == null){
            throw new RuntimeException("JWT token is not valid");
        }
        //Map<String, Object> claims = jwtDecoder.decode(jwt).getClaims();
        ApplicationUser applicationUser = userService.findUserByUsername(claims.get("sub").toString());
        List<RefreshToken> refreshTokens = refreshTokenRepository
                .findAll(refreshTokenBelongsToUser(applicationUser.getId())
                .and(refreshTokenIsEqualToRefreshToken(refreshToken)
                        .and(refreshTokenIsNotExpired()))
                .or(refreshTokenIsEqualToPreviousRefreshToken(refreshToken)
                        .and(previousRefreshTokenIsNotExpired())));
        if(refreshTokens.size() == 0){
            throw new RuntimeException("Refresh token is not valid");
        }else if (refreshTokens.size() > 1){
            throw new RuntimeException("Refresh token is not unique");
        }
        String scope = claims.get("roles").toString();
        JwtClaimsSet newClaims = JwtClaimsSet.builder()
                .issuer("e-shop")
                .issuedAt(Instant.now())
                .expiresAt(Instant.now().plusSeconds(120))
                .subject(claims.get("sub").toString())
                .claim("roles", scope)
                .claim("phone", applicationUser.getPhoneNumber())
                .claim("fullName", applicationUser.getFirstName() + " " + applicationUser.getLastName())
                .build();
        RefreshToken refreshTokenEntity = refreshTokens.get(0);
        refreshTokenEntity.setRefreshToken(UUID.randomUUID().toString());
        refreshTokenEntity.setExpirationTs(Instant.now().plusSeconds(604800));
        refreshTokenEntity.setPreviousRefreshToken(refreshToken);
        refreshTokenEntity.setPreviousExpirationTs(Instant.now().plusSeconds(302400));
        RefreshToken updatedRefreshToken = refreshTokenRepository.save(refreshTokenEntity);

        return new JwtResponse(jwtEncoder.encode(JwtEncoderParameters.from(newClaims)).getTokenValue(), updatedRefreshToken.getRefreshToken(), "");
    }

    public String extractUsernameFromToken(String jwtToken){
        Claims claims = verifyToken(jwtToken);
        return claims.get("sub").toString();
    }

    public Boolean validateToken(String token) {
        Claims claims = verifyToken(token);
        long issued = Long.parseLong(claims.get("iat").toString());
        long expired = Long.parseLong(claims.get("exp").toString());
        long currentTime = Instant.now().getEpochSecond();
        if (currentTime > expired || currentTime < issued) {
            return false;
        }
        return true;
    }

}
