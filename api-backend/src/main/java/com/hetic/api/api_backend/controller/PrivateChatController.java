package com.hetic.api.api_backend.controller;

import com.hetic.api.api_backend.dto.request.MessageRequest;
import com.hetic.api.api_backend.dto.response.MessageResponse;
import com.hetic.api.api_backend.service.PrivateChatService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/private-chat")
public class PrivateChatController {

    @Autowired
    private PrivateChatService privateChatService;

    // Route pour envoyer un message privé
    @PostMapping("/{id}")
    public ResponseEntity<MessageResponse> sendPrivateMessage(
            @PathVariable Long id,
            @RequestBody MessageRequest messageRequest) {
        messageRequest.setSenderId(1L); // Utilisez l'ID de l'utilisateur connecté
        MessageResponse response = privateChatService.sendPrivateMessage(id, messageRequest);
        if (response != null) {
            return ResponseEntity.ok(response);
        } else {
            return ResponseEntity.status(404).build();
        }
    }

    // Route pour voir les messages privés avec un utilisateur
    @GetMapping("/{id}")
    public ResponseEntity<List<MessageResponse>> getPrivateMessagesWithUser(@PathVariable Long id) {
        Long loggedInUserId = 1L; // Utilisez l'ID de l'utilisateur connecté
        List<MessageResponse> messages = privateChatService.getPrivateMessagesWithUser(loggedInUserId, id);
        return ResponseEntity.ok(messages);
    }
}
