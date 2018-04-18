IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GET_OWNERS_LIST]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GET_OWNERS_LIST]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                            
Proc Name       : dbo.Proc_GET_OWNERS_LIST                                      
Created by      : Sumit Chhabra                                          
Date            : 04/05/2006                                            
Purpose         : Get List of Owners
Created by      : Sumit Chhabra                                           
Revison History :                                            
Used In        : Wolverine                                            
------------------------------------------------------------                                            
Date     Review By          Comments                                            
------   ------------       -------------------------*/                                            
CREATe PROC dbo.Proc_GET_OWNERS_LIST                                                                        
@CLAIM_ID int    
AS                                            
BEGIN                                            
	SELECT NAME AS NAME,OWNER_ID FROM 	CLM_OWNER_INFORMATION WHERE CLAIM_ID=@CLAIM_ID

END    
  



GO

