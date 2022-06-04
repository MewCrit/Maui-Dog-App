USE [Doggo]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_create_new_dog]
	( @id nvarchar(50), @name nvarchar(50), @picture nvarchar(350), @breed nvarchar(30), @about nvarchar(300), @gender nvarchar(30), @birthday datetime)
AS
BEGIN
	SET NOCOUNT ON;
    INSERT INTO Dog ( id, name, picture, breed, about, gender, birthday,  date_created )
		VALUES      (@id, @name, @picture, @breed, @about, @gender, @birthday, GETDATE() );
END
GO

CREATE PROCEDURE [dbo].[sp_read_dog]
AS
BEGIN
	SET NOCOUNT ON;
    SELECT * FROM Dog
END
GO


CREATE PROCEDURE [dbo].[sp_read_dog_by_id]
@id nvarchar(100)
AS
BEGIN
	SET NOCOUNT ON;
    SELECT * FROM Dog WHERE id=@id
END
GO

