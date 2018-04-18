IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertPolicyExistingWatercraftDriver]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertPolicyExistingWatercraftDriver]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--begin tran
--drop proc Proc_InsertPolicyExistingWatercraftDriver
--go
  /*----------------------------------------------------------                
Proc Name        : dbo.Proc_InsertPolicyExistingWatercraftDriver               
Created by       : -                 
Date             : -            
Purpose          : retrieving data from POL_WATERCRAFT_DRIVER_DETAILS                
Revison History  :                
Used In          : Wolverine                
------------------------------------------------------------                
Date     Review By          Comments                
------   ------------       -------------------------*/                
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  
    
      
        
        
          
--DROP PROCEDURE Proc_InsertPolicyExistingWatercraftDriver                
CREATE PROCEDURE Proc_InsertPolicyExistingWatercraftDriver                      
(                      
  @TO_CUSTOMER_ID int,                      
  @TO_POLICY_ID int,                    
  @TO_POLICY_VERSION_ID int,                
  @FROM_CUSTOMER_ID int,                    
  @FROM_POLICY_ID int,                     
  @FROM_POLICY_VERSION_ID int,                 
  @FROM_DRIVER_ID int,                      
  @CREATED_BY_USER_ID  int                 
)                      
AS                      
BEGIN                      
 Declare @TO_DRIVER_ID int                          
 SELECT  @TO_DRIVER_ID = ISNULL(MAX(DRIVER_ID),0) + 1                      
 FROM     POL_WATERCRAFT_DRIVER_DETAILS                       
 WHERE  CUSTOMER_ID=@TO_CUSTOMER_ID                      
 AND         POLICY_ID=@TO_POLICY_ID                      
 AND         POLICY_VERSION_ID=@TO_POLICY_VERSION_ID                       
                                        
 DECLARE @DRIVERCODE VARCHAR(20)                      
 DECLARE @LASTCHAR   VARCHAR(10)                       
 DECLARE @FIRSTCHAR   VARCHAR(10)     
  
 --Added by Sibin for Itrack issue 5304             
 DECLARE @TEMP_VIOLATION_TYPE INT               
 DECLARE @TEMP_TO_LOB_ID INT              
 DECLARE @TEMP_FROM_LOB_ID INT                                   
 DECLARE @TEMP_VIOLATION_ID INT               
 DECLARE @TEMP_STATE_ID INT    
  
SELECT @TEMP_STATE_ID=STATE_ID FROM POL_CUSTOMER_POLICY_LIST WHERE CUSTOMER_ID=@FROM_CUSTOMER_ID AND   
POLICY_ID=@FROM_POLICY_ID AND POLICY_VERSION_ID=@FROM_POLICY_VERSION_ID                                                             
                                                  
SELECT @TEMP_TO_LOB_ID=POLICY_LOB FROM POL_CUSTOMER_POLICY_LIST WHERE CUSTOMER_ID=@TO_CUSTOMER_ID AND   
POLICY_ID=@TO_POLICY_ID AND POLICY_VERSION_ID=@TO_POLICY_VERSION_ID                
              
SELECT @TEMP_FROM_LOB_ID=POLICY_LOB FROM POL_CUSTOMER_POLICY_LIST WHERE CUSTOMER_ID=@FROM_CUSTOMER_ID AND POLICY_ID=@FROM_POLICY_ID AND POLICY_VERSION_ID=@FROM_POLICY_VERSION_ID     
  
