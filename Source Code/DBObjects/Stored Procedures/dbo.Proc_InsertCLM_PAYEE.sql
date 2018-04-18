IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertCLM_PAYEE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertCLM_PAYEE]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


 /*----------------------------------------------------------                                          
Proc Name       : dbo.Proc_InsertCLM_PAYEE                                          
Created by      : Vijay Arora                                          
Date            : 6/1/2006                                          
Purpose     : To Insert the record in table named CLM_PAYEE                                          
Revison History :                                          
Used In        : Wolverine                                          
------------------------------------------------------------                                          
Modified By  : Asfa Praveen        
Date   : 19/Sept/2007        
Purpose  : Make a check to enter the exact amount on Payee Screen as specofied on the corresponding Activity        
------------------------------------------------------------                                          
Date     Review By          Comments                                          
------   ------------       -------------------------*/                                          
--drop PROC dbo.Proc_InsertCLM_PAYEE                                          
CREATE PROC [dbo].[Proc_InsertCLM_PAYEE]                                          
(                                          
@CLAIM_ID     int,                                          
@ACTIVITY_ID int,                                        
@EXPENSE_ID int,                                        
@PAYEE_ID     int OUTPUT,                                          
--@PAYEE_ACTIVITY_ID     int,                                          
@PARTY_ID     varchar(250),                                          
@PAYMENT_METHOD   int,                                          
@ADDRESS1     varchar(75),                                          
@ADDRESS2     varchar(75),                                          
@CITY     varchar(25),                                          
@STATE     int,                                          
@ZIP     varchar(11),                                          
@COUNTRY     int,                                          
@NARRATIVE     varchar(300),                                          
@CREATED_BY     int,                                  
@AMOUNT decimal(20,2),                                
@ACTIVITY_REASON int,                        
--@INVOICED_BY     int,                                    
@INVOICE_NUMBER  varchar(50),                                    
@INVOICE_DATE    datetime,       
@INVOICE_DUE_DATE    datetime =NULL,     -- Added by Santosh kumar Gautam on 16 Nov 2010
@INVOICE_SERIAL_NUMBER nvarchar(50),		 -- Added by Santosh kumar Gautam on 10 Dec 2010
@PAYEE_BANK_ID int =null,		         -- Added by Santosh kumar Gautam on 10 Dec 2010
@SERVICE_TYPE    int,          
@SERVICE_DESCRIPTION   varchar(300),                    
@SECONDARY_PARTY_ID     varchar(250),          
@FIRST_NAME VARCHAR(30) = NULL,          
@LAST_NAME VARCHAR(30) = NULL,          
@TO_ORDER_DESC TEXT = NULL ,          
@PAYEE_PARTY_ID VARCHAR(250) = NULL ,                   
@REIN_RECOVERY_NUMBER NVARCHAR(256) ,
@RECOVERY_TYPE     INT           

)                                          
AS                                          
BEGIN                                   
                                
--declare @CALLED_FROM_EXPENSE varchar(10)                                
--declare @CALLED_FROM_PAYMENT varchar(10)                                
DECLARE @TOTAL_PAYEE_AMOUNT DECIMAL(20,2)                                
DECLARE @TOTAL_PAYMENT DECIMAL(20,2)           
DECLARE @ADDITIONAL_EXPENSE DECIMAL(20,2)                                        
                              
                       
                                
--if (upper(@CALLED_FROM)=@CALLED_FROM_PAYMENT)                                
--begin                                
                                
  --Check whether the addition of current amount to the existing amount makes the total amount greater than                                
  --total payment stored under clm_activity_payment         
  
  if(@ACTIVITY_REASON=11775) -- FOR PAYMENT                          
     SELECT @TOTAL_PAYMENT=ISNULL(SUM(PAYMENT_AMOUNT),0) FROM CLM_ACTIVITY_RESERVE WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID          
  
  if(@ACTIVITY_REASON=11776) -- FOR RECOVERY                          
     SELECT @TOTAL_PAYMENT=ISNULL(SUM(RECOVERY_AMOUNT),0) FROM CLM_ACTIVITY_RESERVE WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID                                
  
  if(@ACTIVITY_REASON IN (11773,11836)) -- FOR RESERVE                          
     SELECT @TOTAL_PAYMENT=ISNULL(SUM(OUTSTANDING),0) FROM CLM_ACTIVITY_RESERVE WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID                                
                                                  
                        
                                  
--  SELECT @TOTAL_PAYEE_AMOUNT=ISNULL(SUM(AMOUNT),0) FROM CLM_PAYEE WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID                                
                                  
--  SET @TOTAL_PAYEE_AMOUNT = @TOTAL_PAYEE_AMOUNT + ISNULL(@AMOUNT,0)                                    
                                  
----Commented By Asfa Praveen (19/Sept/2007) - Make a check to enter the exact amount on Payee Screen as specofied on the corresponding Activity        
        
----  IF (@TOTAL_PAYMENT IS NULL OR @TOTAL_PAYMENT<1 OR @TOTAL_PAYEE_AMOUNT>@TOTAL_PAYMENT)                                
    
--RAvindra(04-06-09): Temporary Change    
  --IF (@TOTAL_PAYMENT IS NULL OR @TOTAL_PAYMENT<0 OR @TOTAL_PAYEE_AMOUNT>@TOTAL_PAYMENT OR @TOTAL_PAYEE_AMOUNT<@TOTAL_PAYMENT)                                
 IF (@TOTAL_PAYMENT <> @AMOUNT )     
  RETURN -1                   
                                
