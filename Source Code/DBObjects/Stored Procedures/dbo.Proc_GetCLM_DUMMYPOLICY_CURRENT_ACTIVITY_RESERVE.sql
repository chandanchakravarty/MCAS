IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCLM_DUMMYPOLICY_CURRENT_ACTIVITY_RESERVE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCLM_DUMMYPOLICY_CURRENT_ACTIVITY_RESERVE]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                                                                                                              
Proc Name       : Proc_GetCLM_DUMMYPOLICY_CURRENT_ACTIVITY_RESERVE                                                                
Created by      : Asfa Praveen                                                                
Date            : 07/Dec/2007                                                                                
Purpose         : Get Reserve info of current activity for Dummy Policy
Revison History :                                                                                
Used In         : Wolverine                                                                                                                        
------------------------------------------------------------                                                                                                                              
Date     Review By          Comments                                                                                                                              
------   ------------       -------------------------*/                                                                                                                              
--  Drop PROC dbo.Proc_GetCLM_DUMMYPOLICY_CURRENT_ACTIVITY_RESERVE      
--  Proc_GetCLM_DUMMYPOLICY_CURRENT_ACTIVITY_RESERVE 
--  select * from CLM_ACTIVITY_RESERVE      
      
create PROC dbo.Proc_GetCLM_DUMMYPOLICY_CURRENT_ACTIVITY_RESERVE      
 @CLAIM_ID int,                                                      
 @ACTIVITY_ID int,
 @TRANSACTION_ID INT                                                          
AS                                                                                                                              
BEGIN                                                   
                                                
DECLARE @STATE_ID SMALLINT
DECLARE @VEHICLE_ID INT

SELECT @VEHICLE_ID = INSURED_VEHICLE_ID FROM CLM_INSURED_VEHICLE WHERE CLAIM_ID=@CLAIM_ID

SELECT @STATE_ID=ISNULL(DUMMY_STATE,0) FROM CLM_DUMMY_POLICY WHERE CLAIM_ID=@CLAIM_ID                                                                                                 

SELECT @VEHICLE_ID AS VEHICLE_ID, @STATE_ID AS STATE_ID, MCC.COV_ID, MCC.CLAIM_ID, MCC.COV_DES AS COV_DESC, MCC.LIMIT_1 AS LIMIT, MCC.DEDUCTIBLE_1 AS DEDUCTIBLE, MCC.COV_ID_CLAIM, 
       CAR.OUTSTANDING, CAR.RI_RESERVE, CAR.REINSURANCE_CARRIER, MLV.LOOKUP_VALUE_DESC AS REINSURANCECARRIER, CAR.ACTION_ON_PAYMENT AS CLM_RESERVE_ACTION_ON_PAYMENT,
       CAR.CRACCTS AS CLM_RESERVE_CRACCTS,CAR.DRACCTS AS CLM_RESERVE_DRACCTS,CAR.RESERVE_ID
FROM MNT_CLAIM_COVERAGE MCC
LEFT OUTER JOIN CLM_ACTIVITY_RESERVE CAR
ON MCC.CLAIM_ID = CAR.CLAIM_ID AND MCC.COV_ID = CAR.COVERAGE_ID
LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV 
ON MLV.LOOKUP_UNIQUE_ID = CAR.REINSURANCE_CARRIER
WHERE CAR.CLAIM_ID = @CLAIM_ID
AND CAR.TRANSACTION_ID=@TRANSACTION_ID
AND CAR.ACTIVITY_ID=@ACTIVITY_ID
ORDER BY CAR.RESERVE_ID

END          







GO

