package com.hetic.api.api_backend.controller;

import com.fasterxml.jackson.annotation.JsonIgnore;
import com.hetic.api.api_backend.dto.request.MessagePrivateRequest;
import com.hetic.api.api_backend.dto.request.MessagePublicRequest;
import com.hetic.api.api_backend.dto.response.MessageResponse;
import com.hetic.api.api_backend.dto.response.PrivateMessageResponse;
import com.hetic.api.api_backend.service.PrivateChatService;
import com.hetic.api.api_backend.service.UserService;
import jakarta.servlet.http.HttpSession;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/private-chat")
public class PrivateChatController {

    @Autowired
    private PrivateChatService privateChatService;

    @JsonIgnore
    private HttpSession session;

    private final UserService userService;

    public PrivateChatController(UserService userService, HttpSession session) {
        this.userService = userService;
        this.session = session;
    }

    /*
    // Route pour envoyer un message privé
    @PostMapping("/{id}")
    public ResponseEntity<PrivateMessageResponse> sendPrivateMessage(
            @PathVariable Long id,
            @RequestBody MessagePrivateRequest messageRequest) {

        Long userId = (Long) session.getAttribute("userId");

        messageRequest.setSenderId(userId);
        PrivateMessageResponse response = privateChatService.sendPrivateMessage(id, messageRequest);
        if (response != null) {
            return ResponseEntity.ok(response);
        } else {
            return ResponseEntity.status(404).build();
        }
    }
*/
    // Route pour voir les messages privés avec un utilisateur
    @GetMapping("/{id}")
    public ResponseEntity<List<PrivateMessageResponse>> getPrivateMessagesWithUser(@PathVariable Long id) {

        Long userId = (Long) this.session.getAttribute("userId");

        List<PrivateMessageResponse> messages = privateChatService.getPrivateMessagesWithUser(userId, id);
        return ResponseEntity.ok(messages);
    }
}
