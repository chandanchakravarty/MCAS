IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetEODProcessLog]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetEODProcessLog]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--begin tran
--drop proc dbo.Proc_GetEODProcessLog 
--go
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
/*----------------------------------------------------------        
Proc Name        : dbo.Proc_GetEODProcessLog      
Created by       : Ravinda Gupta       
Date             : 10-05-2007      
Purpose        :       
Revison History :        
Used In   :Wolverine        
Proc_GetEODProcessLog '2008-4-14','2008-4-14'  
------------------------------------------------------------        
Date     Review By          Comments        
------------------------------------------------------------*/        
-- drop proc dbo.Proc_GetEODProcessLog      
--dbo.Proc_GetEODProcessLog '2008-2-19','2008-2-19','','Succeeded',''
--dbo.Proc_GetEODProcessLog '2008-2-19','2008-2-19','','Succeeded',''         
  
CREATE PROC [dbo].[Proc_GetEODProcessLog]           
(          
 @FROM_DATE Datetime = null,          
 @TO_DATE Datetime = null,          
 @StatusId varchar(8000) = Null,          
 @Option  varchar(40) = Null,        
 @ACTIVITY VARCHAR(600) = NULL ,  
 @LANG_ID int = NULL          
)          
AS          
BEGIN           
 IF ( ISNULL(@Option,'')  ='Succeeded')      
 BEGIN       
  SET @Option = @Option +  ''',''SUCCEEDED'      
 END      
      
 DECLARE @SQL Varchar(4000)          
 --DECLARE @WHERE Varchar(1000)          
    if(@LANG_ID is null )  
  set @LANG_ID = 1  
 SET @SQL = 'SELECT ISNULL(CPL.POLICY_NUMBER,'''') AS POLICY_NUMBER,          
 DET.ACTIVITY_DESCRIPTION,DET.STATUS,          
 ((CASE WHEN '+ convert(varchar(10) ,@LANG_ID) +' = 2 THEN   
 CONVERT(VARCHAR(50),DET.START_DATETIME ,103)  
 ELSE CONVERT(VARCHAR(50),DET.START_DATETIME ,101) END   
 )+ '' ''+CONVERT(VARCHAR(50),DET.START_DATETIME ,108))  
  START_DATETIME ,  
  ((CASE WHEN '+ convert(varchar(10) ,@LANG_ID) +' = 2 THEN   
 CONVERT(VARCHAR(50),DET.END_DATETIME ,103)  
 ELSE CONVERT(VARCHAR(50),DET.END_DATETIME ,101) END   
)+'' ''+CONVERT(VARCHAR(50),DET.END_DATETIME ,108))  
   
 END_DATETIME,          
 ISNULL(DET.ADDITIONAL_INFO,'''') AS ADDITIONAL_INFO          
 FROM EOD_PROCESS_LOG DET WITH(NOLOCK)          
 LEFT JOIN POL_CUSTOMER_POLICY_LIST  CPL          
 ON DET.CLIENT_ID = CPL.CUSTOMER_ID           
 AND DET.POLICY_ID = CPL.POLICY_ID           
 AND ISNULL(DET.POLICY_VERSOIN_ID,1) = CPL.POLICY_VERSION_ID          
 LEFT JOIN EOD_ACTIVITY_MASTER  MAS          
 ON MAS.ACTIVITY_ID = DET.ACTIVITY          
 LEFT JOIN EOD_ACTIVITY_MASTER  MAS1          
 ON MAS1.ACTIVITY_ID = DET.SUB_ACTIVITY WHERE 1=1 AND (POLICY_NUMBER IS NOT NULL AND POLICY_NUMBER <> '''')'  --Added by Aditya for TFS BUG # 368    
         
 IF (ISNULL(@Option,'0') <> '0' AND @Option <> '')          
 begin          
  SELECT @SQL = @SQL + ' AND DET.STATUS IN (''' + @Option + ''')'          
 end          
          
 --IF NOT @StatusId IS NULL          
 IF (ISNULL(@StatusId,'0') <> '0' AND @StatusId <> '')          
 begin          
  SELECT @SQL = @SQL + ' AND (MAS.Activity_id IN (' + @StatusId + ') or  MAS.Parent_Activity_id IN (' + @StatusId + '))'          
 end          
          
 IF NOT @FROM_DATE IS NULL AND NOT @TO_DATE IS NULL           
 begin         
  SELECT @SQL = @SQL + ' AND cast((Convert(varchar,DET.START_DATETIME,101)) as datetime) >= '''  + CONVERT(VARCHAR, @FROM_DATE,101) + ''' AND cast((Convert(varchar,DET.START_DATETIME,101)) as datetime) <=  '''          
  + Convert(varchar,@TO_DATE,101) + ''''          
 end          
          
 IF NOT @FROM_DATE IS NULL AND @TO_DATE IS NULL           
 begin          
  SELECT @SQL = @SQL + ' AND cast((Convert(varchar,DET.START_DATETIME,101)) as datetime) >= '''  + CONVERT(VARCHAR, @FROM_DATE,101) + ''''           
 end          
          
 IF  @FROM_DATE IS NULL AND NOT @TO_DATE IS NULL           
 begin         
  SELECT @SQL = @SQL + ' AND cast((Convert(varchar,DET.START_DATETIME,101)) as datetime) <=  '''          
  + Convert(varchar,@TO_DATE,101) + ''''          
 end          
         
        
 IF (ISNULL(@ACTIVITY,'0') <> '0' AND @ACTIVITY <> '')          
 begin          
  SELECT @SQL = @SQL + ' AND DET.SUB_ACTIVITY IN (' + @ACTIVITY + ')'          
 end           
          
    SET @SQL = @SQL + '  ORDER BY IDEN_ROW_ID '      
          
/* IF(@FROM_DATE IS NOT NULL AND @TO_DATE IS NOT NULL)          
 BEGIN           
            
  SET @WHERE =  '   WHERE CAST(CONVERT(VARCHAR,DET.START_DATETIME,101) AS DATETIME) >=           
     CAST(CONVERT(VARCHAR,@FROM_DATE,101) AS DATETIME)          
  AND CAST(CONVERT(VARCHAR,DET.START_DATETIME,101) AS DATETIME) <=           
     CAST(CONVERT(VARCHAR,@TO_DATE,101) AS DATETIME)             
     ORDER BY IDEN_ROW_ID '          
 END          
           
 IF(@FROM_DATE IS NOT NULL AND @TO_DATE IS NULL)          
 BEGIN           
            
  SET @WHERE =  '   WHERE CAST(CONVERT(VARCHAR,DET.START_DATETIME,101) AS DATETIME) >=           
     CAST(CONVERT(VARCHAR,@FROM_DATE,101) AS DATETIME)          
     ORDER BY IDEN_ROW_ID '          
 END          
          
 IF(@FROM_DATE IS NULL AND @TO_DATE IS NOT NULL)          
 BEGIN           
            
  SET @WHERE =  '   WHERE CAST(CONVERT(VARCHAR,DET.START_DATETIME,101) AS DATETIME) <=           
     CAST(CONVERT(VARCHAR,@TO_DATE,101) AS DATETIME)             
     ORDER BY IDEN_ROW_ID '          
 END          
           
 IF(@FROM_DATE IS NULL AND @TO_DATE IS NULL)          
 BEGIN           
            
  SET @WHERE =  '   ORDER BY IDEN_ROW_ID '          
 END          
          
 SET @SQL = @SQL + @WHERE */          
          
 --PRINT @SQL  
 EXECUTE(@SQL)          
END      
--  go    
--exec  Proc_GetEODProcessLog null,null,null,null,12    
--    
--    
--rollback tran     
      
      
GO

