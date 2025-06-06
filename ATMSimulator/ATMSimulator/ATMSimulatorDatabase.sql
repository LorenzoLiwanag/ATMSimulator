DROP DATABASE IF EXISTS ATMSimulator;

CREATE DATABASE ATMSimulator;

USE ATMSimulator;

CREATE TABLE Users (
    UserID INT PRIMARY KEY AUTO_INCREMENT,
    FirstName VARCHAR(50),
    LastName VARCHAR(50),
    CardNumber INT,
    Pin INT
);

CREATE TABLE Accounts (
    AccountID INT PRIMARY KEY AUTO_INCREMENT,
    UserID INT,
    Balance DECIMAL(10, 2),
    FOREIGN KEY (UserID) REFERENCES Users (UserID)
);

CREATE TABLE Transactions (
    TransactionID INT PRIMARY KEY AUTO_INCREMENT,
    TransactionType VARCHAR(50),
    Amount DECIMAL(10, 2),
    TransactionDate DATETIME,
    UserID INT,
    AccountID INT,
    FOREIGN KEY (UserID) REFERENCES Users (UserID),
    FOREIGN KEY (AccountID) REFERENCES Accounts (AccountID)
);

-- Insert 5 sample users
INSERT INTO
    Users (
        FirstName,
        LastName,
        CardNumber,
        Pin
    )
VALUES (
        'Alice',
        'Johnson',
        12345678,
        1234
    ),
    (
        'Bob',
        'Smith',
        23456789,
        2345
    ),
    (
        'Carol',
        'Davis',
        34567890,
        3456
    ),
    (
        'David',
        'Lee',
        45678901,
        4567
    ),
    (
        'Eve',
        'Brown',
        56789012,
        5678
    );

-- Insert sample accounts for users with UserID 1 to 5
INSERT INTO
    Accounts (UserID, Balance)
VALUES (1, 1000.00),
    (2, 2500.50),
    (3, 150.75),
    (4, 3200.00),
    (5, 500.25);