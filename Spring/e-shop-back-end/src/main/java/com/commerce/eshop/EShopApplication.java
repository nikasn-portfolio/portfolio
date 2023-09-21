package com.commerce.eshop;

import com.commerce.eshop.models.*;
import com.commerce.eshop.models.user.ApplicationUser;
import com.commerce.eshop.models.user.Role;
import com.commerce.eshop.repository.CategoryRepository;
import com.commerce.eshop.repository.ProductRepository;
import com.commerce.eshop.repository.RoleRepository;
import com.commerce.eshop.repository.UserRepository;
import com.commerce.eshop.services.CandyService;
import com.commerce.eshop.services.ImageService;
import com.commerce.eshop.services.ProductSizeService;
import com.commerce.eshop.services.SizeService;
import org.springframework.boot.CommandLineRunner;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.context.annotation.Bean;
import org.springframework.security.crypto.password.PasswordEncoder;

import java.math.BigDecimal;
import java.util.*;

@SpringBootApplication
public class EShopApplication {

	public static void main(String[] args) {
		SpringApplication.run(EShopApplication.class, args);
	}

	@Bean
	CommandLineRunner run(RoleRepository roleRepository, UserRepository userRepository,
						  PasswordEncoder encoder, CategoryRepository categoryRepository, ProductRepository productRepository,
						  ProductSizeService productSizeService, ImageService imageService, SizeService sizeService, CandyService candyService){
		 return args -> {
			 if(roleRepository.findByAuthority("ADMIN").isPresent()) return;
			 Role adminRole = roleRepository.save(new Role("ROLE_ADMIN"));
			 roleRepository.save(new Role("ROLE_USER"));

			 Set<Role> roles = new HashSet<>();
			 roles.add(adminRole);
			 ApplicationUser admin = new ApplicationUser(UUID.randomUUID(),"admin", encoder.encode("password"),roles, "John", "Smith", "+37258221929");
			 userRepository.save(admin);

			 //Add categories
			 Category test1 = categoryRepository.save(new Category("Test_1", "https://drive.google.com/uc?id=1xh0RamwTtUHKddxnTaxVQXxLl-XINpMw"));
			 categoryRepository.save(new Category("Test_2", "https://drive.google.com/uc?id=1xh0RamwTtUHKddxnTaxVQXxLl-XINpMw"));
			 categoryRepository.save(new Category("Test_3", "https://drive.google.com/uc?id=1xh0RamwTtUHKddxnTaxVQXxLl-XINpMw"));
			 categoryRepository.save(new Category("Test_4", "https://drive.google.com/uc?id=1xh0RamwTtUHKddxnTaxVQXxLl-XINpMw"));
			 System.out.println(test1.getId());


			 //Add products

			 Product productTest1 = productRepository.save(new Product("TestProduct_1", "TestProduct_1 description", test1));


			 //Add image for product

			 imageService.saveImage(new Image("https://drive.google.com/uc?id=1xh0RamwTtUHKddxnTaxVQXxLl-XINpMw", productTest1));
			 imageService.saveImage(new Image("https://drive.google.com/uc?id=15PMAIJjeGhw56pc61TRWoRJfgk4HmN4L", productTest1));
			 imageService.saveImage(new Image("https://drive.google.com/uc?id=15PMAIJjeGhw56pc61TRWoRJfgk4HmN4L", productTest1));
			 imageService.saveImage(new Image("https://drive.google.com/uc?id=15PMAIJjeGhw56pc61TRWoRJfgk4HmN4L", productTest1));

			 //Add sizes

			 Size sizeTest1 = sizeService.saveSize(new Size("S", 300));
			 Size sizeTest2 = sizeService.saveSize(new Size("M", 500));
			 Size sizeTest3 = sizeService.saveSize(new Size("L", 700));

			 //Add product sizes
			 ProductSizeId productSizeId = new ProductSizeId(productTest1.getId(), sizeTest1.getId());
			 ProductSizeId productSizeId2 = new ProductSizeId(productTest1.getId(), sizeTest2.getId());
			 ProductSizeId productSizeId3 = new ProductSizeId(productTest1.getId(), sizeTest3.getId());
			 productSizeService.saveProductSize(new ProductSize(productSizeId, productTest1, sizeTest1, 0 ,new BigDecimal(10.00), new BigDecimal(30.00)));
			 productSizeService.saveProductSize(new ProductSize(productSizeId2, productTest1, sizeTest2, 10 ,new BigDecimal(20.00), new BigDecimal(40.00)));
			 productSizeService.saveProductSize(new ProductSize(productSizeId3, productTest1, sizeTest3, 10 ,new BigDecimal(30.00), new BigDecimal(50.00)));

			 // Add test candies

			candyService.saveCandy(new Candy("Pilveke", "https://charlot.ee/images/items/153/415387/640x640/0.jpg", 1000));
			candyService.saveCandy(new Candy("Kaseke", "https://rimibaltic-res.cloudinary.com/image/upload/b_white,c_fit,f_auto,h_216,q_auto,w_216/d_ecommerce:backend-fallback.png/MAT_102957_PCE_EE", 1000));
			candyService.saveCandy(new Candy("Kama", "https://rimibaltic-res.cloudinary.com/image/upload/b_white,c_fit,f_auto,h_216,q_auto,w_216/d_ecommerce:backend-fallback.png/MAT_1014237_PCE_EE", 1000));
			candyService.saveCandy(new Candy("Kiss-Kiss", "https://kalev.eu/wp-content/uploads/2023/06/4740012600169-Kiss-Kiss_pehme_150g-614x614.jpg", 1000));
			candyService.saveCandy(new Candy("Pilveke", "https://charlot.ee/images/items/153/415387/640x640/0.jpg", 1000));

		 };
	}

}
