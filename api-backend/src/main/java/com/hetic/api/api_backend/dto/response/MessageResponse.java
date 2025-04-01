package com.hetic.api.api_backend.dto.response;

import lombok.Getter;
import lombok.Setter;

import java.time.LocalDateTime;

public class MessageResponse {
    private Long id;
    private String content;
    private Long senderId;
    private LocalDateTime sentAt;

    public MessageResponse(Long id, String content, Long senderId, LocalDateTime sentAt) {
        this.id = id;
        this.content = content;
        this.senderId = senderId;
        this.sentAt = sentAt;
    }
}