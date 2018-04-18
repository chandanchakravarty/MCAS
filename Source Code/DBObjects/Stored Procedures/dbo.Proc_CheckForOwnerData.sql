IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CheckForOwnerData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CheckForOwnerData]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                      
Proc Name       : dbo.Proc_CheckForOwnerData                                
Created by      : Sumit Chhabra                                    
Date            : 04/05/2006                                      
Purpose         : Check at the Owner (Claims) table for existence of data against the current claim id  
Created by      : Sumit Chhabra                                     
Revison History :                                      
Used In        : Wolverine                                      
------------------------------------------------------------                                      
Date     Review By          Comments                                      
------   ------------       -------------------------*/                                      
CREATE PROC dbo.Proc_CheckForOwnerData                                               
@CLAIM_ID INT                
AS                                      
BEGIN                                      
 IF EXISTS(SELECT CLAIM_ID FROM CLM_OWNER_INFORMATION WHERE CLAIM_ID=@CLAIM_ID)  
  RETURN 1  
 RETURN 0  
END                                
                            
      


GO

