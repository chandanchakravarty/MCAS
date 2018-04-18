IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UPDATE_VEHICLE_ENDORSEMENTS_FROM_COVERAGES_FOR_POLICY]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UPDATE_VEHICLE_ENDORSEMENTS_FROM_COVERAGES_FOR_POLICY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
/*----------------------------------------------------------                    
Proc Name   : dbo.Proc_Get_UPDATE_VEHICLE_ENDORSEMENTS_FROM_COVERAGES          
Created by  : Pradeep          
Date        : mar 16, 2006  
Purpose     : Updates relevant endorsement s based on coverages      
Revison History  :                          
 ------------------------------------------------------------                                
Date     Review By          Comments                              
                     
------   ------------       -------------------------*/                    
CREATE PROCEDURE dbo.Proc_UPDATE_VEHICLE_ENDORSEMENTS_FROM_COVERAGES_FOR_POLICY        
(          
  @CUSTOMER_ID int,          
  @POLICY_ID int,          
  @POLICY_VERSION_ID int,  
  @VEHICLE_ID smallint    
          
)              
AS                   
BEGIN                    
         
 DECLARE @STATE_ID Int  
 DECLARE @LOB_ID Int  
 DECLARE @VEHICLE_USE  NVarChar(5)                                                    
 DECLARE @VEHICLE_TYPE_PER Int                                                    
 DECLARE @USE_VEHICLE Int                                                
              
                                      
SELECT   @VEHICLE_USE = VEHICLE_USE,                                                    
	@VEHICLE_TYPE_PER = APP_VEHICLE_PERTYPE_ID                                                 
	                                            
	FROM POL_VEHICLES                                                    
	WHERE VEHICLE_ID = @VEHICLE_ID AND                                                    
	CUSTOMER_ID = @CUSTOMER_ID AND                                                    
	POLICY_ID = @POLICY_ID AND                                                    
	POLICY_VERSION_ID = @POLICY_VERSION_ID                     
   
 SELECT @STATE_ID = STATE_ID,  
        @LOB_ID = POLICY_LOB  
 FROM POL_CUSTOMER_POLICY_LIST  
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND  
 POLICY_ID = @POLICY_ID AND  
 POLICY_VERSION_ID = @POLICY_VERSION_ID  
	

--Endorsement variables-------------------------------              
DECLARE @ENDORSEMENT_ID Int                                              
DECLARE @SNOWPLOW Int                                              
DECLARE @TRANSPORT Int                                            
DECLARE @PIP Int                                              
DECLARE @CUSTOMIZING Int     
DECLARE @A22 int                   
                                        
--For Customized van or truck, add/remove                                              
--Customizing Equipment (A-14) endorsement                      
IF ( @STATE_ID = 14 )                                              
--INDIANA          
BEGIN                                              
	SET @ENDORSEMENT_ID = 92     
	--SET @SNOWPLOW = 2                                              
	SET @CUSTOMIZING = 49                    
	--Transportation Expense - Amendment (A-90)                                            
	SET @TRANSPORT = 14      
	--SET @A22 = 16    

END                                              
                                              
IF ( @STATE_ID = 22 )                     
--MICHIGAN                                              
BEGIN                                              
	SET @ENDORSEMENT_ID = 93                                              
	--SET @SNOWPLOW = 33                
	SET  @PIP =  43                                          
	SET @CUSTOMIZING = 251                                           
	--Transportation Expense - Amendment (A-90)                                            
	SET @TRANSPORT = 34                                         
	--SET @A22 = 35    
END        
EXEC Proc_Insert_POL_VEHICLE_ENDORSEMENT_BY_ID @CUSTOMER_ID,                                       
         @POLICY_ID,                                              
         @POLICY_VERSION_ID,                                 
         @TRANSPORT,                                              
         @VEHICLE_ID                                              
 IF @@ERROR <> 0                                              
 BEGIN                                              
 RETURN                                              
 END  

