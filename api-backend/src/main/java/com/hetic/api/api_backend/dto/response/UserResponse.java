package com.hetic.api.api_backend.dto.response;

import java.util.Set;

public class UserResponse {
    private Long id;
    private String username;
    private String email;
    private Set<Long> friendIds; // Utilisez les IDs des amis pour éviter les boucles infinies

    public UserResponse(Long id, String username, String email) {
        this.id = id;
        this.username = username;
        this.email = email;
    }

    // Getters et setters

    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
    }

    public String getUsername() {
        return username;
    }

    public void setUsername(String username) {
        this.username = username;
    }

    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public Set<Long> getFriendIds() {
        return friendIds;
    }

    public void setFriendIds(Set<Long> friendIds) {
        this.friendIds = friendIds;
    }
}
