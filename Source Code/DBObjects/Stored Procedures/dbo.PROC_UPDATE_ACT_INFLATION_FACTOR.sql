IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_UPDATE_ACT_INFLATION_FACTOR]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_UPDATE_ACT_INFLATION_FACTOR]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------          
Proc Name       : dbo.PROC_UPDATE_ACT_INFLATION_FACTOR         
Modified by      :  Swarup Pal         
Date                :  07-Mar-2007         
Purpose         :  To update data into INFLATION_COST_FACTORS         
Revison History :          
Used In         :    Wolverine          
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/ 
-- DROP PROC PROC_UPDATE_ACT_INFLATION_FACTOR          
CREATE PROC PROC_UPDATE_ACT_INFLATION_FACTOR          
(            
	@INFLATION_ID int,          
	@STATE_ID INT,            
	@LOB_ID INT,            
	@ZIP_CODE INT,      
	@OLD_STATE_ID INT,      
	@OLD_LOB_ID INT,      
	@OLD_ZIP_CODE INT,            
	@EFFECTIVE_DATE DATETIME,            
	@EXPIRY_DATE DATETIME = NULL,            
	@FACTOR DECIMAL(10,3)          
)            
AS            
BEGIN            

DECLARE @FLAG INT    
SET @FLAG = 1    

IF(@STATE_ID=@OLD_STATE_ID AND @LOB_ID=@OLD_LOB_ID AND @ZIP_CODE=@OLD_ZIP_CODE)    
SET @FLAG=1    
ELSE    
SET @FLAG=0 



IF(@EXPIRY_DATE IS NULL)  
BEGIN   

	/***************************************************************************************/
	/* When we are not providing EXPIRY_DATE and EFFECTIVE_DATE is before of existing EFFECTIVE_DATE */
	/***************************************************************************************/

	IF EXISTS ( SELECT INFLATION_ID FROM INFLATION_COST_FACTORS   
		WHERE ZIP_CODE = @ZIP_CODE  
		AND STATE_ID = @STATE_ID  
		AND LOB_ID = @LOB_ID  
		AND EFFECTIVE_DATE >= @EFFECTIVE_DATE)
		AND @FLAG=0   
	RETURN -1

	/**************************************************************************************************************/
	/* When we are not providing EXPIRY_DATE and EFFECTIVE_DATE is before of existing EFFECTIVE_DATE without having EXPIRY_DATE*/ 
	/***************************************************************************************************************/  

	IF EXISTS ( SELECT INFLATION_ID FROM INFLATION_COST_FACTORS   
	WHERE   
		ZIP_CODE = @ZIP_CODE  
		AND STATE_ID = @STATE_ID  
		AND LOB_ID = @LOB_ID  
		AND @EFFECTIVE_DATE BETWEEN EFFECTIVE_DATE AND ISNULL(EXPIRY_DATE,'3000-12-31')) 
		AND @FLAG=0  
	RETURN -2  

END   
ELSE  
BEGIN   

	/*********************************************************************************/
	/* When we are providing EXPIRY_DATE and it is before of existing EFFECTIVE_DATE */
	/*********************************************************************************/

	IF EXISTS ( SELECT INFLATION_ID FROM INFLATION_COST_FACTORS   
		WHERE  ZIP_CODE = @ZIP_CODE  
		AND STATE_ID = @STATE_ID  
		AND LOB_ID = @LOB_ID     
		AND (  
		(@EFFECTIVE_DATE BETWEEN EFFECTIVE_DATE AND ISNULL(EXPIRY_DATE,'3000-12-31'))  
		OR  
		(@EXPIRY_DATE BETWEEN EFFECTIVE_DATE AND ISNULL(EXPIRY_DATE,'3000-12-31'))  
		)  
		) 
		AND @FLAG=0   
	RETURN -3

	/***********************************************************************************************************/
	/* When we are providing EXPIRY_DATE and it is before of existing EFFECTIVE_DATE without having EXPIRY_DATE*/ 
	/***********************************************************************************************************/ 
 
	IF EXISTS ( SELECT INFLATION_ID FROM INFLATION_COST_FACTORS   
		WHERE  ZIP_CODE = @ZIP_CODE  
		AND STATE_ID = @STATE_ID  
		AND LOB_ID = @LOB_ID   
		AND (  
			(@EFFECTIVE_DATE <= EFFECTIVE_DATE )  
			AND  
			(@EXPIRY_DATE >= ISNULL(EXPIRY_DATE,'3000-12-31'))  
		     )  
		) 
		AND @FLAG=0  
	RETURN -4  
END  

	/*********************************************/
	/* No record is found between existing period*/  
	/*********************************************/

UPDATE INFLATION_COST_FACTORS            
	SET  --STATE_ID = @STATE_ID,            
	-- LOB_ID = @LOB_ID,           
	--ZIP_CODE = @ZIP_CODE,            
	EFFECTIVE_DATE = @EFFECTIVE_DATE,            
	EXPIRY_DATE = @EXPIRY_DATE,            
	FACTOR = @FACTOR            
WHERE 
	--STATE_ID = @OLD_STATE_ID AND ZIP_CODE = @OLD_ZIP_CODE        
	--  AND LOB_ID = @OLD_LOB_ID AND 
	INFLATION_ID=@INFLATION_ID     
RETURN 1        
END            
  




GO

