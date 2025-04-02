package com.hetic.api.api_backend.repository;

import com.hetic.api.api_backend.model.MutedUser;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.List;

public interface MutedUserRepository extends JpaRepository<MutedUser, Long> {
    List<MutedUser> findByUserId(Long userId);
    List<MutedUser> findByChatRoomId(Long chatRoomId);
}
