IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivatePolicyLocation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivatePolicyLocation]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------        
Proc Name       : Proc_ActivateDeactivatePolicyLocation    
Created by      : Anurag Verma        
Date            : 10 Nov,2005        
Purpose         : To Activate/Deactivate the record in POL_LOCATION table        
Revison History :        
modified by		: Pradeep Kushwaha 
Date            : 16-July-2010           
Purpose         : This is applicable to those products only where application location is used in Risk information 
Used In         : Ebix Advantage web                              


------------------------------------------------------------        
Date     Review By          Comments        
------   ------------       -------------------------*/        
-- DROP  PROC dbo.Proc_ActivateDeactivatePolicyLocation    
CREATE  PROC [dbo].[Proc_ActivateDeactivatePolicyLocation]    
(        
 @CUSTOMER_ID Int,    
 @POL_ID Int,    
 @POL_VERSION_ID SmallInt,    
 @LOCATION_ID  SmallInt,        
 @IS_ACTIVE   NChar(1)        
)        
AS        
BEGIN     
 
 DECLARE @LOB_ID INT     
 DECLARE @FLAG SMALLINT  
 SET @FLAG=0
 
 SELECT  @LOB_ID = POLICY_LOB FROM POL_CUSTOMER_POLICY_LIST with(nolock)   WHERE CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POL_ID and POLICY_VERSION_ID=@POL_VERSION_ID    
  
  
IF @IS_ACTIVE = 'Y'   
  
	BEGIN  
		IF EXISTS(SELECT LOCATION_ID FROM POL_LOCATIONS  
		WHERE CUSTOMER_ID = @CUSTOMER_ID AND  POLICY_ID = @POL_ID AND POLICY_VERSION_ID = @POL_VERSION_ID AND IS_ACTIVE = 'Y' AND LOCATION_ID =@LOCATION_ID)    
		RETURN 0  
	END   
    
 IF @IS_ACTIVE = 'N'    
 BEGIN  
     
    IF(@LOB_ID=9)--For the Named Perils prodcut
		BEGIN
			IF EXISTS (SELECT PP.LOCATION FROM POL_PERILS PP WITH(NOLOCK) 
				WHERE PP.CUSTOMER_ID=@CUSTOMER_ID AND 
				PP.POLICY_ID =@POL_ID AND 
				PP.POLICY_VERSION_ID=@POL_VERSION_ID AND 
				PP.LOCATION=@LOCATION_ID
				)     
				BEGIN
					SET @FLAG=1
					RETURN -1
				END
			
	   END
   ELSE IF( @LOB_ID IN(10,11,14,16,19,12))------Comprehensive Condominium (10),Comprehensive Company(11),Diversified Risks(14),Robbery(16),Dwelling(19) and General Civil Liability(12)  
	   BEGIN
			IF EXISTS (SELECT LOCATION FROM POL_PRODUCT_LOCATION_INFO WITH(NOLOCK) 
					WHERE CUSTOMER_ID=@CUSTOMER_ID AND 
					POLICY_ID =@POL_ID AND 
					POLICY_VERSION_ID=@POL_VERSION_ID AND 
					LOCATION=@LOCATION_ID
					)     
					
			BEGIN  
			  SET @FLAG=1
			  RETURN -1  
			END  
		END
  ELSE
  BEGIN
	IF EXISTS( SELECT * FROM POL_DWELLINGS_INFO WITH(NOLOCK)   
		  WHERE CUSTOMER_ID = @CUSTOMER_ID AND  
		  POLICY_ID =  @POL_ID AND  
		  POLICY_VERSION_ID = @POL_VERSION_ID AND  
		  LOCATION_ID =  @LOCATION_ID  
		 )    
	BEGIN  
		  SET @FLAG=1
		  RETURN -1  
	END  
 END
 END    
 IF(@FLAG <> 1)
   BEGIN   
	 UPDATE POL_LOCATIONS    
	 SET         
			IS_ACTIVE  = @IS_ACTIVE       
	 WHERE        
		  CUSTOMER_ID =  @CUSTOMER_ID AND    
		  POLICY_ID = @POL_ID AND    
		  POLICY_VERSION_ID=@POL_VERSION_ID AND     
		  LOCATION_ID    = @LOCATION_ID   
	 RETURN 1    
  END  
END    
    
    
    
  
GO

