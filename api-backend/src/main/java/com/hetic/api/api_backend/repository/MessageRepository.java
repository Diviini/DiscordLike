package com.hetic.api.api_backend.repository;

import com.hetic.api.api_backend.model.Message;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
import java.util.List;

@Repository
public interface MessageRepository extends JpaRepository<Message, Long> {
    // Méthode pour récupérer tous les messages d'un salon spécifique
    List<Message> findByChatRoomId(Long chatRoomId);

    // Vous pouvez ajouter d'autres méthodes de requête personnalisées ici si nécessaire

}
