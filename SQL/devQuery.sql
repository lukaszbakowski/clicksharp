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

--update menu set ParentId = null where id = 0

SELECT child.id AS child_id,
     parent.id AS parent_id
FROM menu child
JOIN menu parent
  ON child.ParentId = parent.id
WHERE child.id = 3012;

IF 1=0
BEGIN
    ;WITH generation AS (
        SELECT Id
        ,[Index]
        ,ParentId
        ,PageId
        ,DisplayName,
            0 AS generation_number
        FROM menu
        WHERE ParentId = 0
 
    UNION ALL
 
        SELECT child.Id
        ,child.[Index]
        ,child.ParentId
        ,child.PageId
        ,child.DisplayName,
            generation_number+1 AS generation_number
        FROM menu child
        JOIN generation g
          ON g.id = child.ParentId
    )
 
    SELECT Id
        ,[Index]
        ,ParentId
        ,PageId
        ,DisplayName,
        generation_number
    FROM generation
    WHERE Id = 3003
END
ELSE
BEGIN
    SELECT *, 0 FROM menu where id = 0
END


--update menu set ParentId = 0 where (ParentId != 0 OR ParentId IS NULL) AND id != 0
DECLARE @cnt int
set @cnt = 0

WHILE (@cnt < 12)
BEGIN
    
    ;WITH toUpd AS (
        SELECT * FROM menu order by Id OFFSET @cnt+1 ROWS FETCH FIRST 1 ROWS ONLY
    )
    --update m set [index] = (select @cnt) from toUpd m

    select 1 as temp from toUpd
    set @cnt = @cnt+1
END
