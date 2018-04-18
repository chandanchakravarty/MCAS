  IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_INSERT_QQ_INVOICE_PARTICULAR_MARINE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].PROC_INSERT_QQ_INVOICE_PARTICULAR_MARINE
GO

SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON
GO  
         
-- =============================================      
-- Author:  Kuldeep Saxena     
-- Create date: 14-MAR-2012     
-- Description: -  
--dROP PROC PROC_QQ_INVOICE_PARTICULAR_MARINE  
--EXEC PROC_INSERT_QQ_INVOICE_PARTICULAR_MARINE
-- =============================================      
      
           
-- =============================================        
-- Author:  Kuldeep Saxena       
-- Create date: 14-MAR-2012       
-- Description: -    
--dROP PROC PROC_QQ_INVOICE_PARTICULAR_MARINE    
--EXEC PROC_INSERT_QQ_INVOICE_PARTICULAR_MARINE  
-- =============================================        
        
CREATE PROCEDURE dbo.PROC_INSERT_QQ_INVOICE_PARTICULAR_MARINE  (    
 @ID int output,  
 @CUSTOMER_ID int ,  
 @QUOTE_ID int,  
 @CUSTOMER_TYPE int,  
 @COMPANY_NAME nvarchar(100),  
 @BUSINESS_TYPE  int,  
 @OPEN_COVER_NO NVARCHAR(10),  
 @INVOICE_TYPE NVARCHAR(20),  
 @INVOICE_AMOUNT DECIMAL(18,2),  
 @CURRENCY_TYPE INT,  
 @BILLING_CURRENCY INT,  
 @MARK_UP_RATE_PERC DECIMAL(5,2),  
 @IS_ACTIVE nchar(1) ,  
 @CREATED_BY  int ,  
 @CREATED_DATETIME  datetime ,  
 @DATE_OF_QUOTATION varchar(20),  
 @POLICY_ID INT,  
 @POLICY_VERSION_ID INT  
 )    
AS        
        
BEGIN        
  
SELECT @ID=ISNULL(MAX(ID),0)+1 FROM QQ_INVOICE_PARTICULAR_MARINE    
INSERT INTO QQ_INVOICE_PARTICULAR_MARINE       
  (        
 ID,  
 CUSTOMER_ID,  
 QUOTE_ID,  
 CUSTOMER_TYPE,  
 COMPANY_NAME ,  
 BUSINESS_TYPE,  
 OPEN_COVER_NO,  
 INVOICE_TYPE,  
 INVOICE_AMOUNT,  
 CURRENCY_TYPE,  
 BILLING_CURRENCY,  
 MARK_UP_RATE_PERC,  
 IS_ACTIVE,  
 CREATED_BY,  
 CREATED_DATETIME,  
 DATE_OF_QUOTATION ,  
 POLICY_ID,  
 POLICY_VERSION_ID   
   )        
        
VALUES         
 (        
 @ID,  
 @CUSTOMER_ID,  
 @QUOTE_ID,  
 @CUSTOMER_TYPE,  
 @COMPANY_NAME ,  
 @BUSINESS_TYPE,  
 @OPEN_COVER_NO,  
 @INVOICE_TYPE,  
 @INVOICE_AMOUNT,  
 @CURRENCY_TYPE,  
 @BILLING_CURRENCY,  
 @MARK_UP_RATE_PERC,  
 @IS_ACTIVE,  
 @CREATED_BY,  
 @CREATED_DATETIME,  
 @DATE_OF_QUOTATION  ,  
  @POLICY_ID,  
  @POLICY_VERSION_ID  
 )        
  END