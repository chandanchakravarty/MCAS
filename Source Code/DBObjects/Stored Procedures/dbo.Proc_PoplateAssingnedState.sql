IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_PoplateAssingnedState]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_PoplateAssingnedState]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                  
Proc Name   : dbo.Proc_PoplateAssingnedState                 
Created by  : Shafi                  
Date        : 23 Dec,2005                
Purpose     : To display the assigned State List                  
Revison History  :                        
------------------------------------------------------------                              
Date     Review By          Comments                            
                   
-------------------------------------------*/              
--     Proc_PoplateAssingnedState              
CREATE PROCEDURE dbo.Proc_PoplateAssingnedState          
(        
 @LobId int       
      
)              
AS                 
BEGIN                  
SELECT distinct MNTS.STATE_ID,MNTS.STATE_NAME FROM MNT_COUNTRY_STATE_LIST MNTS      
INNER JOIN  MNT_LOB_STATE MNTL ON MNTS.STATE_ID= MNTL.STATE_ID      
WHERE  MNTL.LOB_ID= @LobId and MNTL.PARENT_LOB is null  
      
ORDER BY STATE_NAME      
         
END      
  
  
  
      
    
  



GO

