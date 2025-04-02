package com.hetic.api.api_backend.model;

import jakarta.persistence.*;
import java.time.LocalDateTime;

@Entity
@Table(name = "bans")
public class Ban {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @ManyToOne
    @JoinColumn(name = "user_id")
    private User user;

    @ManyToOne
    @JoinColumn(name = "chat_room_id")
    private ChatRoom chatRoom;

    @ManyToOne
    @JoinColumn(name = "banned_by")
    private User bannedBy;

    @Column(name = "ban_reason", nullable = false)
    private String banReason;

    @Column(name = "banned_until")
    private LocalDateTime bannedUntil;

    // Getters and Setters

    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
    }

    public User getUser() {
        return user;
    }

    public void setUser(User user) {
        this.user = user;
    }

    public ChatRoom getChatRoom() {
        return chatRoom;
    }

    public void setChatRoom(ChatRoom chatRoom) {
        this.chatRoom = chatRoom;
    }

    public User getBannedBy() {
        return bannedBy;
    }

    public void setBannedBy(User bannedBy) {
        this.bannedBy = bannedBy;
    }

    public String getBanReason() {
        return banReason;
    }

    public void setBanReason(String banReason) {
        this.banReason = banReason;
    }

    public LocalDateTime getBannedUntil() {
        return bannedUntil;
    }

    public void setBannedUntil(LocalDateTime bannedUntil) {
        this.bannedUntil = bannedUntil;
    }
}
