-- Suppression des tables si elles existent (ordre respectant les clés étrangères)
DROP TABLE IF EXISTS friends, friend_requests, notifications, muted_users, bans, private_messages, messages, user_chat_room, chat_rooms, users, roles CASCADE;

-- Table des utilisateurs
CREATE TABLE IF NOT EXISTS users (
    id SERIAL PRIMARY KEY,
    username VARCHAR(50) UNIQUE NOT NULL,
    email VARCHAR(100) UNIQUE NOT NULL,
    password VARCHAR(255) NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Table des rôles (admin, modérateur, membre)
CREATE TABLE IF NOT EXISTS roles (
    id SERIAL PRIMARY KEY,
    name VARCHAR(50) UNIQUE NOT NULL
);

-- Table des salons de discussion
CREATE TABLE IF NOT EXISTS chat_rooms (
    id SERIAL PRIMARY KEY,
    name VARCHAR(100) UNIQUE NOT NULL,
    created_by INT REFERENCES users(id) ON DELETE CASCADE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Table des utilisateurs dans les salons avec rôle
CREATE TABLE IF NOT EXISTS user_chat_room (
    user_id INT REFERENCES users(id) ON DELETE CASCADE,
    chat_room_id INT REFERENCES chat_rooms(id) ON DELETE CASCADE,
    role_id INT REFERENCES roles(id),
    PRIMARY KEY (user_id, chat_room_id)
);

-- Table des messages publics
CREATE TABLE IF NOT EXISTS messages (
    id SERIAL PRIMARY KEY,
    sender_id INT REFERENCES users(id) ON DELETE CASCADE,
    chat_room_id INT REFERENCES chat_rooms(id) ON DELETE CASCADE,
    content TEXT NOT NULL,
    sent_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Table des messages privés
CREATE TABLE IF NOT EXISTS private_messages (
    id SERIAL PRIMARY KEY,
    sender_id INT REFERENCES users(id) ON DELETE CASCADE,
    receiver_id INT REFERENCES users(id) ON DELETE CASCADE,
    content TEXT NOT NULL,
    sent_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Table des bannissements
CREATE TABLE IF NOT EXISTS bans (
    id SERIAL PRIMARY KEY,
    user_id INT REFERENCES users(id) ON DELETE CASCADE,
    chat_room_id INT REFERENCES chat_rooms(id) ON DELETE CASCADE,
    banned_by INT REFERENCES users(id) ON DELETE SET NULL,
    ban_reason TEXT NOT NULL,
    banned_until TIMESTAMP
);

-- Table des utilisateurs mis en sourdine (mute)
CREATE TABLE IF NOT EXISTS muted_users (
    id SERIAL PRIMARY KEY,
    user_id INT REFERENCES users(id) ON DELETE CASCADE,
    chat_room_id INT REFERENCES chat_rooms(id) ON DELETE CASCADE,
    muted_by INT REFERENCES users(id) ON DELETE SET NULL,
    muted_until TIMESTAMP
);

-- Table des notifications
CREATE TABLE IF NOT EXISTS notifications (
    id SERIAL PRIMARY KEY,
    user_id INT REFERENCES users(id) ON DELETE CASCADE,
    type VARCHAR(50) NOT NULL,
    message TEXT NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Table des demandes d'amis
CREATE TABLE IF NOT EXISTS friend_requests (
    id SERIAL PRIMARY KEY,
    sender_id INT REFERENCES users(id) ON DELETE CASCADE,
    receiver_id INT REFERENCES users(id) ON DELETE CASCADE,
    status VARCHAR(20) CHECK (status IN ('pending', 'accepted', 'rejected')) NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Table des amis (après acceptation)
CREATE TABLE IF NOT EXISTS friends (
    user1_id INT REFERENCES users(id) ON DELETE CASCADE,
    user2_id INT REFERENCES users(id) ON DELETE CASCADE,
    PRIMARY KEY (user1_id, user2_id)
);