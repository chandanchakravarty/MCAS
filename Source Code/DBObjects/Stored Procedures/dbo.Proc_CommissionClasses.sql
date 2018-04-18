IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CommissionClasses]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CommissionClasses]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*                                                          
----------------------------------------------------------                                                              
Proc Name       : dbo.Proc_CommissionClasses                                                          
Created by      : Ravindra Gupta
Date            : 09-14-2006
Purpose         : Select Commision Classes For Particular State,LOB & SUB LOB
Revison History :                                                              
Used In         : Wolverine             

------------------------------------------------------------                                                              
Date     Review By          Comments                                                              
------   ------------       -------------------------                                                             
*/                                                          
                                                          
CREATE PROCEDURE dbo.Proc_CommissionClasses                                                          
(                                                          
  @STATE_ID smallint,
  @LOB_ID smallint,
  @SUB_LOB_ID smallint
)                                                          
AS                                                          
BEGIN                      

 SELECT COMMISSION_CLASS_ID,CLASS_DESCRIPTION FROM ACT_COMMISION_CLASS_MASTER
 WHERE	STATE_ID = @STATE_ID
 AND	LOB_ID   = @LOB_ID 
 AND 	ISNULL(SUB_LOB_ID,0) = ISNULL(@SUB_LOB_ID,0)

END



GO

