IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetEndorsementId]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetEndorsementId]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------    
Proc Name          : Dbo.Proc_GetEndorsementId    
Created by           : Mohit Agarwal    
Date                    : 02/05/2007    
Purpose               :     
Revison History :    
Used In                :   Wolverine      
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
--drop proc Proc_GetEndorsementId     
CREATE   PROCEDURE Proc_GetEndorsementId     
(    
 @COV_ID int    
)    
AS    
BEGIN    

SELECT ENDORSMENT_ID FROM MNT_ENDORSMENT_DETAILS WHERE SELECT_COVERAGE=@COV_ID and IS_ACTIVE='Y'
   
END  
  
  
  




GO

