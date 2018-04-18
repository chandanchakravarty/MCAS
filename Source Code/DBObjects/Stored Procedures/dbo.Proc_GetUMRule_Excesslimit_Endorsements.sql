IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetUMRule_Excesslimit_Endorsements]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetUMRule_Excesslimit_Endorsements]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/* ======================================================================================================    
PROC NAME                : DBO.Proc_GetUMRule_Excesslimit_Endorsements      
CREATED BY               : MANOJ RATHORE                                                                                                
DATE                     : 29 MAY,2007                                                
PURPOSE                  : TO GET EXCESS LIMITS & ENDORSEMENTS FOR UM RULES                                                
REVISON HISTORY          :                                                                                                
USED IN                  : WOLVERINE                                                                                                
======================================================================================================    
DATE     REVIEW BY          COMMENTS                                                                                                
=====  ==============   =============================================================================*/    
-- DROP PROC Proc_GetUMRule_Excesslimit_Endorsements  
CREATE PROC dbo.Proc_GetUMRule_Excesslimit_Endorsements
(                                                                                                
 @CUSTOMERID    INT,                                                                                                
 @APPID    INT,                                                                                                
 @APPVERSIONID   INT                                                                               
)                                                                                                
AS                                                                                                    
BEGIN   
-- MANDATORY ONLY     
	DECLARE @POLICY_LIMITS varchar(22)
	DECLARE @TERRITORY varchar(22)

	DECLARE @IS_RECORD_EXISTS CHAR
	IF EXISTS (SELECT CUSTOMER_ID FROM APP_UMBRELLA_LIMITS                                    
	WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID = @APPVERSIONID)                                                
		BEGIN  
			SET @IS_RECORD_EXISTS='N'    
			SELECT   
			@POLICY_LIMITS=convert(varchar(22),ISNULL(POLICY_LIMITS,'')),@TERRITORY=convert(varchar(22),ISNULL(TERRITORY,''))		
			FROM  APP_UMBRELLA_LIMITS                                               
			WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID = @APPVERSIONID                                                
		END                                                
	ELSE                                                
		BEGIN                                                 
		SET @POLICY_LIMITS=''     
		SET @TERRITORY='' 
		SET @IS_RECORD_EXISTS='Y'     
		END    
	    
	                                              
	SELECT  
	@POLICY_LIMITS AS POLICY_LIMITS, 
	@TERRITORY AS TERRITORY ,
	@IS_RECORD_EXISTS AS IS_RECORD_EXISTS              
	    
END                                                                      






GO

