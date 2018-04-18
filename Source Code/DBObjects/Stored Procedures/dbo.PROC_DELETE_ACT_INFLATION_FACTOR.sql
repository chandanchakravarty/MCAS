IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_DELETE_ACT_INFLATION_FACTOR]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_DELETE_ACT_INFLATION_FACTOR]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------          
Proc Name       : dbo.PROC_DELETE_ACT_INFLATION_FACTOR         
Modified by      :  Swarup Pal         
Date                :  07-Mar-2007         
Purpose         :  To Delete data into INFLATION_COST_FACTORS         
Revison History :          
Used In         :    Wolverine          
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/ 
-- drop proc PROC_DELETE_ACT_INFLATION_FACTOR      
CREATE PROC PROC_DELETE_ACT_INFLATION_FACTOR        
(        
@INFLATION_ID INT
)        
AS        
BEGIN        
 DELETE FROM INFLATION_COST_FACTORS    
 WHERE INFLATION_ID=@INFLATION_ID
END        
  



GO

