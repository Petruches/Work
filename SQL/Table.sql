SET ANSI_PADDING ON
GO 
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON 
GO 

--CREATE DATABASE [BuroPropyskov]

USE [BuroPropyskov]
GO

--ALTER TABLE [DBO].[Rolli] ADD Rolle CHAR(30) NULL
/*
CREATE TABLE [DBO].[Rolli]
(
[ID_role] INT NOT NULL IDENTITY(1,1),
[Rolle] CHAR(30) NOT NULL,
CONSTRAINT [PK_ID_role] Primary key clustered ([ID_role] ASC) on [PRIMARY]
)
*/
/*
CREATE TABLE [DBO].[Sotr]
(
[ID_sotr] INT NOT NULL IDENTITY(1,1),
[FIO_S] VARCHAR(MAX) NOT NULL,
[Log_S] CHAR(10) NOT NULL,
[Pass_S] CHAR(6) NOT NULL,
[Role_ID] INT NOT NULL,
[Dop_Inf] VARCHAR(MAX),
CONSTRAINT [PK_ID_sotr] Primary key clustered ([ID_sotr] ASC) on [PRIMARY],
CONSTRAINT [FK_Role_ID_role] FOREIGN KEY ([Role_ID]) REFERENCES [DBO].[Rolli]([ID_role]),
CONSTRAINT [UQ_Log_S] unique ([Log_S]) 
)
*/

--ALTER TABLE Sotr ADD CONSTRAINT fk_child_parent FOREIGN KEY (parent_id) REFERENCES parent(id);

/*
CREATE TABLE [DBO].[Posetitel]
(
[ID_Posetitel] INT NOT NULL IDENTITY(1,1),
[FIO_P] VARCHAR(MAX) NOT NULL,
[Nomer_and_Seria_Pasport] CHAR(12) NOT NULL,
CONSTRAINT [PK_ID_Posetitel] Primary key clustered ([ID_Posetitel] ASC) on [PRIMARY]
)
*/

/*
CREATE TABLE [DBO].[Zaivka]
(
[ID_zaivka] INT NOT NULL IDENTITY(1,1),
[Sotr_ID] INT NOT NULL,
[Data_oform] VARCHAR(11) NOT NULL,
[Posetitel_ID] INT NOT NULL,
[Nomer_and_Seria_Pasport] CHAR(12),
[Data_prihoda] VARCHAR(20) NOT NULL,
[Vremi_prihoda] VARCHAR(11) NOT NULL,
[Mesto_prihoda] VARCHAR(MAX) NOT NULL,
CONSTRAINT [PK_ID_zaivka] Primary key clustered ([ID_zaivka] ASC) on [PRIMARY],
CONSTRAINT [FK_Zaivka_Sotr_ID] FOREIGN KEY ([Sotr_ID]) REFERENCES [DBO].[Sotr]([ID_sotr]),
CONSTRAINT [FK_Zaivka_Posetitel_ID] FOREIGN KEY ([Posetitel_ID]) REFERENCES [DBO].[Posetitel]([ID_Posetitel])
)
*/
--ALTER TABLE [BuroPropyskov].[DBO].[Zaivka] DROP COLUMN Sotr_ID;

--Alter table Zaivka Alter Column Data_oform VARCHAR(20)

/*
CREATE TABLE [DBO].[Oformlennai_zaivka]
(
[ID_Ozaivka] INT NOT NULL IDENTITY(1,1),
[Mesto_Vidachi] CHAR(50) NOT NULL,
[Data_oform] VARCHAR(20) NOT NULL,
[Pos_ID] INT NOT NULL,
[Nomer_and_Seria_Pasport] CHAR(12) NOT NULL,
[Data_prihoda] VARCHAR(20) NOT NULL,
[Vremi_prihoda] VARCHAR(11) NOT NULL,
[Mesto_prihoda] VARCHAR(MAX) NOT NULL,
[S_ID] INT NOT NULL,
CONSTRAINT [PK_ID_Ozaivka] Primary key clustered ([ID_Ozaivka] ASC) on [PRIMARY],
CONSTRAINT [FK_Sotr_ID2] FOREIGN KEY ([S_ID]) REFERENCES [DBO].[Sotr]([ID_sotr]),
CONSTRAINT [FK_Posetitel_ID] FOREIGN KEY ([Pos_ID]) REFERENCES [DBO].[Posetitel]([ID_Posetitel])
)
*/

/*
CREATE TABLE [DBO].[Time_vhod]
(
[ID_vhod] INT NOT NULL IDENTITY(1,1),
[Sotr_ID] INT NOT NULL,
[FIO_S] VARCHAR(MAX) NOT NULL,
[Vrema] VARCHAR(MAX) NOT NULL,
CONSTRAINT [PK_ID_vhod] Primary key clustered ([ID_vhod] ASC) on [PRIMARY],
CONSTRAINT [FK_Sotr_ID] FOREIGN KEY ([Sotr_ID]) REFERENCES [DBO].[Sotr]([ID_sotr])
)
*/

--UPDATE Rolli SET Rolle = 'Администратор' Where ID_role = 1

--ALTER TABLE Sotr ADD UNIQUE (Log_S);