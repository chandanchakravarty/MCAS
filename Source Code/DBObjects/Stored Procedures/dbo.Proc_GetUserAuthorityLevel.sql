IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetUserAuthorityLevel]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetUserAuthorityLevel]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                
Proc Name       : dbo.Proc_GetUserAuthorityLevel                
Created by      : Sumit Chhabra                
Date            : 4/21/2006                
Purpose        : Get authority limits, payment,reserve limits for the user against the LOB assigned for dummy policy            
Revison History :                
Used In        : Wolverine                
------------------------------------------------------------                
Modified By  : Asfa Praveen    
Date   : 29/Aug/2007    
Purpose  : To add Adjuster_ID column    
------------------------------------------------------------                                                                    
Date     Review By          Comments                
------   ------------       -------------------------*/                
--Drop PROC dbo.Proc_GetUserAuthorityLevel   
CREATE PROC dbo.Proc_GetUserAuthorityLevel               
(                
@USER_ID int,        
@DUMMY_POLICY_ID int            
)                
AS                
BEGIN                
declare @LOB_ID int          
--Get LOB_ID of the current dummy policy            
 SELECT @LOB_ID = CDP.LOB_ID  FROM CLM_DUMMY_POLICY  CDP          
 LEFT OUTER JOIN CLM_CLAIM_INFO CCI ON CDP.CLAIM_ID = CCI.CLAIM_ID           
 WHERE CDP.DUMMY_POLICY_ID=@DUMMY_POLICY_ID            
   
/*The currently logged user must be of Claim Adjuster Type and must have the authority to Match a Dummy Claim   
with a Policy after Adding a claim against an unmatched policy.(Asfa 07-Sept-2007)         
  
SELECT PAYMENT_LIMIT,RESERVE_LIMIT,CLAIM_ON_DUMMY_POLICY,CAA.LOB_ID    
FROM MNT_USER_LIST MUL JOIN CLM_ADJUSTER CA ON MUL.USER_ID=CA.USER_ID    
JOIN CLM_ADJUSTER_AUTHORITY CAA ON CA.ADJUSTER_ID=CAA.ADJUSTER_ID      
JOIN CLM_AUTHORITY_LIMIT CAL ON CAA.LIMIT_ID = CAL.LIMIT_ID  
WHERE MUL.USER_ID=@USER_ID AND CAL.CLAIM_ON_DUMMY_POLICY=1    
AND CAA.LOB_ID = @LOB_ID AND CAA.IS_ACTIVE='Y'   
AND CA.IS_ACTIVE = 'Y' AND CAL.IS_ACTIVE = 'Y'   
*/          
  
 SELECT             
  PAYMENT_LIMIT,RESERVE_LIMIT,CLAIM_ON_DUMMY_POLICY,CAA.LOB_ID            
 FROM            
  CLM_ADJUSTER_AUTHORITY CAA             
 JOIN             
  CLM_ADJUSTER CA            
 ON             
  CA.ADJUSTER_ID = CAA.ADJUSTER_ID            
 JOIN            
  CLM_AUTHORITY_LIMIT CAL            
 ON            
  CAA.LIMIT_ID = CAL.LIMIT_ID            
 WHERE            
 CA.USER_ID = @USER_ID and            
-- CA.ADJUSTER_CODE = @USER_ID and        //Commented by Asfa 29/Aug/2007    
 CAA.LOB_ID = @LOB_ID and          
 CAA.IS_ACTIVE='Y' AND          
 CA.IS_ACTIVE = 'Y' AND          
 CAL.IS_ACTIVE = 'Y'            
  
END                
              
            
          
        
      
    
  



GO

