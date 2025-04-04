package com.hetic.api.api_backend.controller;

import com.fasterxml.jackson.annotation.JsonIgnore;
import com.hetic.api.api_backend.dto.request.MessagePublicRequest;
import com.hetic.api.api_backend.dto.response.ChatRoomResponse;
import com.hetic.api.api_backend.dto.response.MessageResponse;
import com.hetic.api.api_backend.model.ChatRoom;
import com.hetic.api.api_backend.service.ChatService;
import com.hetic.api.api_backend.service.UserService;
import jakarta.servlet.http.HttpSession;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/chat")
public class ChatController {

    @JsonIgnore
    private HttpSession session;

    @Autowired
    private ChatService chatService;

    private final UserService userService;


    public ChatController(UserService userService, HttpSession session) {
        this.userService = userService;
        this.session = session;
    }

    public HttpSession getSession() {
        return session;
    }


    @GetMapping
    public ResponseEntity<List<ChatRoomResponse>> getAllChatRooms() {
        List<ChatRoomResponse> chatRooms = chatService.getAllChatRooms();
        return ResponseEntity.ok(chatRooms);
    }

    // Route pour créer un salon
    @PostMapping
    public ResponseEntity<ChatRoom> createChatRoom(@RequestBody ChatRoom chatRoom) {
        ChatRoom createdChatRoom = chatService.createChatRoom(chatRoom.getName(), chatRoom.getCreatedBy().getId());
        return ResponseEntity.status(201).body(createdChatRoom);
    }

    // Route pour envoyer un message public
    @PostMapping("/{id}/message")
    public ResponseEntity<MessageResponse> sendMessage(
            @PathVariable Long id,
            @RequestBody MessagePublicRequest messageRequest) {

        Long userId = (Long) this.getSession().getAttribute("userId");


        messageRequest.setSenderId(userId);
        MessageResponse response = chatService.sendPublicMessage(id, messageRequest);
        if (response != null) {
            return ResponseEntity.ok(response);
        } else {
            return ResponseEntity.status(404).build();
        }
    }

    @GetMapping("/{id}")
    public ResponseEntity<ChatRoomResponse> getChatRoomById(@PathVariable Long id) {
        ChatRoomResponse chatRoom = chatService.getChatRoomById(id);
        return ResponseEntity.ok(chatRoom);
    }

    @GetMapping("/{id}/message")
    public ResponseEntity<List<MessageResponse>> getMessagesByChatRoomId(@PathVariable Long id) {
        List<MessageResponse> messages = chatService.getMessagesByChatRoomId(id);
        return ResponseEntity.ok(messages);
    }
}
