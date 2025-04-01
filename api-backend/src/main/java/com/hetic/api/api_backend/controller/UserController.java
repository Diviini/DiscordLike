package com.hetic.api.api_backend.controller;

import com.hetic.api.api_backend.dto.response.UserResponse;
import com.hetic.api.api_backend.model.User;
import com.hetic.api.api_backend.repository.UserRepository;
import com.hetic.api.api_backend.service.UserService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
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
    public List<User> getAllUsers() {
        return userRepository.findAll();
    }

    /*
    // Route pour récupérer le profil de l'utilisateur connecté
    @GetMapping("/me")
    public ResponseEntity<UserResponse> getLoggedUser() {
        UserResponse loggedUser = userService.getLoggedInUser();
        if (loggedUser != null) {
            return ResponseEntity.ok(loggedUser);
        } else {
            return ResponseEntity.status(404).build();
        }
    }*/

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