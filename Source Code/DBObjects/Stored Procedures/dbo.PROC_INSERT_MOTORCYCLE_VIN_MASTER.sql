IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_INSERT_MOTORCYCLE_VIN_MASTER]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_INSERT_MOTORCYCLE_VIN_MASTER]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------            
PROC NAME     : DBO.PROC_INSERT_MOTORCYCLE_VIN_MASTER       
CREATED BY    : VIJAY ARORA    
DATE          : 24-10-2005    
PURPOSE         : TO INSERT THE DATA INTO MNT_VIN_MOTORCYCLE_MASTER TABLE.            
REVISON HISTORY :            
USED IN         : WOLVERINE            
------------------------------------------------------------            
DATE     REVIEW BY          COMMENTS            
------   ------------       -------------------------*/            
CREATE PROC DBO.PROC_INSERT_MOTORCYCLE_VIN_MASTER      
(            
 @ID INT,      
 @MANUFACTURER VARCHAR(255),      
 @MODEL VARCHAR(255),      
 @MODEL_YEAR VARCHAR(4),      
 @MODEL_CC INT = NULL      
)            
AS            
BEGIN          
    
 if not exists(select * from   MNT_VIN_MOTORCYCLE_MASTER WHERE [ID] = @ID AND MANUFACTURER=@MANUFACTURER     
				 AND MODEL=@MODEL AND MODEL_YEAR=@MODEL_YEAR  AND MODEL_CC = @MODEL_CC  )  
	begin
		   INSERT INTO MNT_VIN_MOTORCYCLE_MASTER ([ID],  MANUFACTURER, MODEL,  MODEL_YEAR, MODEL_CC)
			   VALUES (@ID,@MANUFACTURER,@MODEL,@MODEL_YEAR,@MODEL_CC)
	end
END      
    
  



GO