IF ( @VEHICLE_TYPE_PER = '11618' )                           
BEGIN                                                    
		IF EXISTS                                            
		(                                            
			SELECT *                                             
				FROM POL_VEHICLE_ENDORSEMENTS                                                      
				INNER JOIN MNT_ENDORSMENT_DETAILS ON                                                      
				POL_VEHICLE_ENDORSEMENTS.ENDORSEMENT_ID = MNT_ENDORSMENT_DETAILS.ENDORSMENT_ID                      
				WHERE MNT_ENDORSMENT_DETAILS.STATE_ID = @STATE_ID AND                                                      
				MNT_ENDORSMENT_DETAILS.LOB_ID = @LOB_ID AND                                                      
				MNT_ENDORSMENT_DETAILS.ENDORS_ASSOC_COVERAGE = 'Y' AND                                                      
				POL_VEHICLE_ENDORSEMENTS.CUSTOMER_ID = @CUSTOMER_ID AND                                                      
				POL_VEHICLE_ENDORSEMENTS.POLICY_ID = @POLICY_ID AND                                             
				POL_VEHICLE_ENDORSEMENTS.POLICY_VERSION_ID = @POLICY_VERSION_ID AND                                                      
				POL_VEHICLE_ENDORSEMENTS.VEHICLE_ID = @VEHICLE_ID                                                      
		                           
		)                                            
		BEGIN                                                
		--Delete all Endorsements  except transportation amendment                                                  
			DELETE POL_VEHICLE_ENDORSEMENTS                                                      
				FROM POL_VEHICLE_ENDORSEMENTS                                                      
				INNER JOIN MNT_ENDORSMENT_DETAILS ON                                                      
				POL_VEHICLE_ENDORSEMENTS.ENDORSEMENT_ID = MNT_ENDORSMENT_DETAILS.ENDORSMENT_ID                                                      
				WHERE MNT_ENDORSMENT_DETAILS.STATE_ID = @STATE_ID AND                                                      
				MNT_ENDORSMENT_DETAILS.LOB_ID = @LOB_ID AND              MNT_ENDORSMENT_DETAILS.ENDORS_ASSOC_COVERAGE = 'Y' AND                                                      
				POL_VEHICLE_ENDORSEMENTS.CUSTOMER_ID = @CUSTOMER_ID AND                                                      
				POL_VEHICLE_ENDORSEMENTS.POLICY_ID = @POLICY_ID AND                                                      
				POL_VEHICLE_ENDORSEMENTS.POLICY_VERSION_ID = @POLICY_VERSION_ID AND                                                      
				POL_VEHICLE_ENDORSEMENTS.VEHICLE_ID = @VEHICLE_ID AND              
				POL_VEHICLE_ENDORSEMENTS.ENDORSEMENT_ID NOT IN (14,34)                                                       
		END                                            
                                       
		IF @@ERROR <> 0                                              
		BEGIN                                              
			RETURN                                              
		END                                   
		RETURN                                   
END                                                    
          

--Commit By Shafi 11-9-2006
--As It has been linked with coverage
--<start 1>
--If vehicle use is Snow plow, remove/ add relevant endorsements         
-- IF ( @VEHICLE_USE = 11272 )                                              
-- BEGIN                                              
-- 	EXEC Proc_Insert_APP_VEHICLE_ENDORSEMENT_BY_ID @CUSTOMER_ID,    
-- 	@APP_ID,                                             
-- 	@APP_VERSION_ID,                                              
-- 	@SNOWPLOW,                                              
-- 	@VEHICLE_ID                                              
-- 	IF @@ERROR <> 0                       
-- 	BEGIN                                              
-- 		RETURN                    
-- 	END                                              
--             
-- END                                              
-- ELSE                           
-- 	BEGIN                                              
-- 	EXEC Proc_Delete_APP_VEHICLE_ENDORSEMENT_BY_ID @CUSTOMER_ID,                                              
-- 	@APP_ID,                                              
-- 	@APP_VERSION_ID,                                              
-- 	@SNOWPLOW,                                              
-- 	@VEHICLE_ID                                              
-- 	IF @@ERROR <> 0                                              
-- 	BEGIN                                              
-- 		RETURN                                              
-- 	END                                              
-- END                                              
-- ---------------                                            
    
