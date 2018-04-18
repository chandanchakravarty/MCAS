IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_CopyFrom_TEMP_ACT_DISTRIBUTION_DETAILS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_CopyFrom_TEMP_ACT_DISTRIBUTION_DETAILS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--14/9/2005
--Ajit singh Chahal
CREATE PROCEDURE PROC_CopyFrom_TEMP_ACT_DISTRIBUTION_DETAILS
(
@ACTUAL_CHECK_ID INT,
@TEMP_CHECK_ID int
)
AS
BEGIN
insert into ACT_DISTRIBUTION_DETAILS 
(GROUP_ID,GROUP_TYPE, ACCOUNT_ID,DISTRIBUTION_PERCT, DISTRIBUTION_AMOUNT,NOTE,IS_ACTIVE, CREATED_BY,CREATED_DATETIME ,MODIFIED_BY ,LAST_UPDATED_DATETIME ) 
select @ACTUAL_CHECK_ID,GROUP_TYPE, ACCOUNT_ID,DISTRIBUTION_PERCT, DISTRIBUTION_AMOUNT,NOTE,IS_ACTIVE, CREATED_BY,CREATED_DATETIME ,MODIFIED_BY ,LAST_UPDATED_DATETIME 
nolock from TEMP_ACT_DISTRIBUTION_DETAILS where GROUP_ID = @TEMP_CHECK_ID 

END


GO

