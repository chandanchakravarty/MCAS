IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Update_QQ_INVOICE_PARTICULAR_MARINE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Update_QQ_INVOICE_PARTICULAR_MARINE]
GO

SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------    
--Proc Name          : dbo.[Proc_Update_QQ_INVOICE_PARTICULAR_MARINE]    
--Created by         :     Kuldeep Saxena         
--Date               :  17-Mar-2012           
--------------------------------------------------------    
--Date     Review By          Comments            
------   ------------       -------------------------*/           
-- drop proc dbo.[Proc_Update_QQ_INVOICE_PARTICULAR_MARINE]          
      
CREATE PROC dbo.Proc_Update_QQ_INVOICE_PARTICULAR_MARINE (        
   @ID int        
   ,@CUSTOMER_ID int        
   ,@QUOTE_ID int      
   ,@CUSTOMER_CODE nvarchar(10)= null      
   ,@CUSTOMER_TYPE int = null        
   ,@COMPANY_NAME nvarchar(100) = null        
   ,@BUSINESS_TYPE int  = null  
   ,@OPEN_COVER_NO NVARCHAR(10)   
   ,@INVOICE_TYPE NVARCHAR(20) = null      
   ,@INVOICE_AMOUNT DECIMAL(18,2)  = null      
   ,@CURRENCY_TYPE int  = null  
   ,@BILLING_CURRENCY INT
   ,@MARK_UP_RATE_PERC DECIMAL(5,2)
   ,@IS_ACTIVE nchar(1)  = null      
   ,@MODIFIED_BY int  = null      
   ,@LAST_UPDATED_DATETIME datetime = null)
           
AS        
        
BEGIN        
IF EXISTS(select * from QQ_INVOICE_PARTICULAR_MARINE where CUSTOMER_ID = @CUSTOMER_ID and QUOTE_ID = @QUOTE_ID)      
BEGIN      
      
      
UPDATE [dbo].[QQ_INVOICE_PARTICULAR_MARINE]      
   SET 
	CUSTOMER_TYPE=@CUSTOMER_TYPE,
	COMPANY_NAME =@COMPANY_NAME,
	BUSINESS_TYPE=@BUSINESS_TYPE,
	OPEN_COVER_NO=@OPEN_COVER_NO,
	INVOICE_TYPE=@INVOICE_TYPE,
	INVOICE_AMOUNT=@INVOICE_AMOUNT,
	CURRENCY_TYPE=@CURRENCY_TYPE,
	BILLING_CURRENCY=@BILLING_CURRENCY,
	MARK_UP_RATE_PERC=@MARK_UP_RATE_PERC,
	IS_ACTIVE = @IS_ACTIVE,      
	[MODIFIED_BY] = @MODIFIED_BY,     
     [LAST_UPDATED_DATETIME] = @LAST_UPDATED_DATETIME      
 WHERE [CUSTOMER_ID] = @CUSTOMER_ID and      
      [QUOTE_ID] = @QUOTE_ID and      
      [ID] = @ID      
      
RETURN 1      
      
END      
      
END 