--Commit By Shafi 11-9-2006
--As It has been linked with coverage

--If Vehicle is not Motorhome, remove Motor Homes, Campers & Travel Trailers (A-22)  if it exists                             
-- IF ( @VEHICLE_TYPE_PER = 11336 OR @VEHICLE_TYPE_PER = 11337 )     
-- BEGIN    
-- 		EXEC Proc_Insert_APP_VEHICLE_ENDORSEMENT_BY_ID @CUSTOMER_ID,                                              
-- 		@APP_ID,                                              
-- 		@APP_VERSION_ID,                                              
-- 		@A22,                                              
-- 		@VEHICLE_ID         
-- 	  IF @@ERROR <> 0                                              
-- 		BEGIN        
-- 	 		RAISERROR('Unable to add A22 endorsment.',16,1 )                                      
-- 			RETURN                                              
--     END      
-- END    
-- ELSE
-- 		BEGIN
-- 			EXEC Proc_Delete_APP_VEHICLE_ENDORSEMENT_BY_ID @CUSTOMER_ID,                                              
-- 			@APP_ID,                                              
-- 			@APP_VERSION_ID,                                              
-- 			@A22,                                              
-- 			@VEHICLE_ID         
-- 		
-- 		IF @@ERROR <> 0                                              
-- 		BEGIN        
-- 			RAISERROR('Unable to delete A22 endorsment.',16,1 )                                      
-- 			RETURN                                              
-- 		END      
-- END
/*  
12  PUMSP Uninsured Motorists (BI Split Limit)  
34  UNDSP Underinsured Motorists (BI Split Limit) 14 2  
9  PUNCS Uninsured Motorists (CSL) 14 2  
14  UNCSL Underinsured Motorists (CSL) 14  
1  SLL Single Limits Liability CSL (BI and PD) 14  
2  BISPL Bodily Injury Liability ( Split Limit) 14  
  
--Motor  
126  RLCSL Single Limits Liability (CSL) 14 3  
127  BISPL Bodily Injury Liability (Split Limit) 14 3  
131  PUNCS Uninsured Motorists (CSL) 14 3  
133  UNCSL Underinsured Motorists (CSL) (M-16) 14  
132  PUMSP Uninsured Motorists (BI Split Limit)  
214  UNDSP Underinsured Motorists (BI Split Limit) (M-16) 14 3  
*/  

