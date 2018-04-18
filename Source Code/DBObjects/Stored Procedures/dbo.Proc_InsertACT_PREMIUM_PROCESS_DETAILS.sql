IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertACT_PREMIUM_PROCESS_DETAILS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertACT_PREMIUM_PROCESS_DETAILS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------                  
Proc Name        : dbo.Proc_InsertACT_PREMIUM_PROCESS_DETAILS      
Created by       : Vijay Arora          
Date             : 05-01-2006      
Purpose          : Insert the Premium XML in table named ACT_PREMIUM_PROCESS_DETAILS      
Revison History  :                  
Used In          : Wolverine                  
------------------------------------------------------------                  
Date     Review By          Comments                  
------   ------------       -------------------------*/                  
--drop proc Dbo.Proc_InsertACT_PREMIUM_PROCESS_DETAILS  
CREATE PROC Dbo.Proc_InsertACT_PREMIUM_PROCESS_DETAILS  
(                  
 @PROCESS_ID int output,      
 @PROCESS_TYPE Int,   
 @TRANSACTION_TYPE varchar(5),       
 @TERM_TYPE varchar(5),       
 @PROCESS_NUM varchar(50),       
 @PROCESS_DESC varchar(50),       
 @CUSTOMER_ID int,       
 @APP_ID int,       
 @APP_VERSION_ID smallint,       
 @POLICY_ID int,       
 @POLICY_VERSION_ID smallint,       
 @PREMIUM_AMOUNT decimal(25,2),       
 @PROCESS_STATUS varchar(5),       
 @PREMIUM_XML varchar(3000),  
 @POSTED_PREMIUM_XML varchar(3000)  
)                  
AS                  
BEGIN                  
      
      
 SELECT @PROCESS_ID = (ISNULL(MAX(PROCESS_ID),0) + 1) FROM ACT_PREMIUM_PROCESS_DETAILS (NOLOCK) 
    
      
  INSERT INTO ACT_PREMIUM_PROCESS_DETAILS   
 (      
  PROCESS_ID,       
  PROCESS_TYPE,       
  TRANSACTION_TYPE,       
  TERM_TYPE,       
  PROCESS_NUM,       
  PROCESS_DESC,       
  CUSTOMER_ID,       
  APP_ID,       
  APP_VERSION_ID,       
  POLICY_ID,       
  POLICY_VERSION_ID,       
  PREMIUM_AMOUNT,       
  PROCESS_STATUS,       
  IS_ACTIVE,       
  CREATED_DATETIME,       
  PREMIUM_XML,  
  POSTED_PREMIUM_XML  
  )      
  VALUES      
  (      
  @PROCESS_ID,       
  @PROCESS_TYPE,  
  @TRANSACTION_TYPE,       
  @TERM_TYPE,       
  @PROCESS_NUM,       
  @PROCESS_DESC,       
  @CUSTOMER_ID,       
  @APP_ID,       
  @APP_VERSION_ID,       
  @POLICY_ID,       
  @POLICY_VERSION_ID,       
  @PREMIUM_AMOUNT,       
  @PROCESS_STATUS,       
  'Y',      
  GETDATE(),      
  @PREMIUM_XML,  
  @POSTED_PREMIUM_XML  
 )      
       
  RETURN @PROCESS_ID      
END  


GO

