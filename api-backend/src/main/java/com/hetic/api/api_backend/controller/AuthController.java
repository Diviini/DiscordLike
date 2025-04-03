package com.hetic.api.api_backend.controller;

import com.hetic.api.api_backend.dto.request.UserRequest;
import com.hetic.api.api_backend.model.User;
import com.hetic.api.api_backend.repository.UserRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("/auth")
public class AuthController {

    @Autowired
    private UserRepository userRepository;

    @Autowired
    private PasswordEncoder passwordEncoder;

    @PostMapping("/register")
    public String registerUser(@RequestBody UserRequest userRequest) {
        System.out.println("Register endpoint hit.");
        System.out.println("Registering user: " + userRequest.getUsername());

        User user = new User();
        user.setUsername(userRequest.getUsername());
        user.setEmail(userRequest.getEmail());
        user.setPassword(passwordEncoder.encode(userRequest.getPassword()));

        User savedUser = userRepository.save(user);
        System.out.println("User registered: " + savedUser.getUsername());

        return "User registered successfully!";
    }



    // Route pour la connexion (Spring Security gère cela automatiquement)
    @GetMapping("/login")
    public String login() {
        System.out.println("Login attempt.");
        return "Please use HTTP Basic Auth for login.";
    }
}
