package com.hetic.api.api_backend.repository;

import com.hetic.api.api_backend.model.Message;
import com.hetic.api.api_backend.model.User;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.List;

public interface MessageRepository extends JpaRepository<Message, Long> {
    List<Message> findByChatRoomId(Long chatRoomId);
}
