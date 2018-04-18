
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

/*----------------------------------------------------------  
Proc Name       : dbo.Proc_InsertACT_GL_ACCOUNTS  
Created by      : Ajit Singh Chahal  
Date            : 5/18/2005  
Purpose     : to insert records in ACT_GL_ACCOUNTS  
Revison History :  
Used In  : Wolverine  
  
Modified by  : Vijay Arora  
Modified Date : 05-10-2005  
Purpose  : To add the budget category id.  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
-- DROP PROC Proc_InsertACT_GL_ACCOUNTS  
CREATE  prOC dbo.Proc_InsertACT_GL_ACCOUNTS  
(  
@GL_ID     int,  
@FISCAL_ID     int = null,  
@ACCOUNT_ID     int output,  
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
@IS_ACTIVE     nchar(2),  
@CREATED_BY     int,  
@CREATED_DATETIME     datetime,  
@ACC_RELATES_TO_TYPE int,  
@BUDGET_CATEGORY_ID int,
@WOLVERINE_USER_ID int  
)  
AS  
BEGIN  
Declare @Count int  
if @ACC_PARENT_ID is null  
 Set @Count= (SELECT count(*) FROM ACT_GL_ACCOUNTS where ACC_NUMBER=@ACC_NUMBER)  
else  
 Set @Count= (SELECT count(*) FROM ACT_GL_ACCOUNTS where ACC_NUMBER=@ACC_NUMBER and ACC_PARENT_ID = @ACC_PARENT_ID)  
  
  
if(@Count=0)  
BEGIN  
select  @ACCOUNT_ID=isnull(Max(ACCOUNT_ID),0)+1 from ACT_GL_ACCOUNTS  
INSERT INTO ACT_GL_ACCOUNTS  
(  
GL_ID,  
--FISCAL_ID,  
ACCOUNT_ID,  
CATEGORY_TYPE,  
GROUP_TYPE,  
ACC_TYPE_ID,  
ACC_PARENT_ID,  
ACC_NUMBER,  
ACC_LEVEL_TYPE,  
ACC_DESCRIPTION,  
ACC_TOTALS_LEVEL,  
ACC_JOURNAL_ENTRY,  
ACC_CASH_ACCOUNT,  
ACC_CASH_ACC_TYPE,  
ACC_DISP_NUMBER,  
ACC_PRODUCE_CHECK,  
ACC_HAS_CHILDREN,  
ACC_NEST_LEVEL,  
ACC_CURRENT_BALANCE,  
IS_ACTIVE,  
CREATED_BY,  
CREATED_DATETIME,  
MODIFIED_BY,  
LAST_UPDATED_DATETIME,  
ACC_RELATES_TO_TYPE,  
BUDGET_CATEGORY_ID,
WOLVERINE_USER_ID  
)  
VALUES  
(  
@GL_ID,  
--@FISCAL_ID,  
@ACCOUNT_ID,  
@CATEGORY_TYPE,  
@GROUP_TYPE,  
@ACC_TYPE_ID,  
@ACC_PARENT_ID,  
@ACC_NUMBER,  
@ACC_LEVEL_TYPE,  
@ACC_DESCRIPTION,  
@ACC_TOTALS_LEVEL,  
@ACC_JOURNAL_ENTRY,  
@ACC_CASH_ACCOUNT,  
@ACC_CASH_ACC_TYPE,  
@ACC_DISP_NUMBER,  
@ACC_PRODUCE_CHECK,  
@ACC_HAS_CHILDREN,  
@ACC_NEST_LEVEL,  
@ACC_CURRENT_BALANCE,  
@IS_ACTIVE,  
@CREATED_BY,  
@CREATED_DATETIME,  
@CREATED_BY,  
@CREATED_DATETIME,  
@ACC_RELATES_TO_TYPE,  
@BUDGET_CATEGORY_ID,
@WOLVERINE_USER_ID  
)  
END  
ELSE  
BEGIN  
/*Record already exist*/  
  SELECT @ACCOUNT_ID = -1  
  
END  
  
end  
  

  
  
  
  
  
  
  
  
  
  
  
  
  



