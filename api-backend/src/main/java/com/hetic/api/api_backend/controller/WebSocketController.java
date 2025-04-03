package com.hetic.api.api_backend.controller;

import com.fasterxml.jackson.annotation.JsonIgnore;
import com.hetic.api.api_backend.dto.request.MessagePrivateRequest;
import com.hetic.api.api_backend.dto.request.MessagePublicRequest;
import com.hetic.api.api_backend.dto.response.MessageResponse;
import com.hetic.api.api_backend.dto.response.PrivateMessageResponse;
import com.hetic.api.api_backend.model.User;
import com.hetic.api.api_backend.service.ChatService;
import com.hetic.api.api_backend.service.PrivateChatService;
import com.hetic.api.api_backend.service.UserService;
import jakarta.servlet.http.HttpSession;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.messaging.handler.annotation.MessageMapping;
import org.springframework.messaging.simp.SimpMessagingTemplate;
import org.springframework.stereotype.Controller;

import java.security.Principal;
import java.time.LocalDateTime;

@Controller
public class WebSocketController {

    private final SimpMessagingTemplate messagingTemplate;
    private final ChatService chatService;
    private final PrivateChatService privateChatService;
    private final UserService userService;

    @Autowired
    public WebSocketController(SimpMessagingTemplate messagingTemplate,
                               ChatService chatService,
                               PrivateChatService privateChatService,
                               UserService userService) {
        this.messagingTemplate = messagingTemplate;
        this.chatService = chatService;
        this.privateChatService = privateChatService;
        this.userService = userService;
    }

    @MessageMapping("/chat/public")
    public void handlePublicMessage(MessagePublicRequest messageRequest, Principal principal) {
        // Validation
        if (messageRequest.getContent() == null || messageRequest.getContent().trim().isEmpty()) {
            throw new IllegalArgumentException("Message content cannot be empty");
        }

        // Ajout des métadonnées
        messageRequest.setTimestamp(LocalDateTime.now());
        messageRequest.setType(MessagePublicRequest.MessageType.PUBLIC);

        // Enregistrement en base
        MessageResponse savedMessage = chatService.sendPublicMessage(
                messageRequest.getChatRoomId(),
                messageRequest
        );

        // Diffusion aux abonnés du salon
        messagingTemplate.convertAndSend(
                "/topic/room." + messageRequest.getChatRoomId(),
                savedMessage
        );
    }

    @MessageMapping("/chat/private")
    public void handlePrivateMessage(MessagePrivateRequest messageRequest, Principal principal) {
// 1. Validation
        if (principal == null) {
            throw new IllegalArgumentException("User not authenticated");
        }

        // 2. Récupération de l'expéditeur
        String username = principal.getName();
        User sender = userService.findByUsername(username);

        // 3. Enregistrement + envoi
        PrivateMessageResponse response = privateChatService.sendPrivateMessage(
                messageRequest.getReceiverId(),
                messageRequest, // Utilisez l'ID du Principal, pas celui du body
                principal
        );

        messagingTemplate.convertAndSendToUser(
                messageRequest.getReceiverId().toString(),
                "/queue/private",
                response
        );
    }
}