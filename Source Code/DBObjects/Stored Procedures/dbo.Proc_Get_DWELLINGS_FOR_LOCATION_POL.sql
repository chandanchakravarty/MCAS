IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Get_DWELLINGS_FOR_LOCATION_POL]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Get_DWELLINGS_FOR_LOCATION_POL]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--drop proc Proc_Get_DWELLINGS_FOR_LOCATION_POL

/*----------------------------------------------------------          
Proc Name       : Dbo.Proc_Get_DWELLINGS_FOR_LOCATION_POL  
Created by      : Ravindra          
Date            : 07-04-2006
Purpose       :   Gets the dwellings associated with this location  
Revison History :          
Used In        : Wolverine          
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/          
CREATE PROCEDURE Proc_Get_DWELLINGS_FOR_LOCATION_POL        
(        
  @CUSTOMER_ID Int,        
  @POLICY_ID Int,        
  @POLICY_VERSION_ID smallint,        
  @LOCATION_ID Int     
)        
AS        
  	
 
--Get Dwellings        
SELECT ISNULL(DWELLING_ID,0)  as DWELLING_ID 
FROM POL_DWELLINGS_INFO WITH (NOLOCK)  
WHERE CUSTOMER_ID = @CUSTOMER_ID AND  
 	POLICY_ID = @POLICY_ID AND  
 	POLICY_VERSION_ID = @POLICY_VERSION_ID AND  
	LOCATION_ID = @LOCATION_ID  
  
   









GO

