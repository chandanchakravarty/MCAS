IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetClaimDataForDummyPolicy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetClaimDataForDummyPolicy]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*Proc Name     : dbo.Proc_GetClaimDataForDummyPolicy  
Created by      : Sumit Chhabra                                
Date            : 05/01/2006                                
Purpose       :Insert                                
Revison History :                                
Used In        : Wolverine                                
            
Modified By :    
Modified On :    
Purpose     :    
                 
------------------------------------------------------------                                
Date     Review By          Comments                                
------   ------------       -------------------------*/                                
CREATE PROC dbo.Proc_GetClaimDataForDummyPolicy                         
(           
 @CLAIM_NUMBER varchar(10),  
 @CLAIM_ID int                       
)                          
AS                                
BEGIN         
 SELECT CASE CLAIMANT_INSURED WHEN 1 THEN CLAIMANT_NAME  ELSE CUSTOMER_FIRST_NAME END  INSURED_NAME,
	CONVERT(CHAR,APP_EFFECTIVE_DATE,101) EFFECTIVE_DATE,POLICY_LOB AS LOB_ID FROM   
  POL_CUSTOMER_POLICY_LIST PCPL JOIN CLM_CLAIM_INFO CCI ON    
   PCPL.CUSTOMER_ID = CCI.CUSTOMER_ID AND  
   PCPL.POLICY_ID = CCI.POLICY_ID AND  
   PCPL.POLICY_VERSION_ID = CCI.POLICY_VERSION_ID  
  JOIN CLT_CUSTOMER_LIST CCL  
   ON PCPL.CUSTOMER_ID = CCL.CUSTOMER_ID  
   WHERE  
    CCI.CLAIM_NUMBER=@CLAIM_NUMBER AND  
    CCI.CLAIM_ID=@CLAIM_ID AND  
    PCPL.IS_ACTIVE='Y' AND  
    CCI.IS_ACTIVE='Y' AND  
    CCL.IS_ACTIVE='Y'  
  
END  



GO

