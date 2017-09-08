CREATE DATABASE Links;
Go

CREATE TABLE dbo.Links
(ID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
Link text NULL,
CheckedAt datetime null,
Problem text null
);
GO