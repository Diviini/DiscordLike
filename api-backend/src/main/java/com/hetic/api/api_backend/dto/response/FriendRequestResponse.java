package com.hetic.api.api_backend.dto.response;

import java.time.LocalDateTime;

public class FriendRequestResponse {
    private Long id;
    private Long senderId;
    private String senderUsername;
    private String status;
    private LocalDateTime createdAt;

    public FriendRequestResponse(Long id, Long senderId, String senderUsername, String status, LocalDateTime createdAt) {
        this.id = id;
        this.senderId = senderId;
        this.senderUsername = senderUsername;
        this.status = status;
        this.createdAt = createdAt;
    }

    // Getters et setters

    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
    }

    public Long getSenderId() {
        return senderId;
    }

    public void setSenderId(Long senderId) {
        this.senderId = senderId;
    }

    public String getSenderUsername() {
        return senderUsername;
    }

    public void setSenderUsername(String senderUsername) {
        this.senderUsername = senderUsername;
    }

    public String getStatus() {
        return status;
    }

    public void setStatus(String status) {
        this.status = status;
    }

    public LocalDateTime getCreatedAt() {
        return createdAt;
    }

    public void setCreatedAt(LocalDateTime createdAt) {
        this.createdAt = createdAt;
    }
}