IF (@TEMP_FROM_LOB_ID = @TEMP_TO_LOB_ID)              
BEGIN           
  SET @DRIVERCODE=CONVERT(INT,RAND()*50)                        
                  
 SELECT @FIRSTCHAR=DRIVER_FNAME, @LASTCHAR=DRIVER_LNAME  FROM POL_WATERCRAFT_DRIVER_DETAILS                   
 WHERE   CUSTOMER_ID=@FROM_CUSTOMER_ID                        
 AND         POLICY_ID=@FROM_POLICY_ID                        
 AND         POLICY_VERSION_ID=@FROM_POLICY_VERSION_ID                        
 AND         DRIVER_ID= @FROM_DRIVER_ID                       
 SET @DRIVERCODE=(SUBSTRING(@FIRSTCHAR,1,3) + SUBSTRING(@LASTCHAR,1,4) + @DRIVERCODE)                       
                      
                      
  if(@TO_CUSTOMER_ID=@FROM_CUSTOMER_ID and @TO_POLICY_ID=@FROM_POLICY_ID and   
  @TO_POLICY_VERSION_ID=@FROM_POLICY_VERSION_ID)                
 BEGIN                
             
   INSERT INTO POL_WATERCRAFT_DRIVER_DETAILS                      
   (                      
    CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,DRIVER_ID,DRIVER_FNAME,DRIVER_MNAME,DRIVER_LNAME,DRIVER_CODE,   
 DRIVER_SUFFIX,DRIVER_ADD1,DRIVER_ADD2,DRIVER_CITY,DRIVER_STATE,DRIVER_ZIP,DRIVER_COUNTRY,DRIVER_DOB,  
 DRIVER_SSN,DRIVER_SEX,DRIVER_DRIV_LIC,DRIVER_LIC_STATE,DRIVER_COST_GAURAD_AUX,IS_ACTIVE,CREATED_BY,  
 CREATED_DATETIME,EXPERIENCE_CREDIT,vehicle_id,percent_driven ,                    
    APP_VEHICLE_PRIN_OCC_ID,DATE_ORDERED,MVR_ORDERED,VIOLATIONS,MARITAL_STATUS,MVR_REMARKS        
                    
   )                      
   SELECT                      
   @TO_CUSTOMER_ID,@TO_POLICY_ID,@TO_POLICY_VERSION_ID,@To_Driver_Id,DRIVER_FNAME,DRIVER_MNAME,DRIVER_LNAME,  
   @DRIVERCODE,DRIVER_SUFFIX,DRIVER_ADD1,DRIVER_ADD2,DRIVER_CITY,DRIVER_STATE,DRIVER_ZIP,DRIVER_COUNTRY,  
   DRIVER_DOB,DRIVER_SSN,DRIVER_SEX,DRIVER_DRIV_LIC,DRIVER_LIC_STATE,DRIVER_COST_GAURAD_AUX,'Y',   
 @CREATED_BY_USER_ID,GETDATE(),EXPERIENCE_CREDIT,vehicle_id,percent_driven,                    
   APP_VEHICLE_PRIN_OCC_ID,DATE_ORDERED,MVR_ORDERED,VIOLATIONS,MARITAL_STATUS,MVR_REMARKS                    
                         
   FROM    POL_WATERCRAFT_DRIVER_DETAILS                      
   WHERE   CUSTOMER_ID=@FROM_CUSTOMER_ID                      
   AND         POLICY_ID=@FROM_POLICY_ID                      
   AND         POLICY_VERSION_ID=@FROM_POLICY_VERSION_ID                      
   AND         DRIVER_ID= @FROM_DRIVER_ID                 
                 
   
   INSERT INTO POL_OPERATOR_ASSIGNED_BOAT        
	 (                                      
	 CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,DRIVER_ID,BOAT_ID,APP_VEHICLE_PRIN_OCC_ID             
	 )                                              
    SELECT                                         
	 @TO_CUSTOMER_ID,@TO_POLICY_ID,@TO_POLICY_VERSION_ID,@To_Driver_Id,BOAT_ID,11936--'Principal'        
	 FROM    POL_WATERCRAFT_INFO                                          
    WHERE   CUSTOMER_ID=@TO_CUSTOMER_ID                      
    AND     POLICY_ID=@TO_POLICY_ID                      
    AND     POLICY_VERSION_ID=@TO_POLICY_VERSION_ID  
  END                
 ELSE                
 BEGIN           
   INSERT INTO POL_WATERCRAFT_DRIVER_DETAILS                      
   (                      
    CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,DRIVER_ID,DRIVER_FNAME,DRIVER_MNAME,DRIVER_LNAME,DRIVER_CODE,   
	 DRIVER_SUFFIX,DRIVER_ADD1,DRIVER_ADD2,DRIVER_CITY,DRIVER_STATE,DRIVER_ZIP,DRIVER_COUNTRY,DRIVER_DOB,  
	 DRIVER_SSN,DRIVER_SEX,DRIVER_DRIV_LIC,DRIVER_LIC_STATE,DRIVER_COST_GAURAD_AUX,IS_ACTIVE,CREATED_BY,   
	 CREATED_DATETIME,EXPERIENCE_CREDIT,percent_driven,DATE_ORDERED,MVR_ORDERED,VIOLATIONS,MARITAL_STATUS,  
	 MVR_REMARKS                 
   )                      
   SELECT                      
   @TO_CUSTOMER_ID,@TO_POLICY_ID,@TO_POLICY_VERSION_ID,@To_Driver_Id,DRIVER_FNAME,DRIVER_MNAME,DRIVER_LNAME,  
   @DRIVERCODE,DRIVER_SUFFIX,DRIVER_ADD1,DRIVER_ADD2,DRIVER_CITY,DRIVER_STATE,DRIVER_ZIP,DRIVER_COUNTRY,  
   DRIVER_DOB,DRIVER_SSN,DRIVER_SEX,DRIVER_DRIV_LIC,DRIVER_LIC_STATE,DRIVER_COST_GAURAD_AUX,'Y',   
   @CREATED_BY_USER_ID,GETDATE(),EXPERIENCE_CREDIT,percent_driven,DATE_ORDERED,MVR_ORDERED,VIOLATIONS,  
   MARITAL_STATUS,MVR_REMARKS                    
                         
   FROM    POL_WATERCRAFT_DRIVER_DETAILS                      
   WHERE   CUSTOMER_ID=@FROM_CUSTOMER_ID                      
   AND     POLICY_ID=@FROM_POLICY_ID                      
   AND     POLICY_VERSION_ID=@FROM_POLICY_VERSION_ID                      
   AND     DRIVER_ID= @FROM_DRIVER_ID           
  
     
   INSERT INTO POL_OPERATOR_ASSIGNED_BOAT        
	 (                                      
	 CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,DRIVER_ID,BOAT_ID,APP_VEHICLE_PRIN_OCC_ID             
	 )                                              
	 SELECT                                         
	 @TO_CUSTOMER_ID,@TO_POLICY_ID,@TO_POLICY_VERSION_ID,@To_Driver_Id,BOAT_ID,11936--'Principal'        
	 FROM    POL_WATERCRAFT_INFO                                          
     WHERE   CUSTOMER_ID=@TO_CUSTOMER_ID          
     AND     POLICY_ID=@TO_POLICY_ID                      
     AND     POLICY_VERSION_ID=@TO_POLICY_VERSION_ID     
 END          
                                      
 INSERT INTO POL_WATERCRAFT_MVR_INFORMATION                       
 (                   
 CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,DRIVER_ID,APP_WATER_MVR_ID,MVR_AMOUNT,MVR_DEATH,MVR_DATE,                  
 VIOLATION_ID,IS_ACTIVE,VIOLATION_SOURCE,VERIFIED,VIOLATION_TYPE,                          
 --Added by Sibin on 16 Jan 09 for Itrack Issue 5304                          
 OCCURENCE_DATE,POINTS_ASSIGNED,ADJUST_VIOLATION_POINTS,DETAILS                          
 --,CREATED_BY,CREATED_DATETIME                            
 )                    
 SELECT                                            
 @TO_CUSTOMER_ID,@TO_POLICY_ID,@TO_POLICY_VERSION_ID,@TO_DRIVER_ID,APP_WATER_MVR_ID,MVR_AMOUNT,MVR_DEATH,MVR_DATE, VIOLATION_ID,IS_ACTIVE,VIOLATION_SOURCE,VERIFIED,VIOLATION_TYPE,        
 --Added by Sibin on 16 Jan 09 for Itrack Issue 5304                          
 OCCURENCE_DATE,POINTS_ASSIGNED,ADJUST_VIOLATION_POINTS,DETAILS                          
 --,CREATED_BY,CREATED_DATETIME                            
                 
 FROM  POL_WATERCRAFT_MVR_INFORMATION                    
 WHERE CUSTOMER_ID=@FROM_CUSTOMER_ID                              
 AND   POLICY_ID=@FROM_POLICY_ID                              
 AND   POLICY_VERSION_ID=@FROM_POLICY_VERSION_ID                              
 AND   DRIVER_ID= @FROM_DRIVER_ID                                                     
                                                                                        
 END                                         
