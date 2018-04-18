IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateRiskInformation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateRiskInformation]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------                                              
Proc Name             : Dbo.Proc_UpdateRiskInformation                                              
Created by            : Santosh Kumar Gautam                                             
Date                  : 11/11/2010                                             
Purpose               : To update the risk information details    
Revison History       :                                              
Used In               : claim module    
------------------------------------------------------------                                              
Date     Review By          Comments                 
        
drop Proc Proc_UpdateRiskInformation                                     
------   ------------       -------------------------*/                                              
--                 
                  
--               
            
CREATE PROCEDURE [dbo].[Proc_UpdateRiskInformation]           
  
 --@CUSTOMER_ID int,                  
 --@POLICY_ID int ,                  
 --@POLICY_VERSION_ID smallint,                
                 
 @INSURED_PRODUCT_ID		int                   
,@CLAIM_ID					int                  
,@POL_RISK_ID				int     =NULL                      
,@YEAR						smallint    =NULL                     
,@VEHICLE_INSURED_PLEADED_GUILTY  char(1)    =NULL                      
,@VEHICLE_MAKER				nvarchar(150)=NULL                      
,@VEHICLE_MODEL				nvarchar(150)=NULL                      
,@VEHICLE_VIN				nvarchar(150)=NULL                      
,@DAMAGE_DESCRIPTION		nvarchar(150)=NULL                      
,@VESSEL_TYPE				nvarchar(70) =NULL                      
,@VESSEL_NAME				nvarchar(70) =NULL                   
,@VESSEL_MANUFACTURER		nvarchar(50) =NULL                   
,@LOCATION_ADDRESS			nvarchar(150)=NULL                   
,@LOCATION_COMPLIMENT		nvarchar(75) =NULL                   
,@LOCATION_DISTRICT			nvarchar(75) =NULL                   
,@LOCATION_ZIPCODE			nvarchar(11) =NULL   
,@CITY1                   	nvarchar(250)=NULL 
,@STATE1			      	nvarchar(150)=NULL                      
,@COUNTRY1			      	nvarchar(150)=NULL      
,@CITY2                   	nvarchar(250)=NULL   
,@STATE2			      	nvarchar(150)=NULL                     
,@COUNTRY2			      	nvarchar(150)=NULL                      
,@VOYAGE_CONVEYENCE_TYPE    nvarchar(150)=NULL                   
,@VOYAGE_DEPARTURE_DATE     datetime=NULL  
,@INSURED_NAME				nvarchar(150)=NULL                   
,@EFFECTIVE_DATE			datetime    =NULL                   
,@EXPIRE_DATE				datetime    =NULL  
,@LICENCE_PLATE_NUMBER 		nvarchar(50)
,@DAMAGE_TYPE				int
,@PERSON_DOB				datetime
,@PERSON_DiSEASE_DATE		datetime
,@VOYAGE_CERT_NUMBER		nvarchar(50)
,@VOYAGE_PREFIX				nvarchar(50)
,@VESSEL_NUMBER				nvarchar(50)
,@VOYAGE_TRAN_COMPANY		nvarchar(150)
,@VOYAGE_IO_DESC			nvarchar(256)
,@VOYAGE_ARRIVAL_DATE		datetime
,@VOYAGE_SURVEY_DATE		datetime  
,@ITEM_NUMBER			    int
,@RURAL_INSURED_AREA        int
,@RURAL_PROPERTY            int
,@RURAL_CULTIVATION         int
,@RURAL_FESR_COVERAGE       int
,@RURAL_MODE				int
,@RURAL_SUBSIDY_PREMIUM		decimal(18,2)
,@PA_NUM_OF_PASS			numeric(18,0)
,@DP_TICKET_NUMBER			int
,@DP_CATEGORY				int  
,@MODIFIED_BY				int    
,@LAST_UPDATED_DATETIME		datetime   
,@ACTUAL_INSURED_OBJECT     nvarchar(250)
,@ERROR_CODE				INT OUT   
                                                                    
                  
AS                  
BEGIN           
      
    DECLARE @VICTIM_COUNT INT =0
    SET @ERROR_CODE=0 
    
      
    -- FOR LOB PERSONAL ACCIDENT FOR PASSENGERS 
    -- IF NUMBER OF PASSENGERS IS LESS THEN THE ADDED PASSENGERS(VICTIM SCREEN) THEN SHOW ERROR
    IF(@PA_NUM_OF_PASS IS NOT NULL AND   @PA_NUM_OF_PASS>0)
     BEGIN
              
		 SELECT @VICTIM_COUNT= COUNT(VICTIM_ID) FROM [CLM_VICTIM_INFO] WITH(NOLOCK) WHERE CLAIM_ID=@CLAIM_ID
	     
	     
		 IF (@PA_NUM_OF_PASS<@VICTIM_COUNT)
		  BEGIN    
			  SET @ERROR_CODE=-4 
			  RETURN  
		  END
	   
   END
     
    
      
    UPDATE [dbo].[CLM_INSURED_PRODUCT]    
    SET    
          [POL_RISK_ID]					      =@POL_RISK_ID					 
          ,VEHICLE_INSURED_PLEADED_GUILTY	  =@VEHICLE_INSURED_PLEADED_GUILTY	 		
          ,VEHICLE_MAKER					  =@VEHICLE_MAKER					 		
          ,VEHICLE_MODEL					  =@VEHICLE_MODEL					 		
          ,VEHICLE_VIN						  =@VEHICLE_VIN						 		
          ,DAMAGE_DESCRIPTION				  =@DAMAGE_DESCRIPTION				 		
          ,VESSEL_TYPE						  =@VESSEL_TYPE						 		
          ,VESSEL_NAME						  =@VESSEL_NAME						 		
          ,VESSEL_MANUFACTURER				  =@VESSEL_MANUFACTURER				 		
          ,LOCATION_ADDRESS				 	  =@LOCATION_ADDRESS				 		
          ,LOCATION_COMPLIMENT				  =@LOCATION_COMPLIMENT				 		  
          ,LOCATION_DISTRICT				  =@LOCATION_DISTRICT				 		
          ,LOCATION_ZIPCODE				 	  =@LOCATION_ZIPCODE				 		
          ,VOYAGE_CONVEYENCE_TYPE			  =@VOYAGE_CONVEYENCE_TYPE			 		
          ,VOYAGE_DEPARTURE_DATE			  =@VOYAGE_DEPARTURE_DATE			 		
          ,INSURED_NAME					 	  =@INSURED_NAME					 		
          ,EFFECTIVE_DATE					  =@EFFECTIVE_DATE					 		
          ,EXPIRE_DATE						  =@EXPIRE_DATE						 		
          ,MODIFIED_BY						  =@MODIFIED_BY					 		
          ,LAST_UPDATED_DATETIME			  =@LAST_UPDATED_DATETIME				 		
          ,VESSEL_NUMBER					  =@VESSEL_NUMBER					 		
          ,PERSON_DISEASE_DATE				  =@PERSON_DISEASE_DATE				 		
          ,VOYAGE_PREFIX					  =@VOYAGE_PREFIX					 		
          ,VOYAGE_ARRIVAL_DATE				  =@VOYAGE_ARRIVAL_DATE				 		
          ,VOYAGE_SURVEY_DATE				  =@VOYAGE_SURVEY_DATE				 		    
          ,DAMAGE_TYPE						  =@DAMAGE_TYPE						 		  
          ,PERSON_DOB						  =@PERSON_DOB						 		 
          ,VOYAGE_CERT_NUMBER				  =@VOYAGE_CERT_NUMBER				 		
          ,VOYAGE_TRAN_COMPANY				  =@VOYAGE_TRAN_COMPANY				 		
          ,VOYAGE_IO_DESC					  =@VOYAGE_IO_DESC					 		
          ,ITEM_NUMBER						  =@ITEM_NUMBER				 		
          ,RURAL_INSURED_AREA				  =@RURAL_INSURED_AREA				 		
          ,RURAL_PROPERTY					  =@RURAL_PROPERTY					 		
		  ,RURAL_CULTIVATION				  =@RURAL_CULTIVATION				        
		  ,RURAL_FESR_COVERAGE				  =@RURAL_FESR_COVERAGE				 	    
		  ,RURAL_MODE						  =@RURAL_MODE						 		
		  ,RURAL_SUBSIDY_PREMIUM			  =@RURAL_SUBSIDY_PREMIUM			 			
		  ,PA_NUM_OF_PASS					  =@PA_NUM_OF_PASS					 	    
		  ,DP_TICKET_NUMBER				 	  =@DP_TICKET_NUMBER				 		
		  ,DP_CATEGORY						  =@DP_CATEGORY						 				
		  ,STATE1							  =@STATE1							 				
		  ,COUNTRY1						      =@COUNTRY1						        
		  ,COUNTRY2						      =@COUNTRY2						      	
		  ,STATE2							  =@STATE2							 			
		  ,CITY1							  =@CITY1							 		
		  ,CITY2							  =@CITY2							 
		  ,LICENCE_PLATE_NUMBER			      =@LICENCE_PLATE_NUMBER			        
		  ,[YEAR]							  =@YEAR 
		  ,[ACTUAL_INSURED_OBJECT]            =@ACTUAL_INSURED_OBJECT
    WHERE(INSURED_PRODUCT_ID=@INSURED_PRODUCT_ID AND IS_ACTIVE='Y')       
        		
     
END                  




GO

