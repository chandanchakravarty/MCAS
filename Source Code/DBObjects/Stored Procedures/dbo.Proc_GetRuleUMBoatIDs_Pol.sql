IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetRuleUMBoatIDs_Pol]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetRuleUMBoatIDs_Pol]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*=================================================================================================
Proc Name   	 : dbo.Proc_GetRuleUMBoatIDs_Pol      
Created by  	 : Ashwini      
Date       	 : 25 Oct. 2006
Purpose    	 : Get the  Boat IDs for UM
Revison History  :                      
====================================================================================================
Date     Review By          Comments                          
                 
====  =============      ===========================================================================*/
create procedure dbo.Proc_GetRuleUMBoatIDs_Pol      
(      
	 @CUSTOMER_ID int,      
	 @POLICYID int,      
	 @POLICYVERSIONID int      
)          
as               
begin                
      
 select   BOAT_ID      
	from       POL_UMBRELLA_WATERCRAFT_INFO       
		where CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID=@POLICYID AND POLICY_VERSION_ID=@POLICYVERSIONID and IS_ACTIVE='Y'       
			order by   BOAT_ID                  
end     
  





GO

