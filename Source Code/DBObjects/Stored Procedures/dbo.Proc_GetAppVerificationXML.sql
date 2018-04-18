IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAppVerificationXML]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAppVerificationXML]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/* ======================================================================= */  
-- Proc Name       : dbo.Proc_GetAppVerificationXML
-- Created by      : Ashwani  
-- Date            : 07 Nov. 2005  
-- Purpose         : Get the verification XML to show the status for Policy 
-- Revison History :            
-- Used In         : Wolverine                   
/*==========================================================================  
Date     Review By          Comments            
========================================================================== */  
  
create proc Proc_GetAppVerificationXML        
@CUSTOMER_ID int,        
@APP_ID int,        
@APP_VERSION_ID smallint        
as        
begin    
select APP_VERIFICATION_XML
from APP_LIST        
where CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID             
end        
        
    
        
         
        
        
        
   
        
      
    
  



GO

