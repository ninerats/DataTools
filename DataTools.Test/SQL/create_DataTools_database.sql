/*
USE [master]
GO
CREATE DATABASE [DataTools]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DataTools', FILENAME = N'C:\Users\tony\DataTools.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'DataTools_log', FILENAME = N'C:\Users\tony\DataTools_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
*/


USE [master]
GO
-- create test login.
if not exists (select 1 from sys.syslogins where name = 'DataToolsSerTestLogin')
	CREATE LOGIN [DataToolsSerTestLogin] WITH PASSWORD=N'Password1', DEFAULT_DATABASE=[DataTools], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO
-- for SqlBulkQuery.
ALTER SERVER ROLE [bulkadmin] ADD MEMBER [DataToolsSerTestLogin]
GO


USE [DataTools]
GO

-- create test user
if not exists (select 1 from sys.sysusers where name = 'DataToolsSerTestUser')
	CREATE USER [DataToolsSerTestUser] FOR LOGIN [DataToolsSerTestLogin]
GO
GRANT ALTER TO [DataToolsSerTestUser]
ALTER ROLE [db_datareader] ADD MEMBER [DataToolsSerTestUser]
ALTER ROLE [db_datawriter] ADD MEMBER [DataToolsSerTestUser]
GO

if not exists (select 1 from sys.tables where name = 'job')
	CREATE TABLE [dbo].[Job](
		[JobID] [int] NOT NULL,
		[person] [varchar](50) NULL,
		[title] [varchar](50) NULL,
	 CONSTRAINT [PK_Job] PRIMARY KEY CLUSTERED 
	(
		[JobID] ASC
	))

if not exists (select 1 from sys.tables where name = 'person')
	CREATE TABLE [dbo].[Person](
		[person] [varchar](50) NOT NULL,
		[age] [int] NULL,
	 CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED 
	(
		[person] ASC
	))

if not exists (select 1 from sys.tables where name = 'Tools')
	CREATE TABLE [dbo].[Tools](
		[ToolID] [int] NOT NULL,
		[ToolName] [varchar](50) NULL,
		[Company] [varchar](50) NULL,
	 CONSTRAINT [PK_Tools] PRIMARY KEY CLUSTERED 
	(
		[ToolID] ASC
	))

GO
if not exists (select 1 from sys.foreign_keys  where name = 'FK_Job_Person')
	ALTER TABLE [dbo].[Job]  WITH CHECK ADD  CONSTRAINT [FK_Job_Person] FOREIGN KEY([person])
	REFERENCES [dbo].[Person] ([person])
GO
--if not exists (select 1 from sys.check_constraints  where name = 'FK_Job_Person')
ALTER TABLE [dbo].[Job] CHECK CONSTRAINT [FK_Job_Person]
GO
