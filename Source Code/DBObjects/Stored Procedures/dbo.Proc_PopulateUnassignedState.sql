IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_PopulateUnassignedState]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_PopulateUnassignedState]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------          
Proc Name   : dbo.Proc_PopulateUnassignedDept         
Created by  : Shafi         
Date        : 23 Dec,2005        
Purpose     : To display the unassigned State List          
Revison History  :                
------------------------------------------------------------                      
Date     Review By          Comments                    
           
-------------------------------------------*/      
--     Proc_PopulateUnassignedDept      
CREATE PROCEDURE dbo.Proc_PopulateUnassignedState  (@LobId int)      
AS         
BEGIN          
 SELECT  
 STATE_ID,  
 STATE_NAME  
 FROM MNT_COUNTRY_STATE_LIST WHERE STATE_ID NOT IN(SELECT STATE_ID  
 FROM MNT_LOB_STATE WHERE LOB_ID=@LobId and PARENT_LOB is null)  
  
  ORDER BY STATE_NAME      
        
End    
    



GO

