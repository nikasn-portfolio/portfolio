package com.commerce.eshop.services;

import com.commerce.eshop.models.Image;
import com.commerce.eshop.repository.ImageRepository;
import jakarta.transaction.Transactional;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
@Transactional
public class ImageService {


    private ImageRepository imageRepository;

    public ImageService(ImageRepository imageRepository) {
        this.imageRepository = imageRepository;
    }

    public Image saveImage(Image image){
        return imageRepository.save(image);
    }

}
