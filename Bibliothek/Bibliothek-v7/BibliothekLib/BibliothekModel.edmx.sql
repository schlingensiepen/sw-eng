
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/23/2017 09:26:44
-- Generated from EDMX file: F:\dev\svn\sweng\prj\Demo\SS2017\main\cns\src\Bibliothek-v6\BibliothekLib\BibliothekModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Bibliothek];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_MediaWork]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Media] DROP CONSTRAINT [FK_MediaWork];
GO
IF OBJECT_ID(N'[dbo].[FK_Loan]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Media] DROP CONSTRAINT [FK_Loan];
GO
IF OBJECT_ID(N'[dbo].[FK_UserWork_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserWork] DROP CONSTRAINT [FK_UserWork_User];
GO
IF OBJECT_ID(N'[dbo].[FK_UserWork_Work]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserWork] DROP CONSTRAINT [FK_UserWork_Work];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[Works]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Works];
GO
IF OBJECT_ID(N'[dbo].[Media]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Media];
GO
IF OBJECT_ID(N'[dbo].[UserWork]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserWork];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NULL,
    [Familyname] nvarchar(max)  NULL,
    [Street] nvarchar(max)  NULL,
    [City] nvarchar(max)  NULL,
    [PostCode] nvarchar(max)  NULL,
    [EMail] nvarchar(max)  NOT NULL,
    [RoleCostumer] bit  NOT NULL,
    [RoleAdministrator] bit  NOT NULL,
    [RoleStaff] bit  NOT NULL
);
GO

-- Creating table 'Works'
CREATE TABLE [dbo].[Works] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Titel] nvarchar(max)  NOT NULL,
    [Author] nvarchar(max)  NOT NULL,
    [Verlag] nvarchar(max)  NOT NULL,
    [Standort] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Media'
CREATE TABLE [dbo].[Media] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Number] nvarchar(max)  NOT NULL,
    [WorkId] int  NOT NULL,
    [UserId] int  NULL
);
GO

-- Creating table 'UserWork'
CREATE TABLE [dbo].[UserWork] (
    [Orderer_Id] int  NOT NULL,
    [Orders_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Works'
ALTER TABLE [dbo].[Works]
ADD CONSTRAINT [PK_Works]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Media'
ALTER TABLE [dbo].[Media]
ADD CONSTRAINT [PK_Media]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Orderer_Id], [Orders_Id] in table 'UserWork'
ALTER TABLE [dbo].[UserWork]
ADD CONSTRAINT [PK_UserWork]
    PRIMARY KEY CLUSTERED ([Orderer_Id], [Orders_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [WorkId] in table 'Media'
ALTER TABLE [dbo].[Media]
ADD CONSTRAINT [FK_MediaWork]
    FOREIGN KEY ([WorkId])
    REFERENCES [dbo].[Works]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MediaWork'
CREATE INDEX [IX_FK_MediaWork]
ON [dbo].[Media]
    ([WorkId]);
GO

-- Creating foreign key on [UserId] in table 'Media'
ALTER TABLE [dbo].[Media]
ADD CONSTRAINT [FK_Loan]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Loan'
CREATE INDEX [IX_FK_Loan]
ON [dbo].[Media]
    ([UserId]);
GO

-- Creating foreign key on [Orderer_Id] in table 'UserWork'
ALTER TABLE [dbo].[UserWork]
ADD CONSTRAINT [FK_UserWork_User]
    FOREIGN KEY ([Orderer_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Orders_Id] in table 'UserWork'
ALTER TABLE [dbo].[UserWork]
ADD CONSTRAINT [FK_UserWork_Work]
    FOREIGN KEY ([Orders_Id])
    REFERENCES [dbo].[Works]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserWork_Work'
CREATE INDEX [IX_FK_UserWork_Work]
ON [dbo].[UserWork]
    ([Orders_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------