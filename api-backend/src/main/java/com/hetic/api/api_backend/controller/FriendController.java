package com.hetic.api.api_backend.controller;

import com.hetic.api.api_backend.dto.response.FriendRequestResponse;
import com.hetic.api.api_backend.dto.response.UserResponse;
import com.hetic.api.api_backend.model.User;
import com.hetic.api.api_backend.service.FriendService;
import com.hetic.api.api_backend.service.UserService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/friends")
public class FriendController {

    @Autowired
    private FriendService friendService;

    private final UserService userService;

    public FriendController(UserService userService) {
        this.userService = userService;
    }

    // Route pour voir la liste des amis
    @GetMapping
    public ResponseEntity<List<UserResponse>> getFriends() {
        // Utilisez un ID fictif pour l'exemple, remplacez par l'ID de l'utilisateur connecté
        Long userId = userService.getCurrentUserId();
        List<UserResponse> friends = friendService.getFriends(userId);
        return ResponseEntity.ok(friends);
    }

    // Route pour voir la liste des demandes d'amis
    @GetMapping("/request")
    public ResponseEntity<List<FriendRequestResponse>> getFriendRequests() {
        Long userId = userService.getCurrentUserId();
        List<FriendRequestResponse> requests = friendService.getFriendRequests(userId);
        return ResponseEntity.ok(requests);
    }

    // Route pour envoyer une demande d'ami
    @PostMapping("/request/{id}")
    public ResponseEntity<Void> sendFriendRequest(@PathVariable Long id) {
        Long senderId = userService.getCurrentUserId(); // Utilisez l'ID de l'utilisateur connecté
        friendService.sendFriendRequest(senderId, id);
        return ResponseEntity.ok().build();
    }
}
