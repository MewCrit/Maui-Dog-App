CREATE DATABASE Doggo;
GO
USE Doggo;
GO

CREATE TABLE [dbo].[Dog](
	[id] [nvarchar](50) NOT NULL PRIMARY KEY,
	[name] [nvarchar](50) NULL,
	[picture] [nvarchar](350) NULL,
	[breed] [nvarchar](50) NULL,
	[about] [nvarchar](500) NULL,
	[gender] [nvarchar](20) NULL,
    [birthday] [datetime] NULL,
	[date_updated] [datetime] NULL,
	[date_created] [datetime] NULL, 
)
GO