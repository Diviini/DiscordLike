package com.hetic.api.api_backend.controller;

import com.hetic.api.api_backend.dto.request.MessageRequest;
import com.hetic.api.api_backend.dto.response.MessageResponse;
import com.hetic.api.api_backend.service.ChatService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.messaging.handler.annotation.DestinationVariable;
import org.springframework.messaging.handler.annotation.MessageMapping;
import org.springframework.messaging.handler.annotation.Payload;
import org.springframework.messaging.handler.annotation.SendTo;
import org.springframework.stereotype.Controller;

@Controller
public class ChatController {

    @Autowired
    private ChatService chatService;

    @MessageMapping("/chat/{chatRoomId}/message")
    @SendTo("/topic/chat/{chatRoomId}/message")
    public MessageResponse sendMessage(@Payload MessageRequest messageRequest,
                                       @DestinationVariable Long chatRoomId) {
        messageRequest.setChatRoomId(chatRoomId);
        return chatService.sendMessage(messageRequest);
    }
}