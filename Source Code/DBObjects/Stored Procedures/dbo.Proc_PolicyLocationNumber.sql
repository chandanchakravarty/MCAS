IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_PolicyLocationNumber]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_PolicyLocationNumber]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------          
Proc Name       : dbo.Proc_PolicyLocationNumber  
Created by      : Anurag verma          
Date            : 10/11/2005          
Purpose       : Return the Query         
Revison History :          
Used In   : Wolverine          
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/          
CREATE PROC Proc_PolicyLocationNumber  
(          
 @CUSTOMER_ID  int   ,  
 @POL_ID  int,  
 @POL_VERSION_ID int  
  
)          
AS          
BEGIN          
 Declare @Max_LOC_NUM numeric  
 SELECT  
  @Max_LOC_NUM = isnull(Max(Loc_Num), 0)
 FROM           
  POL_LOCATIONS  
 WHERE       
  CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POL_ID AND POLICY_VERSION_ID = @POL_VERSION_ID  
  
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

