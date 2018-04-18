IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertVIN_MASTER]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertVIN_MASTER]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------      
Proc Name     : dbo.Proc_InsertVIN_MASTER 
Created by      : Anurag Verma      
Date                  : 29/06/2005      
Purpose         : To insert the data into MNT_VIN_MASTER table.      
Revison History :      
Used In         : Wolverine      
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/      
CREATE PROC Dbo.Proc_InsertVIN_MASTER
(      
@YEAR VARCHAR(4),
@MAKE VARCHAR(4),
@MODEL VARCHAR(25),
@BODYTYPE   VARCHAR(2),
@VIN VARCHAR(17)
)      
AS      
BEGIN    
 /*Check for Unique Code of Agency  */    
 Declare @Count numeric    
  SELECT @Count = 1
  FROM MNT_VIN_MASTER WHERE MODEL_YEAR = @YEAR AND MAKE_CODE=@MAKE AND SERIES_NAME=@MODEL AND VIN=@VIN
 IF @Count = 1     
 BEGIN    
    	RETURN -1	
 END    
 ELSE   
	 BEGIN   
	  INSERT INTO MNT_VIN_MASTER
	  (     
		MODEL_YEAR,
		MAKE_CODE,
		SERIES_NAME,
		BODY_TYPE,
		VIN	     		
	  )      
	  VALUES      
	  (      
		@YEAR,@MAKE,@MODEL,@BODYTYPE,@VIN	
	  )      	
	  END        
END


GO

