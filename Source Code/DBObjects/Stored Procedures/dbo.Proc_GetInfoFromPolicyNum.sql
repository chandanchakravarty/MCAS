IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetInfoFromPolicyNum]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetInfoFromPolicyNum]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--begin tran
--drop proc Proc_GetInfoFromPolicyNum
--go
/*----------------------------------------------------------        
Proc Name       : dbo.Proc_GetInfoFromPolicyNum        
Created by      :         
Date            :     
Purpose         : Fetches PolicyID, CustID , max Policy Ver ID from Policy number      
Revison History :        
Used In         : Wolverine        
------------------------------------------------------------        
Date     Review By          Comments        
------   ------------       -------------------------*/ 
-- Proc_GetInfoFromPolicyNum  'H1004196'   
-- DROP Proc dbo.Proc_GetInfoFromPolicyNum         
CREATE PROCEDURE [dbo].[Proc_GetInfoFromPolicyNum]      
(          
  @POLICY_NUMBER NVARCHAR(25)    
)              
AS                   
DECLARE @CUSTOMER_ID	INT
DECLARE @POLICY_ID		INT

SELECT @CUSTOMER_ID = CUSTOMER_ID, @POLICY_ID = POLICY_ID
FROM POL_CUSTOMER_POLICY_LIST WHERE POLICY_NUMBER = @POLICY_NUMBER
    
BEGIN                        

	SELECT POLICY_ID,POL.CUSTOMER_ID,@POLICY_NUMBER POLICY_NUMBER,POL.POLICY_VERSION_ID as POLICY_VERSION_ID,
	ISNULL(AGN.AGENCY_DISPLAY_NAME,'') AS AGENCY_NAME,ISNULL(CUST.CUSTOMER_FIRST_NAME,'') +' ' + ISNULL(CUST.CUSTOMER_MIDDLE_NAME,'') +' ' + ISNULL(CUST.CUSTOMER_LAST_NAME,'') AS CUSTOMER_NAME,
	AGN.AGENCY_ID,POL.BILL_TYPE
	FROM POL_CUSTOMER_POLICY_LIST POL (NOLOCK)
	INNER JOIN CLT_CUSTOMER_LIST CUST (NOLOCK) ON CUST.CUSTOMER_ID = POL.CUSTOMER_ID
	INNER JOIN MNT_AGENCY_LIST AGN ON AGN.AGENCY_ID = POL.AGENCY_ID
	WHERE POL.CUSTOMER_ID = @CUSTOMER_ID AND POL.POLICY_ID = @POLICY_ID 
	--POLICY_NUMBER = @POLICY_NUMBER
	/*GROUP BY POL.CUSTOMER_ID,POLICY_ID,AGN.AGENCY_DISPLAY_NAME,CUST.CUSTOMER_FIRST_NAME,
	CUST.CUSTOMER_MIDDLE_NAME,CUST.CUSTOMER_LAST_NAME,AGN.AGENCY_ID,POL.BILL_TYPE*/
	AND POL.POLICY_VERSION_ID IN(

	--SELECT TOP 1 POLICY_VERSION_ID FROM POL_CUSTOMER_POLICY_LIST 
	--WHERE CUSTOMER_ID = POL.CUSTOMER_ID AND 
	--POLICY_ID = POL.POLICY_ID AND
	--POLICY_NUMBER = POL.POLICY_NUMBER
	--ORDER BY POLICY_VERSION_ID DESC)     

   --Condition Commented And Added For itrack Issue #5951.
	--URENEW Added For Itrack Issue #6471 
    SELECT TOP 1 PCPL.POLICY_VERSION_ID FROM POL_CUSTOMER_POLICY_LIST  PCPL LEFT JOIN POL_POLICY_PROCESS PPP
    ON PPP.CUSTOMER_ID = PCPL.CUSTOMER_ID AND PPP.POLICY_ID = PCPL.POLICY_ID  AND PPP.NEW_POLICY_VERSION_ID =
    PCPL.POLICY_VERSION_ID        
    WHERE PCPL.CUSTOMER_ID = POL.CUSTOMER_ID AND PCPL.POLICY_ID = POL.POLICY_ID      
    AND  
    (     
     (ISNULL(PPP.PROCESS_STATUS,'') = 'COMPLETE' AND ISNULL(PPP.REVERT_BACK,'N') = 'N')   
     OR 
     PCPL.POLICY_STATUS IN ( 'SUSPENDED','UISSUE','URENEW')
    )     

    ORDER BY PCPL.POLICY_VERSION_ID DESC)

 



END    

--go
--exec Proc_GetInfoFromPolicyNum 'A7000262'
--rollback tran



GO

