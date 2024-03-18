CREATE DATABASE WellDesignedAPI

CREATE TABLE Movies (
    MovieId INT IDENTITY(1,1) PRIMARY KEY,
    ReleaseDate DATE,
    Title NVARCHAR(500),
    Overview NVARCHAR(MAX),
    Popularity DECIMAL(10, 3),
    VoteCount INT,
    VoteAverage DECIMAL(3, 1),
    OriginalLanguage NVARCHAR(10),
    PosterUrl NVARCHAR(MAX)
);


CREATE TABLE Genres (
    GenreId INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) UNIQUE
);

CREATE TABLE MovieGenres (
    MovieGenreId INT IDENTITY(1,1) PRIMARY KEY,
    MovieId INT,
    GenreId INT,
    FOREIGN KEY (MovieId) REFERENCES Movies(MovieId),
    FOREIGN KEY (GenreId) REFERENCES Genres(GenreId)
);

--If needed:

--select * from Movies
--select * from Genres
--select * from MovieGenres


--DELETE FROM Movies
--DBCC CHECKIDENT ('[Movies]', RESEED, 0);

--DELETE FROM Genres
--DBCC CHECKIDENT ('[Genres]', RESEED, 0);


--DELETE FROM MovieGenres
--DBCC CHECKIDENT ('[MovieGenres]', RESEED, 0);

