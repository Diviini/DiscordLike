-- Ajout de données fictives

-- Ajouter des utilisateurs
INSERT INTO users (username, email, password) VALUES
('Alice', 'alice@example.com', 'hashedpassword1'),
('Bob', 'bob@example.com', 'hashedpassword2'),
('Charlie', 'charlie@example.com', 'hashedpassword3'),
('David', 'david@example.com', 'hashedpassword4'),
('Emma', 'emma@example.com', 'hashedpassword5');

-- Ajouter des rôles
INSERT INTO roles (name) VALUES
('admin'), ('moderator'), ('member');

-- Ajouter des salons de discussion
INSERT INTO chat_rooms (name, created_by) VALUES
('Général', 1),
('Développement', 2),
('Gaming', 3);

-- Ajouter des utilisateurs à des salons avec rôles
INSERT INTO user_chat_room (user_id, chat_room_id, role_id) VALUES
(1, 1, 1), -- Alice admin du salon Général
(2, 1, 2), -- Bob modérateur du salon Général
(3, 1, 3), -- Charlie membre du salon Général
(4, 2, 1), -- David admin du salon Développement
(5, 3, 1); -- Emma admin du salon Gaming

-- Ajouter des messages publics
INSERT INTO messages (sender_id, chat_room_id, content) VALUES
(1, 1, 'Bienvenue dans le salon Général !'),
(2, 1, 'Salut tout le monde !'),
(3, 1, 'Hey, comment ça va ?'),
(4, 2, 'Qui bosse sur un projet en Spring Boot ?'),
(5, 3, 'Quelqu’un joue à Elden Ring ici ?');

-- Ajouter des messages privés
INSERT INTO private_messages (sender_id, receiver_id, content) VALUES
(1, 2, 'Salut Bob, ça va ?'),
(2, 1, 'Hey Alice, nickel et toi ?'),
(3, 5, 'Emma, on joue ce soir ?');

-- Ajouter des bannissements
INSERT INTO bans (user_id, chat_room_id, banned_by, ban_reason, banned_until) VALUES
(4, 1, 1, 'Spam', '2025-04-01');

-- Ajouter des utilisateurs mis en sourdine (mute)
INSERT INTO muted_users (user_id, chat_room_id, muted_by, muted_until) VALUES
(3, 1, 2, '2025-04-01 12:00:00');

-- Ajouter des notifications
INSERT INTO notifications (user_id, type, message) VALUES
(2, 'message', 'Vous avez reçu un nouveau message privé.'),
(3, 'ban', 'Vous avez été banni du salon Général.');

-- Ajouter des demandes d’amis
INSERT INTO friend_requests (sender_id, receiver_id, status) VALUES
(1, 2, 'pending'),
(3, 4, 'accepted'),
(5, 1, 'rejected');

-- Ajouter des amis confirmés
INSERT INTO friends (user1_id, user2_id) VALUES
(1, 5),
(3, 4);