IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertDeductiblesCommission]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertDeductiblesCommission]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------  
Proc Name       : dbo.Proc_InsertDeductiblesCommission 
Created by      : Priya  
Date            : 5/25/2005  
Purpose         : To add record in deductibles commission  table  
Revison History :  
Used In         :   Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
CREATE PROC dbo.Proc_InsertDeductiblesCommission  
(  
	 @CUSTOMER_ID                      int,  
	 @APP_ID                           int,  
	 @APP_VERSION_ID                   smallint ,
	 @DEDUCTIBLES_ID                   int out,  
	 @BODILY_INJURY_DEDUCTIBLE_AMOUNT  decimal(18,2),  
        	 @PREMISES_PREMIUM                 decimal(18,2),
	 @TOTAL_ACCOUNT_PREMIUM            decimal(18,2),  
	 @COMMISSION_PERCENT              decimal(18,2),  
	 @COMMISSION_AMOUNT                decimal(18,2) , 
	@IS_ACTIVE nchar(1),
     	 @CREATED_BY               int,  
	 @CREATED_DATETIME         datetime 
)  
AS  
 
BEGIN

  select @DEDUCTIBLES_ID= isnull(max(DEDUCTIBLES_ID)+1,1) from APP_GENERAL_DEDUCTIBLES_COMMISSION		
	INSERT INTO APP_GENERAL_DEDUCTIBLES_COMMISSION
 	(  
		 CUSTOMER_ID,     
		 APP_ID,                 
		 APP_VERSION_ID,          
	         	 DEDUCTIBLES_ID,
		 BODILY_INJURY_DEDUCTIBLE_AMOUNT,        
		 PREMISES_PREMIUM,             
		 TOTAL_ACCOUNT_PREMIUM,
		 COMMISSION_PERCENT,                
		 COMMISSION_AMOUNT,                 
		 IS_ACTIVE,
		 CREATED_BY,           
		 CREATED_DATETIME        
	)     
 	values  
	(  
 
		 @CUSTOMER_ID ,     
		 @APP_ID ,                 
		 @APP_VERSION_ID ,           
		 @DEDUCTIBLES_ID,
		@BODILY_INJURY_DEDUCTIBLE_AMOUNT  ,  
        	@PREMISES_PREMIUM,
		@TOTAL_ACCOUNT_PREMIUM  ,
		@COMMISSION_PERCENT             ,  
	 	@COMMISSION_AMOUNT      , 
		@IS_ACTIVE,
		@CREATED_BY   ,           
		@CREATED_DATETIME
   	)  
                
END


GO

