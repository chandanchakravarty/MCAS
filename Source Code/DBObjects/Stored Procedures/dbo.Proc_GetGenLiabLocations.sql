IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetGenLiabLocations]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetGenLiabLocations]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------        
Proc Name       : dbo.Proc_GetGenLiabLocations        
Created by      : Sumit Chhabra        
Date            : 03/29/2006        
Purpose        : To get locations for General Liability      
Revison History :        
Used In        : Wolverine        
------------------------------------------------------------        
Date     Review By          Comments        
------   ------------       -------------------------*/        
create PROC Dbo.Proc_GetGenLiabLocations      
(      
 @CUSTOMER_ID int,      
 @APP_ID int,      
 @APP_VERSION_ID smallint      
-- @User_Flag char(5)      
)AS        
BEGIN        
    
    
 SELECT  (CAST(ISNULL(LOC_NUM,0) AS VARCHAR(5)) + ' ' + ISNULL(LOC_ADD1,'') + ' ' + ISNULL(LOC_CITY,'')       
      + ' ' + ISNULL(LOC_STATE,'')) AS LOCATIONS
      FROM APP_LOCATIONS WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND @APP_VERSION_ID=@APP_VERSION_ID      
END        
        
  



GO