--End <1>

 IF ( @STATE_ID = 14 )  
 BEGIN  
	DECLARE @CSL_OR_BISPL Decimal(18,0)  
	DECLARE @CSL_OR_BISPL_ID Int  
  DECLARE  @PD_LIAB DECIMAL(18,0)
  DECLARE  @PD_LIAB_ID INT
	DECLARE  @SIGNATURE_OBTAINED_BI_IN VARCHAR(2)
	DECLARE  @SIGNATURE_OBTAINED_BI_UNDER VARCHAR(2)
	DECLARE  @SIGNATURE_OBTAINED_CSL_IN VARCHAR(2)
	DECLARE  @SIGNATURE_OBTAINED_CSL_UNDER VARCHAR(2)


  


  
 DECLARE @UNINSURED_BI Decimal(18,0)  
 DECLARE @UNINSURED_BI_TEXT VarChar(10)  
   
 DECLARE @UNDERINSURED_BI Decimal(18,0)  
 DECLARE @UNDERINSURED_BI_TEXT VarChar(10)  
  
 DECLARE @UNINSURED_CSL Decimal(18,0)  
 DECLARE @UNINSURED_CSL_TEXT VarChar(10)  
  
 DECLARE @UNDERINSURED_CSL Decimal(18,0)  
 DECLARE @UNDERINSURED_CSL_TEXT VarChar(10)  

 
   
 DECLARE @REJECT_OR_REDUCED VarChar(1)  
 DECLARE @A9 Int  
   
   
 SET @REJECT_OR_REDUCED = 'N'   
           
 IF ( @LOB_ID = 2 )  
 BEGIN  
  SET @A9 = 15  
 END 
   
 IF ( @LOB_ID = 3 )  
 BEGIN  
  SET @A9 = 47   
 END  
  
 --Get the amount of CSL or Bodily Injury liability  
 SELECT  @CSL_OR_BISPL = LIMIT_1,  
  @CSL_OR_BISPL_ID = COVERAGE_CODE_ID  
 FROM POL_VEHICLE_COVERAGES  
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND  
 POLICY_ID = @POLICY_ID AND  
 POLICY_VERSION_ID = @POLICY_VERSION_ID AND  
 VEHICLE_ID = @VEHICLE_ID AND  
 COVERAGE_CODE_ID IN (1,2, 126, 127)  


 SELECT  @PD_LIAB = LIMIT_1,  
  @PD_LIAB_ID = COVERAGE_CODE_ID  
 FROM POL_VEHICLE_COVERAGES  
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND  
 POLICY_ID = @POLICY_ID AND  
 POLICY_VERSION_ID = @POLICY_VERSION_ID AND  
 VEHICLE_ID = @VEHICLE_ID AND  
 COVERAGE_CODE_ID IN (4)  
  
 IF ( @CSL_OR_BISPL IS NULL )  
 BEGIN  
  RETURN  
 END  
 
 IF(@PD_LIAB IS NULL)
  BEGIN
   RETURN
  END
   
 IF NOT EXISTS  
 (  
  SELECT  *  
  FROM POL_VEHICLE_COVERAGES  
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND  
  POLICY_ID = @POLICY_ID AND  
  POLICY_VERSION_ID = @POLICY_VERSION_ID AND  
  VEHICLE_ID = @VEHICLE_ID AND  
  COVERAGE_CODE_ID IN (12,132,9,131,36)   
 )  
 BEGIN  
  EXEC Proc_Delete_POL_VEHICLE_ENDORSEMENT_BY_ID_ALL_VEHICLES          
    @CUSTOMER_ID,--@CUSTOMER_ID int,          
    @POLICY_ID,--@POLICY_ID int,          
    @POLICY_VERSION_ID,--@POLICY_VERSION_ID smallint,           
    @A9--@ENDORSEMENT_ID smallint, 
    
  IF @@ERROR <> 0   
  BEGIN  
   RAISERROR ('Unable to delete A-9 endorsement.', 16, 1)  
  END  
  
  RETURN  
 END  
  
 --Bodily Injury Liability ( Split Limit) ----------------------  
 IF ( @CSL_OR_BISPL_ID IN (2, 127) )  
 BEGIN  
  --Get the amount and text of Uninsured Motorists (BI Split Limit)   
  SELECT  @UNINSURED_BI = LIMIT_1,  
   @UNINSURED_BI_TEXT = LIMIT1_AMOUNT_TEXT,
   @SIGNATURE_OBTAINED_BI_IN=ISNULL(SIGNATURE_OBTAINED,'N')
  FROM POL_VEHICLE_COVERAGES  
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND  
  POLICY_ID = @POLICY_ID AND  
  POLICY_VERSION_ID = @POLICY_VERSION_ID AND  
  VEHICLE_ID = @VEHICLE_ID AND  
  COVERAGE_CODE_ID IN (12,132)  
   
  --Get the amount and text of Underinsured Motorists (BI Split Limit)     
  SELECT  @UNDERINSURED_BI = LIMIT_1,  
   @UNDERINSURED_BI_TEXT = LIMIT1_AMOUNT_TEXT ,
    @SIGNATURE_OBTAINED_BI_UNDER=ISNULL(SIGNATURE_OBTAINED,'N')
   
  FROM POL_VEHICLE_COVERAGES  
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND  
  POLICY_ID = @POLICY_ID AND  
  POLICY_VERSION_ID = @POLICY_VERSION_ID AND  
  VEHICLE_ID = @VEHICLE_ID AND  
  COVERAGE_CODE_ID IN (34,214)  AND
  SIGNATURE_OBTAINED='Y'


    
  --Check for reduced limits  
	IF ( ISNULL(@UNINSURED_BI,0) < ISNULL(@CSL_OR_BISPL,0) AND @SIGNATURE_OBTAINED_BI_IN='Y')  
	BEGIN  
	 SET @REJECT_OR_REDUCED = 'Y'  
	END  
	IF ( ISNULL(@UNINSURED_BI_TEXT,'') = 'Reject' AND @SIGNATURE_OBTAINED_BI_IN='Y')  
	BEGIN  
	 SET @REJECT_OR_REDUCED = 'Y'  
	END 

 IF ( (ISNULL(@UNDERINSURED_BI,0) < ISNULL(@CSL_OR_BISPL,0)) AND @SIGNATURE_OBTAINED_BI_UNDER='Y')  
	BEGIN  
	 SET @REJECT_OR_REDUCED = 'Y'  
	END  
	IF ( ISNULL(@UNDERINSURED_BI_TEXT,'') = 'Reject' AND @SIGNATURE_OBTAINED_BI_UNDER='Y')  
	BEGIN  
	 SET @REJECT_OR_REDUCED = 'Y'  
	END

