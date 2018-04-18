IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_AssignDeleteToLob]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_AssignDeleteToLob]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                    
Proc Name       : dbo.Proc_AssignStateToLob             
Created by      : Shafi                    
Date            : 23 Dec,2005                    
Purpose         : To assign the States To Lob.                    
Revison History :                    
Used In         : Wolverine                    
------------------------------------------------------------                    
Date     Review By          Comments                    
------   ------------       -------------------------*/    
                
CREATE PROC Dbo.Proc_AssignDeleteToLob                   
(                
 @LobId smallint  
    
)                    
AS                    
BEGIN       
DELETE FROM MNT_LOB_STATE WHERE LOB_ID=@LobId and PARENT_LOB is null
DELETE FROM MNT_LOB_STATE WHERE PARENT_LOB=@LOBID

    
End      


  





GO

