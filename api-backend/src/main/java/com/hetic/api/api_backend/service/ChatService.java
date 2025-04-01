package com.hetic.api.api_backend.service;

import com.hetic.api.api_backend.dto.request.MessageRequest;
import com.hetic.api.api_backend.dto.response.ChatRoomResponse;
import com.hetic.api.api_backend.dto.response.MessageResponse;
import com.hetic.api.api_backend.model.ChatRoom;
import com.hetic.api.api_backend.model.Message;
import com.hetic.api.api_backend.model.User;
import com.hetic.api.api_backend.repository.ChatRoomRepository;
import com.hetic.api.api_backend.repository.MessageRepository;
import com.hetic.api.api_backend.repository.UserRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.time.LocalDateTime;
import java.util.List;
import java.util.stream.Collectors;

@Service
public class ChatService {

    @Autowired
    private ChatRoomRepository chatRoomRepository;
    @Autowired
    private MessageRepository messageRepository;
    @Autowired
    private UserRepository userRepository;


    public List<ChatRoomResponse> getAllChatRooms() {
        List<ChatRoom> chatRooms = chatRoomRepository.findAll();
        return chatRooms.stream()
                .map(chatRoom -> new ChatRoomResponse(
                        chatRoom.getId(),
                        chatRoom.getName(),
                        chatRoom.getCreatedBy().getUsername(),
                        chatRoom.getCreatedAt()
                ))
                .collect(Collectors.toList());
    }

    public ChatRoomResponse getChatRoomById(Long id) {
        ChatRoom chatRoom = chatRoomRepository.findById(id).orElse(null);
        if (chatRoom != null) {
            return new ChatRoomResponse(
                    chatRoom.getId(),
                    chatRoom.getName(),
                    chatRoom.getCreatedBy().getUsername(),
                    chatRoom.getCreatedAt()
            );
        }
        return null;
    }

    public List<MessageResponse> getMessagesByChatRoomId(Long chatRoomId) {
        List<Message> messages = messageRepository.findByChatRoomId(chatRoomId);
        return messages.stream()
                .map(message -> new MessageResponse(
                        message.getId(),
                        message.getContent(),
                        message.getSenderId(),
                        message.getSentAt()
                ))
                .collect(Collectors.toList());
    }

    public ChatRoom createChatRoom(String name, Long createdById) {
        User createdBy = userRepository.findById(createdById).orElse(null);
        if (createdBy != null) {
            ChatRoom chatRoom = new ChatRoom();
            chatRoom.setName(name);
            chatRoom.setCreatedBy(createdBy);
            chatRoom.setCreatedAt(LocalDateTime.now());
            return chatRoomRepository.save(chatRoom);
        }
        return null;
    }

    public MessageResponse sendMessage(Long chatRoomId, MessageRequest messageRequest) {
        ChatRoom chatRoom = chatRoomRepository.findById(chatRoomId).orElse(null);
        User sender = userRepository.findById(messageRequest.getSenderId()).orElse(null);

        if (chatRoom != null && sender != null) {
            Message message = new Message();
            message.setContent(messageRequest.getContent());
            message.setSender(sender);
            message.setChatRoom(chatRoom);
            message.setSentAt(LocalDateTime.now());

            Message savedMessage = messageRepository.save(message);

            return new MessageResponse(savedMessage.getId(), savedMessage.getContent(), sender.getId(), savedMessage.getSentAt());
        }
        return null;
    }

}