--end                                
--else if (upper(@CALLED_FROM)=@CALLED_FROM_EXPENSE)                                
--begin                               
                          
 --Check whether the addition of current amount to the existing amount makes the total amount greater than                                
  --total expense payment stored under clm_activity_payment                                
          
 -- SELECT @ADDITIONAL_EXPENSE = ISNULL(ADDITIONAL_EXPENSE,0)  FROM CLM_ACTIVITY_EXPENSE WHERE                               
 --  CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID   --AND EXPENSE_ID=@EXPENSE_ID                              
          
          
          
 -- SELECT @TOTAL_PAYMENT=ISNULL(SUM(PAYMENT_AMOUNT),0) FROM CLM_ACTIVITY_EXPENSE WHERE                               
 -- CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID   --AND EXPENSE_ID=@EXPENSE_ID                              
                            
 --SET @TOTAL_PAYMENT = ISNULL(@TOTAL_PAYMENT,0) +   ISNULL(@ADDITIONAL_EXPENSE,0)          
              
 -- SELECT @TOTAL_PAYEE_AMOUNT=ISNULL(SUM(AMOUNT),0) FROM CLM_PAYEE WHERE                               
 -- CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID  --AND EXPENSE_ID=@EXPENSE_ID                              
                                  
 -- SET @TOTAL_PAYEE_AMOUNT = @TOTAL_PAYEE_AMOUNT + ISNULL(@AMOUNT,0)                                    
                               
--Commented By Asfa Praveen (19/Sept/2007) - Make a check to enter the exact amount on Payee Screen as specofied on the corresponding Activity        
--  IF (@TOTAL_PAYMENT IS NULL OR @TOTAL_PAYMENT<1 OR @TOTAL_PAYEE_AMOUNT>@TOTAL_PAYMENT)                                                        
      
--RAvindra(04-06-09): Temporary Change    
----IF (@TOTAL_PAYMENT IS NULL OR @TOTAL_PAYMENT<0 OR @TOTAL_PAYEE_AMOUNT>@TOTAL_PAYMENT OR @TOTAL_PAYEE_AMOUNT<@TOTAL_PAYMENT)                                
--IF (ISNULL(@TOTAL_PAYMENT,0) <> @TOTAL_PAYEE_AMOUNT )     
--  RETURN -1    
--end                              
                                          
select @PAYEE_ID=isnull(Max(PAYEE_ID),0)+1 from CLM_PAYEE WHERE                                         
 CLAIM_ID = @CLAIM_ID and ACTIVITY_ID=@ACTIVITY_ID --AND EXPENSE_ID=@EXPENSE_ID                                        
INSERT INTO CLM_PAYEE                                          
(                                          
CLAIM_ID,                                         
ACTIVITY_ID,                                        
EXPENSE_ID,                                         
PAYEE_ID,                                          
PARTY_ID,                                          
PAYMENT_METHOD,                                          
ADDRESS1,                                          
ADDRESS2,                                          
CITY,                         
STATE,                                          
ZIP,                                          
COUNTRY,                                          
NARRATIVE,         
IS_ACTIVE,                                          
CREATED_BY,                                     
CREATED_DATETIME,                                  
AMOUNT,                        
--INVOICED_BY,            
INVOICE_NUMBER,                        
INVOICE_DATE, 
INVOICE_DUE_DATE,   -- Added by Santosh kumar Gautam on 16 Nov 2010
INVOICE_SERIAL_NUMBER,-- Added by Santosh kumar Gautam on 10 Dec 2010  
PAYEE_BANK_ID, -- Added by Santosh kumar Gautam on 10 Dec 2010  
SERVICE_TYPE,                        
SERVICE_DESCRIPTION,                    
SECONDARY_PARTY_ID,          
FIRST_NAME,          
LAST_NAME,          
TO_ORDER_DESC  ,      
PAYEE_PARTY_ID   ,
REIN_RECOVERY_NUMBER    ,
RECOVERY_TYPE               
)                            
VALUES                                          
(                                          
@CLAIM_ID,                                          
@ACTIVITY_ID,                                        
@EXPENSE_ID,                                        
@PAYEE_ID,                                          
@PARTY_ID,             
@PAYMENT_METHOD,                              
@ADDRESS1,                                          
@ADDRESS2,                                          
@CITY,                                          
@STATE,                                          
@ZIP,                                          
@COUNTRY,                                          
@NARRATIVE,                                          
'Y',                                          
@CREATED_BY,                                          
GETDATE(),                                  
@AMOUNT,                        
--@INVOICED_BY,                        
@INVOICE_NUMBER,                        
@INVOICE_DATE,  
@INVOICE_DUE_DATE,      -- Added by Santosh kumar Gautam on 16 Nov 2010  
@INVOICE_SERIAL_NUMBER, -- Added by Santosh kumar Gautam on 10 Dec 2010  
@PAYEE_BANK_ID,         -- Added by Santosh kumar Gautam on 10 Dec 2010        
@SERVICE_TYPE,                        
@SERVICE_DESCRIPTION,                    
@SECONDARY_PARTY_ID,          
@FIRST_NAME,          
@LAST_NAME,          
@TO_ORDER_DESC  ,       
@PAYEE_PARTY_ID     ,
@REIN_RECOVERY_NUMBER ,
@RECOVERY_TYPE                                  
)                                          
END                  

GO

