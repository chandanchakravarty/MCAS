CREATE PROCEDURE [dbo].[Proc_GetAllTransactionAuditLog]  
AS  
  BEGIN  
   SET FMTONLY OFF;  
   SELECT  
       ISNULL(m1.[TranAuditId],'') as TranAuditId,
       ISNULL(CAST(m1.[TranAuditId] as nvarchar(max)),'') as TranAuditIdString,
       ISNULL(m1.[TimeStamp],'') as [TimeStamp], 
       ISNULL(Convert(varchar,m1.[TimeStamp],103)+ ' ' + CONVERT(VARCHAR(8), m1.[TimeStamp], 108),'') as TimeStampString,  
       ISNULL( m2.[TableDesc],'') AS TableName,  
       ISNULL( m1.[UserName],'') AS UserName,  
       ISNULL( m1.[TansDescription],'') AS TansDescription
      FROM MNT_TransactionAuditLog m1 (nolock) 
      LEFT JOIN MNT_TableDesc m2  (nolock)
        ON m1.TableName = m2.TableName  
      ORDER BY [TimeStamp] DESC  
  END  