package com.hetic.api.api_backend.controller;

import com.hetic.api.api_backend.dto.response.UserResponse;
import com.hetic.api.api_backend.model.User;
import com.hetic.api.api_backend.repository.UserRepository;
import com.hetic.api.api_backend.service.UserService;
import jakarta.servlet.http.HttpSession;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.web.bind.annotation.*;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

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

    @GetMapping("/me")
    public ResponseEntity<Map<String, Object>> getMyProfile(HttpSession session) {
        // Récupération des infos en session
        Object userId = session.getAttribute("userId");
        Object username = session.getAttribute("username");
        Object email = session.getAttribute("email");

        if (userId == null || username == null || email == null) {
            return ResponseEntity.status(401).build(); // Pas connecté
        }

        // Construire un JSON propre avec les infos demandées
        Map<String, Object> userInfo = new HashMap<>();
        userInfo.put("userId", userId);
        userInfo.put("username", username);
        userInfo.put("email", email);

        return ResponseEntity.ok(userInfo);
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