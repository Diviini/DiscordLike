package com.hetic.api.api_backend.service;

import com.hetic.api.api_backend.model.Ban;
import com.hetic.api.api_backend.model.ChatRoom;
import com.hetic.api.api_backend.model.MutedUser;
import com.hetic.api.api_backend.model.User;
import com.hetic.api.api_backend.repository.BanRepository;
import com.hetic.api.api_backend.repository.ChatRoomRepository;
import com.hetic.api.api_backend.repository.MutedUserRepository;
import com.hetic.api.api_backend.repository.UserRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.time.LocalDateTime;

@Service
public class ModerationService {

    @Autowired
    private BanRepository banRepository;

    @Autowired
    private MutedUserRepository mutedUserRepository;

    @Autowired
    private UserRepository userRepository;

    @Autowired
    private ChatRoomRepository chatRoomRepository;

    public void banUser(Long userId, Long bannedById, Long chatRoomId, String banReason, LocalDateTime bannedUntil) {
        User user = userRepository.findById(userId).orElse(null);
        User bannedBy = userRepository.findById(bannedById).orElse(null);
        ChatRoom chatRoom = chatRoomRepository.findById(chatRoomId).orElse(null);

        if (user != null && bannedBy != null && chatRoom != null) {
            Ban ban = new Ban();
            ban.setUser(user);
            ban.setBannedBy(bannedBy);
            ban.setChatRoom(chatRoom);
            ban.setBanReason(banReason);
            ban.setBannedUntil(bannedUntil);

            banRepository.save(ban);
        }
    }

    public void muteUser(Long userId, Long mutedById, Long chatRoomId, LocalDateTime mutedUntil) {
        User user = userRepository.findById(userId).orElse(null);
        User mutedBy = userRepository.findById(mutedById).orElse(null);
        ChatRoom chatRoom = chatRoomRepository.findById(chatRoomId).orElse(null);

        if (user != null && mutedBy != null && chatRoom != null) {
            MutedUser mutedUser = new MutedUser();
            mutedUser.setUser(user);
            mutedUser.setMutedBy(mutedBy);
            mutedUser.setChatRoom(chatRoom);
            mutedUser.setMutedUntil(mutedUntil);

            mutedUserRepository.save(mutedUser);
        }
    }

    public void unbanUser(Long banId) {
        banRepository.deleteById(banId);
    }

    public void unmuteUser(Long muteId) {
        mutedUserRepository.deleteById(muteId);
    }
}
