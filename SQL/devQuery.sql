USE CS_DEV
select * from Users
select * from Roles
select * from Privileges

--Insert into Users ([Name], Email, [Password], CreationDate, Status) Values ('Admin', 'super@admin.com', 'admin', GETDATE(), 3)
--UPDATE Users SET [Password] = '8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918' where Id = 1
--INSERT INTO Roles (Name) Values ('admin')
--INSERT INTO Roles (Name, UserId) Values ('CS.Admin', 1)
--INSERT INTO Privileges (UserId,RoleId) Values (1,1)

select * from menu
select * from pages

--SET IDENTITY_INSERT Menu ON
--GO

--insert into menu (id, [Index],DisplayName) values (0, 0, 'MAIN')

--SET IDENTITY_INSERT Menu OFF

--insert into menu ([Index],ParentId,DisplayName) values (0, 3007, 'tralala')