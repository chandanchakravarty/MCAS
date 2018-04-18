IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetOperators_Pol]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetOperators_Pol]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------        
Proc Name           : Dbo.Proc_GetOperators_Pol        
Created by          : Ashwani       
Date                : 02 Mar 2006
Purpose             : To get the operators for the PPA policy rule implementation      
Revison History     :        
Used In             :   Wolverine          
------------------------------------------------------------        
Date     Review By          Comments        
------   ------------       -------------------------*/        
      
CREATE proc Dbo.Proc_GetOperators_Pol          
@CUSTOMER_ID  int,        
@POLICY_ID  int,        
@POLICY_VERSION_ID int       
as        
begin        
	select DRIVER_ID from POL_WATERCRAFT_DRIVER_DETAILS        
	where    CUSTOMER_ID = @CUSTOMER_ID   and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID     
		and IS_ACTIVE='Y'    
	order by DRIVER_ID         
end      
  



GO