ELSE                                             
               
  IF (@TEMP_FROM_LOB_ID=2 OR @TEMP_FROM_LOB_ID=3)              
  BEGIN                          
   SET @DRIVERCODE=CONVERT(INT,RAND()*50)                        
                  
   SELECT @FIRSTCHAR=DRIVER_FNAME, @LASTCHAR=DRIVER_LNAME  FROM POL_WATERCRAFT_DRIVER_DETAILS                   
   WHERE   CUSTOMER_ID=@FROM_CUSTOMER_ID                        
   AND         POLICY_ID=@FROM_POLICY_ID                        
   AND         POLICY_VERSION_ID=@FROM_POLICY_VERSION_ID                        
   AND         DRIVER_ID= @FROM_DRIVER_ID                       
   SET @DRIVERCODE=(SUBSTRING(@FIRSTCHAR,1,3) + SUBSTRING(@LASTCHAR,1,4) + @DRIVERCODE)                          
                                                            
   IF (@TO_CUSTOMER_ID=@FROM_CUSTOMER_ID and @TO_POLICY_ID=@FROM_POLICY_ID and   
	  @TO_POLICY_VERSION_ID=@FROM_POLICY_VERSION_ID)                                              
   BEGIN                                             
	                     
	   INSERT INTO POL_WATERCRAFT_DRIVER_DETAILS                      
	   (                      
		CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,DRIVER_ID,DRIVER_FNAME,DRIVER_MNAME,DRIVER_LNAME,DRIVER_CODE,   
		DRIVER_SUFFIX,DRIVER_ADD1,DRIVER_ADD2,DRIVER_CITY,DRIVER_STATE,DRIVER_ZIP,DRIVER_COUNTRY,DRIVER_DOB,  
		DRIVER_SSN,DRIVER_SEX,DRIVER_DRIV_LIC,DRIVER_LIC_STATE,IS_ACTIVE,CREATED_BY,  
		CREATED_DATETIME,vehicle_id,percent_driven ,                    
		APP_VEHICLE_PRIN_OCC_ID,DATE_ORDERED,MVR_ORDERED,VIOLATIONS,MARITAL_STATUS,MVR_REMARKS        
	                    
	   )                      
	   SELECT                      
	   @TO_CUSTOMER_ID,@TO_POLICY_ID,@TO_POLICY_VERSION_ID,@To_Driver_Id,DRIVER_FNAME,DRIVER_MNAME,DRIVER_LNAME,  
	   @DRIVERCODE,DRIVER_SUFFIX,DRIVER_ADD1,DRIVER_ADD2,DRIVER_CITY,DRIVER_STATE,DRIVER_ZIP,DRIVER_COUNTRY,  
	   DRIVER_DOB,DRIVER_SSN,DRIVER_SEX,DRIVER_DRIV_LIC,DRIVER_LIC_STATE,'Y',   
	   @CREATED_BY_USER_ID,GETDATE(),vehicle_id,percent_driven,                    
	   APP_VEHICLE_PRIN_OCC_ID,DATE_ORDERED,MVR_ORDERED,VIOLATIONS,DRIVER_MART_STAT,MVR_REMARKS                    
	                         
	   FROM    POL_DRIVER_DETAILS                      
	   WHERE   CUSTOMER_ID=@FROM_CUSTOMER_ID                      
	   AND     POLICY_ID=@FROM_POLICY_ID                      
	   AND     POLICY_VERSION_ID=@FROM_POLICY_VERSION_ID                      
	   AND     DRIVER_ID= @FROM_DRIVER_ID      
  
	   INSERT INTO POL_OPERATOR_ASSIGNED_BOAT        
		(                                      
		 CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,DRIVER_ID,BOAT_ID,APP_VEHICLE_PRIN_OCC_ID             
		)                                              
		SELECT                                         
		@TO_CUSTOMER_ID,@TO_POLICY_ID,@TO_POLICY_VERSION_ID,@To_Driver_Id,BOAT_ID,11936--'Principal'        
		FROM    POL_WATERCRAFT_INFO                                          
		WHERE   CUSTOMER_ID=@TO_CUSTOMER_ID                      
		AND     POLICY_ID=@TO_POLICY_ID                      
		AND     POLICY_VERSION_ID=@TO_POLICY_VERSION_ID             
	                 
	 END                
 ELSE                
 BEGIN            
   INSERT INTO POL_WATERCRAFT_DRIVER_DETAILS                      
	   (                      
		CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,DRIVER_ID,DRIVER_FNAME,DRIVER_MNAME,DRIVER_LNAME,DRIVER_CODE,   
		DRIVER_SUFFIX,DRIVER_ADD1,DRIVER_ADD2,DRIVER_CITY,DRIVER_STATE,DRIVER_ZIP,DRIVER_COUNTRY,DRIVER_DOB,  
		DRIVER_SSN,DRIVER_SEX,DRIVER_DRIV_LIC,DRIVER_LIC_STATE,IS_ACTIVE,CREATED_BY,  
		CREATED_DATETIME,vehicle_id,percent_driven ,                    
		APP_VEHICLE_PRIN_OCC_ID,DATE_ORDERED,MVR_ORDERED,VIOLATIONS,MARITAL_STATUS,MVR_REMARKS        
	                    
	   )                      
	   SELECT                      
	   @TO_CUSTOMER_ID,@TO_POLICY_ID,@TO_POLICY_VERSION_ID,@To_Driver_Id,DRIVER_FNAME,DRIVER_MNAME,DRIVER_LNAME,  
	   @DRIVERCODE,DRIVER_SUFFIX,DRIVER_ADD1,DRIVER_ADD2,DRIVER_CITY,DRIVER_STATE,DRIVER_ZIP,DRIVER_COUNTRY,  
	   DRIVER_DOB,DRIVER_SSN,DRIVER_SEX,DRIVER_DRIV_LIC,DRIVER_LIC_STATE,'Y',   
	   @CREATED_BY_USER_ID,GETDATE(),vehicle_id,percent_driven,                    
	   APP_VEHICLE_PRIN_OCC_ID,DATE_ORDERED,MVR_ORDERED,VIOLATIONS,DRIVER_MART_STAT,MVR_REMARKS                    
                         
   FROM    POL_DRIVER_DETAILS                      
   WHERE   CUSTOMER_ID=@FROM_CUSTOMER_ID                      
   AND     POLICY_ID=@FROM_POLICY_ID                      
   AND     POLICY_VERSION_ID=@FROM_POLICY_VERSION_ID                      
   AND     DRIVER_ID= @FROM_DRIVER_ID                              
       
    INSERT INTO POL_OPERATOR_ASSIGNED_BOAT        
	 (                                      
	 CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,DRIVER_ID,BOAT_ID,APP_VEHICLE_PRIN_OCC_ID             
	 )                                              
	 SELECT                                         
	 @TO_CUSTOMER_ID,@TO_POLICY_ID,@TO_POLICY_VERSION_ID,@To_Driver_Id,BOAT_ID,11936--'Principal'        
	 FROM    POL_WATERCRAFT_INFO                                          
	 WHERE   CUSTOMER_ID=@TO_CUSTOMER_ID                      
	 AND     POLICY_ID=@TO_POLICY_ID                      
	 AND     POLICY_VERSION_ID=@TO_POLICY_VERSION_ID  
                  
   END               

   DECLARE @MVR_ID int,@OLD_VIOLATION_ID int,@VIOLATION_TYPE INT             
   DECLARE CurMVR_INSERT CURSOR              
   FOR SELECT POL_MVR_ID,VIOLATION_ID,VIOLATION_TYPE FROM POL_MVR_INFORMATION               
   WHERE CUSTOMER_ID=@FROM_CUSTOMER_ID AND POLICY_ID=@FROM_POLICY_ID AND POLICY_VERSION_ID=@FROM_POLICY_VERSION_ID AND DRIVER_ID=@FROM_DRIVER_ID                 
               
   OPEN CurMVR_INSERT              
               
   FETCH NEXT FROM CurMVR_INSERT INTO @MVR_ID,@OLD_VIOLATION_ID,@VIOLATION_TYPE   
  IF(@VIOLATION_TYPE IN (SELECT VIOLATION_ID FROM MNT_VIOLATIONS where VIOLATION_ID > =15000            
                         and VIOLATION_CODE!='SUSPN' ))          
  BEGIN-----VIOLATION_TYPE AVAILABLE,VIOLATION_ID NOT AVAILABLE -- Done By Sibin on 12 Feb 09 for Itrack Issue 5304 to enable copying of MVR
   WHILE (@@FETCH_STATUS=0 )              
	BEGIN
		SELECT @TEMP_VIOLATION_TYPE=VIOLATION_ID FROM MNT_VIOLATIONS                       
		WHERE LOB=@TEMP_TO_LOB_ID AND STATE=@TEMP_STATE_ID                     
		AND VIOLATION_CODE=(SELECT VIOLATION_CODE FROM MNT_VIOLATIONS WHERE VIOLATION_ID=@VIOLATION_TYPE)                  
	               
		INSERT INTO POL_WATERCRAFT_MVR_INFORMATION                         
		(                                          
		APP_WATER_MVR_ID,CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,VIOLATION_ID,DRIVER_ID,                                                
		MVR_AMOUNT, MVR_DEATH,MVR_DATE,IS_ACTIVE,VERIFIED,VIOLATION_TYPE,                    
		--Added by Sibin on 16 Jan 09 for Itrack Issue 5304                    
		OCCURENCE_DATE,POINTS_ASSIGNED,ADJUST_VIOLATION_POINTS,DETAILS                    
		--,CREATED_BY,CREATED_DATETIME                        
		)                      
		SELECT                                              
		POL_MVR_ID,@TO_CUSTOMER_ID,@TO_POLICY_ID,@TO_POLICY_VERSION_ID,0,@TO_DRIVER_ID,MVR_AMOUNT,
		MVR_DEATH,MVR_DATE,IS_ACTIVE,VERIFIED,@TEMP_VIOLATION_TYPE,                     
		--Added by Sibin on 16 Jan 09 for Itrack Issue 5304                    
		OCCURENCE_DATE,POINTS_ASSIGNED,ADJUST_VIOLATION_POINTS,DETAILS                     
		--,CREATED_BY,GETDATE()                      
	     
		 FROM  POL_MVR_INFORMATION                    
		 WHERE CUSTOMER_ID=@FROM_CUSTOMER_ID                              
		 AND   POLICY_ID=@FROM_POLICY_ID                              
		 AND   POLICY_VERSION_ID=@FROM_POLICY_VERSION_ID                              
		 AND   DRIVER_ID= @FROM_DRIVER_ID                                                     
		 AND   POL_MVR_ID  = @MVR_ID             
	                  
		 FETCH NEXT FROM CurMVR_INSERT INTO @MVR_ID,@OLD_VIOLATION_ID,@VIOLATION_TYPE                             
	 END              
	  CLOSE CurMVR_INSERT              
	  DEALLOCATE CurMVR_INSERT              
  END  
  ELSE
   BEGIN                     
	 WHILE (@@FETCH_STATUS=0 )              
	  BEGIN                     
		SELECT  @TEMP_VIOLATION_TYPE=VIOLATION_ID FROM MNT_VIOLATIONS                 
		WHERE VIOLATION_GROUP=              
		 (SELECT VIOLATION_PARENT FROM MNT_VIOLATIONS WHERE VIOLATION_ID=@OLD_VIOLATION_ID)              
		AND LOB=@TEMP_TO_LOB_ID AND STATE=@TEMP_STATE_ID               
	               
		SELECT @TEMP_VIOLATION_ID=VIOLATION_ID FROM MNT_VIOLATIONS              
		WHERE VIOLATION_PARENT=(SELECT VIOLATION_GROUP FROM MNT_VIOLATIONS               
		WHERE VIOLATION_ID= @TEMP_VIOLATION_TYPE)           
		AND LOB=@TEMP_TO_LOB_ID AND STATE=@TEMP_STATE_ID               
		AND VIOLATION_CODE=(SELECT VIOLATION_CODE FROM MNT_VIOLATIONS WHERE VIOLATION_ID=@OLD_VIOLATION_ID)                 
	               
		INSERT INTO POL_WATERCRAFT_MVR_INFORMATION                         
		(                                          
		APP_WATER_MVR_ID,CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,VIOLATION_ID,DRIVER_ID,                                                
		MVR_AMOUNT, MVR_DEATH,MVR_DATE,IS_ACTIVE,VERIFIED,VIOLATION_TYPE,                    
		--Added by Sibin on 16 Jan 09 for Itrack Issue 5304                    
		OCCURENCE_DATE,POINTS_ASSIGNED,ADJUST_VIOLATION_POINTS,DETAILS                    
		--,CREATED_BY,CREATED_DATETIME                        
		)                      
		SELECT                                              
		POL_MVR_ID,@TO_CUSTOMER_ID,@TO_POLICY_ID,@TO_POLICY_VERSION_ID,@TEMP_VIOLATION_ID,@TO_DRIVER_ID,                   MVR_AMOUNT,MVR_DEATH,MVR_DATE,IS_ACTIVE,VERIFIED,@TEMP_VIOLATION_TYPE,                     
		--Added by Sibin on 16 Jan 09 for Itrack Issue 5304                    
		OCCURENCE_DATE,POINTS_ASSIGNED,ADJUST_VIOLATION_POINTS,DETAILS                     
		--,CREATED_BY,GETDATE()                      
	     
		 FROM  POL_MVR_INFORMATION                    
		 WHERE CUSTOMER_ID=@FROM_CUSTOMER_ID                              
		 AND   POLICY_ID=@FROM_POLICY_ID                              
		 AND   POLICY_VERSION_ID=@FROM_POLICY_VERSION_ID                              
		 AND   DRIVER_ID= @FROM_DRIVER_ID                                                     
		 AND   POL_MVR_ID  = @MVR_ID             
	                  
		FETCH NEXT FROM CurMVR_INSERT INTO @MVR_ID,@OLD_VIOLATION_ID,@VIOLATION_TYPE                   
	             
	  END              
	  CLOSE CurMVR_INSERT              
	  DEALLOCATE CurMVR_INSERT              
    END  
  END                                                                                                             
END                    
            

--go
--
--exec Proc_InsertPolicyExistingWatercraftDriver 864,79,4,864,95,1,5,334
--rollback tran
GO

