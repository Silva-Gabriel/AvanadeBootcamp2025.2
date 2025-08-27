INSERT INTO TB_ACCESS_GROUP (ID, NAME) VALUES
(1, 'ADMIN'),
(2, 'USER');

-- Inserção de usuários com hashes novos (BCrypt, custo 12)
INSERT INTO TB_USERS (USERNAME, PASSWORD_HASH, ACTIVE, ACCESS_GROUP) VALUES 
('gabriel.silva', '$2y$10$y3pjM8kD.voTFMS0qatp7OmUTrDjlA5uiT3sPeo6uClgmIrDF/Cba', '1', 1), -- senha123
('jesus', '$2y$10$RiSoDDjdVWcoy12kh0A08e9vAd7ULqgkgFITFStzyz7J0lNDKi/r6', '1', 2); -- teste123