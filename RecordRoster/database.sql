-- Set up database & tables

CREATE DATABASE IF NOT EXISTS RecordRoster;
USE RecordRoster;

CREATE TABLE IF NOT EXISTS Album (
    ID INT AUTO_INCREMENT PRIMARY KEY,
    Title TEXT,
    ArtistName TEXT,
    ReleaseYear INT,
    Cover TEXT
);
ALTER TABLE Album ADD CONSTRAINT IF NOT EXISTS AlbumUniqueness UNIQUE (Title, ArtistName);

CREATE TABLE IF NOT EXISTS Song (
    AlbumID INT,
    Title TEXT,
    TrackNumber INT,
    FOREIGN KEY (AlbumID) REFERENCES Album(ID)
);
ALTER TABLE Song ADD CONSTRAINT IF NOT EXISTS TracklistIntegrity UNIQUE (AlbumID, TrackNumber);

-- Populate tables with some sample data

INSERT INTO Album (Title, ArtistName, ReleaseYear, Cover) VALUES
    ('Abbey Road', 'The Beatles', 1969, 'https://upload.wikimedia.org/wikipedia/en/4/42/Beatles_-_Abbey_Road.jpg'),
    ('The Dark Side of the Moon', 'Pink Floyd', 1973, 'https://upload.wikimedia.org/wikipedia/en/3/3b/Dark_Side_of_the_Moon.png'),
    ('Thriller', 'Michael Jackson', 1982, 'https://upload.wikimedia.org/wikipedia/en/5/55/Michael_Jackson_-_Thriller.png'),
    ('Led Zeppelin IV', 'Led Zeppelin', 1971, 'https://upload.wikimedia.org/wikipedia/en/2/26/Led_Zeppelin_-_Led_Zeppelin_IV.jpg'),
    ('Hotel California', 'Eagles', 1976, 'https://upload.wikimedia.org/wikipedia/en/4/49/Hotelcalifornia.jpg')
;

INSERT INTO Song (AlbumID, Title, TrackNumber) VALUES
    (1, 'Come Together', 1),
    (1, 'Something', 2),
    (1, 'Maxwell''s Silver Hammer', 3),
    (1, 'Oh! Darling', 4),
    (1, 'Octopus''s Garden', 5),
    (1, 'I Want You (She''s So Heavy)', 6),
    (1, 'Here Comes the Sun', 7),
    (1, 'Because', 8),
    (1, 'You Never Give Me Your Money', 9),
    (1, 'Sun King', 10),
    (1, 'Mean Mr. Mustard', 11),
    (1, 'Polythene Pam', 12),
    (1, 'She Came in Through the Bathroom Window', 13),
    (1, 'Golden Slumbers', 14),
    (1, 'Carry That Weight', 15),
    (1, 'The End', 16),
    (1, 'Her Majesty', 17),
    (2, 'Speak to Me', 1),
    (2, 'Breathe', 2),
    (2, 'On the Run', 3),
    (2, 'Time', 4),
    (2, 'The Great Gig in the Sky', 5),
    (2, 'Money', 6),
    (2, 'Us and Them', 7),
    (2, 'Any Colour You Like', 8),
    (2, 'Brain Damage', 9),
    (2, 'Eclipse', 10),
    (3, 'Wanna Be Startin'' Somethin''', 1),
    (3, 'Baby Be Mine', 2),
    (3, 'The Girl Is Mine', 3),
    (3, 'Thriller', 4),
    (3, 'Beat It', 5),
    (3, 'Billie Jean', 6),
    (3, 'Human Nature', 7),
    (3, 'P.Y.T. (Pretty Young Thing)', 8),
    (3, 'The Lady in My Life', 9),
    (4, 'Black Dog', 1),
    (4, 'Rock and Roll', 2),
    (4, 'The Battle of Evermore', 3),
    (4, 'Stairway to Heaven', 4),
    (4, 'Misty Mountain Hop', 5),
    (4, 'Four Sticks', 6),
    (4, 'Going to California', 7),
    (4, 'When the Levee Breaks', 8),
    (5, 'Hotel California', 1),
    (5, 'New Kid in Town', 2),
    (5, 'Life in the Fast Lane', 3),
    (5, 'Wasted Time', 4),
    (5, 'Wasted Time (Reprise)', 5),
    (5, 'Victim of Love', 6),
    (5, 'Pretty Maids All in a Row', 7),
    (5, 'Try and Love Again', 8),
    (5, 'The Last Resort', 9)
;

-- Create stored procedures

DELIMITER //

-- Album CRUD operations

CREATE PROCEDURE CreateAlbum(IN Title TEXT, IN ArtistName TEXT, IN ReleaseYear INT, IN Cover TEXT)
BEGIN
    INSERT INTO Album (Title, ArtistName, ReleaseYear, Cover) VALUES (Title, ArtistName, ReleaseYear, Cover);
END
//

CREATE PROCEDURE ReadAlbum(IN Title TEXT, IN ArtistName TEXT)
BEGIN
    SELECT * FROM Album WHERE Title = Title AND ArtistName = ArtistName;
END
//

CREATE PROCEDURE ReadAllAlbums()
BEGIN
    SELECT * FROM Album;
END
//

CREATE PROCEDURE UpdateAlbum(IN Title TEXT, IN ArtistName TEXT, IN ReleaseYear INT, IN Cover TEXT)
BEGIN
    UPDATE Album SET ReleaseYear = ReleaseYear, Cover = Cover WHERE Title = Title AND ArtistName = ArtistName;
END
//

CREATE PROCEDURE DeleteAlbum(IN Title TEXT, IN ArtistName TEXT)
BEGIN
    DELETE FROM Album WHERE Title = Title AND ArtistName = ArtistName;
END
//

-- Song CRUD operations

CREATE PROCEDURE CreateSong(IN AlbumID INT, IN Title TEXT, IN TrackNumber INT)
BEGIN
    INSERT INTO Song (AlbumID, Title, TrackNumber) VALUES (AlbumID, Title, TrackNumber);
END
//

CREATE PROCEDURE ReadSong(IN AlbumID INT, IN Title TEXT)
BEGIN
    SELECT * FROM Song WHERE AlbumID = AlbumID AND Title = Title;
END
//

CREATE PROCEDURE ReadAllSongs(IN AlbumID INT)
BEGIN
    SELECT * FROM Song WHERE AlbumID = AlbumID;
END
//

CREATE PROCEDURE UpdateSong(IN AlbumID INT, IN Title TEXT, IN TrackNumber INT)
BEGIN
    UPDATE Song SET TrackNumber = TrackNumber WHERE AlbumID = AlbumID AND Title = Title;
END
//

CREATE PROCEDURE DeleteSong(IN AlbumID INT, IN Title TEXT)
BEGIN
    DELETE FROM Song WHERE AlbumID = AlbumID AND Title = Title;
END
//

DELIMITER ;