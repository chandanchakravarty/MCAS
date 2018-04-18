IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateProductsCoveragesPremium]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateProductsCoveragesPremium]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                              
Proc Name       : dbo.Proc_UpdateProductsCoveragesPremium              
Created by      : Pradeep Kushwaha                    
Date            : 16-July-2010                                       
Purpose         : Update the WRITTEN_PREMIUM of the old product coverages from QuoteXml (QOT_CUSTOMER_QUOTE_LIST_POL table)
Revison History :                              
modified by		: 
Used In         : Ebix Advantage web                              
                             
------------------------------------------------------------                              
Date     Review By          Comments                              
------   ------------       -------------------------*/                              
--drop proc DBO.Proc_UpdateProductsCoveragesPremium                  
create PROC [dbo].[Proc_UpdateProductsCoveragesPremium]              
(                              
 @CUSTOMER_ID INT,              
 @POLICY_ID INT,
 @POLICY_VERSION_ID SMALLINT,
 @RISK_ID INT,              
 @COVERAGE_CODE_ID INT,
 @WRITTEN_PREMIUM  DECIMAL(18,2)
 )                             
AS              
BEGIN 
	DECLARE @LOBID INT   
	
	IF EXISTS(SELECT POLICY_LOB FROM POL_CUSTOMER_POLICY_LIST with(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID)
	   BEGIN
			   SELECT  @LOBID = POLICY_LOB FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID
			    
				IF (@LOBID = 3 or @LOBID = 2)    
					BEGIN     
					   UPDATE POL_VEHICLE_COVERAGES SET WRITTEN_PREMIUM=@WRITTEN_PREMIUM 
					   WHERE     
						   CUSTOMER_ID=@CUSTOMER_ID AND        
						   POLICY_ID=@POLICY_ID AND        
						   POLICY_VERSION_ID=@POLICY_VERSION_ID AND       
						   VEHICLE_ID=@RISK_ID AND 
						   COVERAGE_CODE_ID=@COVERAGE_CODE_ID    
					 END    
				ELSE IF (@LOBID = 4)    
				BEGIN      
					   UPDATE POL_WATERCRAFT_COVERAGE_INFO SET WRITTEN_PREMIUM=@WRITTEN_PREMIUM 
					   WHERE     
						   CUSTOMER_ID=@CUSTOMER_ID AND        
						   POLICY_ID=@POLICY_ID AND        
						   POLICY_VERSION_ID=@POLICY_VERSION_ID AND       
						   BOAT_ID=@RISK_ID AND 
						   COVERAGE_CODE_ID=@COVERAGE_CODE_ID   
			 
				END    
			 ELSE IF (@LOBID = 1 OR @LOBID = 6)    
					BEGIN  
							UPDATE POL_DWELLING_SECTION_COVERAGES SET WRITTEN_PREMIUM=@WRITTEN_PREMIUM 
							WHERE     
							   CUSTOMER_ID=@CUSTOMER_ID AND        
							   POLICY_ID=@POLICY_ID AND        
							   POLICY_VERSION_ID=@POLICY_VERSION_ID AND       
							   DWELLING_ID=@RISK_ID AND 
							   COVERAGE_CODE_ID=@COVERAGE_CODE_ID 	 
				   END    
			 ELSE IF (@LOBID = 8)    
				BEGIN  
							UPDATE POL_AVIATION_VEHICLE_COVERAGES SET WRITTEN_PREMIUM=@WRITTEN_PREMIUM 
							WHERE     
							   CUSTOMER_ID=@CUSTOMER_ID AND        
							   POLICY_ID=@POLICY_ID AND        
							   POLICY_VERSION_ID=@POLICY_VERSION_ID AND       
							   VEHICLE_ID=@RISK_ID AND 
							   COVERAGE_CODE_ID=@COVERAGE_CODE_ID 
				END  
	   END
END              
              
              
GO

