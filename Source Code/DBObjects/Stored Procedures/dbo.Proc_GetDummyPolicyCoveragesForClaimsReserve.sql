IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetDummyPolicyCoveragesForClaimsReserve]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetDummyPolicyCoveragesForClaimsReserve]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                                       
Proc Name   	: dbo.Proc_GetDummyPolicyCoveragesForClaimsReserve                                                        
Created by  	: Asfa Praveen                                                     
Date		: 07 Dec,2007                                                      
Purpose 	: Get the Dummy Policy coverages for Claims Reserves                                                        
Revison History  :                                                         
------------------------------------------------------------                                                      
Date Review By  Comments                                                       
------   ------------   -------------------------*/                             
--drop proc dbo.Proc_GetDummyPolicyCoveragesForClaimsReserve                                                  
CREATE PROC dbo.Proc_GetDummyPolicyCoveragesForClaimsReserve   
(                                                      
 @CLAIM_ID INT                                                       
)                                                       
AS                                                      
BEGIN                                                      

DECLARE @STATE_ID SMALLINT
DECLARE @VEHICLE_ID INT

SELECT @VEHICLE_ID = INSURED_VEHICLE_ID FROM CLM_INSURED_VEHICLE WHERE CLAIM_ID=@CLAIM_ID

SELECT @STATE_ID=ISNULL(DUMMY_STATE,0) FROM CLM_DUMMY_POLICY WHERE CLAIM_ID=@CLAIM_ID                                                                                                 

SELECT @VEHICLE_ID AS VEHICLE_ID, @STATE_ID AS STATE_ID, COV_ID, CLAIM_ID, COV_DES AS COV_DESC, LIMIT_1 AS LIMIT, DEDUCTIBLE_1 AS DEDUCTIBLE, COV_ID_CLAIM, 
	'' AS OUTSTANDING,'' AS RI_RESERVE,'' AS RESERVE_ID, '' AS REINSURANCE_CARRIER
	FROM MNT_CLAIM_COVERAGE 
	WHERE CLAIM_ID = @CLAIM_ID
	ORDER BY COV_ID
END


GO

