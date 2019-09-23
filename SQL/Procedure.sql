USE [BuroPropyskov]

SET QUOTED_IDENTIFIER ON
GO

--ALTER TABLE Sotr 
--ALTER COLUMN FIO_S VARCHAR(MAX) NOT NULL

/*
--Добавление(Сотрудник)
CREATE PROCEDURE [DBO].[sotr_insertinto]
@FIO_S varchar(MAX),
@Log_S varchar(15),
@Pass_S varchar(15),
@Role_ID int,
@Dop_Inf VARCHAR(50)
AS
INSERT INTO [DBO].[Sotr] (FIO_S, Log_S, Pass_S, Role_ID, Dop_Inf) VALUES (@FIO_S, @Log_S, @Pass_S, @Role_ID, @Dop_Inf)
GO

--ALTER TABLE Sotr 
--ALTER COLUMN FIO_S VARCHAR(MAX) NOT NULL


--Обновление
CREATE PROCEDURE [DBO].[sotr_update]
@FIO_S varchar(MAX),
@Log_S varchar(15),
@Pass_S varchar(15),
@Role_ID int,
@Dop_Inf VARCHAR(50)
AS
UPDATE [DBO].[sotr] SET FIO_S = @FIO_S, 
				        Log_S = @Log_S, 
						Pass_S = @Pass_S, 
						Role_ID = @Role_ID,
						Dop_Inf = @Dop_Inf
GO

--Удаление
CREATE PROCEDURE [DBO].[sotr_delete]
@ID_sotr int
AS
DELETE FROM [DBO].[Sotr] WHERE ID_sotr = @ID_sotr
GO
*/
/*
--Добавление(Роль)
CREATE PROCEDURE [DBO].[role_insertinto]
@Rolle varchar(30)
AS
INSERT INTO [DBO].[Rolli] (Rolle) VALUES (@Rolle)
GO

--Обновление
CREATE PROCEDURE [DBO].[role_update]
@Rolle varchar(30)
AS
UPDATE [DBO].[Rolli] SET Rolle = @Rolle
GO

--Удаление
CREATE PROCEDURE [DBO].[role_delete]
@ID_role int
AS
DELETE FROM [DBO].[Rolli] WHERE ID_role = @ID_role
GO
*/
/*
--Добавление(Посетитель)
CREATE PROCEDURE [DBO].[Posetitel_insertinto]
@FIO_P varchar(MAX),
@Nomer_and_Seria_Pasport varchar(12)
AS
INSERT INTO [DBO].[Posetitel] (FIO_P, Nomer_and_Seria_Pasport) VALUES (@FIO_P, @Nomer_and_Seria_Pasport)
GO

--Обновление
CREATE PROCEDURE [DBO].[Posetitel_update]
@FIO_P varchar(MAX),
@Nomer_and_Seria_Pasport varchar(12)
AS
UPDATE [DBO].[Posetitel] SET FIO_P = @FIO_P, 
							 Nomer_and_Seria_Pasport = @Nomer_and_Seria_Pasport
GO

--ALTER TABLE Posetitel 


CREATE PROCEDURE [DBO].[Posetitel_delete]
@ID_Posetitel int
AS
DELETE FROM [DBO].[Posetitel] WHERE ID_Posetitel = @ID_Posetitel
GO
*/

--ALTER COLUMN FIO_P VARCHAR(MAX) NOT NULL
--ALTER TABLE Zaivka 
--ALTER COLUMN Nomer_and_Seria_Pasport VARCHAR(12) NOT NULL
/*
--Добавление(заявка)
CREATE PROCEDURE [DBO].[Zaivka_insertinto]
@Sotr_ID INT,
@Data_oform VARCHAR(10),
@Posetitel_ID INT,
@Nomer_and_Seria_Pasport VARCHAR(12),
@Data_prihoda VARCHAR(10),
@Vremi_prihoda VARCHAR(11),
@Mesto_prihoda VARCHAR(MAX)
AS
INSERT INTO [DBO].[Zaivka] (Sotr_ID, Data_oform, Posetitel_ID, Nomer_and_Seria_Pasport, Data_prihoda, Vremi_prihoda, Mesto_prihoda) VALUES (@Sotr_ID, @Data_oform, @Posetitel_ID, @Nomer_and_Seria_Pasport, @Data_prihoda, @Vremi_prihoda, @Mesto_prihoda)
GO

--Обновление
CREATE PROCEDURE [DBO].[Zaivka_update]
@Sotr_ID INT,
@Data_oform VARCHAR(10),
@Posetitel_ID INT,
@Nomer_and_Seria_Pasport VARCHAR(12),
@Data_prihoda VARCHAR(10),
@Vremi_prihoda VARCHAR(11),
@Mesto_prihoda VARCHAR(MAX)
AS
UPDATE [DBO].[Zaivka] SET Sotr_ID = @Sotr_ID,
						  Data_oform = @Data_oform, 
						  Posetitel_ID = @Posetitel_ID,
						  Nomer_and_Seria_Pasport = @Nomer_and_Seria_Pasport,
						  Data_prihoda = @Data_prihoda,
						  Vremi_prihoda = @Vremi_prihoda,
						  Mesto_prihoda = @Mesto_prihoda
GO

--Удаление
CREATE PROCEDURE [DBO].[Zaivka_delete]
@ID_zaivka int
AS
DELETE FROM [DBO].[Zaivka] WHERE ID_zaivka = @ID_zaivka
GO
*/

