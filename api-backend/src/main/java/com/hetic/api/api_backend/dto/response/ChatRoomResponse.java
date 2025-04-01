package com.hetic.api.api_backend.dto.response;

import java.time.LocalDateTime;

public class ChatRoomResponse {
    private Long id;
    private String name;
    private String createdByUsername;
    private LocalDateTime createdAt;

    // Constructeur, getters et setters

    public ChatRoomResponse(Long id, String name, String createdByUsername, LocalDateTime createdAt) {
        this.id = id;
        this.name = name;
        this.createdByUsername = createdByUsername;
        this.createdAt = createdAt;
    }

    // Getters et setters*

    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getCreatedByUsername() {
        return createdByUsername;
    }

    public void setCreatedByUsername(String createdByUsername) {
        this.createdByUsername = createdByUsername;
    }

    public LocalDateTime getCreatedAt() {
        return createdAt;
    }

    public void setCreatedAt(LocalDateTime createdAt) {
        this.createdAt = createdAt;
    }
}
