-- Database definitions

CREATE TABLE [dbo].[Albums] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [Title]       VARCHAR (MAX) NOT NULL,
    [Artist]      VARCHAR (MAX) NOT NULL,
    [ReleaseYear] INT           NOT NULL,
    [Cover]       VARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Songs] (
    [AlbumId]     INT           NOT NULL,
    [Title]       VARCHAR (MAX) NOT NULL,
    [TrackNumber] INT           NOT NULL
);

-- ALBUM PROCEDURES

GO CREATE PROCEDURE [dbo].[CreateAlbum]
	@title VARCHAR(MAX),
	@artist VARCHAR(MAX),
	@releaseYear INT,
	@cover VARCHAR(MAX)
AS
	INSERT INTO Albums (Title, Artist, ReleaseYear, Cover)
	VALUES (@title, @artist, @releaseYear, @cover);

GO CREATE PROCEDURE [dbo].[GetAlbum]
	@title VARCHAR(MAX),
	@artist VARCHAR(MAX)
AS
	SELECT * FROM Albums
	WHERE @title = Title AND @artist = Artist;

GO CREATE PROCEDURE [dbo].[GetAllAlbums]
AS
	SELECT * FROM Albums;

GO CREATE PROCEDURE [dbo].[UpdateAlbum]
	@id INT,
	@title VARCHAR(MAX),
	@artist VARCHAR(MAX),
	@releaseYear INT,
	@cover VARCHAR(MAX)
AS
	UPDATE Albums SET
		Albums.Title = @title,
		Albums.Artist = @artist,
		Albums.ReleaseYear = @releaseYear,
		Albums.Cover = @cover
	WHERE Albums.Id = @id;

GO CREATE PROCEDURE [dbo].[DeleteAlbum]
	@id INT
AS
	DELETE FROM Albums
	WHERE Albums.Id = @id;

-- SONG PROCEDURES

GO CREATE PROCEDURE [dbo].[CreateSong]
	@albumId VARCHAR(MAX),
	@title VARCHAR(MAX),
	@trackNumber INT
AS
	INSERT INTO Songs (AlbumId, Title, TrackNumber)
	VALUES (@albumId, @title, @trackNumber);

GO CREATE PROCEDURE [dbo].[GetSong]
	@albumId INT,
	@trackNumber INT
AS
	SELECT * FROM Songs
	WHERE Songs.AlbumId = @albumId AND Songs.TrackNumber = @trackNumber;

GO CREATE PROCEDURE [dbo].[GetAllSongsFromAlbum]
	@albumId INT
AS
	SELECT * FROM Songs
	WHERE Songs.AlbumId = @albumId;

GO CREATE PROCEDURE [dbo].[UpdateSong]
	@albumId INT,
	@title VARCHAR(MAX),
	@trackNumber INT
AS
	UPDATE Songs
	SET Songs.Title = @title, Songs.TrackNumber = @trackNumber
	WHERE Songs.AlbumId = @albumId;

GO CREATE PROCEDURE [dbo].[DeleteSong]
	@albumId VARCHAR(MAX),
	@trackNumber INT
AS
	DELETE FROM Songs
	WHERE Songs.AlbumId = @albumId AND Songs.TrackNumber = @trackNumber;