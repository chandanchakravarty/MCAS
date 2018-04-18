IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_MNT_REIN_DELETE_PREMIUM_BUILDER]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_MNT_REIN_DELETE_PREMIUM_BUILDER]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                  
Proc Name       : dbo.Proc_MNT_REIN_DELETE_PREMIUM_BUILDER                  
Created by      : Swarup                
Date            : Aug 20, 2007                
      
Used In         : Wolverine           
                 
------------------------------------------------------------                  
Date     Review By          Comments                  
------   ------------       -------------------------*/                  
CREATE PROC [dbo].Proc_MNT_REIN_DELETE_PREMIUM_BUILDER              
(                  
                 
 @PREMIUM_BUILDER_ID int   
   
 )                  
AS                  
BEGIN        
                  
               
   DELETE MNT_REIN_PREMIUM_BUILDER  
 WHERE   
 PREMIUM_BUILDER_ID=@PREMIUM_BUILDER_ID;       
    
  END  
  
  



GO

