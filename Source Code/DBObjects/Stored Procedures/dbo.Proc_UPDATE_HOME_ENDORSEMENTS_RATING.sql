IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UPDATE_HOME_ENDORSEMENTS_RATING]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UPDATE_HOME_ENDORSEMENTS_RATING]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                      
Proc Name       : Proc_UPDATE_HOME_ENDORSEMENTS_RATING              
Created by      : Sumit Chhabra              
Date            : 01/01/2006                                     
Purpose        :Get Under Construction Information from RatingInfo              
Revison History :                                      
Used In  : Wolverine                                      
------------------------------------------------------------                                      
Date     Review By          Comments                                      
------   ------------       -------------------------*/                                      
CREATE           PROC dbo.Proc_UPDATE_HOME_ENDORSEMENTS_RATING                  
(                                      
 @CUSTOMER_ID     int,                                      
 @APP_ID     int,                                      
 @APP_VERSION_ID     smallint,                                      
 @DWELLING_ID smallint,            
 @CALLED_FROM VARCHAR(10)                
)                   
                  
AS                   
                  
BEGIN                      
declare @IS_UNDER_CONSTRUCTION INT              
DECLARE @NUM_LOC_ALARMS_APPLIES int      
DECLARE @STATE_ID INT              
DECLARE @POLICY_TYPE INT            
DECLARE @ENDORSEMENT_ID INT        
DECLARE @LOB_ID Int      
  
DECLARE @CENT_ST_BURG_FIRE Char(1)  
DECLARE @DIR_FIRE_AND_POLICE Char(1) 
DECLARE @DIR_FIRE Char(1)
DECLARE @DIR_POLICE Char(1)  
DECLARE @CENT_ST_FIRE varchar(1)
DECLARE @CENT_ST_BURG varchar(1)
DECLARE @ALARM_CERT_ATTACHED varchar(10)
return 1




           
SELECT @IS_UNDER_CONSTRUCTION = ISNULL(IS_UNDER_CONSTRUCTION,0),      
  @NUM_LOC_ALARMS_APPLIES = NUM_LOC_ALARMS_APPLIES,  
  @CENT_ST_BURG_FIRE = CENT_ST_BURG_FIRE ,  
  @DIR_FIRE_AND_POLICE = DIR_FIRE_AND_POLICE ,
  @DIR_FIRE =DIR_FIRE,
  @DIR_POLICE=DIR_POLICE ,
  @CENT_ST_FIRE =CENT_ST_FIRE,
  @CENT_ST_BURG =CENT_ST_BURG,
  @ALARM_CERT_ATTACHED =ALARM_CERT_ATTACHED
FROM APP_HOME_RATING_INFO       
WHERE               
 CUSTOMER_ID=@CUSTOMER_ID AND              
 APP_ID=@APP_ID AND              
 APP_VERSION_ID=@APP_VERSION_ID AND              
 DWELLING_ID=@DWELLING_ID   
              
SELECT @STATE_ID=STATE_ID,  
 @LOB_ID = APP_LOB FROM APP_LIST WHERE              
  CUSTOMER_ID=@CUSTOMER_ID AND              
 APP_ID=@APP_ID AND              
 APP_VERSION_ID=@APP_VERSION_ID               
            
            
--RENTAL**************************************************************************              
	IF(@LOB_ID = 6)            
	BEGIN            
	
		SELECT @POLICY_TYPE=POLICY_TYPE FROM APP_LIST            
		WHERE            
		CUSTOMER_ID=@CUSTOMER_ID AND              
		APP_ID=@APP_ID AND              
		APP_VERSION_ID=@APP_VERSION_ID               
		
		--For all products--------------------      
		DECLARE @ALARMS Int      
		
		--Indiana      
		IF ( @STATE_ID = 14 )      
		BEGIN      
			SET @ALARMS = 253      
		END      
		
		--Michigan      
		IF ( @STATE_ID = 22 )      
		BEGIN      
			SET @ALARMS = 266      
		END      
		
		--DP-216 Premises Alarm or Fire Protection System      
		IF @NUM_LOC_ALARMS_APPLIES > 0      
		BEGIN      
			EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID       
			@CUSTOMER_ID,@APP_ID,@APP_VERSION_ID,@ALARMS,@DWELLING_ID                          
		END      
		ELSE      
		BEGIN      
			EXEC Proc_Delete_HOME_ENDORSEMENT_BY_ID @CUSTOMER_ID,@APP_ID,      
			@APP_VERSION_ID,@ALARMS,@DWELLING_ID                          
		END      
	
	----------End of all products--------------------------------      
	
		IF(@POLICY_TYPE=11458) --FOR DP-3 PREMIER            
		BEGIN            
			BEGIN              
			IF(@STATE_ID=14)        
				EXEC Proc_Delete_HOME_ENDORSEMENT_BY_ID @CUSTOMER_ID,@APP_ID,@APP_VERSION_ID,258,@DWELLING_ID                           
			ELSE IF(@STATE_ID=22)                
				EXEC Proc_Delete_HOME_ENDORSEMENT_BY_ID @CUSTOMER_ID,@APP_ID,@APP_VERSION_ID,271,@DWELLING_ID                          
			END              
		END       
	--End of DP-3 PREMIER                   
		RETURN            
	
		IF(@IS_UNDER_CONSTRUCTION=1)        
		BEGIN              
			IF(@STATE_ID=14)              
			BEGIN                 
				EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID @CUSTOMER_ID,@APP_ID,@APP_VERSION_ID,258,@DWELLING_ID                          
			END              
			ELSE IF(@STATE_ID=22)                
			BEGIN                 
				EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID @CUSTOMER_ID,@APP_ID,@APP_VERSION_ID,271,@DWELLING_ID                          
			END              
		END                
		ELSE              
		BEGIN              
			IF(@STATE_ID=14)              
			BEGIN                 
				EXEC Proc_Delete_HOME_ENDORSEMENT_BY_ID @CUSTOMER_ID,@APP_ID,@APP_VERSION_ID,258,@DWELLING_ID                          
			END          
			ELSE IF(@STATE_ID=22)                
			BEGIN                 
				EXEC Proc_Delete_HOME_ENDORSEMENT_BY_ID @CUSTOMER_ID,@APP_ID,@APP_VERSION_ID,271,@DWELLING_ID                          
			END              
		END              
	END            
	--ELSE  --HOME LOB*****************************************************************          
