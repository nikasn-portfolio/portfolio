package com.commerce.eshop.services;

import com.commerce.eshop.models.Order;
import com.commerce.eshop.repository.OrderRepository;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.UUID;

import static com.commerce.eshop.specifications.OrderSpecification.orderBelongsToApplicationUserAndSortedByDate;

@Service
public class OrderService {
    private OrderRepository orderRepository;

    public OrderService(OrderRepository orderRepository) {
        this.orderRepository = orderRepository;
    }

    public Order saveOrder(Order order){
        return orderRepository.save(order);
    }

    public Order getOrderById(UUID id){
        return orderRepository.findById(id).orElse(null);
    }

    public List<Order> getAllOrdersByApplicationUserId(UUID applicationUserId){
        return orderRepository.findAll(orderBelongsToApplicationUserAndSortedByDate(applicationUserId));
    }
}
