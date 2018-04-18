CREATE PROCEDURE [dbo].[Proc_GetTransactionAuditLog]
	@AccidentId [int]
WITH EXECUTE AS CALLER
AS
CREATE TABLE #Temp (
    [TranAuditId] [int] NULL,
    [TimeStamp] [datetime] NULL,
    [TableName] [nvarchar](50) NULL,
    [UserName] [nvarchar](50) NULL,
    [Actions] [nvarchar](1) NULL,
    [OldData] [xml] NULL,
    [NewData] [xml] NULL,
    [ChangedColumns] [nvarchar](4000) NULL,
    [TansDescription] [nvarchar](400) NULL,
    [ClaimID] [int]
  )
  BEGIN
   SET FMTONLY OFF;
   SELECT
        m1.[TranAuditId],
        m1.[TimeStamp],
        m2.[TableDesc] AS TableName,
        m1.[UserName],
        m1.[Actions],
        m1.[OldData],
        m1.[NewData],
        m1.[ChangedColumns],
        m1.[TansDescription],
        m1.[EntityCode],
        m1.[EntityType],
        m1.[EntityTypeId],
        m1.[AccidentId],
        m1.[ClaimID]
      FROM MNT_TransactionAuditLog m1
      LEFT JOIN MNT_TableDesc m2
        ON m1.TableName = m2.TableName
      WHERE m1.[AccidentId] = @AccidentId
      ORDER BY [TimeStamp] DESC
  END


