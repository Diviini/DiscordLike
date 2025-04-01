package com.hetic.api.api_backend.service;

import com.hetic.api.api_backend.dto.request.MessageRequest;
import com.hetic.api.api_backend.dto.response.MessageResponse;
import com.hetic.api.api_backend.model.PrivateMessage;
import com.hetic.api.api_backend.model.User;
import com.hetic.api.api_backend.repository.PrivateMessageRepository;
import com.hetic.api.api_backend.repository.UserRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.time.LocalDateTime;
import java.util.List;
import java.util.stream.Collectors;

@Service
public class PrivateChatService {

    @Autowired
    private PrivateMessageRepository privateMessageRepository;

    @Autowired
    private UserRepository userRepository;

    public MessageResponse sendPrivateMessage(Long receiverId, MessageRequest messageRequest) {
        User sender = userRepository.findById(messageRequest.getSenderId()).orElse(null);
        User receiver = userRepository.findById(receiverId).orElse(null);

        if (sender != null && receiver != null) {
            PrivateMessage privateMessage = new PrivateMessage();
            privateMessage.setSender(sender);
            privateMessage.setReceiver(receiver);
            privateMessage.setContent(messageRequest.getContent());
            privateMessage.setSentAt(LocalDateTime.now());

            PrivateMessage savedMessage = privateMessageRepository.save(privateMessage);

            return new MessageResponse(savedMessage.getId(), savedMessage.getContent(), sender.getId(), savedMessage.getSentAt());
        }
        return null;
    }

    public List<MessageResponse> getPrivateMessagesWithUser(Long userId, Long otherUserId) {
        List<PrivateMessage> messages = privateMessageRepository.findBySenderIdAndReceiverIdOrSenderIdAndReceiverId(userId, otherUserId, otherUserId, userId);
        return messages.stream()
                .map(message -> new MessageResponse(message.getId(), message.getContent(), message.getSender().getId(), message.getSentAt()))
                .collect(Collectors.toList());
    }
}
