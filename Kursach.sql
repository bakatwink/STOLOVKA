create database FinLab
use FinLab
go

create table Users
(
	Id int identity(1,1) primary key,
	FirstName nvarchar(20) not null check(len(FirstName) > 0),
	LastName nvarchar(20) not null check(len(LastName) > 0),
	Email nvarchar(50) not null unique check(Email like '%@%.%'),
	PhoneNumber nvarchar(12) not null unique check(PhoneNumber like '+7[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
	Password nvarchar(max) not null,
	CreatedAt date not null default Getdate()
)

insert into Users(FirstName, LastName, Email, PhoneNumber, Password) values
('Николай','Ефимов','efim@mail.ru','+79804992999',dbo.PasswordCipher('qwerty')),
('Константин','Фалько','kons@mail.ru','+79804932999',dbo.PasswordCipher('zxcvbnm')),
('Максим','Пирожков','pirov@mail.ru','+79804432999',dbo.PasswordCipher('jfdje'))

select * from Transactions

create table TransactionTypes
(
	Id int identity(1,1) primary key,
	Title nvarchar(20) not null unique
)

insert into TransactionTypes(Title) values
('Продуктовый'),
('Аптека'),
('Шиномонтаж')


create table Transactions
(
	Id bigint identity(1,1) primary key,
	UserId int references Users(Id) on delete cascade not null,
	TypeId int references TransactionTypes(Id) on update cascade on delete no action not null,
	Cost money not null default 0,
	TransactionDate date not null check(TransactionDate <= getdate()) default getdate(),
	Description nvarchar(500) not null default ''
)

insert into Transactions(UserId, TypeId, Cost, TransactionDate, Description) values
(1,2,300,'2025-12-14','Покупка финозипама'),
(3,1,90,'2025-12-12','Покупка макарон'),
(1,3,100000,'2025-12-14','Починка двигателя')



-- Functions
go
create function PasswordCipher(@Password as nvarchar(50)) returns nvarchar(max) as
begin
	return hashbytes('SHA2_256', @Password+'salt')
end

go
create function VerifyUser(@Email as nvarchar(50), @Password as nvarchar(20), @PhoneNumber as nvarchar(12)) returns int as
begin
	if (select Email from Users) = @Email and dbo.PasswordCipher(@Password) = (select Password from Users)
	begin
		return (select Id from Users where (Email=@Email or @PhoneNumber=PhoneNumber) and dbo.PasswordCipher(@Password)=Password)
	end
	return -1
end


-- Procedures
go
create proc CreateUser
(@FirstName as nvarchar(20), @LastName as nvarchar(20), @Password as varchar(20),
@Email as nvarchar(50), @PhoneNumber as nvarchar(12)) as
begin
insert into Users(FirstName, LastName, Email, PhoneNumber, Password, CreatedAt) values 
(@FirstName, @LastName, @Email, @PhoneNumber, dbo.PasswordCipher(@Password), default);
end
exec CreateUser 'Константин', 'Чесник', 'qwerty', 'email@mail.com', '+79493994959'


go
create proc DeleteUser(@UserId as int) as delete from Users where @UserId=Id
exec DeleteUser 1


go
create proc CreateTransactionType(@Title as nvarchar(20)) as insert into TransactionTypes(Title) values (@Title);
exec CreateTransactionType 'Аптека'


go
create proc MakeTransaction(@UserId as int, @TypeId as int, @Cost as money, @TransactionDate as datetime, @Description as nvarchar(500)) as
insert into Transactions(UserId, TypeId, Cost, TransactionDate, Description) values
(@UserId, @TypeId, @Cost, @TransactionDate, @Description)
exec MakeTransaction 1, 1, 300, '2025-09-03', 'Купил Анауран'
