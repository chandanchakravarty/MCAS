IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_MNT_REIN_SELECT_CONTRACT]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_MNT_REIN_SELECT_CONTRACT]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 

/*----------------------------------------------------------        
Proc Name        : dbo.[Proc_MNT_REIN_SELECT_CONTRACT]        
Created by       : Harmanjeet Singh      
Date             : April 30,2007     
Purpose          : retrieving data from MNT_REIN_split       
Revison History  :        
Used In         : Wolverine        
------------------------------------------------------------        
Date     Review By          Comments        
------   ------------       -------------------------*/    
--drop PROC [dbo].[Proc_MNT_REIN_SELECT_CONTRACT]   

     
CREATE PROC [dbo].[Proc_MNT_REIN_SELECT_CONTRACT]        
AS        
        
BEGIN        
SELECT CONTRACT_NUMBER
FROM MNT_REINSURANCE_CONTRACT  where IS_ACTIVE='Y'     
      
  
END 






GO

