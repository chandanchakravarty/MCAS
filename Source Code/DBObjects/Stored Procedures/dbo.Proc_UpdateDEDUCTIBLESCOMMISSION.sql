IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateDEDUCTIBLESCOMMISSION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateDEDUCTIBLESCOMMISSION]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------  
Proc Name       : dbo.Proc_UpdateDEDUCTIBLESCOMMISSION
Created by      : Priya  
Date            : 8/22/2005  
Purpose         : To update  record in DEDUCTIBLES COMMISSION table  
Revison History :  
Used In         :   Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
CREATE PROC Proc_UpdateDEDUCTIBLESCOMMISSION  
(  
 @CUSTOMER_ID              int,  
 @APP_ID                   int,  
 @APP_VERSION_ID           smallint,  
 @DEDUCTIBLES_ID           int,  
 @BODILY_INJURY_DEDUCTIBLE_AMOUNT      decimal(18,2),   
 @PREMISES_PREMIUM         decimal(18,2), 
 @TOTAL_ACCOUNT_PREMIUM    decimal(18,2),  
 @COMMISSION_PERCENT       decimal(18,2),  
 @COMMISSION_AMOUNT        decimal(18,2) ,    
@IS_ACTIVE nchar(1),
 @MODIFIED_BY               int,  
 @LAST_UPDATED_DATETIME     datetime 
    
)  
AS  
  
BEGIN  
  
 UPDATE APP_GENERAL_DEDUCTIBLES_COMMISSION
 
 SET
 CUSTOMER_ID=@CUSTOMER_ID ,     
 APP_ID=@APP_ID ,                 
 APP_VERSION_ID=@APP_VERSION_ID ,           
 DEDUCTIBLES_ID=@DEDUCTIBLES_ID ,      
 BODILY_INJURY_DEDUCTIBLE_AMOUNT=@BODILY_INJURY_DEDUCTIBLE_AMOUNT ,   
 PREMISES_PREMIUM=@PREMISES_PREMIUM ,                
 TOTAL_ACCOUNT_PREMIUM=@TOTAL_ACCOUNT_PREMIUM ,                 
 COMMISSION_PERCENT= @COMMISSION_PERCENT,                
 COMMISSION_AMOUNT=@COMMISSION_AMOUNT, 
IS_ACTIVE=@IS_ACTIVE,               
 MODIFIED_BY=@MODIFIED_BY   ,           
 LAST_UPDATED_DATETIME=@LAST_UPDATED_DATETIME 
      
 WHERE 

 CUSTOMER_ID=@CUSTOMER_ID AND     
 APP_ID=@APP_ID AND                 
 APP_VERSION_ID=@APP_VERSION_ID AND
DEDUCTIBLES_ID =@DEDUCTIBLES_ID 
  
END


GO

