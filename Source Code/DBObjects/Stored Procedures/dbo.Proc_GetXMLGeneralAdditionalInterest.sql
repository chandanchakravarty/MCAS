IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetXMLGeneralAdditionalInterest]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetXMLGeneralAdditionalInterest]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------        
Proc Name       : dbo.Proc        
Created by      : Gaurav        
Date            : 8/23/2005        
Purpose       :Evaluation        
Revison History :  Modified by Sumit on Oct6,2005 to retrieve individual records for a customer       
Used In        : Wolverine        
------------------------------------------------------------        
Date     Review By          Comments        
------   ------------       -------------------------*/        
CREATE PROC Dbo.Proc_GetXMLGeneralAdditionalInterest        
(        
@CUSTOMER_ID     int,        
@APP_ID int,        
@APP_VERSION_ID smallint,      
@ADD_INT_ID int      
)        
AS        
BEGIN        
select CUSTOMER_ID,        
APP_ID,        
APP_VERSION_ID,        
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
from  APP_GENERAL_HOLDER_INTEREST               
        
where  CUSTOMER_ID = @CUSTOMER_ID        
AND APP_ID=@APP_ID        
AND APP_VERSION_ID=@APP_VERSION_ID      
AND ADD_INT_ID=@ADD_INT_ID      
END      
      
    
  



GO

