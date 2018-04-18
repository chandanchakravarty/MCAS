IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_INSERT_UMBRELLA_UMDERLYING_POLICY_COVERAGES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_INSERT_UMBRELLA_UMDERLYING_POLICY_COVERAGES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--drop proc Proc_INSERT_UMBRELLA_UMDERLYING_POLICY_COVERAGES        
CREATE PROC dbo.Proc_INSERT_UMBRELLA_UMDERLYING_POLICY_COVERAGES                                    
(                                    
 @CUSTOMER_ID     int,                                    
 @APP_ID     int,                                    
 @APP_VERSION_ID     smallint,                                    
 @POLICY_NUMBER     varchar(75),                                    
 @COVERAGE_DESC varchar(150),              
 @COVERAGE_AMOUNT varchar(100),              
 @POLICY_TEXT varchar(75),            
 @IS_POLICY bit,            
 @COV_CODE varchar(10),    
 @POLICY_COMPANY nvarchar(150),    
@COVERAGE_TYPE nvarchar (10)  =null         
)                                    
AS                                    
BEGIN                                    
              
              
              
--Insert values in APP_UMBRELLA_UNDERLYING_POLICIES_COVERAGES     

if exists(
	SELECT CUSTOMER_ID FROM  APP_UMBRELLA_UNDERLYING_POLICIES_COVERAGES  WITH(NOLOCK)     
		WHERE  CUSTOMER_ID=@CUSTOMER_ID      
		AND APP_ID=@APP_ID       
		AND APP_VERSION_ID=@APP_VERSION_ID      
		AND POLICY_NUMBER=@POLICY_NUMBER   
		AND POLICY_COMPANY = @POLICY_COMPANY AND COV_CODE=@COV_CODE)
	return -2
              
              
INSERT INTO APP_UMBRELLA_UNDERLYING_POLICIES_COVERAGES              
(              
 CUSTOMER_ID,                                    
 APP_ID,                                    
 APP_VERSION_ID,                                    
 POLICY_NUMBER,              
 COVERAGE_DESC ,              
 COVERAGE_AMOUNT ,              
 POLICY_TEXT,            
 IS_POLICY,        
 COV_CODE,    
 POLICY_COMPANY,
 COVERAGE_TYPE            
            
)              
              
VALUES              
(              
 @CUSTOMER_ID ,                                    
 @APP_ID ,                                    
 @APP_VERSION_ID ,                                    
 @POLICY_NUMBER  ,              
 @COVERAGE_DESC ,              
 @COVERAGE_AMOUNT ,              
 @POLICY_TEXT,            
 @IS_POLICY,        
 @COV_CODE,    
 @POLICY_COMPANY,
 @COVERAGE_TYPE    
)                                   
END            
          
        
 




GO

