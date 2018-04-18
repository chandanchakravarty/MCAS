IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetNewLocationNumber]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetNewLocationNumber]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------          
Proc Name       : dbo.Proc_GetNewLocationNumber          
Created by      : Sumit Chhabra          
Date            : 10/10/2005          
Purpose         : To Get the new location number          
Revison History :     
Used In         :   Wolverine          
Modified Date : 11/10/2005    
Modification : Added a parameter CalledFrom to generate new location number based on the page called from    
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/          
CREATE PROC dbo.Proc_GetNewLocationNumber          
(          
 @CUSTOMER_ID varchar(5),  
 @APP_ID varchar(5),  
 @APP_VERSION_ID varchar(5),   
 @CALLEDFROM varchar(5),    
 @CODE INT OUTPUT          
)          
AS          
          
BEGIN          
    
if(@CALLEDFROM='GEN')    
 SELECT  @CODE =ISNULL(MAX(ISNULL(LOC_NUM,0)),0) +1  FROM APP_LOCATIONS WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID   
else if(@CALLEDFROM='UMB')    
 SELECT  @CODE =ISNULL(MAX(ISNULL(LOCATION_NUMBER,0)),0) +1   FROM APP_UMBRELLA_REAL_ESTATE_LOCATION  WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID   
     else    
 SELECT @CODE=0            
          
END          
  


GO

