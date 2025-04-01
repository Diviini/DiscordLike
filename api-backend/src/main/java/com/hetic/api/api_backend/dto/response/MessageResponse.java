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

    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
    }

    public String getContent() {
        return content;
    }

    public void setContent(String content) {
        this.content = content;
    }

    public Long getSenderId() {
        return senderId;
    }

    public void setSenderId(Long senderId) {
        this.senderId = senderId;
    }

    public LocalDateTime getSentAt() {
        return sentAt;
    }

    public void setSentAt(LocalDateTime sentAt) {
        this.sentAt = sentAt;
    }
}