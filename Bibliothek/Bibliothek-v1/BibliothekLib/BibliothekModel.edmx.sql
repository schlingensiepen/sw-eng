
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/24/2019 20:07:26
-- Generated from EDMX file: F:\dev\svn\sweng\prj\Demo\WS2019\Bibliothek-v1\BibliothekLib\BibliothekModel.edmx
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
    ALTER TABLE [dbo].[MediaSet] DROP CONSTRAINT [FK_MediaWork];
GO
IF OBJECT_ID(N'[dbo].[FK_Loan]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MediaSet] DROP CONSTRAINT [FK_Loan];
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

IF OBJECT_ID(N'[dbo].[UserSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserSet];
GO
IF OBJECT_ID(N'[dbo].[WorkSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WorkSet];
GO
IF OBJECT_ID(N'[dbo].[MediaSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MediaSet];
GO
IF OBJECT_ID(N'[dbo].[UserWork]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserWork];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'UserSet'
CREATE TABLE [dbo].[UserSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Familyname] nvarchar(max)  NOT NULL,
    [Street] nvarchar(max)  NOT NULL,
    [City] nvarchar(max)  NOT NULL,
    [PostCode] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'WorkSet'
CREATE TABLE [dbo].[WorkSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Titel] nvarchar(max)  NOT NULL,
    [Author] nvarchar(max)  NOT NULL,
    [Verlag] nvarchar(max)  NOT NULL,
    [Standort] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'MediaSet'
CREATE TABLE [dbo].[MediaSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Number] nvarchar(max)  NOT NULL,
    [Work_Id] int  NOT NULL,
    [User_Id] int  NULL
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

-- Creating primary key on [Id] in table 'UserSet'
ALTER TABLE [dbo].[UserSet]
ADD CONSTRAINT [PK_UserSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'WorkSet'
ALTER TABLE [dbo].[WorkSet]
ADD CONSTRAINT [PK_WorkSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MediaSet'
ALTER TABLE [dbo].[MediaSet]
ADD CONSTRAINT [PK_MediaSet]
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

-- Creating foreign key on [Work_Id] in table 'MediaSet'
ALTER TABLE [dbo].[MediaSet]
ADD CONSTRAINT [FK_MediaWork]
    FOREIGN KEY ([Work_Id])
    REFERENCES [dbo].[WorkSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MediaWork'
CREATE INDEX [IX_FK_MediaWork]
ON [dbo].[MediaSet]
    ([Work_Id]);
GO

-- Creating foreign key on [User_Id] in table 'MediaSet'
ALTER TABLE [dbo].[MediaSet]
ADD CONSTRAINT [FK_Loan]
    FOREIGN KEY ([User_Id])
    REFERENCES [dbo].[UserSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Loan'
CREATE INDEX [IX_FK_Loan]
ON [dbo].[MediaSet]
    ([User_Id]);
GO

-- Creating foreign key on [Orderer_Id] in table 'UserWork'
ALTER TABLE [dbo].[UserWork]
ADD CONSTRAINT [FK_UserWork_User]
    FOREIGN KEY ([Orderer_Id])
    REFERENCES [dbo].[UserSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Orders_Id] in table 'UserWork'
ALTER TABLE [dbo].[UserWork]
ADD CONSTRAINT [FK_UserWork_Work]
    FOREIGN KEY ([Orders_Id])
    REFERENCES [dbo].[WorkSet]
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