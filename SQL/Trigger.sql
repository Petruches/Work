USE [BuroPropyskov]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
 /*
CREATE TRIGGER contacts_before_delete
BEFORE DELETE
   ON contacts FOR EACH ROW
   
BEGIN
 
   IF ID_sotr = 1 THEN 
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'DELETE canceled';
    END IF;
   
END;*/
/*
CREATE TRIGGER [DBO].[Sotr_pyst] on [DBO].[Sotr]
FOR DELETE
AS
DECLARE @ID_sotr int
select @ID_sotr = deleted.ID_sotr from
Sotr, deleted if (select count(@ID_sotr) from Sotr)<1
 BEGIN rollback tran
  raiserror('Таблица не может быть пустой!',16, 10)
 END
GO

CREATE TRIGGER DontDelete12 ON [DBO].[Rolli]
FOR DELETE
AS
--DECLARE @COUNT INT
SELECT ID_role FROM [DBO].[Rolli]
WHERE ID_role = '1'
BEGIN 
--ROLLBACK TRANSACTION
RAISERROR ('Полегче)), эту информацию нельзя удалить',16,10);
END
*/
--IF EXISTS (SELECT * FROM deleted WHERE Количество=300 OR Количество=500)BEGIN

CREATE TRIGGER [DBO].[pass_jgr] on [DBO].[Sotr]
FOR INSERT, UPDATE
AS
  begin
  if EXISTS(select [DBO].[Sotr].[Pass_S] FROM [DBO].[Sotr]
  WHERE [DBO].[Sotr].[Pass_S] in (select [DBO].[Sotr].[Pass_S] FROM inserted)
  GROUP BY [DBO].[Sotr].[Pass_S] having len([DBO].[Sotr].[Pass_S])<5)
   BEGIN
       raiserror('Пароль не должен превышать 5 символов!',16,1)
       rollback tran
   END
  END
GO