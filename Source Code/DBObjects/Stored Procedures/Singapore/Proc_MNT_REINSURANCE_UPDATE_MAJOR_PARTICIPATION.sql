IF  EXISTS (SELECT * FROM SYS.OBJECTS WHERE OBJECT_ID = OBJECT_ID(N'[DBO].[Proc_MNT_REINSURANCE_UPDATE_MAJOR_PARTICIPATION]') AND TYPE IN (N'P', N'PC'))
DROP PROCEDURE [DBO].[Proc_MNT_REINSURANCE_UPDATE_MAJOR_PARTICIPATION]
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
     DECLARE @TOTAL_WHOLE_PERC DECIMAL(18,0)
     --DECLARE @RETENTION_PERCENTAGE DECIMAL(18,0)
     	
	 --SELECT @RETENTION_PERCENTAGE = RETENTION_PERCENTAGE from MNT_REIN_LOSSLAYER where CONTRACT_ID=@CONTRACT_ID AND LAYER=@LAYER	
	 SELECT @TOTAL_WHOLE_PERC = SUM(WHOLE_PERCENT) FROM MNT_REINSURANCE_MAJORMINOR_PARTICIPATION WHERE CONTRACT_ID=@CONTRACT_ID AND LAYER=@LAYER AND PARTICIPATION_ID NOT IN (@PARTICIPATION_ID) 
	
	IF(@TOTAL_WHOLE_PERC + @WHOLE_PERCENT)>100
		BEGIN
			RETURN -1
		END   
    ELSE   
		BEGIN
			IF  EXISTS (SELECT PARTICIPATION_ID FROM MNT_REINSURANCE_MAJORMINOR_PARTICIPATION WHERE REINSURANCE_COMPANY=@REINSURANCE_COMPANY AND  CONTRACT_ID=@CONTRACT_ID AND LAYER=@LAYER)
			  BEGIN
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
			  RETURN 1
			  END 
		END
 END