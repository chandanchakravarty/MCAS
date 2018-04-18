IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyState]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyState]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------    
Proc Name          : Dbo.Proc_GetPolicyState    
Created by           :Anurag Verma    
Date                    : Nov 04 , 2005    
Purpose               :     
Revison History :    
Modify by              :Pravesh Chandel    
Date                    : Nov 13 , 2006    
Purpose               :     add new Fetch Feild  APP_EFFECTIVE_DATE1
Modify by              :Pravesh Chandel    
Date                    : Nov 13 , 2006    
Purpose               :     update Fetch Feild  APP_EFFECTIVE_DATE1

Modify by              :Pravesh K Chandel    
Date                    : Jan 16 , 2009    
Purpose               :  Converting State name In Camel Case( for Coverage Screens)


Used In                :   Wolverine      
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/   
--DROP PROC Proc_GetPolicyState   
CREATE    PROCEDURE dbo.Proc_GetPolicyState    
(    
 @CUSTOMER_ID int,    
 @POL_ID int,    
 @POL_VERSION_ID int     
     
)    
AS    
BEGIN    
    
SELECT CS.STATE_ID,    
		--CS.STATE_NAME,
		upper(substring(CS.STATE_NAME,1,1)) + substring(lower(CS.STATE_NAME),2,len(CS.STATE_NAME)-1) as STATE_NAME,
       A.POLICY_LOB as LOB_ID,  
       YEAR(CONVERT(VARCHAR(20),A.APP_EFFECTIVE_DATE,109)) as APP_EFFECTIVE_DATE, 
       A.APP_EFFECTIVE_DATE as APP_EFF_DATE,  A. APP_INCEPTION_DATE AS INCEPTION_DATE,
	   ALL_DATA_VALID         
FROM POL_CUSTOMER_POLICY_LIST A    
INNER JOIN MNT_COUNTRY_STATE_LIST CS ON    
 A.STATE_ID = CS.STATE_ID    
WHERE  A.CUSTOMER_ID=@CUSTOMER_ID    AND    
               A.POLICY_ID=@POL_ID     AND    
               A.POLICY_VERSION_ID=@POL_VERSION_ID AND    
  CS.COUNTRY_ID = 1    
    
END  
  


GO

