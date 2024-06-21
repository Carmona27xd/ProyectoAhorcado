CREATE DATABASE ahorcado;

USE ahorcado;

CREATE TABLE Player (
    PlayerID INT IDENTITY(1,1) PRIMARY KEY,
    Email NVARCHAR(100) NOT NULL,
    Password NVARCHAR(100) NOT NULL,
    NickName NVARCHAR(50) NOT NULL,
    Names NVARCHAR(50) NOT NULL,
    FirstSurname NVARCHAR(50) NOT NULL,
    SecondSurname NVARCHAR(50) NOT NULL,
    BirthDate NVARCHAR(20) NOT NULL,
    PhoneNumber NVARCHAR(10) NOT NULL,
    PointsEarned INT
);

CREATE TABLE Match(
    MatchID INT IDENTITY(1,1) PRIMARY KEY, 
    WordID INT NOT NULL,
    DateMatch NVARCHAR(20) NOT NULL,
    WinnerID INT,
    ChallengerID INT NOT NULL,
    GuestID INT,
    StatusMatchID INT NOT NULL,
    SelectLetter CHAR,
    RemainingAttempts INT
    NickNameChallenger NVARCHAR(50),
    EmailChallenger NVARCHAR(50),
    WinnerNickname NVARCHAR(50)
);

CREATE TABLE Word(
    WordID INT IDENTITY(1,1) PRIMARY KEY,
    EnglishWord NVARCHAR(50) NOT NULL,
    SpanishWord NVARCHAR(50) NOT NULL,
    EnglishClue NVARCHAR(100) NOT NULL,
    SpanishClue NVARCHAR(100) NOT NULL,
    CategoryID INT NOT NULL
);

CREATE TABLE StatusMatch(
    StatusMatchID INT IDENTITY(1,1) PRIMARY KEY,
    StatusMatch NVARCHAR(20)
);

CREATE TABLE Category(
    CategoryID int IDENTITY(1,1) PRIMARY KEY,
    SpanishCategory NVARCHAR(50),
    EnglishCategory NVARCHAR(50)
);


INSERT INTO Player (Email, Password, NickName, Names, FirstSurname, SecondSurname, BirthDate, PhoneNumber, PointsEarned)
VALUES 
    ('ejemplo1@example.com', 'contrasena1', 'user1', 'Juan', 'Perez', 'Garcia', '1990-01-01', '123456789', 100),
    ('ejemplo2@example.com', 'contrasena2', 'user2', 'Maria', 'Lopez', 'Martinez', '1995-05-15', '987654321', 80),
    ('ejemplo3@example.com', 'contrasena3', 'user3', 'Pedro', 'Gomez', 'Sanchez', '1985-11-30', '555555555', 120);

INSERT INTO StatusMatch(StatusMatch) VALUES
    ('Created'),
    ('Abandoned'),
    ('Finished'),
    ('In progress');

INSERT INTO Category(SpanishCategory, EnglishCategory) VALUES 
    ('Animales', 'Animals'),
    ('Frutas', 'Fruits');

INSERT INTO Word(EnglishWord, SpanishWord, EnglishClue, SpanishClue, CategoryID) VALUES 
    ('Dog', 'Perro', 'Animal tha borks', 'Animal que ladra', 1),
    ('Apple', 'Manzana', 'Fruit red or green', 'Fruta verde o roja', 2);