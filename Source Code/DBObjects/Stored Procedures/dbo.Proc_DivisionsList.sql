IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DivisionsList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DivisionsList]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------      
Proc Name   : dbo.Proc_DivisionsList     
Created by  : Ashwani      
Date        : 15 Mar,2005    
Purpose     :       
Revison History  :            
 ------------------------------------------------------------                  
Date     Review By          Comments                
       
------   ------------       -------------------------*/      
CREATE PROCEDURE dbo.Proc_DivisionsList    
AS     
BEGIN      
   SELECT      
	DIV_ID,DIV_NAME   ,DIV_CODE 
	FROM  MNT_DIV_LIST    
	WHERE IS_ACTIVE = 'y'
	ORDER BY DIV_NAME
		              
End      
  




GO

