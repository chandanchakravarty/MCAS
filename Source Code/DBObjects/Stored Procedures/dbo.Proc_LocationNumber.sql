IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_LocationNumber]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_LocationNumber]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------          
Proc Name       : dbo.Proc_LocationNumber  
Created by      : Gaurav          
Date            : 5/31/2005          
Purpose       : Return the Query         
Revison History :          
Used In   : Wolverine          
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/          
CREATE PROC Proc_LocationNumber  
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
  APP_LOCATIONS  
 WHERE       
  CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID  
  
 /*if (SELECT Len(Convert(Varchar, @Max_LOC_NUM))) > 4  
  SELECT null AS Loc_Num  
 ELSE  
  SELECT @Max_LOC_NUM as Loc_Num  */
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