PRINT @REJECT_OR_REDUCED

	

    
   
  
 END  
 --End of BISPL--------------------------------------------------------  
   
  
 --CSL----------------------------------  
 IF ( @CSL_OR_BISPL_ID IN (1,126) )  
 BEGIN  
  --Get the amount and text of Uninsured Motorists (CSL)   
  SELECT  @UNINSURED_CSL = LIMIT_1,  
   @UNINSURED_CSL_TEXT = LIMIT1_AMOUNT_TEXT ,
   @SIGNATURE_OBTAINED_CSL_IN=ISNULL(SIGNATURE_OBTAINED,'N')
  FROM POL_VEHICLE_COVERAGES  
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND  
  POLICY_ID = @POLICY_ID AND  
  POLICY_VERSION_ID = @POLICY_VERSION_ID AND  
  VEHICLE_ID = @VEHICLE_ID AND  
  COVERAGE_CODE_ID IN (9,131)  
   
  --Get the amount and text of Underinsured Motorists (CSL)     
  SELECT  @UNDERINSURED_CSL = LIMIT_1,  
   @UNDERINSURED_CSL_TEXT = LIMIT1_AMOUNT_TEXT , 
   @SIGNATURE_OBTAINED_CSL_UNDER=ISNULL(SIGNATURE_OBTAINED,'N')  
  FROM POL_VEHICLE_COVERAGES  
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND  
  POLICY_ID = @POLICY_ID AND  
  POLICY_VERSION_ID = @POLICY_VERSION_ID AND  
  VEHICLE_ID = @VEHICLE_ID AND  
  COVERAGE_CODE_ID IN (14,133) 
    


 --Check for reduced limits  
	IF ( ISNULL(@UNINSURED_CSL,0) < ISNULL(@CSL_OR_BISPL,0) AND @SIGNATURE_OBTAINED_CSL_IN='Y')  
	BEGIN  
	 SET @REJECT_OR_REDUCED = 'Y'  
	END  
	IF ( ISNULL(@UNINSURED_CSL_TEXT,'') = 'Reject' AND @SIGNATURE_OBTAINED_CSL_IN='Y')  
	BEGIN  
	 SET @REJECT_OR_REDUCED = 'Y'  
	END 

 IF ( ISNULL(@UNDERINSURED_CSL,0) < ISNULL(@CSL_OR_BISPL,0) AND @SIGNATURE_OBTAINED_CSL_UNDER='Y')  
	BEGIN  
	 SET @REJECT_OR_REDUCED = 'Y'  
	END  
	IF ( ISNULL(@UNDERINSURED_CSL_TEXT,'') = 'Reject' AND @SIGNATURE_OBTAINED_CSL_UNDER='Y')  
	BEGIN  
	 SET @REJECT_OR_REDUCED = 'Y'  
	END

