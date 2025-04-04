package com.hetic.api.api_backend.controller;

import com.hetic.api.api_backend.service.ModerationService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.time.LocalDateTime;

@RestController
@RequestMapping("/moderation")
public class ModerationController {

    @Autowired
    private ModerationService moderationService;

    // Route pour bannir un utilisateur
    @PostMapping("/ban")
    public ResponseEntity<Void> banUser(
            @RequestParam Long userId,
            @RequestParam Long bannedById,
            @RequestParam Long chatRoomId,
            @RequestParam String banReason,
            @RequestParam LocalDateTime bannedUntil) {
        moderationService.banUser(userId, bannedById, chatRoomId, banReason, bannedUntil);
        return ResponseEntity.ok().build();
    }

    // Route pour mettre en sourdine un utilisateur
    @PostMapping("/mute")
    public ResponseEntity<Void> muteUser(
            @RequestParam Long userId,
            @RequestParam Long mutedById,
            @RequestParam Long chatRoomId,
            @RequestParam LocalDateTime mutedUntil) {
        moderationService.muteUser(userId, mutedById, chatRoomId, mutedUntil);
        return ResponseEntity.ok().build();
    }

    // Route pour lever un bannissement
    @DeleteMapping("/unban/{banId}")
    public ResponseEntity<Void> unbanUser(@PathVariable Long banId) {
        moderationService.unbanUser(banId);
        return ResponseEntity.ok().build();
    }

    // Route pour lever une mise en sourdine
    @DeleteMapping("/unmute/{muteId}")
    public ResponseEntity<Void> unmuteUser(@PathVariable Long muteId) {
        moderationService.unmuteUser(muteId);
        return ResponseEntity.ok().build();
    }
}
