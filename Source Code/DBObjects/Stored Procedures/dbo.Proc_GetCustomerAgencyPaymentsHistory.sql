IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCustomerAgencyPaymentsHistory]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCustomerAgencyPaymentsHistory]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--BEGIN TRAN  
--drop PROC dbo.Proc_GetCustomerAgencyPaymentsHistory    
--go
/*----------------------------------------------------------                                  
Proc Name       :  dbo.Proc_GetCustomerAgencyPaymentsHistory                  
Created by      :  Swarup                  
Date            :  10-12-2007                  
Purpose         :                    
Revison History :                                  
Used In         :  Wolverine                                  
                               
exec dbo.Proc_GetCustomerAgencyPaymentsHistory   NULL,NULL,NULL,NULL,NULL,'10'            
exec dbo.Proc_GetCustomerAgencyPaymentsHistory   NULL,NULL,NULL,NULL,NULL,'null'    
------------------------------------------------------------                                  
Date     Review By          Comments                                  
------   ------------       -------------------------*/                                  
-- drop PROC dbo.Proc_GetCustomerAgencyPaymentsHistory                  
CREATE PROC [dbo].[Proc_GetCustomerAgencyPaymentsHistory]                 
(               
 @POLICY_NUMBER  varchar(30) =null,                 
 @FROM_DATE Datetime = null,                  
 @TO_DATE   DateTime = null,                  
 @AGENCY_ID varchar(5) = null,                  
 @CUSTOMER_ID varchar(5)= null,                
 @AMOUNT  varchar(30)  = null                
)                  
AS                  
BEGIN                  
DECLARE @QUERY varchar(8000)                 
       
create table #temp_rpt      
(   CUSTOMER_NAME varchar(1000),      
  POLICY_NUMBER varchar(200),      
  AGENCY_NAME varchar(100),   
  USER_NAME   VARCHAR(100),  
  AMOUNT varchar(100), 
  AMOUNT1  varchar(100),
  DATE_COMMITTED varchar(100),
  DATE_COMMITTED1 varchar(100),     
  MODE varchar(50));      
  --AMOUNT1 and DATE_COMMITTED1 added For Itrack Issue #6610                 
SET @QUERY =                                  
'SELECT                   
ISNULL(CUST.CUSTOMER_FIRST_NAME,'''') + '' ''  +                   
ISNULL(CUST.CUSTOMER_MIDDLE_NAME,'''') + '' '' +                   
ISNULL(CUST.CUSTOMER_LAST_NAME,'''') AS CUSTOMER_NAME ,              
UPPER(ISNULL(PAYMENTS.POLICY_NUMBER,'''')) AS  POLICY_NUMBER,              
ISNULL(AGEN.AGENCY_DISPLAY_NAME,'''') AS AGENCY_NAME,    
ISNULL(MUL.USER_FNAME,'''') + '' '' +  
ISNULL(MUL.USER_LNAME,'''') AS USER_NAME ,          
convert(varchar(30),convert(money,AMOUNT),1) as AMOUNT,AMOUNT AS AMOUNT1,          
CONVERT(VARCHAR,PAYMENTS.DATE_COMMITTED,101)AS DATE_COMMITTED,PAYMENTS.DATE_COMMITTED as DATE_COMMITTED1  ,               
CASE MODE  WHEN ''11975'' Then ''Check''               
WHEN ''11976'' Then ''EFT-Sweep'' END MODE             
FROM ACT_CUSTOMER_PAYMENTS_FROM_AGENCY PAYMENTS    
LEFT JOIN MNT_USER_LIST MUL   
ON PAYMENTS.CREATED_BY_USER = MUL.USER_ID           
LEFT JOIN CLT_CUSTOMER_LIST CUST                  
ON PAYMENTS.CUSTOMER_ID  = CUST.CUSTOMER_ID               
LEFT JOIN MNT_AGENCY_LIST AGEN              
ON PAYMENTS.AGENCY_ID = AGEN.AGENCY_ID            
WHERE  PAYMENTS.CREATED_BY_USER is not null             
'                                 
            
               
IF( @FROM_DATE IS NOT NULL)                  
BEGIN                   
 SET @QUERY   = @QUERY  +  ' AND CAST(CONVERT(VARCHAR,PAYMENTS.DATE_COMMITTED,101) AS DATETIME) >= ''' + CONVERT(VARCHAR,@FROM_DATE) + ''''                  
END                  
                  
IF( @TO_DATE IS NOT NULL)                  
BEGIN                   
 SET @QUERY   = @QUERY  +  ' AND CAST(CONVERT(VARCHAR,PAYMENTS.DATE_COMMITTED,101) AS DATETIME) <= '''                   
  + CONVERT(VARCHAR,@TO_DATE) + ''''                  
END                  
                  
IF( @POLICY_NUMBER IS NOT NULL)                  
BEGIN                   
 SET @QUERY   = @QUERY  +  ' AND CONVERT(VARCHAR,PAYMENTS.POLICY_NUMBER)= '''+ @POLICY_NUMBER  + ''''                    
END                  
                  
IF( @AGENCY_ID IS NOT NULL)                  
BEGIN                   
 SET @QUERY   = @QUERY  +  ' AND PAYMENTS.AGENCY_ID= '+ @AGENCY_ID                    
END                 
                
 IF( @CUSTOMER_ID IS NOT NULL)                  
BEGIN           
 SET @QUERY  = @QUERY  +  ' AND PAYMENTS.CUSTOMER_ID= '+ @CUSTOMER_ID                    
END            
             
IF( @AMOUNT IS NOT NULL)                  
BEGIN                   
 SET @QUERY  = @QUERY  +  ' AND PAYMENTS.AMOUNT like '''+'%'+ @AMOUNT +'%'''                   
END             
          
              
--select   @QUERY              
 --SET @QUERY   = @QUERY + '  group BY CUST.CUSTOMER_FIRST_NAME,CUST.CUSTOMER_MIDDLE_NAME,CUST.CUSTOMER_LAST_NAME,POLICY_NUMBER,AGEN.AGENCY_DISPLAY_NAME,AMOUNT,DATE_COMMITTED,MODE,PAYMENTS.IDEN_ROW_ID'                  
              
insert into #temp_rpt exec (@QUERY)      
      
      
--select sum(convert(decimal(10,2),AMOUNT)) FROM #temp_rpt      
--  SET @QUERY   = @QUERY + '  UNION select '''' CUSTOMER_NAME, '''' POLICY_NUMBER,      
--        '''' AGENCY_NAME,      
--        sum(convert(decimal(10,2),AMOUNT)) AS AMOUNT,      
--        '''' DATE_COMMITTED, '''' MODE, 1000 as IDEN_ROW_ID      
--        FROM #temp_rpt '               
       
  SET @QUERY   = @QUERY + '  ORDER BY IDEN_ROW_ID'                  
             
--print @QUERY     
EXEC (@QUERY)                  
drop table #temp_rpt      
END          
  
--GO  
--exec dbo.Proc_GetCustomerAgencyPaymentsHistory   NULL,NULL,NULL,NULL,NULL,'10'   
--ROLLBACK TRAN          
        
        
        
        
        
    


GO

