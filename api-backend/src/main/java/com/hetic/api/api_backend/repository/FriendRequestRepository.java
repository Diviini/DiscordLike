package com.hetic.api.api_backend.repository;

import com.hetic.api.api_backend.model.FriendRequest;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.List;

public interface FriendRequestRepository extends JpaRepository<FriendRequest, Long> {
    List<FriendRequest> findByReceiverId(Long receiverId);
}