/*
--Добавление(Учёт входа пользователей)
CREATE PROCEDURE [DBO].[Time_vhod_insertinto]
@Sotr_ID INT,
@FIO_S VARCHAR(MAX),
@Vrema VARCHAR(MAX)
AS
INSERT INTO [DBO].[Time_vhod] (Sotr_ID, FIO_S, Vrema) VALUES (@Sotr_ID, @FIO_S, @Vrema)
GO

--Обновление
CREATE PROCEDURE [DBO].[Time_vhod_update]
@Sotr_ID INT,
@FIO_S VARCHAR(MAX),
@Vrema VARCHAR(MAX)
AS
UPDATE [DBO].[Time_vhod] SET Sotr_ID = @Sotr_ID,
							 FIO_S  =@FIO_S,
						     Vrema = @Vrema
GO

--ALTER TABLE Time_vhod 
--ALTER COLUMN FIO_TV VARCHAR(MAX) NOT NULL

--Удаление
CREATE PROCEDURE [DBO].[Time_vhod_delete]
@ID_vhod int
AS
DELETE FROM [DBO].[Time_vhod] WHERE ID_vhod = @ID_vhod
GO
*/
/*
--Добавление(Оформленная заявка)
CREATE PROCEDURE [DBO].[Oformlennai_zaivka_insertinto]
@Mesto_Vidachi CHAR(50),
@Data_oform VARCHAR(20),
@Pos_ID INT,
@Nomer_and_Seria_Pasport CHAR(12),
@Data_prihoda VARCHAR(20),
@Vremi_prihoda VARCHAR(11),
@Mesto_prihoda VARCHAR(MAX),
@S_ID INT
AS
INSERT INTO [DBO].[Oformlennai_zaivka] (Mesto_Vidachi, Data_oform, Pos_ID, Nomer_and_Seria_Pasport, Data_prihoda, Vremi_prihoda, Mesto_prihoda, S_ID) VALUES (@Mesto_Vidachi, @Data_oform, @Pos_ID, @Nomer_and_Seria_Pasport, @Data_prihoda, @Vremi_prihoda, @Mesto_prihoda, @S_ID)
GO


--Обновление
CREATE PROCEDURE [DBO].[Oformlennai_zaivka_update]
@Mesto_Vidachi CHAR(50),
@Data_oform VARCHAR(20),
@Pos_ID INT,
@Nomer_and_Seria_Pasport CHAR(12),
@Data_prihoda VARCHAR(20),
@Vremi_prihoda VARCHAR(11),
@Mesto_prihoda VARCHAR(MAX),
@S_ID INT
AS
UPDATE [DBO].[Oformlennai_zaivka] SET Mesto_Vidachi = @Mesto_Vidachi,
									  Data_oform = @Data_oform,
									  Pos_ID = @Pos_ID,
									  Nomer_and_Seria_Pasport = @Nomer_and_Seria_Pasport,
									  Data_prihoda = @Data_prihoda,
									  Vremi_prihoda = @Vremi_prihoda,
									  Mesto_prihoda = @Mesto_prihoda,
									  S_ID = @S_ID
GO

--Удаление
CREATE PROCEDURE [DBO].[Oformlennai_zaivka_delete]
@ID_Ozaivka int
AS
DELETE FROM [DBO].[Oformlennai_zaivka] WHERE ID_Ozaivka = @ID_Ozaivka
GO
*/

/*alter table Oformlennai_zaivka drop column Tip_vidachi*/
