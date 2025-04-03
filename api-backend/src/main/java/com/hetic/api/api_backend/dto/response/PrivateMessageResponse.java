package com.hetic.api.api_backend.dto.response;

import java.time.LocalDateTime;

public class PrivateMessageResponse {
    private Long id;
    private String content;
    private Long senderId;
    private Long receiverId;
    private LocalDateTime sentAt;

    public PrivateMessageResponse(Long id, String content, Long senderId, LocalDateTime sentAt, Long receiverId) {
        this.id = id;
        this.content = content;
        this.senderId = senderId;
        this.sentAt = sentAt;
        this.receiverId = receiverId;
    }

    public Long getReceiverId() {
        return receiverId;
    }

    public void setReceiverId(Long receiverId) {
        this.receiverId = receiverId;
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