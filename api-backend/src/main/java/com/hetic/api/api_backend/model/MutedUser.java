package com.hetic.api.api_backend.model;

import jakarta.persistence.*;
import java.time.LocalDateTime;

@Entity
@Table(name = "muted_users")
public class MutedUser {

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
    @JoinColumn(name = "muted_by")
    private User mutedBy;

    @Column(name = "muted_until")
    private LocalDateTime mutedUntil;

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

    public User getMutedBy() {
        return mutedBy;
    }

    public void setMutedBy(User mutedBy) {
        this.mutedBy = mutedBy;
    }

    public LocalDateTime getMutedUntil() {
        return mutedUntil;
    }

    public void setMutedUntil(LocalDateTime mutedUntil) {
        this.mutedUntil = mutedUntil;
    }
}
