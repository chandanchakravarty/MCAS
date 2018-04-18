IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_GetpdfAdjusted_Premium]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_GetpdfAdjusted_Premium]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[PROC_GetpdfAdjusted_Premium]
(
 @CUSTOMERID   NVARCHAR(20),                                                
 @POLID                NVARCHAR(20),                                                
 @VERSIONID   NVARCHAR(20)                                                
)
As
BEGIN 

SELECT  	sum(
		CASE WHEN CPSD.WRITTEN_PREM IS NULL OR CPSD.WRITTEN_PREM='0' or CPSD.WRITTEN_PREM=''  THEN 0.00    
			 ELSE CONVERT(decimal(18,2),CPSD.WRITTEN_PREM) END  
	  
	    ) AS WRITTEN_PREMIUM

	FROM CLT_PREMIUM_SPLIT_DETAILS CPSD WITH(NOLOCK)        
	INNER JOIN CLT_PREMIUM_SPLIT CPS    WITH(NOLOCK) 
	ON CPS.UNIQUE_ID = CPSD.SPLIT_UNIQUE_ID   
	WHERE CPS.CUSTOMER_ID=@CUSTOMERID AND CPS.POLICY_ID=@POLID AND CPS.POLICY_VERSION_ID=@VERSIONID and CPSD.COMPONENT_CODE in ('SUMTOTAL','SUMTOTAL_S','BOAT_UNATTACH_PREMIUM')

END


GO

