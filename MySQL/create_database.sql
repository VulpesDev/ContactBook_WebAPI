-- Check if the database exists before creating it
CREATE DATABASE IF NOT EXISTS PhoneAddressData;

-- Use the PhoneAddressData database
USE PhoneAddressData;

-- Check if the Contacts table exists before creating it
CREATE TABLE IF NOT EXISTS Contacts (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    FullName VARCHAR(255) NOT NULL,
    HomeAddress VARCHAR(255) NOT NULL,
    BusinessAddress VARCHAR(255) NOT NULL,
    -- Add a unique constraint to prevent duplicate FullName values
    CONSTRAINT UC_FullName UNIQUE (FullName)
);

-- Check if the PhoneNumbers table exists before creating it
CREATE TABLE IF NOT EXISTS PhoneNumbers (
    PhoneNumberId INT AUTO_INCREMENT PRIMARY KEY,
    ContactId INT NOT NULL,
    PhoneNumber VARCHAR(20) NOT NULL,
    -- Add a unique constraint to prevent duplicate PhoneNumber values for each ContactId
    CONSTRAINT UC_ContactId_PhoneNumber UNIQUE (ContactId, PhoneNumber),
    FOREIGN KEY (ContactId) REFERENCES Contacts(Id)
);


-- Insert data into tables if they're empty
INSERT IGNORE INTO Contacts (FullName, HomeAddress, BusinessAddress) VALUES
('Hans Muller', 'Hauptstrase 1', 'Geschaftsstrase 2'),
('Anna Schmidt', 'Nebenstrase 3', 'Einkaufsstrase 4'),
('Thomas Fischer', 'Am Park 5', 'Industriestrase 6');

INSERT IGNORE INTO PhoneNumbers (ContactId, PhoneNumber) VALUES
(1, '+49 123 456789'),
(1, '+49 987 654321'),
(2, '+49 111 222333');
