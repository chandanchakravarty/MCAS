IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UPDATE_DWELLING_LINKED_COVERAGES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UPDATE_DWELLING_LINKED_COVERAGES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                      
                
Proc Name       : dbo.Proc_UPDATE_DWELLING_LINKED_COVERAGES                                      
Created by      : Pradeep                                      
Date            : 12/23/2005                                      
Purpose      : Updates coverages linked with this particular coverage                                   
Revison History :                                      
modfied by	:Pravesh K chandel
modified Date	:5 dec 2007
purpose		: iTRACK 2962
		Homeowners Type HO-3 Replacement - Indiana Only 
		If they purchase H0-24 (optional) then grey out HO-82 
		- no check mark 
		- included in the H0-24 coverage automatically 

Used In  : Wolverine                                      
------------------------------------------------------------                                      
Date     Review By          Comments                                      
------   ------------       -------------------------*/                                      
--- drop proc dbo.Proc_UPDATE_DWELLING_LINKED_COVERAGES  
CREATE           PROC dbo.Proc_UPDATE_DWELLING_LINKED_COVERAGES                  
(                                      
 @CUSTOMER_ID     int,                                      
 @APP_ID     int,                                      
 @APP_VERSION_ID     smallint,                                      
 @DWELLING_ID smallint               
)                   
                  
AS                   
                  
BEGIN                  
                   
DECLARE @STATEID SmallInt                                          
DECLARE @LOBID NVarCHar(5)                       
DECLARE @PRODUCT Int      
  
SELECT @STATEID = STATE_ID,                                          
@LOBID = APP_LOB,        
@PRODUCT = POLICY_TYPE                                        
FROM APP_LIST                                          
WHERE CUSTOMER_ID = @CUSTOMER_ID AND                                          
APP_ID = @APP_ID AND                                          
APP_VERSION_ID = @APP_VERSION_ID                     
  

  --------------------------          
          
--If HO-24 is selected, then insert   Personal Injury (HO-82)  in Section 2          
--143  EBP24 Premier V.I.P.(HO-24)         
--For  HO-3  Replacement Indiana   
-- 11149  HO-5 Replacement  
IF (@PRODUCT = 11148)        
BEGIN        
	IF EXISTS          
	(          
		SELECT * FROM APP_DWELLING_SECTION_COVERAGES          
		WHERE COVERAGE_CODE_ID = 143          
		AND CUSTOMER_ID = @CUSTOMER_ID AND            
		APP_ID = @APP_ID AND            
		APP_VERSION_ID = @APP_VERSION_ID AND            
		DWELLING_ID = @DWELLING_ID           
	)          
	BEGIN          
		--274  PERIJ Personal Injury (HO-82)                        
		/* COMMENTET BY PRAVESH ON 5 DEC 07 AS PER ITRACK 2962
		EXEC Proc_SAVE_DWELLING_COVERAGES                                           
		@CUSTOMER_ID, --@CUSTOMER_ID     int,                            
		@APP_ID, --@APP_ID     int,                                                                
		@APP_VERSION_ID,--@APP_VERSION_ID     smallint,                                        
		@DWELLING_ID, --@DWELLING_ID smallint,           
		-1,  --@COVERAGE_ID int,                                                                
		274, --@COVERAGE_CODE_ID int,                      
		NULL,  --@LIMIT_1 Decimal(18,2),             
		NULL,  --@LIMIT_2 Decimal(18,2),                                                                     
		NULL,  --@DEDUCTIBLE_1 DECIMAL(18,2),                                        
		NULL, --@DEDUCTIBLE_2 DECIMAL(18,2)                          
		'S2' --COVERAGE_TYPE                
		*/
		EXEC    Proc_DELETE_DWELLING_COVERAGES_BY_ID                                                           
		@CUSTOMER_ID,--@CUSTOMER_ID     int,                               
		@APP_ID,--@APP_ID     int,                                        
		@APP_VERSION_ID,--@APP_VERSION_ID     smallint,                                        
		@DWELLING_ID,--@DWELLING_ID smallint,                                        
		274--@COV_CODE_ID Int 
		IF ( @@ERROR <> 0 )                   
		BEGIN                                             
			RAISERROR ('Unable to save Personal Injury (HO-82).',16, 1)                                            
			RETURN                                           
		END          
	END          
  	ELSE          
	BEGIN          
		EXEC    Proc_DELETE_DWELLING_COVERAGES_BY_ID                                                           
		@CUSTOMER_ID,--@CUSTOMER_ID     int,                               
		@APP_ID,--@APP_ID     int,                                        
		@APP_VERSION_ID,--@APP_VERSION_ID     smallint,                                        
		@DWELLING_ID,--@DWELLING_ID smallint,                                        
		274--@COV_CODE_ID Int                
	END          
        
END -- OF HO-5 and HO-3  

END  
  

















GO

