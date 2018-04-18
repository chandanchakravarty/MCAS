IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetProductClaimReciptHTML]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetProductClaimReciptHTML]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

    
/*----------------------------------------------------------       
      
Proc Name : dbo.Proc_GetProductClaimReciptHTML       
      
Created by : Shubhanshu Pandey       
      
Date : 1/3/2011       
      
Purpose : Generation of Calim receipt      
      
Revison History :       
      
Used In : Ebix Advantage      
      
------ ------------ -------------------------*/       
--DROP PROC [Proc_GetProductClaimReciptHTML] 566,1       
      
      
CREATE PROCEDURE  [dbo].[Proc_GetProductClaimReciptHTML]      
(      
 @CLAIM_ID INT,      
 @ACTIVITY_ID INT,  
 @PROCESS_TYPE VARCHAR(20)     
   
)      
AS      
BEGIN      
      
    SELECT DOC_TEXT     
    FROM CLM_PROCESS_LOG WITH(NOLOCK)    
    WHERE CLAIM_ID = @CLAIM_ID AND  ACTIVITY_ID = @ACTIVITY_ID  AND PROCESS_TYPE=@PROCESS_TYPE   
        
END 
GO

