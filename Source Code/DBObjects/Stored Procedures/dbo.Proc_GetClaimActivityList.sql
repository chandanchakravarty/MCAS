IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetClaimActivityList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetClaimActivityList]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

  
/*----------------------------------------------------------                                                        
Proc Name             : Dbo.[Proc_GetClaimActivityList]                                                        
Created by            : Santosh Kumar Gautam                                 
Date                  : 17 June 2011                             
Purpose               :          
Revison History       :                                                        
Used In               :           
------------------------------------------------------------                                                        
Date     Review By          Comments                           
      
drop Proc [Proc_GetClaimActivityList]  'SH000020',1                                       
------   ------------       -------------------------*/             
CREATE PROC [dbo].[Proc_GetClaimActivityList]          
(                
 @CLAIM_NUMBER     NVARCHAR(20),
 @LANG_ID		   INT           
)                
AS                
BEGIN    
 
  
   SELECT 
     CUSTOMER_ID,
     POLICY_ID,
     POLICY_VERSION_ID,
     CLAIM_ID
   FROM CLM_CLAIM_INFO  WITH(NOLOCK)
   WHERE CLAIM_NUMBER=@CLAIM_NUMBER
   
   SELECT      
        (CAST(ACTIVITY_ID AS NVARCHAR(10))+' - '+ISNULL(TM.TYPE_DESC,T.DETAIL_TYPE_DESCRIPTION)) AS ACTIVITY_TEXT,
        ACTIVITY_ID
   FROM CLM_ACTIVITY  ACT WITH(NOLOCK) LEFT OUTER JOIN
        CLM_TYPE_DETAIL T ON ACT.ACTION_ON_PAYMENT =T.DETAIL_TYPE_ID LEFT OUTER JOIN
        CLM_TYPE_DETAIL_MULTILINGUAL TM ON TM.DETAIL_TYPE_ID=T.DETAIL_TYPE_ID AND LANG_ID=@LANG_ID
   WHERE ACT.ACTIVITY_STATUS=11801 AND  ACT.IS_ACTIVE='Y' AND ACTION_ON_PAYMENT IN(180,181,190,192) AND CLAIM_ID IN (SELECT CLAIM_ID FROM CLM_CLAIM_INFO WHERE CLAIM_NUMBER=@CLAIM_NUMBER)
   ORDER BY ACTIVITY_ID
    
  
END          
          
        
      
    
    
    
    
  
GO

