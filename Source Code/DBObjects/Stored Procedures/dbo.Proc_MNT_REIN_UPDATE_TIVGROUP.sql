IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_MNT_REIN_UPDATE_TIVGROUP]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_MNT_REIN_UPDATE_TIVGROUP]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                  
Proc Name       : dbo.[MNT_REIN_UPDATE_TIVGROUP]                  
Created by      : Harmanjeet Singh                 
Date            : April 27, 2007                
Purpose         : To insert the data into Reinsurer TIV GROUP table.                  
Revison History :                  
Used In         : Wolverine           
                 
------------------------------------------------------------                  
Date     Review By          Comments                  
------   ------------       -------------------------*/  
--drop PROC [dbo].[Proc_MNT_REIN_UPDATE_TIVGROUP]                
CREATE PROC [dbo].[Proc_MNT_REIN_UPDATE_TIVGROUP]                  
(                  
                 
 @REIN_TIV_GROUP_ID int ,  
 @REIN_TIV_CONTRACT_NUMBER NVARCHAR(100),  
 @REIN_TIV_EFFECTIVE_DATE DATETIME,  
 @REIN_TIV_FROM NVARCHAR(15),  
 @REIN_TIV_TO NVARCHAR(15),  
 @REIN_TIV_GROUP_CODE CHAR(2),   
 @MODIFIED_BY INT,  
 @LAST_UPDATED_DATETIME DATETIME  
 )                  
AS                  
BEGIN        
                  
               
   UPDATE MNT_REIN_TIV_GROUP  
 SET  
 REIN_TIV_GROUP_ID=@REIN_TIV_GROUP_ID,  
 REIN_TIV_CONTRACT_NUMBER=@REIN_TIV_CONTRACT_NUMBER,  
 REIN_TIV_EFFECTIVE_DATE=@REIN_TIV_EFFECTIVE_DATE,        
 REIN_TIV_FROM=@REIN_TIV_FROM,  
 REIN_TIV_TO=@REIN_TIV_TO,   
 REIN_TIV_GROUP_CODE=@REIN_TIV_GROUP_CODE,   
 MODIFIED_BY=@MODIFIED_BY,  
 LAST_UPDATED_DATETIME=@LAST_UPDATED_DATETIME  
 WHERE   
 REIN_TIV_GROUP_ID=@REIN_TIV_GROUP_ID;         
  END  



GO

