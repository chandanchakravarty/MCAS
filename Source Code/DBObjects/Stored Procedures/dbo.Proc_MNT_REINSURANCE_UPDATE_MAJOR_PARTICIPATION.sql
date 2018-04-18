IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_MNT_REINSURANCE_UPDATE_MAJOR_PARTICIPATION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_MNT_REINSURANCE_UPDATE_MAJOR_PARTICIPATION]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

  /*----------------------------------------------------------                  
Proc Name       : dbo.Proc_MNT_REINSURANCE_UPDATE_MAJOR_PARTICIPATION                  
Created by      : Harmanjeet Singh                 
Date            : May 7, 2007                
Purpose         : To insert the data into Reinsurer TIV GROUP table.                  
Revison History :                  
Used In         : Wolverine           


DROP PROC dbo.Proc_MNT_REINSURANCE_UPDATE_MAJOR_PARTICIPATION                       
------------------------------------------------------------                  
Date     Review By          Comments                  
------   ------------       -------------------------*/                  
CREATE PROC [dbo].[Proc_MNT_REINSURANCE_UPDATE_MAJOR_PARTICIPATION]                  
(                  
                 
 @PARTICIPATION_ID int ,  
 @REINSURANCE_COMPANY NVARCHAR(100),  
 @LAYER int,  
 @NET_RETENTION int,  
 @WHOLE_PERCENT decimal(18,0),  
 @MINOR_PARTICIPANTS int,   
 @CONTRACT_ID int ,
 @MODIFIED_BY INT,  
 @LAST_UPDATED_DATETIME DATETIME ,
 @HAS_ERROR int OUTPUT  

 )                  
AS                  
BEGIN        
        
        
  --===============================================================
  -- ADDED BY SANTOSH KUMAR GAUTAM ON 28 MARCH 2011 ITRACK:462
  -- IF LAYER IS ALREADY EXISTS THEN RETURN ERROR
  --===============================================================

  IF EXISTS( SELECT PARTICIPATION_ID 
  
             FROM MNT_REINSURANCE_MAJORMINOR_PARTICIPATION
             WHERE CONTRACT_ID=@CONTRACT_ID 
                   AND LAYER=@LAYER 
                   AND PARTICIPATION_ID!=@PARTICIPATION_ID 
                   AND IS_ACTIVE='Y'
           )
  BEGIN
   
   SET @HAS_ERROR =-4
   RETURN
   
  END          
               
   UPDATE MNT_REINSURANCE_MAJORMINOR_PARTICIPATION  
	 SET  
	 PARTICIPATION_ID=@PARTICIPATION_ID,  
	 REINSURANCE_COMPANY=@REINSURANCE_COMPANY,  
	 LAYER=@LAYER,        
	 NET_RETENTION=@NET_RETENTION,  
	 WHOLE_PERCENT=@WHOLE_PERCENT,   
	 MINOR_PARTICIPANTS=@MINOR_PARTICIPANTS,   
	 MODIFIED_BY=@MODIFIED_BY,  
	 LAST_UPDATED_DATETIME=@LAST_UPDATED_DATETIME  
 WHERE   
	 PARTICIPATION_ID=@PARTICIPATION_ID;    
	      
  END  
  
GO

