USE [BuroPropyskov]

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*
--Роль
INSERT INTO [DBO].[Rolli] (Rolle) VALUES ('Администратор')
INSERT INTO [DBO].[Rolli] (Rolle) VALUES ('Оператор')
INSERT INTO [DBO].[Rolli] (Rolle) VALUES ('Декан')
*/

--Сотрудник
--INSERT INTO [DBO].[Sotr] (FIO_S, Log_S, Pass_S, Role_ID, Dop_Inf) VALUES ('Тутаева Динара Рафаиловна', 'FDO', '123456', '3', 'ФДО')
--INSERT INTO [DBO].[Sotr] (FIO_S, Log_S, Pass_S, Role_ID, Dop_Inf) VALUES ('Быков Пётр Андреевич', 'Admin', '654321', '1', 'Админ')


--INSERT INTO [dbo].[Zaivka] (Sotr_ID, Data_oform, Posetitel_ID, Nomer_and_Seria_Pasport, Data_prihoda, Vremi_prihoda, Mesto_prihoda) VALUES ('50', '22.22.2019', '2', '11 11 111111', '22.22.2019', '11:00-12:00', 'Кпп 3')

--DELETE FROM [DBO].[Sotr]

--DELETE FROM [DBO].[Time_vhod]

--DELETE FROM [Rolli] WHERE ID_role = '3'