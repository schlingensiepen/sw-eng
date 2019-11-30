
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/30/2019 16:49:59
-- Generated from EDMX file: C:\Work\sw-eng\StadtLandFluss\StadtLandFluss_05\StadtLandFlussLibDB\StadtLandFlussModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [StadtLandFluss];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_LandStadt]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[NamedObjects_Stadt] DROP CONSTRAINT [FK_LandStadt];
GO
IF OBJECT_ID(N'[dbo].[FK_StadtFluss_Stadt]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StadtFluss] DROP CONSTRAINT [FK_StadtFluss_Stadt];
GO
IF OBJECT_ID(N'[dbo].[FK_StadtFluss_Fluss]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StadtFluss] DROP CONSTRAINT [FK_StadtFluss_Fluss];
GO
IF OBJECT_ID(N'[dbo].[FK_StadtPerson]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[People] DROP CONSTRAINT [FK_StadtPerson];
GO
IF OBJECT_ID(N'[dbo].[FK_StadtPerson1_Stadt]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StadtPerson1] DROP CONSTRAINT [FK_StadtPerson1_Stadt];
GO
IF OBJECT_ID(N'[dbo].[FK_StadtPerson1_Person]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StadtPerson1] DROP CONSTRAINT [FK_StadtPerson1_Person];
GO
IF OBJECT_ID(N'[dbo].[FK_HauptStadt]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[NamedObjects_Land] DROP CONSTRAINT [FK_HauptStadt];
GO
IF OBJECT_ID(N'[dbo].[FK_Land_inherits_NamedObject]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[NamedObjects_Land] DROP CONSTRAINT [FK_Land_inherits_NamedObject];
GO
IF OBJECT_ID(N'[dbo].[FK_Stadt_inherits_NamedObject]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[NamedObjects_Stadt] DROP CONSTRAINT [FK_Stadt_inherits_NamedObject];
GO
IF OBJECT_ID(N'[dbo].[FK_Fluss_inherits_NamedObject]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[NamedObjects_Fluss] DROP CONSTRAINT [FK_Fluss_inherits_NamedObject];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[People]', 'U') IS NOT NULL
    DROP TABLE [dbo].[People];
GO
IF OBJECT_ID(N'[dbo].[NamedObjects]', 'U') IS NOT NULL
    DROP TABLE [dbo].[NamedObjects];
GO
IF OBJECT_ID(N'[dbo].[NamedObjects_Land]', 'U') IS NOT NULL
    DROP TABLE [dbo].[NamedObjects_Land];
GO
IF OBJECT_ID(N'[dbo].[NamedObjects_Stadt]', 'U') IS NOT NULL
    DROP TABLE [dbo].[NamedObjects_Stadt];
GO
IF OBJECT_ID(N'[dbo].[NamedObjects_Fluss]', 'U') IS NOT NULL
    DROP TABLE [dbo].[NamedObjects_Fluss];
GO
IF OBJECT_ID(N'[dbo].[StadtFluss]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StadtFluss];
GO
IF OBJECT_ID(N'[dbo].[StadtPerson1]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StadtPerson1];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'People'
CREATE TABLE [dbo].[People] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Vorname] nvarchar(max)  NOT NULL,
    [Familienname] nvarchar(max)  NOT NULL,
    [GeborenInId] int  NOT NULL
);
GO

-- Creating table 'NamedObjects'
CREATE TABLE [dbo].[NamedObjects] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'NamedObjects_Land'
CREATE TABLE [dbo].[NamedObjects_Land] (
    [HauptStadtId] int  NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'NamedObjects_Stadt'
CREATE TABLE [dbo].[NamedObjects_Stadt] (
    [LiegtInId] int  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'NamedObjects_Fluss'
CREATE TABLE [dbo].[NamedObjects_Fluss] (
    [Male] bit  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'StadtFluss'
CREATE TABLE [dbo].[StadtFluss] (
    [Durchfließt_Id] int  NOT NULL,
    [LiegtAn_Id] int  NOT NULL
);
GO

-- Creating table 'StadtPerson1'
CREATE TABLE [dbo].[StadtPerson1] (
    [WohntIn_Id] int  NOT NULL,
    [Einwohner_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'People'
ALTER TABLE [dbo].[People]
ADD CONSTRAINT [PK_People]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'NamedObjects'
ALTER TABLE [dbo].[NamedObjects]
ADD CONSTRAINT [PK_NamedObjects]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'NamedObjects_Land'
ALTER TABLE [dbo].[NamedObjects_Land]
ADD CONSTRAINT [PK_NamedObjects_Land]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'NamedObjects_Stadt'
ALTER TABLE [dbo].[NamedObjects_Stadt]
ADD CONSTRAINT [PK_NamedObjects_Stadt]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'NamedObjects_Fluss'
ALTER TABLE [dbo].[NamedObjects_Fluss]
ADD CONSTRAINT [PK_NamedObjects_Fluss]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Durchfließt_Id], [LiegtAn_Id] in table 'StadtFluss'
ALTER TABLE [dbo].[StadtFluss]
ADD CONSTRAINT [PK_StadtFluss]
    PRIMARY KEY CLUSTERED ([Durchfließt_Id], [LiegtAn_Id] ASC);
GO

-- Creating primary key on [WohntIn_Id], [Einwohner_Id] in table 'StadtPerson1'
ALTER TABLE [dbo].[StadtPerson1]
ADD CONSTRAINT [PK_StadtPerson1]
    PRIMARY KEY CLUSTERED ([WohntIn_Id], [Einwohner_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [LiegtInId] in table 'NamedObjects_Stadt'
ALTER TABLE [dbo].[NamedObjects_Stadt]
ADD CONSTRAINT [FK_LandStadt]
    FOREIGN KEY ([LiegtInId])
    REFERENCES [dbo].[NamedObjects_Land]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LandStadt'
CREATE INDEX [IX_FK_LandStadt]
ON [dbo].[NamedObjects_Stadt]
    ([LiegtInId]);
GO

-- Creating foreign key on [Durchfließt_Id] in table 'StadtFluss'
ALTER TABLE [dbo].[StadtFluss]
ADD CONSTRAINT [FK_StadtFluss_Stadt]
    FOREIGN KEY ([Durchfließt_Id])
    REFERENCES [dbo].[NamedObjects_Stadt]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [LiegtAn_Id] in table 'StadtFluss'
ALTER TABLE [dbo].[StadtFluss]
ADD CONSTRAINT [FK_StadtFluss_Fluss]
    FOREIGN KEY ([LiegtAn_Id])
    REFERENCES [dbo].[NamedObjects_Fluss]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StadtFluss_Fluss'
CREATE INDEX [IX_FK_StadtFluss_Fluss]
ON [dbo].[StadtFluss]
    ([LiegtAn_Id]);
GO

-- Creating foreign key on [GeborenInId] in table 'People'
ALTER TABLE [dbo].[People]
ADD CONSTRAINT [FK_StadtPerson]
    FOREIGN KEY ([GeborenInId])
    REFERENCES [dbo].[NamedObjects_Stadt]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StadtPerson'
CREATE INDEX [IX_FK_StadtPerson]
ON [dbo].[People]
    ([GeborenInId]);
GO

-- Creating foreign key on [WohntIn_Id] in table 'StadtPerson1'
ALTER TABLE [dbo].[StadtPerson1]
ADD CONSTRAINT [FK_StadtPerson1_Stadt]
    FOREIGN KEY ([WohntIn_Id])
    REFERENCES [dbo].[NamedObjects_Stadt]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Einwohner_Id] in table 'StadtPerson1'
ALTER TABLE [dbo].[StadtPerson1]
ADD CONSTRAINT [FK_StadtPerson1_Person]
    FOREIGN KEY ([Einwohner_Id])
    REFERENCES [dbo].[People]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StadtPerson1_Person'
CREATE INDEX [IX_FK_StadtPerson1_Person]
ON [dbo].[StadtPerson1]
    ([Einwohner_Id]);
GO

-- Creating foreign key on [HauptStadtId] in table 'NamedObjects_Land'
ALTER TABLE [dbo].[NamedObjects_Land]
ADD CONSTRAINT [FK_HauptStadt]
    FOREIGN KEY ([HauptStadtId])
    REFERENCES [dbo].[NamedObjects_Stadt]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_HauptStadt'
CREATE INDEX [IX_FK_HauptStadt]
ON [dbo].[NamedObjects_Land]
    ([HauptStadtId]);
GO

-- Creating foreign key on [Id] in table 'NamedObjects_Land'
ALTER TABLE [dbo].[NamedObjects_Land]
ADD CONSTRAINT [FK_Land_inherits_NamedObject]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[NamedObjects]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'NamedObjects_Stadt'
ALTER TABLE [dbo].[NamedObjects_Stadt]
ADD CONSTRAINT [FK_Stadt_inherits_NamedObject]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[NamedObjects]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'NamedObjects_Fluss'
ALTER TABLE [dbo].[NamedObjects_Fluss]
ADD CONSTRAINT [FK_Fluss_inherits_NamedObject]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[NamedObjects]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------