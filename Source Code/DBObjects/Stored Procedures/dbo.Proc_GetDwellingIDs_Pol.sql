IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetDwellingIDs_Pol]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetDwellingIDs_Pol]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/* ----------------------------------------------------------                
Proc Name   : dbo.Proc_GetDwellingIDs_Pol      
Created by  : Ashwani      
Date        : 02 Mar 2006 
Purpose     : Get the Dwelling IDs  for HO policy rules  
Revison History  :                      
 ------------------------------------------------------------                            
Date     Review By          Comments                          
                 
------   ------------       -------------------------*/                
CREATE PROCEDURE dbo.Proc_GetDwellingIDs_Pol      
(      
 @CUSTOMER_ID int,      
 @POLICY_ID int,      
 @POLICY_VERSION_ID int      
)          
AS               
BEGIN                    
	SELECT DWELLING_ID  
	FROM POL_DWELLINGS_INFO with(nolock)
	WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID     
		AND IS_ACTIVE='Y'       
	ORDER BY DWELLING_ID                 
End    
    
  
  





GO

