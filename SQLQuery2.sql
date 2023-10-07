create database mds3
go
use mds3
go
Create table Formats(
FormatId int primary key identity(1,1),
FormatName nvarchar(100) not null
)
go
insert into Formats (FormatName) values ('Test')
insert into Formats (FormatName) values ('ODI')
insert into Formats (FormatName) values ('T20')
insert into Formats (FormatName) values ('T10')
insert into Formats (FormatName) values ('T5')
insert into Formats (FormatName) values ('T1')
go
create table Players(
PlayerId int primary key identity(1,1),
PlayerName nvarchar(100) not null,
BirthDate datetime not null,
Age int Not null,
Picture Nvarchar(max) not null,
MaritualStatus bit not null
)
go
create table SeriesEnty(
SeriesEntryId int primary key identity(1,1),
PlayerId int references Players(PlayerId),
FormatId int references Formats(FormatId)

)
go
