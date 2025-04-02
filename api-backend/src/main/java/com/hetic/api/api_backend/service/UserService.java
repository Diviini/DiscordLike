package com.hetic.api.api_backend.service;

import com.hetic.api.api_backend.dto.response.UserResponse;
import com.hetic.api.api_backend.model.User;
import com.hetic.api.api_backend.repository.UserRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.stereotype.Service;

import java.security.Principal;
import java.util.List;
import java.util.stream.Collectors;

@Service
public class UserService {

    private final UserRepository userRepository;

    @Autowired
    public UserService(UserRepository userRepository) {
        this.userRepository = userRepository;
    }

    public List<UserResponse> getAllUsers() {
        return userRepository.findAll().stream()
                .map(user -> new UserResponse(user.getId(), user.getUsername(), user.getEmail()))
                .collect(Collectors.toList());
    }


    /*
    public UserResponse getLoggedInUser() {
        Object principal = SecurityContextHolder.getContext().getAuthentication().getPrincipal();
        if (principal instanceof UserDetails) {
            String username = ((UserDetails) principal).getUsername();
            User user = userRepository.findByUsername(username);
            return new UserResponse(user.getId(), user.getUsername(), user.getEmail());
        }
        return null;
    }
    */

    public UserResponse getUserById(Long id) {
        User user = userRepository.findById(id).orElse(null);
        if (user != null) {
            return new UserResponse(user.getId(), user.getUsername(), user.getEmail());
        }
        return null;
    }

    public String getCurrentUsername() {
        Object principal = SecurityContextHolder.getContext().getAuthentication().getPrincipal();
        if (principal instanceof UserDetails userDetails) {
            return userDetails.getUsername();
        } else {
            return principal.toString(); // fallback
        }
    }

    public Long getCurrentUserId(Principal principal) {
        if (principal == null) {
            return null; // ou une valeur par défaut si approprié
        }
        User user = userRepository.findByUsername(principal.getName());
        return user != null ? user.getId() : null;
    }


    public Long getCurrentUserId() {
        String username = getCurrentUsername();
        User user = userRepository.findByUsername(username);
        return user != null ? user.getId() : null;
    }

}
