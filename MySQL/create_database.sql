-- Check if the database exists before creating it
CREATE DATABASE IF NOT EXISTS PhoneAddressData;

-- Use the PhoneAddressData database
USE PhoneAddressData;

-- Check if the Contacts table exists before creating it
CREATE TABLE IF NOT EXISTS Contacts (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    FullName VARCHAR(255) NOT NULL
);

CREATE TABLE IF NOT EXISTS Addresses (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Addr VARCHAR(255) NOT NULL,
    IsBusinessAddress BOOLEAN NOT NULL DEFAULT FALSE
);

-- Junction table
CREATE TABLE IF NOT EXISTS ContactAddresses (
    ContactId INT NOT NULL,
    AddressId INT NOT NULL,
    PRIMARY KEY (ContactId, AddressId),
    FOREIGN KEY (ContactId) REFERENCES Contacts(Id),
    FOREIGN KEY (AddressId) REFERENCES Addresses(Id)
);

-- Check if the PhoneNumbers table exists before creating it
CREATE TABLE IF NOT EXISTS PhoneNumbers (
    PhoneNumberId INT AUTO_INCREMENT PRIMARY KEY,
    AddressId INT NOT NULL,
    PhoneNumber VARCHAR(20) NOT NULL,
    -- Add a unique constraint to prevent duplicate PhoneNumber values for each AddressId
    CONSTRAINT UC_AddressId_PhoneNumber UNIQUE (AddressId, PhoneNumber),
    FOREIGN KEY (AddressId) REFERENCES Addresses(Id)
);


INSERT IGNORE INTO Addresses (Addr, IsBusinessAddress) VALUES
('Hauptstrase 1', FALSE),
('Geschaftsstrase 2', TRUE),
('Nebenstrase 3', FALSE),
('Einkaufsstrase 4', TRUE),
('Am Park 5', FALSE),
('Industriestrase 6', TRUE);

INSERT IGNORE INTO Contacts (FullName) VALUES
('Hans Muller'),
('Anna Schmidt'),
('Thomas Fischer');

INSERT IGNORE INTO ContactAddresses (ContactId, AddressId) VALUES
(1, 1),
(1, 2),
(2, 3),
(2, 4),
(3, 5),
(3, 6);

INSERT IGNORE INTO PhoneNumbers (AddressId, PhoneNumber) VALUES
(1, '+49 123 456789'),
(1, '+49 987 654321'),
(2, '+49 111 222333');