--   --Check for reduced limits  
--   IF ( ISNULL(@UNINSURED_CSL,0) < ISNULL(@CSL_OR_BISPL,0) )  
--   BEGIN  
--    SET @REJECT_OR_REDUCED = 'Y'  
--   END  
--      
--       
--     
--    
--   --Check for reject in Uninsured and Underinsured  
--   IF ( ISNULL(@UNINSURED_CSL_TEXT,'') = 'Reject' AND   
--    ISNULL(@UNDERINSURED_CSL_TEXT,'') = 'Reject' )  
--   BEGIN  
--    SET @REJECT_OR_REDUCED = 'Y'  
--   END  
  
 END  
 --End of CSL-------------------------------------------------------- 
if( @PD_LIAB_ID=4)
BEGIN
		DECLARE @UNINSURED_PD INT
		DECLARE @UNINSURED_PD_TEXT VARCHAR(20)
		
		SELECT  @UNINSURED_PD = LIMIT_1,  
		@UNINSURED_PD_TEXT = LIMIT1_AMOUNT_TEXT 
		
		FROM POL_VEHICLE_COVERAGES  
		WHERE CUSTOMER_ID = @CUSTOMER_ID AND  
		POLICY_ID = @POLICY_ID AND  
		POLICY_VERSION_ID = @POLICY_VERSION_ID AND  
		VEHICLE_ID = @VEHICLE_ID AND  
		COVERAGE_CODE_ID =36
		IF ( ISNULL(@UNINSURED_PD,0) < ISNULL(@PD_LIAB,0) )  
		BEGIN  
			SET @REJECT_OR_REDUCED = 'Y'  
		END
		
		IF ( ISNULL(@UNINSURED_PD_TEXT,'') = 'Reject' )  
		BEGIN  
			SET @REJECT_OR_REDUCED = 'Y'  
		END
END
    
   
 IF ( @REJECT_OR_REDUCED = 'Y' )  
 BEGIN  
  EXEC Proc_Insert_POL_VEHICLE_ENDORSEMENT_BY_ID_ALL_VEHICLES                      
    @CUSTOMER_ID,--@CUSTOMER_ID int,            
    @POLICY_ID,--@POLICY_ID int,            
    @POLICY_VERSION_ID,--@POLICY_VERSION_ID smallint,             
    @A9--@ENDORSEMENT_ID smallint,        

    
  IF @@ERROR <> 0   
  BEGIN  
   RAISERROR ('Unable to update A-9 endorsement.', 16, 1)  
  END  
           
 END  
 ELSE  
 BEGIN  
  EXEC Proc_Delete_POL_VEHICLE_ENDORSEMENT_BY_ID_ALL_VEHICLES          
    @CUSTOMER_ID,--@CUSTOMER_ID int,          
    @POLICY_ID,--@POLICY_ID int,          
    @POLICY_VERSION_ID,--@POLICY_VERSION_ID smallint,           
    @A9--@ENDORSEMENT_ID smallint,      

    
  IF @@ERROR <> 0   
  BEGIN  
   RAISERROR ('Unable to delete A-9 endorsement.', 16, 1)  
  END  
 END  
   
 END  
        
        
END          
          
          
        
      
    
    
  















GO

