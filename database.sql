-- ===============================
-- Base de datos: Rick & Morty
-- ===============================

-- Crear base de datos si no existe
CREATE DATABASE IF NOT EXISTS rickmortydb
  CHARACTER SET utf8mb4
  COLLATE utf8mb4_unicode_ci;

-- Usar la base de datos
USE rickmortydb;

-- ===============================
-- Tabla: Characters
-- ===============================

CREATE TABLE IF NOT EXISTS Characters (
    Id INT NOT NULL,
    Name VARCHAR(255) NOT NULL,
    Status VARCHAR(50) NOT NULL,
    Species VARCHAR(100) NOT NULL,
    CreatedAt DATETIME NOT NULL,
    PRIMARY KEY (Id)
);
