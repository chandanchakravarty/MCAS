IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertCLM_AUTHORITY_LIMIT]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertCLM_AUTHORITY_LIMIT]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                    
                    
Proc Name       : Proc_InsertCLM_AUTHORITY_LIMIT    
Created by      : Sumit Chhabra    
Date            : 20/04/2006                    
Purpose         : Insert of Authority Limit data in CLM_AUTHORITY_LIMIT    
Revison History :                    
Used In                   : Wolverine                    
------------------------------------------------------------                    
Date     Review By          Comments                    
------   ------------       -------------------------*/                    
-- drop PROC Dbo.Proc_InsertCLM_AUTHORITY_LIMIT                    
CREATE PROC [dbo].[Proc_InsertCLM_AUTHORITY_LIMIT]                      
(                      
 @AUTHORITY_LEVEL int,      
 @TITLE varchar(50),      
 @PAYMENT_LIMIT decimal(18,2),      
 @RESERVE_LIMIT decimal(18,2), 
 @REOPEN_CLAIM_LIMIT decimal(18,2), 
 @GRATIA_CLAIM_AMOUNT decimal(18,2),      
 @CLAIM_ON_DUMMY_POLICY bit,      
 @CREATED_BY int,      
 @CREATED_DATETIME datetime,      
 @LIMIT_ID int output      
                  
)                      
AS                      
BEGIN                      
--Check for Duplicacy of Authority Level data    
 IF EXISTS(SELECT LIMIT_ID FROM CLM_AUTHORITY_LIMIT WHERE AUTHORITY_LEVEL=@AUTHORITY_LEVEL)    
  return -1    
 /*Generating the new limit id*/                      
 SELECT @LIMIT_ID=ISNULL(MAX(LIMIT_ID),0)+1 FROM CLM_AUTHORITY_LIMIT                      
    
 INSERT INTO CLM_AUTHORITY_LIMIT      
 (       
  LIMIT_ID,      
  AUTHORITY_LEVEL,      
  TITLE,      
  PAYMENT_LIMIT,      
  RESERVE_LIMIT,      
  CLAIM_ON_DUMMY_POLICY, 
  REOPEN_CLAIM_LIMIT,
  GRATIA_CLAIM_AMOUNT,     
  CREATED_BY,      
  CREATED_DATETIME,      
  IS_ACTIVE      
 )      
 VALUES                      
 (      
  @LIMIT_ID,      
  @AUTHORITY_LEVEL,      
  @TITLE,      
  @PAYMENT_LIMIT,      
  @RESERVE_LIMIT,      
  @CLAIM_ON_DUMMY_POLICY, 
  @REOPEN_CLAIM_LIMIT,
  @GRATIA_CLAIM_AMOUNT,     
  @CREATED_BY,      
  @CREATED_DATETIME,      
  'Y'      
 )      

END     


GO

