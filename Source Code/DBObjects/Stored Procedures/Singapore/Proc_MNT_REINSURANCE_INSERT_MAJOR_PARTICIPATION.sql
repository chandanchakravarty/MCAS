
IF  EXISTS (SELECT * FROM SYS.OBJECTS WHERE OBJECT_ID = OBJECT_ID(N'[DBO].[PROC_MNT_REINSURANCE_INSERT_MAJOR_PARTICIPATION]') AND TYPE IN (N'P', N'PC'))
DROP PROCEDURE [DBO].[PROC_MNT_REINSURANCE_INSERT_MAJOR_PARTICIPATION]
GO

SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON
GO

--BEGIN TRAN
--DROP PROC PROC_MNT_REINSURANCE_INSERT_MAJOR_PARTICIPATION
--GO
/*----------------------------------------------------------                    
PROC NAME       : DBO.PROC_MNT_REINSURANCE_INSERT_MAJOR_PARTICIPATION                    
CREATED BY      : HARMANJEET SINGH                   
DATE            : MAY 7, 2007                  
PURPOSE         : TO INSERT THE DATA INTO REINSURER TIV GROUP TABLE.                    
REVISON HISTORY :                    
USED IN         : WOLVERINE             
                   
------------------------------------------------------------                    
DATE     REVIEW BY          COMMENTS                    
------   ------------       -------------------------    
DROP PROC [DBO].PROC_MNT_REINSURANCE_INSERT_MAJOR_PARTICIPATION     178,10,10964,50,10964,135,965,'2012-02-15',119
*/                    
CREATE PROC [DBO].PROC_MNT_REINSURANCE_INSERT_MAJOR_PARTICIPATION                    
(                    
                   
 @PARTICIPATION_ID INT OUTPUT,    
 @REINSURANCE_COMPANY NVARCHAR(100),    
 @LAYER INT,    
 @NET_RETENTION INT,    
 @WHOLE_PERCENT DECIMAL(18,0),    
 @MINOR_PARTICIPANTS INT,     
 @CREATED_BY INT,    
 @CREATED_DATETIME DATETIME,    
 @CONTRACT_ID INT    
 )                    
AS                    
BEGIN          
		DECLARE @TOTAL_WHOLE_PERC DECIMAL(18,0)
		
		
		SELECT @TOTAL_WHOLE_PERC = ISNULL(SUM(WHOLE_PERCENT),0) FROM MNT_REINSURANCE_MAJORMINOR_PARTICIPATION WHERE  LAYER=@LAYER AND CONTRACT_ID=@CONTRACT_ID
		IF EXISTS( SELECT CONTRACT_ID  FROM MNT_REIN_LOSSLAYER WHERE CONTRACT_ID = @CONTRACT_ID AND LAYER = @LAYER)
		BEGIN
			
			SELECT @PARTICIPATION_ID =  ISNULL(CONTRACT_ID,0)  FROM MNT_REIN_LOSSLAYER WHERE CONTRACT_ID = @CONTRACT_ID
			IF(@TOTAL_WHOLE_PERC + @WHOLE_PERCENT)>100 
			BEGIN
				SET @PARTICIPATION_ID =  -1
			END
			ELSE
			BEGIN  
					IF EXISTS( SELECT CONTRACT_ID  FROM MNT_REIN_LOSSLAYER WHERE CONTRACT_ID = @CONTRACT_ID and LAYER=@LAYER )
					BEGIN
							IF NOT EXISTS (SELECT PARTICIPATION_ID FROM MNT_REINSURANCE_MAJORMINOR_PARTICIPATION WHERE REINSURANCE_COMPANY=@REINSURANCE_COMPANY AND  CONTRACT_ID=@CONTRACT_ID AND LAYER=@LAYER)
							BEGIN
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
							ELSE
								SET @PARTICIPATION_ID = -3
					END  
					ELSE
					BEGIN
						SET @PARTICIPATION_ID =  -1
					END	
			END  
		END
		ELSE
		BEGIN
			SET @PARTICIPATION_ID = -2
		END
END 


--GO
--DECLARE @R INT
--EXEC PROC_MNT_REINSURANCE_INSERT_MAJOR_PARTICIPATION @R OUT,170,1,10964,10.0,10964,965,'12/22/2012',65
--select @R
--ROLLBACK TRAN