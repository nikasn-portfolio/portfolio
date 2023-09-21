package com.commerce.eshop.services;

import com.commerce.eshop.models.Size;
import com.commerce.eshop.repository.SizeRepository;
import jakarta.transaction.Transactional;
import org.springframework.stereotype.Service;

import java.util.UUID;

@Service
@Transactional
public class SizeService {

    private SizeRepository sizeRepository;

    public SizeService(SizeRepository sizeRepository) {
        this.sizeRepository = sizeRepository;
    }

    public Size saveSize(Size size){
        return sizeRepository.save(size);
    }

    public Size findSizeById(UUID id){
        return sizeRepository.findById(id).orElse(null);
    }
}
