IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetXMLGeneralAdditionalInterestForPolicy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetXMLGeneralAdditionalInterestForPolicy]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------          
Proc Name       : dbo.Proc_GetXMLGeneralAdditionalInterestForPolicy          
Created by      : Gaurav          
Date            : 8/23/2005          
Purpose       :Evaluation          
Used In        : Wolverine          
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/          
CREATE PROC Dbo.Proc_GetXMLGeneralAdditionalInterestForPolicy          
(          
@CUSTOMER_ID     int,          
@POLICY_ID int,          
@POLICY_VERSION_ID smallint,        
@ADD_INT_ID int        
)          
AS          
BEGIN          
select CUSTOMER_ID,          
POLICY_ID,          
POLICY_VERSION_ID,          
ADD_INT_ID,    
MODIFIED_BY,          
LAST_UPDATED_DATETIME,          
IS_ACTIVE,      
HOLDER_ADD1,          
HOLDER_ADD2,          
HOLDER_CITY,          
HOLDER_COUNTRY,          
HOLDER_STATE,          
HOLDER_ZIP    
from  POL_ADD_OTHER_INT                                 
where  CUSTOMER_ID = @CUSTOMER_ID          
AND POLICY_ID=@POLICY_ID          
AND POLICY_VERSION_ID=@POLICY_VERSION_ID        
AND ADD_INT_ID=@ADD_INT_ID        
END  


GO

