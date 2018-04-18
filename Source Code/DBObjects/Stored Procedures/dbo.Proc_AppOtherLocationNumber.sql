IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_AppOtherLocationNumber]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_AppOtherLocationNumber]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------          
Proc Name       : dbo.Proc_AppOtherLocationNumber  
Created by      : Swastika          
Date            : 20th Jun'06          
Purpose         : Return the Query         
Revison History :          
Used In   : Wolverine          
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/    
-- drop proc dbo.Proc_AppOtherLocationNumber        
CREATE PROC dbo.Proc_AppOtherLocationNumber  
(          
 @CUSTOMER_ID  int   ,  
 @APP_ID  int,  
 @APP_VERSION_ID int  
  
)          
AS          
BEGIN          
 Declare @Max_LOC_NUM numeric  
 SELECT  
  @Max_LOC_NUM = isnull(Max(Loc_Num), 0)
 FROM           
  APP_OTHER_LOCATIONS  
 WHERE       
  CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID  
 
 IF @Max_LOC_NUM = 2147483647  
 BEGIN  
  select @Max_LOC_NUM=-1
  select @Max_LOC_NUM as Loc_Num
  return
 END  
 else

  
 SELECT @Max_LOC_NUM + 1  AS Loc_Num

END  
  
  
  






GO

