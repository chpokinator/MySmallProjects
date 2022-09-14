use BlogsAppDB

create table Photo
(
    Id nvarchar(450) primary key,
    Photo nvarchar(MAX) not null
);

create table Blog(
    Id nvarchar(450) primary key,
    PreviewPhotoId nvarchar(450) foreign key references Photo(Id) on delete set null,
    Title nvarchar(150) not null,
    InnerText nvarchar(MAX) not null,
    AuthorId nvarchar(450) not null,
)