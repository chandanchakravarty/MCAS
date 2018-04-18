IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetClaimsToClose]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetClaimsToClose]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*---------------------------------------------------------- 
Proc Name       : Proc_GetClaimsToClose                                          
Created by      : Pravesh K Chandel                                                                 
Date            : 17 Jan 2010                                                                  
Purpose         : USED get Claiom List to CLOSE CLAIM BY EOD PROCESS                                                                                                           
Revison History :                                                                          
Used In         : CLAIM                                                                                            
------------------------------------------------------------                                                                          
Date     Review By          Comments                                                                          
------   ------------       -------------------------                                      
*/                                                                          
--DROP PROC Proc_GetClaimsToClose
CREATE PROC [dbo].[Proc_GetClaimsToClose]  
                                                                             
AS                                                                          
BEGIN                                            

 
 -- FETCH CLAIM ID (UNCOMPLETED) AND RELATED LAST ACTIVITY ID HAVING STATUS COMPLETED AND OUSTANDING AMOUNT IS ZER0
 SELECT CLM.CUSTOMER_ID,CLM.POLICY_ID,CLM.POLICY_VERSION_ID, P.CLAIM_ID FROM CLM_ACTIVITY (nolock) P 
 INNER JOIN   
  (
	 -- FETCH CLAIM ID (UNCOMPLETED) AND RELATED LAST ACTIVITY ID
	 SELECT MAX(A.ACTIVITY_ID) AS ACTIVITY_ID ,L.CLAIM_ID  
	 FROM   CLM_CLAIM_INFO (nolock) L INNER JOIN
			CLM_ACTIVITY (nolock) A ON A.CLAIM_ID=L.CLAIM_ID 
		 where  L.CLAIM_STATUS=11739 -- FOR INCOMPLETED CLAIM
		 group by L.CLAIM_ID
	  )T ON P.CLAIM_ID=T.CLAIM_ID AND P.ACTIVITY_ID=T.ACTIVITY_ID 
JOIN CLM_CLAIM_INFO (NOLOCK) CLM 
ON CLM.CLAIM_ID=P.CLAIM_ID
WHERE P.ACTIVITY_STATUS=11801 AND  P.CLAIM_RESERVE_AMOUNT=0.00
                   
END     



GO

