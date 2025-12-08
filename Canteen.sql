create database Canteen;
use Canteen;

create table Students
(
	Id int identity(1,1) primary key,
	FirstName nvarchar(30) not null,
	LastName nvarchar(30) not null,
	Patronymic nvarchar(30) not null	
)

create table Employees
(
	Id int identity(1,1) primary key,
	FirstName nvarchar(30) not null,
	LastName nvarchar(30) not null,
	Patronymic nvarchar(30) not null
)

create table Transactions
(
	StudentId int references Students(Id) on delete cascade on update cascade,
	EmployeeId int references Employees(Id) on delete cascade,
	TransactionDate date not null default getdate(),

)