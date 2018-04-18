IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetClaimsToAutoReserveUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetClaimsToAutoReserveUpdate]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                                  
Proc Name             : Dbo.Proc_GetClaimsToAutoReserveUpdate                                                  
Created by            : Pravesh K Chandel                                       
Date                  : 10 Feb 2011                                      
Purpose               : To get list of claims to update Reserve a month End
Revison History       :                                                  
Used In               : claim module        
------------------------------------------------------------                                                  
Date     Review By          Comments                     
            
drop Proc Proc_GetClaimsToAutoReserveUpdate                            
------   ------------       -------------------------*/                                                  
              
CREATE PROCEDURE [dbo].[Proc_GetClaimsToAutoReserveUpdate]
          
AS                      
BEGIN               
  -- FETCH CLAIM ID (UNCOMPLETED) AND RELATED LAST ACTIVITY ID HAVING STATUS COMPLETED AND OUSTANDING AMOUNT IS ZER0  
  SELECT CLM.CUSTOMER_ID,CLM.POLICY_ID,CLM.POLICY_VERSION_ID, P.CLAIM_ID 
  FROM CLM_ACTIVITY P 
    INNER JOIN     
       (  
			 -- FETCH CLAIM ID (UNCOMPLETED) AND RELATED LAST ACTIVITY ID  
		  SELECT MAX(A.ACTIVITY_ID) AS ACTIVITY_ID ,L.CLAIM_ID    
		  FROM   CLM_CLAIM_INFO (NOLOCK) L 
		        INNER JOIN CLM_ACTIVITY (NOLOCK)  A ON A.CLAIM_ID=L.CLAIM_ID   
		  where  L.CLAIM_STATUS=11739 -- FOR INCOMPLETED CLAIM  
		  group by L.CLAIM_ID  
	    
	  )T ON P.CLAIM_ID=T.CLAIM_ID AND P.ACTIVITY_ID=T.ACTIVITY_ID   
	 JOIN CLM_CLAIM_INFO (NOLOCK) CLM ON CLM.CLAIM_ID=P.CLAIM_ID 
	  WHERE    P.ACTIVITY_STATUS=11801  -- FOR COMPLETED ACTIVITY (CLAIM DOES NOT HAVE ANY IN PROGRESS ACTIVITY)
	       AND P.ACTIVITY_REASON<>167   -- FOR CLOSE ACTIVITY(LAST ACTIVITY SHOULD NOT A CLOSE ACTIVITY)
  
          
END                      
    


GO

