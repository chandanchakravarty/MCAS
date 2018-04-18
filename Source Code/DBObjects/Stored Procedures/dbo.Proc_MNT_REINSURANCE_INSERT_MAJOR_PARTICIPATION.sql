IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_MNT_REINSURANCE_INSERT_MAJOR_PARTICIPATION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_MNT_REINSURANCE_INSERT_MAJOR_PARTICIPATION]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------                  
Proc Name       : dbo.Proc_MNT_REINSURANCE_INSERT_MAJOR_PARTICIPATION                  
Created by      : Harmanjeet Singh                 
Date            : May 7, 2007                
Purpose         : To insert the data into Reinsurer TIV GROUP table.                  
Revison History :                  
Used In         : Wolverine           
                 
------------------------------------------------------------                  
Date     Review By          Comments                  
------   ------------       -------------------------  
drop proc [dbo].Proc_MNT_REINSURANCE_INSERT_MAJOR_PARTICIPATION   
*/                  
CREATE PROC [dbo].[Proc_MNT_REINSURANCE_INSERT_MAJOR_PARTICIPATION]                 
(                  
                 
 @PARTICIPATION_ID int OUTPUT,  
 @REINSURANCE_COMPANY NVARCHAR(100),  
 @LAYER int,  
 @NET_RETENTION int,  
 @WHOLE_PERCENT decimal(18,0),  
 @MINOR_PARTICIPANTS int,   
 @CREATED_BY INT,  
 @CREATED_DATETIME DATETIME,  
 @CONTRACT_ID int  
 )                  
AS                  
BEGIN        
                  
               
  --===============================================================
  -- ADDED BY SANTOSH KUMAR GAUTAM ON 28 MARCH 2011 ITRACK:462
  -- IF LAYER IS ALREADY EXISTS THEN RETURN ERROR
  --===============================================================

  IF EXISTS( SELECT PARTICIPATION_ID 
  
             FROM MNT_REINSURANCE_MAJORMINOR_PARTICIPATION
             WHERE CONTRACT_ID=@CONTRACT_ID AND LAYER=@LAYER AND IS_ACTIVE='Y'
           )
  BEGIN
   
   SET @PARTICIPATION_ID =-4
   RETURN
   
  END
            
 

  SELECT @PARTICIPATION_ID=ISNULL(MAX(PARTICIPATION_ID),0)+ 1 FROM MNT_REINSURANCE_MAJORMINOR_PARTICIPATION  
  
 INSERT INTO MNT_REINSURANCE_MAJORMINOR_PARTICIPATION  
 (  
  PARTICIPATION_ID,  
  REINSURANCE_COMPANY,  
  LAYER,        
  NET_RETENTION,  
  WHOLE_PERCENT,   
  MINOR_PARTICIPANTS,   
  CREATED_BY,  
  CREATED_DATETIME,  
  IS_ACTIVE,  
  CONTRACT_ID  
 )  
 VALUES  
 (  
  @PARTICIPATION_ID,  
  @REINSURANCE_COMPANY,  
  @LAYER,        
  @NET_RETENTION,  
  @WHOLE_PERCENT,   
  @MINOR_PARTICIPANTS,    
  @CREATED_BY,  
  @CREATED_DATETIME,  
  'Y',  
  @CONTRACT_ID  
 )  
     
   
                 
  END  
  
  
  
GO

