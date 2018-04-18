IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetWatercraftRule_TrailerInfo_Pol]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetWatercraftRule_TrailerInfo_Pol]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                                                                                                                                              
Proc Name                : Dbo.Proc_GetWatercraftRule_TrailerInfo_Pol         
Created by               : Manoj Rathore                                                                                                                                                                           
Date                     : 18 June .,2007                                                                                                                                                
Purpose                  : To get the Trailer info for Rules                                                                                                                                                                  
Revison History          :                                                                                                                                                                              
Used In                  : Wolverine                                                                                                                                                                              
------------------------------------------------------------                                                                                                                                                                              
Date     Review By          Comments                                                                                                                                                                              
------   ------------       -------------------------*/         
-- drop proc Proc_GetWatercraftRule_TrailerInfo_Pol                                                                                                                    
CREATE proc dbo.Proc_GetWatercraftRule_TrailerInfo_Pol                                                                                                                                                                                    
(                                                                                                                                                                                        
@CUSTOMER_ID    int,                                                                                          
@POLICY_ID    int,                                                                                          
@POLICY_VERSION_ID   int,                                                                                                                                                                              
@TRAILER_ID int                                                                                                                                     
)                               
                                                                                                                                                         
AS                 
BEGIN   
	DECLARE @TRAILER_TYPE CHAR(10)          
	DECLARE @INSURED_VALUE VARCHAR(15) 
	DECLARE @ASSOCIATED_BOAT  VARCHAR(5)
	DECLARE @TRAILER_DED_ID VARCHAR(5) 
	                                                       
		IF EXISTS (SELECT CUSTOMER_ID FROM POL_WATERCRAFT_TRAILER_INFO WHERE CUSTOMER_ID=@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID  AND  POLICY_VERSION_ID=@POLICY_VERSION_ID   AND TRAILER_ID=@TRAILER_ID)                                                             



                
		BEGIN                                                                          
			SELECT   @TRAILER_TYPE=ISNULL(TRAILER_TYPE,''),                                                    
			@INSURED_VALUE=ISNULL(CONVERT(VARCHAR(15),INSURED_VALUE),''),                                                                                                      
			@ASSOCIATED_BOAT=ISNULL(CONVERT(VARCHAR(5),ASSOCIATED_BOAT),''),                                                                                                                                 
			@TRAILER_DED_ID= ISNULL(CONVERT(VARCHAR(5),TRAILER_DED_ID),'')                                              
			                                                                             
			FROM POL_WATERCRAFT_TRAILER_INFO                                                                                                                                                                  
			WHERE CUSTOMER_ID=@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID  AND  POLICY_VERSION_ID=@POLICY_VERSION_ID   AND TRAILER_ID=@TRAILER_ID                                                                                                                          






		END                                                                                                                        
		                                                                                       
		--========================================= GRANDFATHER DEDUCTIBLE ====================================================  
	       
		DECLARE @APP_EFF_DATE datetime                                                
		SELECT @APP_EFF_DATE = APP_EFFECTIVE_DATE                                                                                   
		FROM POL_CUSTOMER_POLICY_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID 
                DECLARE @TRAILER_DEDUCTIBLE CHAR 

		SET @TRAILER_DEDUCTIBLE='N'                                                                                                                               
	 	IF EXISTS(SELECT  AVC.TRAILER_DED_ID FROM POL_WATERCRAFT_TRAILER_INFO AVC                                  
		INNER JOIN MNT_COVERAGE_RANGES MNTC ON   AVC.TRAILER_DED_ID= MNTC.LIMIT_DEDUC_ID                              
		INNER JOIN MNT_COVERAGE MNT ON AVC.TRAILER_DED_ID = MNT.COV_ID   
		WHERE CUSTOMER_ID=@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID  AND  POLICY_VERSION_ID=@POLICY_VERSION_ID   AND TRAILER_ID=@TRAILER_ID AND LIMIT_DEDUC_TYPE='DEDUCT'  
		AND NOT (@APP_EFF_DATE  BETWEEN ISNULL(MNTC.EFFECTIVE_FROM_DATE,'1950-1-1') AND ISNULL(MNTC.EFFECTIVE_TO_DATE,'3000-12-31')) ) 
		BEGIN
			SET @TRAILER_DEDUCTIBLE ='Y'
		END   
		SET @TRAILER_DEDUCTIBLE ='N' -- ADDED BY PRAVESH ON 25 APRIL 2008 AS THIS RULE NOT REQUIED NOW 

--==================================================================================================
DECLARE @JETSKI_TYPE_TRAILERINFO CHAR
DECLARE @BOATID Int

SELECT @BOATID = ASSOCIATED_BOAT  FROM POL_WATERCRAFT_TRAILER_INFO 
WHERE 	TRAILER_ID = @TRAILER_ID 
	AND POLICY_ID = @POLICY_ID
	AND CUSTOMER_ID = @CUSTOMER_ID
	AND POLICY_VERSION_ID = @POLICY_VERSION_ID and IS_ACTIVE='Y'


DECLARE @JETTYPEBOAT CHAR
DECLARE @JETSKI_TYPE_TRAILER CHAR 

SET @JETTYPEBOAT='N'

IF EXISTS(SELECT CUSTOMER_ID 
		FROM POL_WATERCRAFT_INFO                   
		WHERE POLICY_ID = @POLICY_ID
		AND CUSTOMER_ID = @CUSTOMER_ID
		AND POLICY_VERSION_ID = @POLICY_VERSION_ID
		AND  TYPE_OF_WATERCRAFT IN (11390 , 11387) 
		and BOAT_ID=@BOATID and IS_ACTIVE='Y') 
BEGIN 
	SET @JETTYPEBOAT ='Y'
END



SET @JETSKI_TYPE_TRAILER='N'

IF EXISTS(SELECT CUSTOMER_ID 
	FROM  POL_WATERCRAFT_TRAILER_INFO  with(nolock)                                                  
	WHERE  POLICY_ID = @POLICY_ID
	AND CUSTOMER_ID = @CUSTOMER_ID
	AND POLICY_VERSION_ID = @POLICY_VERSION_ID
	AND TRAILER_TYPE=11761                  
	AND TRAILER_ID = @TRAILER_ID and IS_ACTIVE='Y' ) 
BEGIN                   
	SET @JETSKI_TYPE_TRAILER ='Y'  
              
END 


IF ((@JETTYPEBOAT='Y' AND @JETSKI_TYPE_TRAILER='N') 
	OR (@JETTYPEBOAT='N' AND @JETSKI_TYPE_TRAILER='Y') )  --OR ( @OTHER_TYPE_TRAILER='Y' AND @JETTYPEBOAT='Y'))
BEGIN 
	SET @JETSKI_TYPE_TRAILERINFO='Y'
END
ELSE
BEGIN 
	SET @JETSKI_TYPE_TRAILERINFO='N'
END
		              
	             
	                                                                                        
	SELECT                                    
	
        @TRAILER_DEDUCTIBLE AS TRAILER_DEDUCTIBLE ,
     	@JETSKI_TYPE_TRAILERINFO AS JETSKI_TYPE_TRAILERINFO                  
END  













GO

