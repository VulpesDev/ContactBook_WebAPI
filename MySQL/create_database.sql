CREATE DATABASE IF NOT EXISTS PhoneAddressData;

USE PhoneAddressData;

CREATE TABLE IF NOT EXISTS Contacts (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    FullName VARCHAR(255) NOT NULL,
    HomeAddress VARCHAR(255) NOT NULL,
    BusinessAddress VARCHAR(255) NOT NULL
);

CREATE TABLE IF NOT EXISTS PhoneNumbers (
    ContactId INT NOT NULL,
    PhoneNumber VARCHAR(20) NOT NULL,
    FOREIGN KEY (ContactId) REFERENCES Contacts(Id)
);

-- Insert data into tables
INSERT INTO Contacts (FullName, HomeAddress, BusinessAddress) VALUES
('John Doe', '123 Main St', '456 Business Rd'),
('Jane Smith', '789 Elm St', '101 Park Ave'),
('Pop Ssarr', '144 Wop St', '001 Up Down');

INSERT INTO PhoneNumbers (ContactId, PhoneNumber) VALUES
(1, '555-1234'),
(1, '555-5678'),
(2, '555-9876');
