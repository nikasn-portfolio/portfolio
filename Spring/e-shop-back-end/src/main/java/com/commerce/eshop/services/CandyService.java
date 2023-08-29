package com.commerce.eshop.services;

import com.commerce.eshop.models.Candy;
import com.commerce.eshop.models.Image;
import com.commerce.eshop.repository.CandyRepository;
import jakarta.transaction.Transactional;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.UUID;

@Service
@Transactional
public class CandyService {
    private CandyRepository candyRepository;
    public CandyService(CandyRepository candyRepository){
        this.candyRepository = candyRepository;
    }

    public Candy saveCandy(Candy candy){
        return candyRepository.save(candy);
    }

    public Candy getCandyById(UUID id){
        return candyRepository.findById(id).orElse(null);
    }
    public List<Candy> getAllCandies(){
        return candyRepository.findAll();
    }
}
