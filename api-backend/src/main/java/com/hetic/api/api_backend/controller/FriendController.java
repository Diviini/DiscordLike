package com.hetic.api.api_backend.controller;

import com.fasterxml.jackson.annotation.JsonIgnore;
import com.hetic.api.api_backend.dto.response.FriendRequestResponse;
import com.hetic.api.api_backend.dto.response.UserResponse;
import com.hetic.api.api_backend.model.User;
import com.hetic.api.api_backend.service.FriendService;
import com.hetic.api.api_backend.service.UserService;
import jakarta.servlet.http.HttpSession;
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

    @JsonIgnore
    private HttpSession session;

    public FriendController(UserService userService, HttpSession session) {
        this.userService = userService;
        this.session = session;
    }

    // Route pour voir la liste des amis
    @GetMapping
    public ResponseEntity<List<UserResponse>> getFriends() {

        Long userId = (Long) this.session.getAttribute("userId");
        List<UserResponse> friends = friendService.getFriends(userId);
        return ResponseEntity.ok(friends);
    }

    // Route pour voir la liste des demandes d'amis
    @GetMapping("/request")
    public ResponseEntity<List<FriendRequestResponse>> getFriendRequests() {
        Long userId = (Long) this.session.getAttribute("userId");
        List<FriendRequestResponse> requests = friendService.getFriendRequests(userId);
        return ResponseEntity.ok(requests);
    }

    // Route pour envoyer une demande d'ami
    @PostMapping("/request/{id}")
    public ResponseEntity<Void> sendFriendRequest(@PathVariable Long id) {
        Long userId = (Long) this.session.getAttribute("userId");
        friendService.sendFriendRequest(userId, id);
        return ResponseEntity.ok().build();
    }
}
