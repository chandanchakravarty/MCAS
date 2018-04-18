IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_INSERT_ACT_INFLATION_FACTOR]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_INSERT_ACT_INFLATION_FACTOR]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------          
Proc Name       : dbo.PROC_INSERT_ACT_INFLATION_FACTOR         
Modified by      :  Swarup Pal         
Date                :  07-Mar-2007         
Purpose         :  To add data into INFLATION_COST_FACTORS         
Revison History :          
Used In         :    Wolverine          
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/          
-- DROP PROC dbo.PROC_INSERT_ACT_INFLATION_FACTOR        
CREATE PROC dbo.PROC_INSERT_ACT_INFLATION_FACTOR          
(          
        
	@STATE_ID INT,          
	@LOB_ID INT,          
	@ZIP_CODE INT,          
	@EFFECTIVE_DATE DATETIME,          
	@EXPIRY_DATE DATETIME=null,          
	@FACTOR DECIMAL(10,3),          
	@IS_ACTIVE     NCHAR(2),  
	@INFLATION_ID INT OUT    
)          
AS          
BEGIN   
  
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
	    return -5 
 
	 /**************************************************************************************************************/
	/* When we are not providing EXPIRY_DATE and EFFECTIVE_DATE is before of existing EFFECTIVE_DATE without having EXPIRY_DATE*/ 
	/***************************************************************************************************************/

	 IF EXISTS ( SELECT INFLATION_ID FROM INFLATION_COST_FACTORS   
	   WHERE   
	   ZIP_CODE = @ZIP_CODE  
	   AND STATE_ID = @STATE_ID  
	   AND LOB_ID = @LOB_ID  
	   AND @EFFECTIVE_DATE BETWEEN EFFECTIVE_DATE AND ISNULL(EXPIRY_DATE,'3000-12-31'))  
	    return -2   
  
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
    return -3 

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
    return -4  
END  
         
	/*********************************************/
	/* No record is found between existing period*/  
	/*********************************************/

SELECT @INFLATION_ID=ISNULL(MAX(INFLATION_ID),0)+1 FROM INFLATION_COST_FACTORS   
INSERT INTO INFLATION_COST_FACTORS          
(  
INFLATION_ID,              
STATE_ID,          
LOB_ID,          
ZIP_CODE,          
EFFECTIVE_DATE,          
EXPIRY_DATE,          
FACTOR,          
IS_ACTIVE  
  
)          
VALUES          
(   
@INFLATION_ID,         
@STATE_ID,          
@LOB_ID,          
@ZIP_CODE,          
@EFFECTIVE_DATE,          
@EXPIRY_DATE,          
@FACTOR,          
@IS_ACTIVE  
  
)          
RETURN 1      
END         
  
  
  
        
  
  
  
  
  
  



GO

