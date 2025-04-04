package com.hetic.api.api_backend.repository;

import com.hetic.api.api_backend.model.Ban;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.List;

public interface BanRepository extends JpaRepository<Ban, Long> {
    List<Ban> findByUserId(Long userId);
    List<Ban> findByChatRoomId(Long chatRoomId);
}
