IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertACT_FEE_REVERSAL]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertACT_FEE_REVERSAL]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*--------------------------------------------------------------------------      
Proc Name       : dbo.Proc_InsertACT_FEE_REVERSAL      
Created by      : Ashwani      
Date            : 19 June 2006      
Purpose        : To insert records         
Revison History :        
Used In         : Wolverine        
------------------------------------------------------------------------------        
Date     Review By          Comments        
------   ------------       -------------------------------------------------------*/        
-- drop proc Dbo.Proc_InsertACT_FEE_REVERSAL       
CREATE PROC Dbo.Proc_InsertACT_FEE_REVERSAL       
(        
 @IDEN_ROW_ID smallint out,      
 @FEE_TYPE  varchar(30),      
 @FEES_AMOUNT decimal(18,2),      
 @FEES_REVERSE decimal(18,2),      
 @IS_ACTIVE char(2),      
 @CREATED_BY int,      
 @CREATED_DATETIME datetime,      
 @CUSTOMER_OPEN_ITEM_ID int,      
 @CUSTOMER_ID int,      
 @POLICY_ID int ,      
 @POLICY_VERSION_ID int,      
 @IS_COMMITTED bit,      
 @MODIFIED_BY int ,      
 @LAST_UPDATED_DATETIME datetime      
)        
AS        
BEGIN        
--select  @IDEN_ROW_ID=isnull(Max(IDEN_ROW_ID),0)+1 from ACT_FEE_REVERSAL        
      
INSERT INTO ACT_FEE_REVERSAL      
(        
 CUSTOMER_OPEN_ITEM_ID,CUSTOMER_ID,POLICY_ID,      
 POLICY_VERSION_ID,FEE_TYPE,FEES_AMOUNT,FEES_REVERSE,      
 IS_COMMITTED,IS_ACTIVE,CREATED_BY,CREATED_DATETIME,      
 MODIFIED_BY,LAST_UPDATED_DATETIME      
)        
VALUES        
(        
 @CUSTOMER_OPEN_ITEM_ID ,@CUSTOMER_ID ,@POLICY_ID ,      
 @POLICY_VERSION_ID ,@FEE_TYPE,@FEES_AMOUNT,@FEES_REVERSE  ,      
 @IS_COMMITTED, @IS_ACTIVE, @CREATED_BY ,@CREATED_DATETIME ,      
 @MODIFIED_BY , @LAST_UPDATED_DATETIME       
)        
    
SET @IDEN_ROW_ID = @@IDENTITY
END        
        
        
      
        
      
    
    
    
  





GO

