IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAL3TransactionsSplitDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAL3TransactionsSplitDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*                                                                    
----------------------------------------------------------                                          
Proc Name       : dbo.Proc_GetAL3TransactionsSplitDetails                                                                
Created by      :                                                                     
Date            : 06/23/2008                                 
Purpose         : Selects premium records for AL3 Input XML

Revison History :       
Modified By 	: Pravesh K chandel
Modified Date 	: 9 july 08
Purpose  	    : premium for Revert back process 

Used In         : Wolverine                    
      
------------------------------------------------------------                                        
                                       
Date     Review By          Comments                                                                
               
------   ------------       -------------------------                                                                        
*/      
-- drop proc dbo.Proc_GetAL3TransactionsSplitDetails  
      
CREATE PROC [dbo].[Proc_GetAL3TransactionsSplitDetails]      
(
	@AGENCY_ID 		INT,
	@PROCESS_ID 		INT,
	@CUSTOMER_ID 		INT,
	@POLICY_ID 		INT,
	@POLICY_VERSION_ID 	SMALLINT,
	@LOB_ID 		INT
)
AS      
BEGIN      
      
	/*SELECT 
		ISNULL(CPS.RISK_ID,0) AS RISK_ID,
		ISNULL(CPSD.COMPONENT_CODE,'''') AS COMPONENT_CODE, 
		ISNULL(CPSD.PREMIUM,0) AS PREMIUM  
		
	FROM CLT_PREMIUM_SPLIT CPS    
	LEFT OUTER JOIN CLT_PREMIUM_SPLIT_DETAILS CPSD 
	ON CPS.UNIQUE_ID = CPSD.SPLIT_UNIQUE_ID     
		WHERE 
		CUSTOMER_ID 		= @CUSTOMER_ID
		AND POLICY_ID 		= @POLICY_ID
		AND POLICY_VERSION_ID  	= @POLICY_VERSION_ID*/

	SELECT       
		 POL_CUS.AGENCY_ID AS AGENCY_ID,      
		 POL_POL.PROCESS_ID AS PROCESS_ID,       
		 POL_PROC.PROCESS_CODE AS PROCESS_CODE,     
		 POL_POL.CUSTOMER_ID AS CUSTOMER_ID, 
		 POL_CUS.CURRENT_TERM As CURRENT_TERM,      
		 POL_POL.POLICY_ID AS POLICY_ID, 
		 POL_CUS.POLICY_VERSION_ID AS POLICY_VERSION_ID,      
		 POL_CUS.POLICY_LOB AS LOB_ID, 
		 ISNULL(CPS.RISK_ID,0) AS RISK_ID,
		 ISNULL(CPSD.COMPONENT_CODE,'''') AS COMPONENT_CODE, 

		 CASE WHEN @PROCESS_ID = 37 THEN CONVERT(VARCHAR, ISNULL(CPSD.WRITTEN_PREM,'0'))
		 	ELSE CONVERT(VARCHAR, ISNULL(PREMIUM,'0'))
		 END AS PREMIUM

	FROM       
 		POL_POLICY_PROCESS POL_POL      
 	INNER JOIN POL_CUSTOMER_POLICY_LIST POL_CUS      
 		ON POL_POL.CUSTOMER_ID = POL_CUS.CUSTOMER_ID      
 		AND POL_POL.POLICY_ID = POL_CUS.POLICY_ID      
		AND POL_POL.NEW_POLICY_VERSION_ID = POL_CUS.POLICY_VERSION_ID      

	 INNER JOIN POL_PROCESS_MASTER POL_PROC      
		 ON POL_POL.PROCESS_ID = POL_PROC.PROCESS_ID      
      
	 INNER JOIN MNT_AGENCY_LIST AGL       
		 ON AGL.AGENCY_ID = POL_CUS.AGENCY_ID      
    
	 LEFT OUTER JOIN CLT_PREMIUM_SPLIT CPS    
 		ON POL_POL.CUSTOMER_ID = CPS.CUSTOMER_ID      
 		AND POL_POL.POLICY_ID = CPS.POLICY_ID      
 		AND POL_POL.NEW_POLICY_VERSION_ID = CPS.POLICY_VERSION_ID     
    
 	LEFT OUTER JOIN VIEW_CLT_PREMIUM_SPLIT_DETAILS CPSD     
 		ON CPS.UNIQUE_ID = CPSD.SPLIT_UNIQUE_ID     
 

	WHERE      
		POL_POL.PROCESS_ID 			=  @PROCESS_ID      
		AND POL_CUS.POLICY_LOB  		=  @LOB_ID
		AND POL_CUS.AGENCY_ID 			=  @AGENCY_ID
		AND POL_CUS.CUSTOMER_ID 		=  @CUSTOMER_ID
	 	AND POL_CUS.POLICY_ID 			=  @POLICY_ID
		AND POL_CUS.POLICY_VERSION_ID 		=  @POLICY_VERSION_ID
		AND ISNULL(IS_PART_OF_REVERT, '') 	<> 'Y'
 	ORDER BY AGENCY_ID, CUSTOMER_ID, PROCESS_ID, LOB_ID      
            
      
END      





GO

