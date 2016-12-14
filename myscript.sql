




-----------------------------------------------------------------------------------------------------------------------

CREATE DATABASE PresidentAggregator
GO

USE PresidentAggregator
GO

CREATE TABLE XMLTable
(
Id INT IDENTITY PRIMARY KEY,
XMLData XML,
LoadedDateTime DATETIME
)

INSERT INTO XMLTable(XMLData, LoadedDateTime)
SELECT CONVERT(XML, BulkColumn) AS BulkColumn, GETDATE()
FROM OPENROWSET(BULK 'C:\Users\silva\Documents\EDC\tpfinal\EDC-Trabalho-Final\EDC-Trabalho-Final\App_Data\presidentes.xml', SINGLE_BLOB) AS x;

---------------------------------------------------------------------------------------------------------------------------------

USE PresidentAggregator
GO

DECLARE @XML AS XML, @hDoc AS INT, @SQL NVARCHAR (MAX)


SELECT @XML = XMLData FROM XMLTable

EXEC sp_xml_preparedocument @hDoc OUTPUT, @XML

SELECT nome, partido
FROM OPENXML(@hDoc, 'root/presidentes/presidente')
WITH
(
nome [varchar](30) 'nome',
partido [varchar](30) 'partido'
)

EXEC sp_xml_removedocument @hDoc
GO

----------------------------------------------------------------------------------------------------------------------------

DECLARE @XML AS XML, @hDoc AS INT, @SQL NVARCHAR (MAX)

SELECT @XML = XMLData FROM XMLTable

EXEC sp_xml_preparedocument @hDoc OUTPUT, @XML

SELECT nome, partido, numero, idade, inicio, fim ,vice, vice1, vice2, vice3, mandatos
Into Presidents
FROM OPENXML(@hDoc, 'root/presidentes/presidente')
WITH
(
nome [varchar](30) 'nome',
partido [varchar](30) 'partido',
numero int 'atributos/numero',
idade int 'atributos/idade',
inicio [varchar](30) 'atributos/inicio',
fim [varchar](30) 'atributos/fim',
vice [varchar](30) 'atributos/vice',
vice1 [varchar](30) 'atributos/vice1',
vice2 [varchar](30) 'atributos(vice2',
vice3 [varchar](30) 'atributos/vice3',
mandatos int 'atributos/mandatos'
)

-----------------------------------------------------------------------------------------------------------------------
--Select * from udf_show_presidents()
--Drop function udf_show_presidents
go
CREATE FUNCTION udf_show_presidents()
RETURNS TABLE
WITH ENCRYPTION
AS
	RETURN (SELECT nome as 'Nome', partido as 'Partido', numero as 'Numero', idade as 'Idade', inicio as 'Inicio', fim as 'Fim', vice as 'Vice', 
			vice1 as 'Vice1', vice2 as 'Vice2', vice3 as 'Vice3', mandatos as 'Mandatos'
	FROM Presidentes);


--------------------------------------------------------------------------------------------------------------------------
--Select * from udf_getPresident_name()
--Drop function udf_getPresident_name
go
CREATE FUNCTION udf_getPresident_name(@given_name [varchar](15))
RETURNS TABLE
WITH ENCRYPTION
AS
RETURN (SELECT nome as 'Nome', partido as 'Partido', numero as 'Numero', idade as 'Idade', inicio as 'Inicio', fim as 'Fim', vice as 'Vice', 
			vice1 as 'Vice1', vice2 as 'Vice2', vice3 as 'Vice3', mandatos as 'Mandatos'
		FROM Presidentes
		WHERE Presidentes.nome like '%' + @given_name + '%');

------------------------------------------------------------------------------------------------------------------------
--Select * from udf_getPresident_partidos()
--Drop function udf_getPresident_partidos
go
CREATE FUNCTION udf_getPresident_partidos(@given_name [varchar](15))
RETURNS TABLE
WITH ENCRYPTION 
AS
	RETURN (SELECT partido as 'Partido' FROM Presidentes WHERE Presidentes.nome like '%' + @given_name + '%');

-------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE sp_createPresident
	@nome varchar (30),
	@partido varchar (30),
	@numero int,
	@idade int,
	@inicio varchar (30),
	@fim varchar (30),
	@vice varchar (30),
	@vice1 varchar (30),
	@vice2 varchar (30),
	@vice3 varchar (30),
	@mandatos int

WITH ENCRYPTION
AS
	BEGIN TRANSACTION;

	BEGIN TRY
		INSERT INTO Presidentes
					([nome],
					[partido],
					[numero],
					[idade],
					[inicio],
					[fim],
					[vice],
					[vice1],
					[vice2],
					[vice3],
					[mandatos])
		VALUES		(@nome,
					 @partido,
					 @numero,
					 @idade,
					 @inicio,
					 @fim,
					 @vice,
					 @vice1,
					 @vice2,
					 @vice3,
					 @mandatos)
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		RAISERROR ('An error has occurred while creating the president!', 14, 1)
		ROLLBACK TRANSACTION;
	END CATCH;
go


