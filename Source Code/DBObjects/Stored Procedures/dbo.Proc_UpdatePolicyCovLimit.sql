IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdatePolicyCovLimit]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdatePolicyCovLimit]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

    
             
 /*                                                                            
 ----------------------------------------------------------                                                                                
 Proc Name       : dbo.Proc_UpdatePolicyCovLimit                                                                           
 Created by      : Lalit Kumar Chauhan                                                                             
 Date            : 09/01/2010        
 Purpose         : Update Policy Cov Limit by Auto Endorsement           
 Revison History :                                                                                
 Used In         : EbixAdvantage Web    
 Modified by     :     
 Modification Date:     
 ------------------------------------------------------------                                                                                
 Date     Review By          Comments                                                                                
 ------   ------------       -------------------------                  
 LOBID   DESC          
 1   Homeowners          
 2  Automobile          
 3  Motorcycle          
 4  Watercraft          
 5  Umbrella          
 6  Rental          
 7  General Liability          
 8  Aviation          
 9  All Risks and Named Perils			Done     
 10  Comprehensive Condominium			Done    
 11  Comprehensive Company				Done    
 12  General Civil Liability			Done    
 13  Maritime							Done    
 14  Diversified Risks					Done       
 15  Individual Personal Accident		Done        
 16  Robbery							Done    
 17  Facultative Liability				Done    
 18  Civil Liability Transportation		Done         
 19  Dwelling                           Done    
 20  National Cargo Transport			Done    
 21  Group Passenger Personal Accident  Done         
 22  Passenger Personal Accident        Done    
 23  International Cargo Transport		Done         
      
---drop proc  Proc_UpdatePolicyCovLimit  2156,126,1,9,1,1086,20    
 */                                                                            
 CREATE PROC [dbo].[Proc_UpdatePolicyCovLimit]    
 (           
 @CUSTOMER_ID INT,          
 @POLICY_ID INT,        
 @POLICY_VERSION_ID SMALLINT,          
 @LOBID INT ,    
 @RISK_ID INT,    
 @COVERAGE_CODE_ID INT,    
 @LIMIT DECIMAL(12,2)              
 )            
 AS            
 BEGIN          
    
	IF (@LOBID = 3 or @LOBID = 2)    --For Automobile and Motorcycle
		BEGIN 
			  UPDATE POL_VEHICLE_COVERAGES SET LIMIT_1 = (LIMIT_1 - @LIMIT)  WHERE           
			  CUSTOMER_ID = @CUSTOMER_ID AND   
			  POLICY_ID = @POLICY_ID AND   
			  POLICY_VERSION_ID = @POLICY_VERSION_ID AND   
			  VEHICLE_ID  = @RISK_ID AND   
			  COVERAGE_CODE_ID = @COVERAGE_CODE_ID    
		END
     ELSE IF (@LOBID = 4)      --For watercraft
		BEGIN      
			 UPDATE POL_WATERCRAFT_COVERAGE_INFO SET LIMIT_1 = (LIMIT_1 - @LIMIT)  WHERE           
			  CUSTOMER_ID = @CUSTOMER_ID AND   
			  POLICY_ID = @POLICY_ID AND   
			  POLICY_VERSION_ID = @POLICY_VERSION_ID AND   
			  BOAT_ID  = @RISK_ID AND   
			  COVERAGE_CODE_ID = @COVERAGE_CODE_ID    
		END 
    ELSE IF (@LOBID = 1 OR @LOBID = 6)     --for homeowner and rental
		BEGIN      
			  UPDATE POL_DWELLING_SECTION_COVERAGES SET LIMIT_1 = (LIMIT_1 - @LIMIT)  WHERE           
			  CUSTOMER_ID = @CUSTOMER_ID AND   
			  POLICY_ID = @POLICY_ID AND   
			  POLICY_VERSION_ID = @POLICY_VERSION_ID AND   
			  DWELLING_ID  = @RISK_ID AND   
			  COVERAGE_CODE_ID = @COVERAGE_CODE_ID   
		END  
	ELSE IF (@LOBID = 8)     --For aviation
		BEGIN     
			  UPDATE POL_AVIATION_VEHICLE_COVERAGES SET LIMIT_1 = (LIMIT_1 - @LIMIT)  WHERE           
			  CUSTOMER_ID = @CUSTOMER_ID AND   
			  POLICY_ID = @POLICY_ID AND   
			  POLICY_VERSION_ID = @POLICY_VERSION_ID AND   
			  VEHICLE_ID = @RISK_ID AND   
			  COVERAGE_CODE_ID = @COVERAGE_CODE_ID   
		END    
		
	ELSE IF (@LOBID > 8) --For all Product     
	   BEGIN      
		 IF EXISTS(SELECT LIMIT_1 FROM POL_PRODUCT_COVERAGES WHERE  CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID       
		 AND RISK_ID  = @RISK_ID AND COVERAGE_CODE_ID = @COVERAGE_CODE_ID  )  
			BEGIN  
			  UPDATE POL_PRODUCT_COVERAGES SET LIMIT_1 = (LIMIT_1 - @LIMIT)  WHERE           
			  CUSTOMER_ID = @CUSTOMER_ID AND   
			  POLICY_ID = @POLICY_ID AND   
			  POLICY_VERSION_ID = @POLICY_VERSION_ID AND   
			  RISK_ID  = @RISK_ID AND   
			  COVERAGE_CODE_ID = @COVERAGE_CODE_ID    
			END  
	   END  --end    
END
GO

