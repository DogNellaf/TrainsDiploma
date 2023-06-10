INSERT INTO dbo.Role (Name, IsWorker, IsAdmin) VALUES ('Клиент', 0, 0);
INSERT INTO dbo.Role (Name, IsWorker, IsAdmin) VALUES ('Диспетчер', 1, 0);
INSERT INTO dbo.Role (Name, IsWorker, IsAdmin) VALUES ('Администратор', 1, 1);
INSERT INTO dbo."User" (Login, Token, Balance, RoleId) VALUES ('admin', '8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918', 5000, 3);
INSERT INTO City (Name) VALUES ('Москва');
INSERT INTO City (Name) VALUES ('Санкт-Петербург');