IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeletePolLocations]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeletePolLocations]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------            
            
Proc Name       : Proc_DeletePolLocations           
Created by      : Swastika Gaur            
Date            : 5th Apr'06            
Purpose         : Delete policy level location details.            
Revison History :           
modified by		: Pradeep Kushwaha 
Date            : 16-July-2010           
Purpose         : To check those products only where application location is used in Risk information 
Used In         : Ebix Advantage web                              

------------------------------------------------------------            
Date     Review By          Comments            
------   ------------       -------------------------*/ 
-- DROP PROC dbo.Proc_DeletePolLocations  2160,4,1,3            
CREATE PROC [dbo].[Proc_DeletePolLocations]            
(            
 @CUSTOMER_ID INT,      
 @POLICY_ID INT,      
 @POLICY_VERSION_ID smallint,      
 @LOCATION_ID int     
    
)            
AS            
BEGIN   
  DECLARE @LOB_ID INT     
  DECLARE @FLAG SMALLINT  
  SET @FLAG=0
 
 SELECT  @LOB_ID = POLICY_LOB FROM POL_CUSTOMER_POLICY_LIST with(nolock)   WHERE CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID    
 
IF(@LOB_ID=9)--For the Named Perils prodcut
	 
	BEGIN
		IF EXISTS (SELECT PP.LOCATION FROM POL_PERILS PP WITH(NOLOCK) 
			WHERE PP.CUSTOMER_ID=@CUSTOMER_ID AND 
			PP.POLICY_ID =@POLICY_ID AND 
			PP.POLICY_VERSION_ID=@POLICY_VERSION_ID AND 
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
				POLICY_ID =@POLICY_ID AND 
				POLICY_VERSION_ID=@POLICY_VERSION_ID AND 
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
		  POLICY_ID =  @POLICY_ID AND  
		  POLICY_VERSION_ID = @POLICY_VERSION_ID AND  
		  LOCATION_ID =  @LOCATION_ID  
		 )    
	BEGIN  
		  SET @FLAG=1
		  RETURN -1  
	END  
 END   
	IF(@FLAG <> 1)
	BEGIN  -- Delete Location Info  
	   DELETE FROM POL_LOCATIONS  
	   WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND LOCATION_ID=@LOCATION_ID   
	END       
END  
  
 
 
GO

