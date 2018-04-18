IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_MNT_REINSURANCE_GETXML_MAJOR_PARTICIPATION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_MNT_REINSURANCE_GETXML_MAJOR_PARTICIPATION]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                  
Proc Name       : dbo.Proc_MNT_REINSURANCE_GETXML_MAJOR_PARTICIPATION                  
Created by      : Harmanjeet Singh                 
Date            : April 27, 2007                
Purpose         : To insert the data into Reinsurer Construction Translation table.                  
Revison History :                  
Used In         : Wolverine           
                 
------------------------------------------------------------                  
Date     Review By          Comments                  
------   ------------       -------------------------*/   
--drop proc [dbo].Proc_MNT_REINSURANCE_GETXML_MAJOR_PARTICIPATION                 
CREATE proc [dbo].Proc_MNT_REINSURANCE_GETXML_MAJOR_PARTICIPATION  
(                
 @PARTICIPATION_ID int    
  )                  
AS                  
BEGIN        
                  
               
    SELECT  
  
 ISNULL(PARTICIPATION_ID,0) PARTICIPATION_ID,  
  
 ISNULL(REINSURANCE_COMPANY,'') REIN_TIV_CONTRACT_NUMBER,   
 ISNULL(REINSURANCE_COMPANY,'') REINSURANCE_COMPANY,  
 ISNULL(LAYER,'') LAYER,  
 ISNULL(NET_RETENTION,'') NET_RETENTION,   
 ISNULL(WHOLE_PERCENT,'') WHOLE_PERCENT,   
 ISNULL(MINOR_PARTICIPANTS,'') MINOR_PARTICIPANTS,   
 ISNULL(CREATED_BY,0) CREATED_BY,  
 CONVERT(VARCHAR,CREATED_DATETIME,101) CREATED_DATETIME,  
 ISNULL(MODIFIED_BY,0) MODIFIED_BY,  
 CONVERT(VARCHAR,LAST_UPDATED_DATETIME,101) LAST_UPDATED_DATETIME,        
 ISNULL(IS_ACTIVE,'') IS_ACTIVE  
  
 FROM MNT_REINSURANCE_MAJORMINOR_PARTICIPATION  
      
 WHERE  
 PARTICIPATION_ID=@PARTICIPATION_ID;  
   
                 
  END  
  
  



GO

