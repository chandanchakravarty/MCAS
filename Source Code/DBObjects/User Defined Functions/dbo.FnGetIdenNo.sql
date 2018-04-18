IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FnGetIdenNo]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[FnGetIdenNo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--DROP FUNCTION [dbo].[FnGetIdenNo] 
CREATE FUNCTION [dbo].[FnGetIdenNo] 
(
	-- Add the parameters for the function here
	@POLICY_NUMBER VArchar(12)
)
RETURNS Varchar(100)
AS
BEGIN

	
DECLARE @IDs VARCHAR(1000) 

/*SELECT @IDs = COALESCE(cast(@IDs as varchar) + ',', '') + cast(OPEN_ITEM.IDEN_ROW_ID  as varchar)
			  FROM POL_CUSTOMER_POLICY_LIST CPL          
		      INNER JOIN ACT_CUSTOMER_OPEN_ITEMS OPEN_ITEM          
		      ON CPL.CUSTOMER_ID = OPEN_ITEM.CUSTOMER_ID          
		      AND CPL.POLICY_ID  = OPEN_ITEM.POLICY_ID          
		      AND CPL.POLICY_VERSION_ID = OPEN_ITEM.POLICY_VERSION_ID          
		      INNER JOIN CLT_CUSTOMER_LIST CL          
		      ON CL.CUSTOMER_ID = CPL.CUSTOMER_ID           
		      WHERE (ISNULL(OPEN_ITEM.TOTAL_PAID,0) - ISNULL(OPEN_ITEM.TOTAL_DUE,0)) > 0          
		      AND          
		      ( SELECT ISNULL(SUM(ISNULL(OI.TOTAL_DUE,0) - ISNULL(OI.TOTAL_PAID,0)),0)           
		      FROM ACT_CUSTOMER_OPEN_ITEMS OI           
		      WHERE OI.CUSTOMER_ID = CPL.CUSTOMER_ID AND OI.POLICY_ID = CPL.POLICY_ID           
		      ) < 0           
		      AND ITEM_STATUS in ('RSP','ROP','OP','RP')          
		      AND ISNULL(IS_CHECK_CREATED,'N') = 'N'     
		      --AND CPL.POLICY_NUMBER = 'C1001222'  
			  AND POLICY_NUMBER = @POLICY_NUMBER    */ 


SELECT @IDs = COALESCE(cast(@IDs as varchar(1000)) + ',', '') + cast(IDEN_ROW_ID  as varchar(1000))
			  FROM VW_GetOpenItemsForCheck with(nolock)
			  WHERE POLICY_NUMBER = @POLICY_NUMBER

--set @IDS = (select  top 1 cast(IDEN_ROW_ID  as varchar(500)) + ',' 
--FROM VW_GetOpenItemsForCheck with(nolock)
--WHERE POLICY_NUMBER =@POLICY_NUMBER
--ORDER BY IDEN_ROW_ID
--FOR XML PATH(''))

	RETURN @IDS
END
--select dbo.FnGetIdenNo('A10000020')


GO

