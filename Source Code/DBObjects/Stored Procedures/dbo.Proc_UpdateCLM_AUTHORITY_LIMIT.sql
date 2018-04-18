IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateCLM_AUTHORITY_LIMIT]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateCLM_AUTHORITY_LIMIT]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                    
                    
Proc Name       : Proc_UpdateCLM_AUTHORITY_LIMIT    
Created by      : Sumit Chhabra    
Date            : 20/04/2006                    
Purpose         : Update of Authority Limit data in CLM_AUTHORITY_LIMIT    
Revison History :                    
Used In                   : Wolverine      
Modified By		: Agniswar
Modified On		: 26 Sep 2011
Purpose			: Singapore Implementation              
------------------------------------------------------------                    
Date     Review By          Comments                    
------   ------------       -------------------------*/                    
-- drop PROC dbo.Proc_UpdateCLM_AUTHORITY_LIMIT
CREATE PROC [dbo].[Proc_UpdateCLM_AUTHORITY_LIMIT]                      
(                      
 @AUTHORITY_LEVEL int,      
 @TITLE varchar(50),      
 @PAYMENT_LIMIT decimal(18,2),      
 @RESERVE_LIMIT decimal(18,2),      
 @CLAIM_ON_DUMMY_POLICY bit,
 @REOPEN_CLAIM_LIMIT decimal(18,2),
 @GRATIA_CLAIM_AMOUNT decimal(18,2),      
 @MODIFIED_BY int,      
 @LAST_UPDATED_DATETIME datetime,      
 @LIMIT_ID int      
                  
)                      
AS                      
BEGIN       
--Checking the duplicacy of Authority Level Data    
IF EXISTS(SELECT LIMIT_ID FROM CLM_AUTHORITY_LIMIT WHERE AUTHORITY_LEVEL=@AUTHORITY_LEVEL AND LIMIT_ID<>@LIMIT_ID)    
 return -1      
  
            
 UPDATE CLM_AUTHORITY_LIMIT SET       
  AUTHORITY_LEVEL=@AUTHORITY_LEVEL,      
  TITLE=@TITLE,      
  PAYMENT_LIMIT=@PAYMENT_LIMIT,      
  RESERVE_LIMIT=@RESERVE_LIMIT,      
  CLAIM_ON_DUMMY_POLICY=@CLAIM_ON_DUMMY_POLICY,
  REOPEN_CLAIM_LIMIT = @REOPEN_CLAIM_LIMIT,
  GRATIA_CLAIM_AMOUNT = @GRATIA_CLAIM_AMOUNT  ,  
  MODIFIED_BY=@MODIFIED_BY,      
  LAST_UPDATED_DATETIME=@LAST_UPDATED_DATETIME      
 WHERE      
  LIMIT_ID=@LIMIT_ID      
       
END            
    
    
  







GO

