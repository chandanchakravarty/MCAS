IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateFireProtClean]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateFireProtClean]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------  
Proc Name       : dbo.Proc_InsertSolidFuel  
Created by      : Priya  
Date            : 5/20/2005  
Purpose         : To update record in solid fuel table
Revison History :  
Used In         :   Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
CREATE PROC dbo.Proc_UpdateFireProtClean 
(  
 @CUSTOMER_ID                 int,  
 @APP_ID                      int,  
 @APP_VERSION_ID              smallint,
 @FUEL_ID                     smallint,  
 @IS_SMOKE_DETECTOR           nchar(1),
 @IS_PROTECTIVE_MAT_FLOOR     nchar(1),
 @IS_PROTECTIVE_MAT_WALLS     nchar(1),
 @PROT_MAT_SPACED             nvarchar(265),  
 @STOVE_SMOKE_PIPE_CLEANED     nvarchar(20) ,  
 @STOVE_CLEANER               nvarchar(20),  
 @REMARKS                     nvarchar(2000)
    
)  
AS  
  
BEGIN  
 
  
 UPDATE APP_HOME_OWNER_FIRE_PROT_CLEAN
  SET  
	  FUEL_ID=@FUEL_ID,
	  CUSTOMER_ID =@CUSTOMER_ID,  
	  APP_ID=@APP_ID  ,  
	  APP_VERSION_ID=@APP_VERSION_ID   ,   
	  IS_SMOKE_DETECTOR=@IS_SMOKE_DETECTOR ,         
	  IS_PROTECTIVE_MAT_FLOOR=@IS_PROTECTIVE_MAT_FLOOR ,                        
	  IS_PROTECTIVE_MAT_WALLS=@IS_PROTECTIVE_MAT_WALLS ,    
	  PROT_MAT_SPACED=@PROT_MAT_SPACED,            
	  STOVE_SMOKE_PIPE_CLEANED=@STOVE_SMOKE_PIPE_CLEANED,
	  STOVE_CLEANER=@STOVE_CLEANER ,                            
	  REMARKS =@REMARKS                    

WHERE          
                CUSTOMER_ID = @CUSTOMER_ID AND
		APP_ID = @APP_ID AND	
		APP_VERSION_ID = @APP_VERSION_ID AND
		FUEL_ID = @FUEL_ID
END






GO

