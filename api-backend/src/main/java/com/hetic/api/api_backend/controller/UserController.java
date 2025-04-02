package com.hetic.api.api_backend.controller;

import com.hetic.api.api_backend.dto.response.UserResponse;
import com.hetic.api.api_backend.model.User;
import com.hetic.api.api_backend.repository.UserRepository;
import com.hetic.api.api_backend.service.UserService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/users")
public class UserController {

    private final UserRepository userRepository;
    private final UserService userService;

    @Autowired
    public UserController(UserRepository userRepository, UserService userService) {
        this.userRepository = userRepository;
        this.userService = userService;
    }

    @GetMapping
    public List<UserResponse> getAllUsers() {
        return userService.getAllUsers();
    }

    // Route pour récupérer le profil de l'utilisateur connecté
    @GetMapping("/me")
    public ResponseEntity<UserResponse> getMyProfile() {
        Long userId = userService.getCurrentUserId();
        if (userId == null) return ResponseEntity.status(401).build();
        return ResponseEntity.ok(userService.getUserById(userId));
    }

    // Route pour récupérer un utilisateur par son ID
    @GetMapping("/{id}")
    public ResponseEntity<UserResponse> getUserById(@PathVariable Long id) {
        UserResponse user = userService.getUserById(id);
        if (user != null) {
            return ResponseEntity.ok(user);
        } else {
            return ResponseEntity.status(404).build();
        }
    }
}