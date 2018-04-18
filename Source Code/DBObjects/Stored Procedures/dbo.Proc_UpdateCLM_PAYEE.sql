IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateCLM_PAYEE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateCLM_PAYEE]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  
--BEGIN TRAN    
--DROP PROC dbo.Proc_UpdateCLM_PAYEE     
--GO    
/*----------------------------------------------------------                                      
Proc Name       : dbo.Proc_UpdateCLM_PAYEE                                      
Created by      : Vijay Arora                                      
Date            : 6/1/2006                                      
Purpose     : To update the record in table named CLM_PAYEE                                      
Revison History :                                      
Used In  : Wolverine                                      
------------------------------------------------------------                                      
Date     Review By          Comments                                      
------   ------------       -------------------------*/                                      
--drop PROC dbo.Proc_UpdateCLM_PAYEE 833,6,0,1,'1','153 Test Street test inc','a','Anytown','22','48323','1',''  
--,299,14.44,'Payment','','03-19-2009'  
--,0,'','Bill &amp;Richard','','','','1'                                     
CREATE PROC [dbo].[Proc_UpdateCLM_PAYEE]                                      
(                                      
@CLAIM_ID     int,                                      
@ACTIVITY_ID int,                                    
@EXPENSE_ID int,                                    
@PAYEE_ID     int,                                      
@PARTY_ID     varchar(250),                                      
@PAYMENT_METHOD     int=null,                                      
@ADDRESS1     varchar(75),                                      
@ADDRESS2     varchar(75),                                      
@CITY     varchar(25),                                      
@STATE     int,                                      
@ZIP     varchar(11),                                      
@COUNTRY     int,                                      
@NARRATIVE     varchar(300),                                      
@MODIFIED_BY     int,                                
@AMOUNT decimal(12,2),                              
@ACTIVITY_REASON varchar(10),                      
--@INVOICED_BY     int,                                  
@INVOICE_NUMBER  varchar(50),                                  
@INVOICE_DATE    datetime,     
@INVOICE_DUE_DATE   datetime =NULL,   -- Added by Santosh Kumar Gautam on 16 Nov 2010
@INVOICE_SERIAL_NUMBER nvarchar(50), -- Added by Santosh Kumar Gautam on 10 Dec 2010
@PAYEE_BANK_ID int =null, -- Added by Santosh Kumar Gautam on 10 Dec 2010
@SERVICE_TYPE    int,                                  
@SERVICE_DESCRIPTION   varchar(300),                  
@SECONDARY_PARTY_ID   varchar(250),        
@FIRST_NAME VARCHAR(30)=NULL,        
@LAST_NAME VARCHAR(30)=NULL,        
@TO_ORDER_DESC TEXT = NULL ,        
@PAYEE_PARTY_ID VARCHAR(250) = NULL ,                     
@REIN_RECOVERY_NUMBER NVARCHAR(256) ,
@RECOVERY_TYPE     INT           
           
                           
)                                      
AS                                      
BEGIN                                     
                          
--declare @CALLED_FROM_EXPENSE varchar(10)                              
--declare @CALLED_FROM_PAYMENT varchar(10)                              
                                     
--DECLARE @TOTAL_PAYEE_AMOUNT DECIMAL(12,2)                              
DECLARE @TOTAL_PAYMENT DECIMAL(18,2)           
--declare @ADDITIONAL_EXPENSE DECIMAL(12,2)                           
                            
--set @CALLED_FROM_EXPENSE = 'EXPENSE'                              
--set @CALLED_FROM_PAYMENT = 'PAYMENT'                              
                              
--if (upper(@CALLED_FROM)=@CALLED_FROM_PAYMENT)                              
--begin    
--  --Check whether the addition of current amount to the existing amount makes the total amount greater than                              
--  --total payment stored under clm_activity_payment                                                        
--  SELECT @TOTAL_PAYMENT=ISNULL(SUM(PAYMENT_AMOUNT),0) FROM CLM_ACTIVITY_PAYMENT WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID                              
                                
--  SELECT @TOTAL_PAYEE_AMOUNT=ISNULL(SUM(AMOUNT),0) FROM CLM_PAYEE WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID and PAYEE_ID<>@PAYEE_ID                              
                                
--  SET @TOTAL_PAYEE_AMOUNT = @TOTAL_PAYEE_AMOUNT + ISNULL(@AMOUNT,0)                              
                                
                                
--  IF (@TOTAL_PAYMENT IS NULL OR @TOTAL_PAYMENT<0 OR @TOTAL_PAYEE_AMOUNT>@TOTAL_PAYMENT OR @TOTAL_PAYEE_AMOUNT<@TOTAL_PAYMENT)                               
--   RETURN -1   
     
--end             
--else if (upper(@CALLED_FROM)=@CALLED_FROM_EXPENSE)                              
    
--begin                      
-- --Check whether the addition of current amount to the existing amount makes the total amount greater than                              
--  --total expense payment stored under clm_activity_payment    
-- SELECT @ADDITIONAL_EXPENSE=ISNULL(ADDITIONAL_EXPENSE,0) FROM CLM_ACTIVITY_EXPENSE WHERE                             
--  CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID  -- AND EXPENSE_ID=@EXPENSE_ID                            
                            
