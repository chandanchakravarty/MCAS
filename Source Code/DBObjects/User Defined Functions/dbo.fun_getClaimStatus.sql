IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fun_getClaimStatus]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fun_getClaimStatus]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--drop FUNCTION dbo.fun_getClaimStatus
/* Function 
created By		: Pravesh K chandel
Created Date	: 31 May 10
Purpose			: To get a Claim Status on given Month and Year
*/
CREATE FUNCTION [dbo].[fun_getClaimStatus](@ClaimID INT ,@Month SMALLINT,@Year SMALLINT) 
RETURNS nvarchar(20)
AS  
BEGIN 
declare @ClaimStatus nvarchar(20)
 DECLARE @MONTH_END_DATE DATETIME  
  
SET @MONTH_END_DATE = DATEADD(DD,-1,DATEADD(MM,1,CAST(CONVERT(VARCHAR,@MONTH) + '/01/' + CONVERT(VARCHAR,@YEAR) AS DATETIME)))  

SELECT @ClaimStatus = 

CASE WHEN CLOSED_DATE IS NULL THEN 'Open'
     WHEN datediff(dd,REOPENED_DATE,@MONTH_END_DATE)>= 0 AND datediff(dd,CLOSED_DATE,@MONTH_END_DATE)< 0 THEN 'Open'
	 WHEN datediff(dd,CLOSED_DATE,@MONTH_END_DATE)< 0 THEN 'Open'
	 WHEN datediff(dd,CLOSED_DATE,@MONTH_END_DATE)>= 0 THEN 'Closed'
END
FROM CLM_CLAIM_INFO CLM WITH(NOLOCK)
--LEFT OUTER JOIN CLM_REOPEN_CLAIM CRC WITH(NOLOCK)
--ON CLM.CLAIM_ID=CLM.CLAIM_ID

WHERE CLAIM_ID=@ClaimID

Return isnull(@ClaimStatus,'')

END








GO

