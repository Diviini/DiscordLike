package com.hetic.api.api_backend.controller;

import com.hetic.api.api_backend.dto.response.FriendRequestResponse;
import com.hetic.api.api_backend.dto.response.UserResponse;
import com.hetic.api.api_backend.service.FriendService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/friends")
public class FriendController {

    @Autowired
    private FriendService friendService;

    // Route pour voir la liste des amis
    @GetMapping
    public ResponseEntity<List<UserResponse>> getFriends() {
        // Utilisez un ID fictif pour l'exemple, remplacez par l'ID de l'utilisateur connecté
        Long userId = 1L;
        List<UserResponse> friends = friendService.getFriends(userId);
        return ResponseEntity.ok(friends);
    }

    // Route pour voir la liste des demandes d'amis
    @GetMapping("/request")
    public ResponseEntity<List<FriendRequestResponse>> getFriendRequests() {
        // Utilisez un ID fictif pour l'exemple, remplacez par l'ID de l'utilisateur connecté
        Long userId = 1L;
        List<FriendRequestResponse> requests = friendService.getFriendRequests(userId);
        return ResponseEntity.ok(requests);
    }

    // Route pour envoyer une demande d'ami
    @PostMapping("/request/{id}")
    public ResponseEntity<Void> sendFriendRequest(@PathVariable Long id) {
        Long senderId = 1L; // Utilisez l'ID de l'utilisateur connecté
        friendService.sendFriendRequest(senderId, id);
        return ResponseEntity.ok().build();
    }
}