--	BEGIN          
	
--        Commited By Shafi
-- -- 		--Dwelling under construction         
-- -- 		IF(@IS_UNDER_CONSTRUCTION=1)              
-- -- 		BEGIN              
-- -- 			IF(@STATE_ID=14)              
-- -- 			BEGIN                 
-- -- 				EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID @CUSTOMER_ID,@APP_ID,@APP_VERSION_ID,210,@DWELLING_ID                          
-- -- 			END              
-- -- 			ELSE IF(@STATE_ID=22)                
-- -- 			BEGIN                 
-- -- 				EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID @CUSTOMER_ID,@APP_ID,@APP_VERSION_ID,159,@DWELLING_ID                          
-- -- 			END              
-- -- 		END              
-- -- 		ELSE              
-- -- 		BEGIN              
-- -- 			IF(@STATE_ID=14)              
-- -- 			BEGIN                 
-- -- 				EXEC Proc_Delete_HOME_ENDORSEMENT_BY_ID @CUSTOMER_ID,@APP_ID,@APP_VERSION_ID,210,@DWELLING_ID                          
-- -- 			END              
-- -- 			ELSE IF(@STATE_ID=22)                
-- -- 			BEGIN               
-- -- 				EXEC Proc_Delete_HOME_ENDORSEMENT_BY_ID @CUSTOMER_ID,@APP_ID,@APP_VERSION_ID,159,@DWELLING_ID                          
-- -- 			END           
-- -- 	--End of dwelling under construction  
-- --     --HO-216***************************************************    
-- -- 		DECLARE @HO216 INT  
-- -- 		IF ( @STATE_ID = 14 )  
-- -- 		BEGIN  
-- -- 			SET @HO216 = 239  
-- -- 		END  
-- -- 		
-- -- 		IF ( @STATE_ID = 22 )  
-- -- 		BEGIN  
-- -- 			SET @HO216 = 189  
-- -- 		END  
-- 	
-- 	--If Central Stations Burglary and Fire Alarm System or Direct to Fire and Police  
-- 	--then attach HO-216 endorsement  
-- 	/*
-- 	@CENT_ST_BURG_FIRE = CENT_ST_BURG_FIRE ,  
-- 	@DIR_FIRE_AND_POLICE = DIR_FIRE_AND_POLICE ,
-- 	@DIR_FIRE =DIR_FIRE,
-- 	@DIR_POLICE=DIR_POLICE ,
-- 	@CENT_ST_FIRE =CENT_ST_FIRE,
-- 	@CENT_ST_BURG =CENT_ST_BURG,
-- 	@ALARM_CERT_ATTACHED =ALARM_CERT_ATTACHED
-- 	*/
-- --Commited By Shafi
-- 
-- -- 		IF ( (@CENT_ST_BURG = 'Y' AND @ALARM_CERT_ATTACHED='10963') OR (@CENT_ST_BURG_FIRE ='Y' AND @ALARM_CERT_ATTACHED='10963' )  OR @DIR_FIRE_AND_POLICE = 'Y' OR @DIR_FIRE= 'Y' OR @DIR_POLICE='Y' OR @CENT_ST_FIRE='Y' )  
-- -- 		BEGIN  
-- -- 			EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID   
-- -- 			@CUSTOMER_ID,  
-- -- 			@APP_ID,  
-- -- 			@APP_VERSION_ID,  
-- -- 			@HO216,  
-- -- 			@DWELLING_ID                          
-- -- 		END  
-- -- 		ELSE  
-- -- 		BEGIN  
-- -- 			EXEC Proc_Delete_HOME_ENDORSEMENT_BY_ID   
-- -- 			@CUSTOMER_ID,  
-- -- 			@APP_ID,  
-- -- 			@APP_VERSION_ID,  
-- -- 			@HO216,  
-- -- 			@DWELLING_ID                          
-- -- 		END  
-- 		--End of HO-216************************   
-- 		
-- 		
-- -- 		RETURN           
-- -- 		
 --	 END     
--  END     --End Of Lob If Else
--END OF SP--------------------------------------------------------------  
END                                     
                
              
              
            
          
        
      
    
  







GO

