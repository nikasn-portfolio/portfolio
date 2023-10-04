package com.commerce.eshop.controller;

import com.commerce.eshop.DTO.*;
import com.commerce.eshop.Helpers.HelperMethods;
import com.commerce.eshop.models.*;
import com.commerce.eshop.services.*;
import org.springframework.web.bind.annotation.*;

import java.util.List;
import java.util.UUID;

@RestController
@RequestMapping("/api/orders")
public class OrderController {
    private OrderService orderService;
    private CandyService candyService;

    private UserService userService;

    private ProductService productService;

    private SizeService sizeService;

    public OrderController(OrderService orderService, CandyService candyService, UserService userService, ProductService productService, SizeService sizeService) {
        this.orderService = orderService;
        this.candyService = candyService;
        this.userService = userService;
        this.productService = productService;
        this.sizeService = sizeService;
    }

    @PostMapping("/save")
    public OrderDTO saveOrder(@RequestBody OrderPayloadDTO orderPayloadDTO){
        Order order = new Order();
        order.setId(orderPayloadDTO.getUuid());
        order.setStatus(orderPayloadDTO.getPaymentStatus());
        order.setBankName(orderPayloadDTO.getBankName());
        order.setPrice(orderPayloadDTO.getGrandTotal());
        order.setCreatedAt(orderPayloadDTO.getCreatedAt());
        order.setPaymentUrl(orderPayloadDTO.getPaymentUrl());
        order.setApplicationUser(userService.findUserById(orderPayloadDTO.getCustomerId()));
        order.setBillingAddress(new BillingAddress(orderPayloadDTO.getBillingAddress().getFirstName(), orderPayloadDTO.getBillingAddress().getLastName(), orderPayloadDTO.getBillingAddress().getEmail(), orderPayloadDTO.getBillingAddress().getAddressLine1(), orderPayloadDTO.getBillingAddress().getLocality(), orderPayloadDTO.getBillingAddress().getRegion(), orderPayloadDTO.getBillingAddress().getCountry(), orderPayloadDTO.getBillingAddress().getPostalCode()));
        for (CartListDTO cartItem:  orderPayloadDTO.getOrderRow()) {
            if(cartItem.getItem().getCandies().isEmpty()){
                OrderRow orderRow = new OrderRow();
                orderRow.setOrder(order);
                orderRow.setProduct(productService.getProductById(cartItem.getItem().getId()));
                orderRow.setSize(sizeService.findSizeById(cartItem.getItem().getSizeId()));
                orderRow.setCandy(null);
                orderRow.setQuantity(1);
                order.getOrderRows().add(orderRow);
            }

            for (CandyDTO candyDTO: cartItem.getItem().getCandies()) {
                OrderRow orderRow = new OrderRow();
                orderRow.setOrder(order);
                orderRow.setProduct(productService.getProductById(cartItem.getItem().getId()));
                orderRow.setSize(sizeService.findSizeById(cartItem.getItem().getSizeId()));
                orderRow.setCandy(candyService.getCandyById(candyDTO.getId()));
                orderRow.setQuantity(candyDTO.getQuantity());
                order.getOrderRows().add(orderRow);
            }
        }
        Order saveOrder = orderService.saveOrder(order);

        OrderDTO orderDTO = HelperMethods.convertToOrderDTO(saveOrder);

        return orderDTO;
    }

    @GetMapping("/getById/{id}")
    public OrderDTO getOrderById(@PathVariable UUID id){
        Order order = orderService.getOrderById(id);
        OrderDTO orderDTO = HelperMethods.convertToOrderDTO(order);
        return orderDTO;
    }

    @GetMapping("/getByCustomerId/{id}")
    public List<OrderDTO> getShortOrdersByCustomerId(@PathVariable UUID id){
        return null;
    }
}
