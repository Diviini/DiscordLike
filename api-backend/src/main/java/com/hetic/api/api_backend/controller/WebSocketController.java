package com.hetic.api.api_backend.controller;

import com.hetic.api.api_backend.dto.request.MessageRequest;
import com.hetic.api.api_backend.dto.response.MessageResponse;
import com.hetic.api.api_backend.service.ChatService;
import com.hetic.api.api_backend.service.PrivateChatService;
import com.hetic.api.api_backend.service.UserService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.messaging.handler.annotation.DestinationVariable;
import org.springframework.messaging.handler.annotation.MessageMapping;
import org.springframework.messaging.handler.annotation.SendTo;
import org.springframework.messaging.simp.SimpMessagingTemplate;
import org.springframework.stereotype.Controller;

import java.security.Principal;
import java.time.LocalDateTime;

@Controller
public class WebSocketController {

    @Autowired
    private SimpMessagingTemplate messagingTemplate;

    @Autowired
    private ChatService chatService;

    @Autowired
    private PrivateChatService privateChatService;

    @Autowired
    private UserService userService;

    @MessageMapping("/chat/public")
    public void handlePublicMessage(MessageRequest messageRequest, Principal principal) {
        System.out.println("Message public reçu de " + principal.getName() + ": " + messageRequest.toString());
        String username = principal.getName();
        Long userId = userService.getCurrentUserId(principal);

        messageRequest.setSenderId(userId);
        messageRequest.setType(MessageRequest.MessageType.PUBLIC);
        messageRequest.setTimestamp(LocalDateTime.now());

        // Enregistrement en base
        MessageResponse savedMessage = chatService.sendMessage(
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
    public void handlePrivateMessage(MessageRequest messageRequest, Principal principal) {
        System.out.println("Message privé reçu de " + principal.getName() + " à " + messageRequest.getReceiverId());
        String username = principal.getName();
        Long senderId = userService.getCurrentUserId(principal);

        messageRequest.setSenderId(senderId);
        messageRequest.setType(MessageRequest.MessageType.PRIVATE);
        messageRequest.setTimestamp(LocalDateTime.now());

        // Enregistrement en base
        MessageResponse savedMessage = privateChatService.sendPrivateMessage(
                messageRequest.getReceiverId(),
                messageRequest
        );

        // Envoi au destinataire
        messagingTemplate.convertAndSendToUser(
                messageRequest.getReceiverId().toString(),
                "/queue/private",
                savedMessage
        );

        // Copie à l'expéditeur
        messagingTemplate.convertAndSendToUser(
                senderId.toString(),
                "/queue/private",
                savedMessage
        );
    }

    @MessageMapping("/chat/typing")
    public void handleTypingNotification(MessageRequest messageRequest, Principal principal) {
        Long userId = userService.getCurrentUserId();
        messageRequest.setType(MessageRequest.MessageType.NOTIFICATION);
        messageRequest.setContent(principal.getName() + " is typing...");

        if (messageRequest.getChatRoomId() != null) {
            // Notification dans un salon public
            messagingTemplate.convertAndSend(
                    "/topic/room." + messageRequest.getChatRoomId() + ".typing",
                    messageRequest
            );
        } else if (messageRequest.getReceiverId() != null) {
            // Notification dans un chat privé
            messagingTemplate.convertAndSendToUser(
                    messageRequest.getReceiverId().toString(),
                    "/queue/typing",
                    messageRequest
            );
        }
    }
}