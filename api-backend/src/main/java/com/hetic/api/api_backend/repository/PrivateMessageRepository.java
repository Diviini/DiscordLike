package com.hetic.api.api_backend.repository;

import com.hetic.api.api_backend.model.PrivateMessage;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.List;

public interface PrivateMessageRepository extends JpaRepository<PrivateMessage, Long> {
    List<PrivateMessage> findBySenderIdAndReceiverIdOrSenderIdAndReceiverId(Long senderId, Long receiverId, Long receiverId2, Long senderId2);
}
