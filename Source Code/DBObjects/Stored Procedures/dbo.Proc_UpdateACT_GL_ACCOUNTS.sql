IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateACT_GL_ACCOUNTS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateACT_GL_ACCOUNTS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------      
Proc Name       : dbo.Proc_UpdatetACT_GL_ACCOUNTS      
Created by      : Ajit Singh Chahal      
Date            : 5/18/2005      
Purpose     : to insert records in ACT_GL_ACCOUNTS      
Revison History :      
Used In  : Wolverine      
      
Modified By : Vijay Arora      
Modified Date : 05-10-2005      
Purpose : Update the Budget_Category_ID.      
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/      
-- DROP PROC dbo.Proc_UpdateACT_GL_ACCOUNTS      
CREATE  PROC dbo.Proc_UpdateACT_GL_ACCOUNTS      
(      
@GL_ID     int ,      
@FISCAL_ID int = null,      
@ACCOUNT_ID     int ,      
@CATEGORY_TYPE     smallint,      
@GROUP_TYPE     smallint,      
@ACC_TYPE_ID     int,      
@ACC_PARENT_ID     int,      
@ACC_NUMBER     decimal(5),      
@ACC_LEVEL_TYPE     nchar(10),      
@ACC_DESCRIPTION     nvarchar(100),      
@ACC_TOTALS_LEVEL     nchar(510),      
@ACC_JOURNAL_ENTRY     nchar(2),      
@ACC_CASH_ACCOUNT     nchar(2),      
@ACC_CASH_ACC_TYPE     nchar(2),      
@ACC_DISP_NUMBER     nvarchar(100),      
@ACC_PRODUCE_CHECK     char(1),      
@ACC_HAS_CHILDREN     nchar(2),      
@ACC_NEST_LEVEL     smallint,      
@ACC_CURRENT_BALANCE     decimal(9,2),      
@MODIFIED_BY    int,      
@LAST_UPDATED_DATETIME     datetime,      
@ACC_RELATES_TO_TYPE int,      
@BUDGET_CATEGORY_ID int ,
@WOLVERINE_USER_ID int      
)      
AS      
BEGIN      
 Declare @Count int      
 Set @Count= (SELECT count(*) FROM ACT_GL_ACCOUNTS where ACC_DISP_NUMBER=@ACC_DISP_NUMBER and ACCOUNT_ID<>@ACCOUNT_ID)      
 if(@Count=0)      
 BEGIN      
  update ACT_GL_ACCOUNTS      
  set      
  CATEGORY_TYPE= @CATEGORY_TYPE,      
  GROUP_TYPE= @GROUP_TYPE,      
  ACC_TYPE_ID= @ACC_TYPE_ID,      
  ACC_PARENT_ID= @ACC_PARENT_ID,      
  ACC_NUMBER= @ACC_NUMBER,      
  ACC_LEVEL_TYPE= @ACC_LEVEL_TYPE,      
  ACC_DESCRIPTION= @ACC_DESCRIPTION,      
  ACC_TOTALS_LEVEL= @ACC_TOTALS_LEVEL,      
  ACC_JOURNAL_ENTRY= @ACC_JOURNAL_ENTRY,      
  ACC_CASH_ACCOUNT= @ACC_CASH_ACCOUNT,      
  ACC_CASH_ACC_TYPE= @ACC_CASH_ACC_TYPE,      
  ACC_DISP_NUMBER= @ACC_DISP_NUMBER,      
  ACC_PRODUCE_CHECK= @ACC_PRODUCE_CHECK,      
  ACC_HAS_CHILDREN= @ACC_HAS_CHILDREN,      
  ACC_NEST_LEVEL=@ACC_NEST_LEVEL ,      
  ACC_CURRENT_BALANCE= @ACC_CURRENT_BALANCE,      
  MODIFIED_BY= @MODIFIED_BY,      
  LAST_UPDATED_DATETIME = @LAST_UPDATED_DATETIME,      
  ACC_RELATES_TO_TYPE = @ACC_RELATES_TO_TYPE,      
  BUDGET_CATEGORY_ID = @BUDGET_CATEGORY_ID,
  WOLVERINE_USER_ID = @WOLVERINE_USER_ID 
  where  GL_ID = @GL_ID and ACCOUNT_ID = @ACCOUNT_ID --and FISCAL_ID = @FISCAL_ID      
  --updating child a/c's display A/c no.      
    
  update ACT_GL_ACCOUNTS       
  set       
  ACC_DISP_NUMBER = convert(varchar,@ACC_NUMBER)+'.'+convert(varchar,floor(ACC_NUMBER))      
  where  GL_ID = @GL_ID and ACC_PARENT_ID = @ACCOUNT_ID --and FISCAL_ID = @FISCAL_ID      
      
 END        
END      
      
      
    
    
  



GO

