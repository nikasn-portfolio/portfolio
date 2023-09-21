package com.commerce.eshop.controller;

import com.commerce.eshop.DTO.*;
import com.commerce.eshop.Helpers.HelperMethods;
import com.commerce.eshop.models.*;
import com.commerce.eshop.services.CandyService;
import com.commerce.eshop.services.OrderService;
import com.commerce.eshop.services.ProductService;
import com.commerce.eshop.services.SizeService;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("/api/orders")
public class OrderController {
    private OrderService orderService;
    private CandyService candyService;

    private ProductService productService;

    private SizeService sizeService;

    public OrderController(OrderService orderService, CandyService candyService, ProductService productService, SizeService sizeService) {
        this.orderService = orderService;
        this.candyService = candyService;
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
}
