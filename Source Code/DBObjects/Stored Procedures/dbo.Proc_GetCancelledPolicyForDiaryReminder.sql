IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCancelledPolicyForDiaryReminder]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCancelledPolicyForDiaryReminder]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------              
Proc Name       : dbo.Proc_GetCancelledPolicyForDiaryReminder      
Created by      : Praveen kasana        
Date            : 25 feb 2008  
Purpose         : Selects the records from policy which has been cancelled  
Revison History :              
Modified by		; Ravindra	
Purpose			: TO include policies which are marked for cancellation 
					and to select only desired columns no need to select all columns of both tables
Used In         : Wolverine       
------------------------------------------------------------              
Date     Review By          Comments              
------   ------------       -------------------------*/      
--DROP PROC dbo.Proc_GetCancelledPolicyForDiaryReminder            
CREATE PROC [dbo].[Proc_GetCancelledPolicyForDiaryReminder]      
(  
 @DEPOSIT_ID int  
)      
AS          
BEGIN          

--Ravindra : Send diary if Policy is marked for cancellation and rescinded
--SELECT POL.CUSTOMER_ID , POL.POLICY_VERSION_ID , POL.POLICY_ID , 
--POL.POLICY_NUMBER 
--FROM POL_CUSTOMER_POLICY_LIST POL WITH(NOLOCK)INNER JOIN   
--ACT_CURRENT_DEPOSIT_LINE_ITEMS DEP WITH(NOLOCK)ON POL.CUSTOMER_ID = DEP.CUSTOMER_ID   
--AND POL.POLICY_ID = DEP.POLICY_ID AND POL.POLICY_VERSION_ID = DEP.POLICY_VERSION_ID   
--WHERE DEPOSIT_ID=@DEPOSIT_ID AND POLICY_STATUS IN ('CANCEL' ,'SCANCEL' , 'RESCIND' ) 

--Praveen K : Send diary if Policy is marked for cancellation and rescinded - For Latest Version
SELECT POL.CUSTOMER_ID , POL.POLICY_VERSION_ID , POL.POLICY_ID , 
POL.POLICY_NUMBER 
FROM POL_CUSTOMER_POLICY_LIST POL WITH(NOLOCK)INNER JOIN
ACT_CURRENT_DEPOSIT_LINE_ITEMS DEP WITH(NOLOCK)ON POL.CUSTOMER_ID = DEP.CUSTOMER_ID
AND POL.POLICY_ID = DEP.POLICY_ID --AND POL.POLICY_VERSION_ID = DEP.POLICY_VERSION_ID
WHERE DEPOSIT_ID = @DEPOSIT_ID
AND POL.POLICY_VERSION_ID IN

	(SELECT MAX(POLICY_VERSION_ID) FROM POL_CUSTOMER_POLICY_LIST WHERE
	 CUSTOMER_ID = POL.CUSTOMER_ID AND POLICY_ID = POL.POLICY_ID)

AND POLICY_STATUS IN ('CANCEL' ,'SCANCEL' , 'RESCIND')

      
END          
        
  
  






GO

