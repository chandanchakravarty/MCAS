IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetMNT_COUNTRY_STATE_LIST]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetMNT_COUNTRY_STATE_LIST]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*-----------------------------------------------------                                                        
Proc Name       : dbo.Proc_GetMNT_COUNTRY_STATE_LIST                                                  
Created by      : Sumit Chhabra                                                      
Date            : 27/04/2006                                                        
Purpose         : Fetch data from CLM_CLAIM_INFO table for claim notification screen                                    
Created by      : Sumit Chhabra                                                       
Revison History :                                                        
Used In        : Wolverine                                                        
------------------------------------------------------------                                                        
Date     Review By          Comments                                                        
------   ------------       -------------------------*/                                                        
--DROP PROC dbo.Proc_GetMNT_COUNTRY_STATE_LIST                                                                           
CREATE PROC dbo.Proc_GetMNT_COUNTRY_STATE_LIST                                                                           
@STATE_ID int
AS                                                        
BEGIN                                                        
 SELECT                                         
	ISNULL(STATE_CODE,'') AS STATE_CODE,
	ISNULL(STATE_NAME,'') AS STATE_NAME,
	ISNULL(STATE_DESC,'') AS STATE_DESC,
	ISNULL(IS_ACTIVE,'') AS IS_ACTIVE,
    ISNULL(COUNTRY_ID,'') AS COUNTRY_ID
FROM                               
  MNT_COUNTRY_STATE_LIST                                      
 WHERE                                                
 STATE_ID = @STATE_ID                                     
  

END                                                  
                                    
                        
        
      
      
    
  
  











GO

