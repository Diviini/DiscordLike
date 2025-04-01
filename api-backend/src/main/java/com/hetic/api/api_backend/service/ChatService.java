package com.hetic.api.api_backend.service;

import com.hetic.api.api_backend.dto.request.MessageRequest;
import com.hetic.api.api_backend.dto.response.MessageResponse;
import com.hetic.api.api_backend.model.Message;
import com.hetic.api.api_backend.repository.MessageRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.time.LocalDateTime;
import java.util.List;

@Service
public class ChatService {

    @Autowired
    private MessageRepository messageRepository;

    public MessageResponse sendMessage(MessageRequest messageRequest) {
        Message message = new Message();
        message.setContent(messageRequest.getContent());
        message.setSenderId(messageRequest.getSenderId());
        message.setChatRoomId(messageRequest.getChatRoomId());
        message.setSentAt(LocalDateTime.now());

        Message savedMessage = messageRepository.save(message);

        return new MessageResponse(savedMessage.getId(), savedMessage.getContent(), savedMessage.getSenderId(), savedMessage.getSentAt());
    }

    public List<Message> getMessagesByChatRoomId(Long chatRoomId) {
        return messageRepository.findByChatRoomId(chatRoomId);
    }
}