--  SELECT @TOTAL_PAYMENT=ISNULL(SUM(PAYMENT_AMOUNT),0) FROM clm_activity_expense WHERE                             
--  CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID  -- AND EXPENSE_ID=@EXPENSE_ID               
        
-- SET @TOTAL_PAYMENT = ISNULL(@TOTAL_PAYMENT,0) + ISNULL(@ADDITIONAL_EXPENSE,0)        
                                
--SELECT @TOTAL_PAYEE_AMOUNT=ISNULL(SUM(AMOUNT),0) FROM CLM_PAYEE WHERE                             
--  CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID  --AND EXPENSE_ID=@EXPENSE_ID           
-- and PAYEE_ID<>@PAYEE_ID                            
                      
     
    
-- SET @TOTAL_PAYEE_AMOUNT = @TOTAL_PAYEE_AMOUNT + ISNULL(@AMOUNT,0)     
 
  if(@ACTIVITY_REASON=11775) -- FOR PAYMENT                          
     SELECT @TOTAL_PAYMENT=ISNULL(SUM(PAYMENT_AMOUNT),0) FROM CLM_ACTIVITY_RESERVE WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID          
  
  if(@ACTIVITY_REASON=11776) -- FOR RECOVERY                          
     SELECT @TOTAL_PAYMENT=ISNULL(SUM(RECOVERY_AMOUNT),0) FROM CLM_ACTIVITY_RESERVE WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID                                
  
  if(@ACTIVITY_REASON IN (11773,11836)) -- FOR RESERVE                          
     SELECT @TOTAL_PAYMENT=ISNULL(SUM(OUTSTANDING),0) FROM CLM_ACTIVITY_RESERVE WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID                                
                                                      
 --IF (@TOTAL_PAYMENT IS NULL OR @TOTAL_PAYMENT<0 OR @TOTAL_PAYEE_AMOUNT>@TOTAL_PAYMENT OR @TOTAL_PAYEE_AMOUNT<@TOTAL_PAYMENT)                            
 --   RETURN -1    
 --end                            
                  
 IF (@TOTAL_PAYMENT <> @AMOUNT )     
  RETURN -1                   
                       
                            
UPDATE CLM_PAYEE                        
SET                                      
PARTY_ID  =  @PARTY_ID,                                      
PAYMENT_METHOD  =  @PAYMENT_METHOD,                                      
ADDRESS1  =  @ADDRESS1,                                      
ADDRESS2  =  @ADDRESS2,                                     
CITY  =  @CITY,                                      
STATE  =  @STATE,                                      
ZIP  =  @ZIP,                                      
COUNTRY  =  @COUNTRY,                                      
NARRATIVE  =  @NARRATIVE,                                      
MODIFIED_BY  =  @MODIFIED_BY,                                      
LAST_UPDATED_DATETIME  =  GETDATE(),                                
AMOUNT = @AMOUNT,                      
--INVOICED_BY=@INVOICED_BY,                      
INVOICE_NUMBER=@INVOICE_NUMBER,                      
INVOICE_DATE=@INVOICE_DATE,     
INVOICE_DUE_DATE=@INVOICE_DUE_DATE,                  -- Added by Santosh Kumar Gautam on 16 Nov 2010
INVOICE_SERIAL_NUMBER=@INVOICE_SERIAL_NUMBER,
PAYEE_BANK_ID=@PAYEE_BANK_ID,
SERVICE_TYPE=@SERVICE_TYPE,                      
SERVICE_DESCRIPTION=@SERVICE_DESCRIPTION,                  
SECONDARY_PARTY_ID = @SECONDARY_PARTY_ID,        
FIRST_NAME=@FIRST_NAME,        
LAST_NAME=@LAST_NAME,        
PAYEE_PARTY_ID=@PAYEE_PARTY_ID,      
TO_ORDER_DESC = @TO_ORDER_DESC ,
REIN_RECOVERY_NUMBER =@REIN_RECOVERY_NUMBER ,
RECOVERY_TYPE=@RECOVERY_TYPE
           
       
WHERE CLAIM_ID = @CLAIM_ID AND PAYEE_ID = @PAYEE_ID and ACTIVITY_ID=@ACTIVITY_ID --and EXPENSE_ID=@EXPENSE_ID                                    
END                                      
                                      
--GO    
--EXEC Proc_UpdateCLM_PAYEE 833,6,0,1,'1','153 Test Street test inc','a','Anytown','22','48323','1','',299,12.34,'expense','44','',0,'','','','','',''                                     
----EXEC Proc_UpdateCLM_PAYEE 833,6,0,1,'1','153 Test Street test inc','a','Anytown','22','48323','1','',299,14.44,'Payment','','03-19-2009',0,'','Bill &amp;Richard','','','','1'  
--ROLLBACK TRAN                                     
                                  
                                
                              
                            
                          
                        
       
                    
                  
                
              
            
            
          
        
        
        
        
        
        
        
  

GO

