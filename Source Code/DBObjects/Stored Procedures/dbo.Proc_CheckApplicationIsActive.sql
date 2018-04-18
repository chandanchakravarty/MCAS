IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CheckApplicationIsActive]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CheckApplicationIsActive]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------    
Proc Name          : Dbo.Proc_CheckApplicationIsActive    
Created by         : praveen kasana   
Date               : 6/09/2007    
Purpose            : get the Status of an Application (IS_ACTIVE)    
Revison History    :    
Used In                :   Wolverine      
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
CREATE   PROCEDURE dbo.Proc_CheckApplicationIsActive    
(    
 @CUSTOMER_ID int,                                                             
 @APP_ID int,                                         
 @APP_VERSION_ID smallint,      
 @IS_ACTIVE varchar(2) output    
)    
AS    
BEGIN    
 SELECT @IS_ACTIVE = IS_ACTIVE FROM APP_LIST with(nolock)  
 WHERE CUSTOMER_ID=@CUSTOMER_ID    
 and APP_ID = @APP_ID  
 and @APP_VERSION_ID = @APP_VERSION_ID  
END  
  



GO

