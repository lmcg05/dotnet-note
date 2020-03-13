CREATE database if not exists datbase2;
use datbase2;
create table NoteBook(
Id int auto_increment,
Title varchar(200),
NoteId int,
primary key(Id));
create table Note(
Id int auto_increment,
NotebookId int,
Text varchar(200),
primary key(Id),
foreign key(NotebookId) references NoteBook(Id));

drop database datbase2;
Select * from Note;



