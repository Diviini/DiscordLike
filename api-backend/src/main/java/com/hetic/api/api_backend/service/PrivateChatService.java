package com.hetic.api.api_backend.service;

import com.fasterxml.jackson.annotation.JsonIgnore;
import com.hetic.api.api_backend.dto.request.MessagePrivateRequest;
import com.hetic.api.api_backend.dto.request.MessagePublicRequest;
import com.hetic.api.api_backend.dto.response.MessageResponse;
import com.hetic.api.api_backend.dto.response.PrivateMessageResponse;
import com.hetic.api.api_backend.model.Message;
import com.hetic.api.api_backend.model.PrivateMessage;
import com.hetic.api.api_backend.model.User;
import com.hetic.api.api_backend.repository.PrivateMessageRepository;
import com.hetic.api.api_backend.repository.UserRepository;
import jakarta.servlet.http.HttpServletRequest;
import jakarta.servlet.http.HttpSession;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.web.context.request.RequestContextHolder;
import org.springframework.web.context.request.ServletRequestAttributes;


import java.security.Principal;
import java.time.LocalDateTime;
import java.util.List;
import java.util.stream.Collectors;

@Service
public class PrivateChatService {

    @Autowired
    private PrivateMessageRepository privateMessageRepository;

    @Autowired
    private UserRepository userRepository;



    public PrivateMessageResponse sendPrivateMessage(Long receiverId, MessagePrivateRequest messageRequest, Principal principal) {
        // Récupère la session via la requête HTTP
        ServletRequestAttributes attributes = (ServletRequestAttributes) RequestContextHolder.getRequestAttributes();
        assert attributes != null;


        String username = principal.getName();
        User sender = userRepository.findByUsername(username); // Supposons que vous avez cette méthode
        User receiver = userRepository.findById(receiverId).orElse(null);

        if (sender != null && receiver != null) {
            PrivateMessage privateMessage = new PrivateMessage();
            privateMessage.setSender(sender);
            privateMessage.setReceiver(receiver);
            privateMessage.setContent(messageRequest.getContent());
            privateMessage.setSentAt(LocalDateTime.now());

            PrivateMessage savedMessage = privateMessageRepository.save(privateMessage);

            return new PrivateMessageResponse(savedMessage.getId(), savedMessage.getContent(), sender.getId(), savedMessage.getSentAt(), receiver.getId());
        }
        return null;
    }

    public List<PrivateMessageResponse> getPrivateMessagesWithUser(Long userId, Long otherUserId) {
        List<PrivateMessage> messages = privateMessageRepository.findBySenderIdAndReceiverIdOrSenderIdAndReceiverId(userId, otherUserId, otherUserId, userId);
        return messages.stream()
                .map(message -> new PrivateMessageResponse(
                        message.getId(),
                        message.getContent(),
                        message.getSender().getId(),
                        message.getSentAt(),
                        message.getReceiver().getId()))
                .collect(Collectors.toList());
    }

}
