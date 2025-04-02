package com.hetic.api.api_backend.repository;

import com.hetic.api.api_backend.model.ChatRoom;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.List;

public interface ChatRoomRepository extends JpaRepository<ChatRoom, Long> {
    List<ChatRoom> findAll();
    ChatRoom findByName(String name);
}
