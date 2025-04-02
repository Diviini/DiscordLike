package com.hetic.api.api_backend.controller;

import com.hetic.api.api_backend.dto.request.MessageRequest;
import com.hetic.api.api_backend.dto.response.ChatRoomResponse;
import com.hetic.api.api_backend.dto.response.MessageResponse;
import com.hetic.api.api_backend.model.ChatRoom;
import com.hetic.api.api_backend.service.ChatService;
import com.hetic.api.api_backend.service.UserService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/chat")
public class ChatController {

    @Autowired
    private ChatService chatService;

    private final UserService userService;

    public ChatController(UserService userService) {
        this.userService = userService;
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
            @RequestBody MessageRequest messageRequest) {
        messageRequest.setSenderId(userService.getCurrentUserId());
        MessageResponse response = chatService.sendMessage(id, messageRequest);
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
