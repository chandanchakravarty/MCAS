IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_AppVerificationXML]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_AppVerificationXML]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*======================================================================================================
Proc Name       : dbo.Proc_AppVerificationXML  
Created by      : Ashwani   
Date            : 27 Oct.2005
Purpose       	: Return the string  against the given 
		  customerID,appID,appVersionID  
Revison History :  
Used In        :    
======================================================================================================
Date     Review By          Comments  
======================================================================================================*/
create   proc Dbo.Proc_AppVerificationXML
(  
 @CUSTOMER_ID int,  
 @APP_ID int,  
 @APP_VERSION_ID smallint 
)  
as  
begin  
select  APP_VERIFICATION_XML
from APP_LIST     
 where   CUSTOMER_ID =@CUSTOMER_ID AND   APP_ID=@APP_ID AND   APP_VERSION_ID=@APP_VERSION_ID  
end  




GO

