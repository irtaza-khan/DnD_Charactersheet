--User -> Character (One-to-Many)--
--CREATE TABLE Users (
--    UserID INT IDENTITY(1,1) PRIMARY KEY,
--    Username NVARCHAR(50) UNIQUE NOT NULL,
--    PasswordHash NVARCHAR(255) NOT NULL,
--    Role NVARCHAR(10) CHECK (Role IN ('Admin', 'User')) NOT NULL
--);

CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY,
	Name NVARCHAR(100) NOT NULL,
    Username NVARCHAR(100) UNIQUE NOT NULL,
    PasswordHash NVARCHAR(256) NOT NULL,
    Salt NVARCHAR(64) NOT NULL,  -- Store salt separately
    Role NVARCHAR(50) NOT NULL
);






CREATE TABLE Characters (
    CharacterID INT IDENTITY(1,1) PRIMARY KEY,
    UserID INT FOREIGN KEY REFERENCES Users(UserID) ON DELETE CASCADE,
    CharacterName NVARCHAR(100) NOT NULL,
    Class NVARCHAR(50) NOT NULL,
    Level INT CHECK (Level >= 1) NOT NULL,
    Race NVARCHAR(50) NOT NULL,
    Background NVARCHAR(100),
    ArmorClass INT CHECK (ArmorClass >= 0),
    Initiative INT,
    Speed INT CHECK (Speed >= 0),
    HitPointMax INT CHECK (HitPointMax >= 0),
    CurrentHitPoints INT CHECK (CurrentHitPoints >= 0),
    HitDice NVARCHAR(50),
    ProficiencyBonus INT CHECK (ProficiencyBonus >= 0),
    CharacterNotes NVARCHAR(MAX),
    FeaturesTraits NVARCHAR(MAX),
    Equipments NVARCHAR(MAX)  -- Free text field for items, weapons, gear
);


CREATE TABLE Abilities (
    AbilityID INT IDENTITY(1,1) PRIMARY KEY,
    AbilityName NVARCHAR(50) UNIQUE NOT NULL
);


--Character -> Abilities (Many-to-Many)--
CREATE TABLE Character_Abilities (
    CharacterID INT FOREIGN KEY REFERENCES Characters(CharacterID) ON DELETE CASCADE,
    AbilityID INT FOREIGN KEY REFERENCES Abilities(AbilityID) ON DELETE CASCADE,
    PRIMARY KEY (CharacterID, AbilityID)
);


CREATE TABLE Skills (
    SkillID INT IDENTITY(1,1) PRIMARY KEY,
    SkillName NVARCHAR(50) UNIQUE NOT NULL
);

--Character -> Skills (Many-to-Many)--
CREATE TABLE Character_Skills (
    CharacterID INT FOREIGN KEY REFERENCES Characters(CharacterID) ON DELETE CASCADE,
    SkillID INT FOREIGN KEY REFERENCES Skills(SkillID) ON DELETE CASCADE,
    HasProficiency BIT NOT NULL DEFAULT 0,  -- Boolean field (0 = No, 1 = Yes)
    PRIMARY KEY (CharacterID, SkillID)
);

select* from users

