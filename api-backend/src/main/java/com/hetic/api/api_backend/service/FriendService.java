package com.hetic.api.api_backend.service;

import com.hetic.api.api_backend.dto.response.FriendRequestResponse;
import com.hetic.api.api_backend.dto.response.UserResponse;
import com.hetic.api.api_backend.model.FriendRequest;
import com.hetic.api.api_backend.model.User;
import com.hetic.api.api_backend.repository.FriendRequestRepository;
import com.hetic.api.api_backend.repository.UserRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.stream.Collectors;

@Service
public class FriendService {

    @Autowired
    private UserRepository userRepository;

    @Autowired
    private FriendRequestRepository friendRequestRepository;

    public List<UserResponse> getFriends(Long userId) {
        User user = userRepository.findById(userId).orElse(null);
        if (user != null) {
            return user.getFriends().stream()
                    .map(friend -> new UserResponse(friend.getId(), friend.getUsername(), friend.getEmail()))
                    .collect(Collectors.toList());
        }
        return List.of();
    }

    public List<FriendRequestResponse> getFriendRequests(Long userId) {
        List<FriendRequest> requests = friendRequestRepository.findByReceiverId(userId);
        return requests.stream()
                .map(request -> new FriendRequestResponse(
                        request.getId(),
                        request.getSender().getId(),
                        request.getSender().getUsername(),
                        request.getStatus(),
                        request.getCreatedAt()
                ))
                .collect(Collectors.toList());
    }

    public void sendFriendRequest(Long senderId, Long receiverId) {
        User sender = userRepository.findById(senderId).orElse(null);
        User receiver = userRepository.findById(receiverId).orElse(null);

        if (sender != null && receiver != null) {
            FriendRequest friendRequest = new FriendRequest();
            friendRequest.setSender(sender);
            friendRequest.setReceiver(receiver);
            friendRequest.setStatus("pending");

            friendRequestRepository.save(friendRequest);
        }
    }
}
