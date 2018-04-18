IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Get_DWELLINGS_FOR_LOCATION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Get_DWELLINGS_FOR_LOCATION]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
--drop proc Proc_Get_DWELLINGS_FOR_LOCATION

/*----------------------------------------------------------          
Proc Name       : Dbo.Proc_Get_DWELLINGS_FOR_LOCATION  
Created by      : Pradeep          
Date            : 12/13/2005     
Purpose       :   Gets the dwellings associated with this location  
Revison History :          
Used In        : Wolverine          
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/          
CREATE PROCEDURE Proc_Get_DWELLINGS_FOR_LOCATION        
(        
  @CUSTOMER_ID Int,        
  @APP_ID Int,        
  @APP_VERSION_ID smallint,        
  @LOCATION_ID Int     
)        
AS        
  	
 
--Get Dwellings        
SELECT ISNULL(DWELLING_ID,0)  as DWELLING_ID 
FROM APP_DWELLINGS_INFO WITH (NOLOCK)  
WHERE CUSTOMER_ID = @CUSTOMER_ID AND  
 APP_ID = @APP_ID AND  
 APP_VERSION_ID = @APP_VERSION_ID AND  
 LOCATION_ID = @LOCATION_ID  
  
   







GO

