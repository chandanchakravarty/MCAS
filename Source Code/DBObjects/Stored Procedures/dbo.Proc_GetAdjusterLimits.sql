IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAdjusterLimits]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAdjusterLimits]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                        
Proc Name       : dbo.Proc_GetAdjusterLimits                                        
Created by      : Sumit Chhabra                                  
Date            : 07/07/2006                                        
Purpose         : Get the Reserve and Payment limits for an Adjuster    
Revison History :                                        
Used In  : Wolverine                                        
------------------------------------------------------------                                        
Modified By 	: Asfa Praveen
Date 		: 29/Aug/2007
Purpose		: To add Adjuster_ID column
------------------------------------------------------------                                                                
Date     Review By          Comments                                        
------   ------------       -------------------------*/                                        
--DROP PROC dbo.Proc_GetAdjusterLimits      
CREATE PROC dbo.Proc_GetAdjusterLimits                                        
(                                        
 @CLAIM_ID     int,      
 @ADJUSTER_ID int                           
)                                        
AS                                        
BEGIN              
      
DECLARE @LOB_ID INT          
SELECT @LOB_ID=POLICY_LOB FROM POL_CUSTOMER_POLICY_LIST PCPL JOIN CLM_CLAIM_INFO CCI     
 ON PCPL.CUSTOMER_ID = CCI.CUSTOMER_ID AND    
   PCPL.POLICY_ID = CCI.POLICY_ID AND    
   PCPL.POLICY_VERSION_ID = CCI.POLICY_VERSION_ID     
 WHERE    
   CCI.CLAIM_ID=@CLAIM_ID    
    
 SELECT ISNULL(RESERVE_LIMIT,0) AS RESERVE_LIMIT, ISNULL(PAYMENT_LIMIT,0) AS PAYMENT_LIMIT     
  FROM CLM_ADJUSTER_AUTHORITY CAA JOIN CLM_AUTHORITY_LIMIT CAL                                              
   ON CAA.LIMIT_ID = CAL.LIMIT_ID LEFT JOIN CLM_ADJUSTER CA ON CA.ADJUSTER_ID = CAA.ADJUSTER_ID                                                                    
   WHERE CA.ADJUSTER_ID=@ADJUSTER_ID 
   --CA.ADJUSTER_CODE=@ADJUSTER_ID //Commented by Asfa 29/Aug/2007
   AND CAA.LOB_ID=@LOB_ID AND CAA.IS_ACTIVE='Y'             
      
           
END               
    
    
  



GO

