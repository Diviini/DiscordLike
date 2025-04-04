package com.hetic.api.api_backend.controller;

import com.hetic.api.api_backend.dto.request.UserRequest;
import com.hetic.api.api_backend.model.User;
import com.hetic.api.api_backend.repository.UserRepository;
import jakarta.servlet.http.HttpSession;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.security.core.Authentication;
import org.springframework.security.core.context.SecurityContextHolder;
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
        User user = new User();
        user.setUsername(userRequest.getUsername());
        user.setEmail(userRequest.getEmail());
        user.setPassword(passwordEncoder.encode(userRequest.getPassword()));

        userRepository.save(user);
        return "User registered successfully!";
    }

    // ✅ Stocker userId, username et email en session
    @PostMapping("/login")
    public ResponseEntity<String> login(HttpSession session) {
        Authentication auth = SecurityContextHolder.getContext().getAuthentication();

        if (auth != null && auth.isAuthenticated()) {
            String username = auth.getName();
            User user = userRepository.findByUsername(username);

            if (user != null) {
                session.setAttribute("userId", user.getId()); // Stocke l'ID
                session.setAttribute("username", user.getUsername()); // Stocke le username
                session.setAttribute("email", user.getEmail()); // Stocke l'email

                return ResponseEntity.ok("User logged in: " + user.getUsername());
            }
        }
        return ResponseEntity.status(401).body("Unauthorized");
    }
}