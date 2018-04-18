IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_MNT_REIN_INSERT_TIVGROUP]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_MNT_REIN_INSERT_TIVGROUP]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                  
Proc Name       : dbo.[MNT_REIN_INSERT_TIVGROUP]                  
Created by      : Harmanjeet Singh                 
Date            : April 27, 2007                
Purpose         : To insert the data into Reinsurer TIV GROUP table.                  
Revison History :                  
Used In         : Wolverine           
                 
------------------------------------------------------------                  
Date     Review By          Comments                  
------   ------------       -------------------------*/   
-- drop PROC [dbo].[Proc_MNT_REIN_INSERT_TIVGROUP]                 
CREATE PROC [dbo].[Proc_MNT_REIN_INSERT_TIVGROUP]                  
(                  
                 
 @REIN_TIV_GROUP_ID int OUTPUT,  
 @REIN_TIV_CONTRACT_NUMBER NVARCHAR(100),  
 @REIN_TIV_EFFECTIVE_DATE DATETIME,  
 @REIN_TIV_FROM NVARCHAR(15),  
 @REIN_TIV_TO NVARCHAR(15),  
 @REIN_TIV_GROUP_CODE CHAR(2),   
 @CREATED_BY INT,  
 @CREATED_DATETIME DATETIME  
 )                  
AS                  
BEGIN        
                  
               
    SELECT @REIN_TIV_GROUP_ID=ISNULL(MAX(REIN_TIV_GROUP_ID),0)+ 1 FROM MNT_REIN_TIV_GROUP  
  
 INSERT INTO MNT_REIN_TIV_GROUP  
 (  
 REIN_TIV_GROUP_ID,  
 REIN_TIV_CONTRACT_NUMBER,  
 REIN_TIV_EFFECTIVE_DATE,        
 REIN_TIV_FROM,  
 REIN_TIV_TO,   
 REIN_TIV_GROUP_CODE,   
 CREATED_BY,  
 CREATED_DATETIME,  
 IS_ACTIVE  
 )  
 VALUES  
 (  
  @REIN_TIV_GROUP_ID,  
  @REIN_TIV_CONTRACT_NUMBER,  
  @REIN_TIV_EFFECTIVE_DATE,        
  @REIN_TIV_FROM,  
  @REIN_TIV_TO,   
  @REIN_TIV_GROUP_CODE,   
  @CREATED_BY,  
  @CREATED_DATETIME,  
  'Y'  
 )  
     
   
                 
  END  

        
      
    
  



GO

