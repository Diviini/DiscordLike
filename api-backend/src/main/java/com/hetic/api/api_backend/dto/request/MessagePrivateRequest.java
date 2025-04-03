package com.hetic.api.api_backend.dto.request;

import lombok.Getter;
import lombok.Setter;

import java.time.LocalDateTime;

@Getter
@Setter
public class MessagePrivateRequest {
    private Long senderId;
    private Long receiverId;
    private String content;
    private MessageType type; // Nouveau champ pour distinguer le type de message
    private LocalDateTime timestamp; // Nouveau champ

    public enum MessageType {
        PUBLIC, PRIVATE, NOTIFICATION, SYSTEM
    }

    public String getContent() {
        return content;
    }

    public Long getSenderId() {
        return senderId;
    }



    public void setSenderId(Long senderId) {
        this.senderId = senderId;
    }

    public void setContent(String content) {
        this.content = content;
    }

    public Long getReceiverId() {
        return receiverId;
    }

    public void setReceiverId(Long receiverId) {
        this.receiverId = receiverId;
    }

    public MessageType getType() {
        return type;
    }

    public void setType(MessageType type) {
        this.type = type;
    }

    public LocalDateTime getTimestamp() {
        return timestamp;
    }

    public void setTimestamp(LocalDateTime timestamp) {
        this.timestamp = timestamp;
    }

    @Override
    public String toString() {
        return "MessageRequest{" +
                "senderId=" + senderId +
                ", receiverId=" + receiverId +
                ", content='" + content + '\'' +
                ", type=" + type +
                ", timestamp=" + timestamp +
                '}';
    }
}