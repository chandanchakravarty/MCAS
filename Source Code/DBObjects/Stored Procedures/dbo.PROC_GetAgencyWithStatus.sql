IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_GetAgencyWithStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_GetAgencyWithStatus]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                                                    
Proc Name       : dbo.PROC_GetAgencyWithStatus                                                                              
Created by      : Asfa Praveen                                                                                  
Date            : 04/Jan/2008                                                                                    
Purpose         : Fetch Agency Info with Active, Inactive and Terminated Status from MNT_AGENCY_LIST table                
Revison History :                                                                                    
Used In        : Wolverine                                                                                    
------------------------------------------------------------                                                                                    
Date     Review By          Comments                                                                                    
------   ------------       -------------------------*/                              
--drop proc dbo.PROC_GetAgencyWithStatus '06/24/2011',null,1473,2               
CREATE PROC [dbo].[PROC_GetAgencyWithStatus]           
(              
  @APP_EFFECTIVE_DATE DATETIME,              
  @CUSTOMER_ID INT = NULL, --Added by Charles on 18-Aug-09 for APP/POL OPTIMISATION            
  @AGENCY_ID INT = NULL  -- BY PRAVESH ON 11 AUG 2010
)              
AS                
BEGIN              
              
SELECT DISTINCT AGENCY_ID,(RTRIM(ISNULL (AGENCY_CODE,'')) +'-'+               
cast(isnull(NUM_AGENCY_CODE,'') as varchar) +'-'+               
 ISNULL(AGENCY_DISPLAY_NAME,'')) AS AGENCY_DISPLAY_NAME,              
/*CASE when ISNULL(DATEDIFF(DAY,TERMINATION_DATE_RENEW, @APP_EFFECTIVE_DATE),0) <=0              
THEN */ --Commented by Charles on 24-Aug-09 for APP/POL OPTIMISATION                      
 CASE when ISNULL(DATEDIFF(DAY,TERMINATION_DATE, @APP_EFFECTIVE_DATE),0)<= 0              
 THEN              
  CASE MAL.IS_ACTIVE              
   WHEN 'Y' THEN RTRIM(ISNULL (AGENCY_CODE,'')) +'-'+ cast(isnull(NUM_AGENCY_CODE,'') as varchar) +'-'+  ISNULL(AGENCY_DISPLAY_NAME,'') + '- (Active)'              
   WHEN 'N' THEN RTRIM(ISNULL (AGENCY_CODE,'')) +'-'+ cast(isnull(NUM_AGENCY_CODE,'') as varchar) +'-'+ ISNULL(AGENCY_DISPLAY_NAME,'') + '- (Inactive)'              
  END              
 ELSE              
  case when ISNULL(DATEDIFF(DAY,TERMINATION_DATE, @APP_EFFECTIVE_DATE),0) >0              
  then  RTRIM(ISNULL (AGENCY_CODE,'')) +'-'+ cast(isnull(NUM_AGENCY_CODE,'') as varchar) +'-'+  ISNULL(AGENCY_DISPLAY_NAME,'') + '- (Terminated)' end              
 END AS AGENCY_NAME_ACTIVE_STATUS,             
/*ELSE              
 case when ISNULL(DATEDIFF(DAY,TERMINATION_DATE_RENEW, @APP_EFFECTIVE_DATE),0) >0              
 then  RTRIM(ISNULL (AGENCY_CODE,'')) +'-'+ cast(isnull(NUM_AGENCY_CODE,'') as varchar) +'-'+ ISNULL(AGENCY_DISPLAY_NAME,'') + '- (Terminated)' end          
END AS AGENCY_NAME_ACTIVE_STATUS */ --Commented by Charles on 24-Aug-09 for APP/POL OPTIMISATION          
CASE  --Added by Charles on 19-Aug-09 for APP/POL OPTIMISATION                  
 WHEN ISNULL(DATEDIFF(DAY,TERMINATION_DATE, @APP_EFFECTIVE_DATE),0)>0 THEN 'Y'          
 ELSE 'N'          
END AS IS_TERMINATED                
--Added till here          
FROM MNT_AGENCY_LIST MAL            
--Added by Charles on 18-Aug-09 for APP/POL OPTIMISATION            
LEFT JOIN CLT_CUSTOMER_LIST CLT WITH(NOLOCK) ON MAL.AGENCY_ID=CLT.CUSTOMER_AGENCY_ID  
			                                                    
WHERE MAL.AGENCY_ID = ISNULL(@AGENCY_ID , MAL.AGENCY_ID) 
	AND CLT.CUSTOMER_ID = ISNULL(@CUSTOMER_ID , CLT.CUSTOMER_ID)     
--Added till here            
ORDER BY  AGENCY_DISPLAY_NAME ASC                
END 



  
GO

