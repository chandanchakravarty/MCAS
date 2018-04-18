IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertExistingWatercraftDriver]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertExistingWatercraftDriver]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--begin tran  
--drop proc Proc_InsertExistingWatercraftDriver  
--go  
/*----------------------------------------------------------                        
PROC NAME          : DBO.Proc_InsertExistingWatercraftDriver                        
CREATED BY           : NIDHI                        
DATE                    : 27/06/2005                        
PURPOSE               :                         
REVISON HISTORY :                        
USED IN                :   WOLVERINE                          
                        
MODIFIED BY : ANURAG VERNA                        
MODIFIED ON : 22/09/2005                         
PURPOSE :REMOVING FIELDS AND ADDING EXPERIENCE_CREDIT FIELD                        
                        
MODIFIED BY : MOHIT GUPTA                        
MODIFIED ON : 29/09/2005                        
PURPOSE  : WHILE LOOP FOR GENERATING UNIQUE DRIVER CODE.                         
                      
MODIFIED BY : PRADEEP                      
MODIFIED ON : 10/21/2005                        
PURPOSE  : INCLUDED VEHICLE PRINCIPALLY DRIVEN WHILE COPYING                      
MODIFIED BY : SUMIT CHHABRA                      
MODIFIED ON : 11/30/2005                                  
PURPOSE  : MODIFIED DRIVER CODE GENERATION PROCEDURE                     
MODIFIED BY : SUMIT CHHABRA                      
MODIFIED ON : 22/12/2005                                  
PURPOSE    : ADDED CHECK TO COPY ASSIGNED VEHICLE AND OPERATOR ONLY WHEN THE DRIVER BELONGS TO CURRENT APPLICATION                  
                      
MODIFIED BY : ANURAG VERNA                        
MODIFIED ON : 21/07/2006                         
PURPOSE :ADDING FIELDS WAT_SAFETY_COURSE AND CERT_COAST_GUARD                  
              
MODIFIED BY :PRAVEEN KASANA              
MODIFIED ON : 13/9/2007              
PURPOSE : APP_OPERATOR_ASSIGNED_BOAT              
                  
                  
------------------------------------------------------------                        
DATE     REVIEW BY          COMMENTS                        
------   ------------       -------------------------*/                  
-- DROP PROC Proc_InsertExistingWatercraftDriver                      
CREATE PROCEDURE Proc_InsertExistingWatercraftDriver                     
(                
                          
 @TO_CUSTOMER_ID INT,                        
 @TO_APP_ID INT,                        
 @TO_APP_VERSION_ID INT,                        
 @FROM_CUSTOMER_ID INT,                        
 @FROM_APP_ID INT,                        
 @FROM_APP_VERSION_ID INT,                        
 @FROM_DRIVER_ID INT,                        
 @CREATED_BY_USER_ID  INT                
 --@APP_WATER_MVR_ID  INT OUTPUT                           
)                        
AS                        
BEGIN                        
DECLARE @TO_DRIVER_ID INT                            
SELECT  @TO_DRIVER_ID = ISNULL(MAX(DRIVER_ID),0) + 1                        
FROM    APP_WATERCRAFT_DRIVER_DETAILS                         
WHERE  CUSTOMER_ID=@TO_CUSTOMER_ID                        
AND    APP_ID=@TO_APP_ID                        
AND    APP_VERSION_ID=@TO_APP_VERSION_ID                         
                        
                        
                        
DECLARE @DRIVERCODE VARCHAR(20)                        
DECLARE @LASTCHAR   VARCHAR(10)                         
DECLARE @FIRSTCHAR   VARCHAR(10)                         
                        
--Added by Sibin on 19 Jan 09 for Itrack issue 5304               
DECLARE @TEMP_VIOLATION_TYPE INT                 
DECLARE @TEMP_TO_LOB_ID INT                
DECLARE @TEMP_FROM_LOB_ID INT                                     
DECLARE @TEMP_VIOLATION_ID INT                 
DECLARE @TEMP_STATE_ID INT                 
                                                                              
SELECT @TEMP_STATE_ID=STATE_ID FROM APP_LIST WHERE CUSTOMER_ID=@TO_CUSTOMER_ID AND APP_ID=@TO_APP_ID AND APP_VERSION_ID=@TO_APP_VERSION_ID                    
                                                    
SELECT @TEMP_TO_LOB_ID=APP_LOB FROM APP_LIST WHERE CUSTOMER_ID=@TO_CUSTOMER_ID AND APP_ID=@TO_APP_ID AND APP_VERSION_ID=@TO_APP_VERSION_ID                  
                
SELECT @TEMP_FROM_LOB_ID=APP_LOB FROM APP_LIST WHERE CUSTOMER_ID=@FROM_CUSTOMER_ID AND APP_ID=@FROM_APP_ID AND APP_VERSION_ID=@FROM_APP_VERSION_ID            
 --Added till here             
                        
/*                    
THE EARLIER METHOD TO DETERMINE UNIQUE DRIVER_CODE HAS BEEN REMOVED.                      
DUPLICACY IS ALLOWED BUT PREVENTED USING RANDOM NUMBER GENERATOR DRIVER CODE INTRODUCTED NOW                      
SELECT @DRIVERCODE=DRIVER_CODE FROM APP_WATERCRAFT_DRIVER_DETAILS WHERE DRIVER_ID=@FROM_DRIVER_ID                         
                        
WHILE (EXISTS (SELECT DRIVER_CODE FROM APP_WATERCRAFT_DRIVER_DETAILS WHERE DRIVER_CODE=@DRIVERCODE))                        
BEGIN                        
  SET @LASTCHAR=RIGHT(@DRIVERCODE,1)                         
 SET @ASCII = ASCII(@LASTCHAR)                        
 -- IF THE LAST CHARACTER IS INT THEN INCREMENT & REPLACE THE LAST CHAR.                           
 IF (@ASCII >= 48 AND @ASCII <= 57)--INT                        
 BEGIN                        
  SET @INCNUM=CONVERT(INT,@LASTCHAR) + 1                        
  SET @LEN=LEN(@DRIVERCODE)                        
  SET @CODE=SUBSTRING(@DRIVERCODE,0,@LEN)                        
  SET @DRIVERCODE=@CODE+CONVERT(VARCHAR(2),@INCNUM)                         
 END                         
 ELSE --IF THE LAST CHAR IS CHAR THEN CONCATENATE 1 AT THE END.                        
 BEGIN                        
  SET @DRIVERCODE=@DRIVERCODE +'1'                        
 END                        
END                        
*/                      
                        
                      
IF (@TEMP_FROM_LOB_ID = @TEMP_TO_LOB_ID)                
BEGIN             
  SET @DRIVERCODE=CONVERT(INT,RAND()*50)            
  SELECT @FIRSTCHAR=DRIVER_FNAME, @LASTCHAR=DRIVER_LNAME  FROM APP_WATERCRAFT_DRIVER_DETAILS                     
  WHERE  CUSTOMER_ID=@FROM_CUSTOMER_ID                          
  AND    APP_ID=@FROM_APP_ID                          
  AND    APP_VERSION_ID=@FROM_APP_VERSION_ID                          
  AND    DRIVER_ID= @FROM_DRIVER_ID                         
          
  SET @DRIVERCODE=(SUBSTRING(@FIRSTCHAR,1,3) + SUBSTRING(@LASTCHAR,1,4) + @DRIVERCODE)                         
                        
                        
  IF(@TO_CUSTOMER_ID=@FROM_CUSTOMER_ID AND @TO_APP_ID=@FROM_APP_ID AND @TO_APP_VERSION_ID=@FROM_APP_VERSION_ID)                  
 BEGIN                  
  INSERT INTO APP_WATERCRAFT_DRIVER_DETAILS                        
  (                        
  CUSTOMER_ID,APP_ID,APP_VERSION_ID,DRIVER_ID,DRIVER_FNAME,DRIVER_MNAME,DRIVER_LNAME,DRIVER_CODE,  
  DRIVER_SUFFIX,DRIVER_ADD1,DRIVER_ADD2,DRIVER_CITY,DRIVER_STATE,DRIVER_ZIP,DRIVER_COUNTRY,  
  DRIVER_DOB,DRIVER_SSN,DRIVER_SEX,DRIVER_DRIV_LIC,DRIVER_LIC_STATE,DRIVER_COST_GAURAD_AUX,IS_ACTIVE,  
  CREATED_BY,CREATED_DATETIME,            
  EXPERIENCE_CREDIT,VEHICLE_ID,PERCENT_DRIVEN,APP_VEHICLE_PRIN_OCC_ID,WAT_SAFETY_COURSE,CERT_COAST_GUARD,           
  MARITAL_STATUS,DATE_ORDERED,MVR_ORDERED,VIOLATIONS,            
  --Added by Sibin on 16 Jan 09 for Itrack Issue 5304                  
  MVR_STATUS,MVR_REMARKS                          
  )                        
  SELECT                        
  @TO_CUSTOMER_ID,@TO_APP_ID,@TO_APP_VERSION_ID,@TO_DRIVER_ID,DRIVER_FNAME,DRIVER_MNAME,DRIVER_LNAME,          
  @DRIVERCODE,DRIVER_SUFFIX,DRIVER_ADD1,DRIVER_ADD2,DRIVER_CITY,DRIVER_STATE,DRIVER_ZIP,DRIVER_COUNTRY,           
  DRIVER_DOB,DRIVER_SSN,DRIVER_SEX,DRIVER_DRIV_LIC,DRIVER_LIC_STATE,DRIVER_COST_GAURAD_AUX,'Y',          
  @CREATED_BY_USER_ID,GETDATE(),EXPERIENCE_CREDIT,VEHICLE_ID,PERCENT_DRIVEN,APP_VEHICLE_PRIN_OCC_ID,           
  WAT_SAFETY_COURSE,CERT_COAST_GUARD,MARITAL_STATUS,DATE_ORDERED,MVR_ORDERED,VIOLATIONS,            
  --Added by Sibin on 16 Jan 09 for Itrack Issue 5304                  
  MVR_STATUS,MVR_REMARKS               
                                 
                          
  FROM    APP_WATERCRAFT_DRIVER_DETAILS                        
  WHERE   CUSTOMER_ID=@FROM_CUSTOMER_ID                        
  AND         APP_ID=@FROM_APP_ID                        
  AND         APP_VERSION_ID=@FROM_APP_VERSION_ID                        
  AND         DRIVER_ID= @FROM_DRIVER_ID                        
                  
               
 --Added 14 sep 2007              
  INSERT INTO APP_OPERATOR_ASSIGNED_BOAT              
  (                                
  CUSTOMER_ID,APP_ID,APP_VERSION_ID,DRIVER_ID,BOAT_ID,APP_VEHICLE_PRIN_OCC_ID                   
  )                                                    
  SELECT               
  @TO_CUSTOMER_ID,@TO_APP_ID,@TO_APP_VERSION_ID,@To_Driver_Id,BOAT_ID,11936--'Principal'              
  FROM    APP_WATERCRAFT_INFO                                 
  WHERE   CUSTOMER_ID=@TO_CUSTOMER_ID                                                    
  AND         APP_ID=@TO_APP_ID                                                    
  AND         APP_VERSION_ID=@TO_APP_VERSION_ID                  
                   
    END                  
 ELSE                  
 BEGIN                   
                   
 INSERT INTO APP_WATERCRAFT_DRIVER_DETAILS                        
 (                        
 CUSTOMER_ID,APP_ID,APP_VERSION_ID,DRIVER_ID,DRIVER_FNAME,DRIVER_MNAME,DRIVER_LNAME,DRIVER_CODE,DRIVER_SUFFIX,           
 DRIVER_ADD1,DRIVER_ADD2,DRIVER_CITY,DRIVER_STATE,DRIVER_ZIP,DRIVER_COUNTRY,DRIVER_DOB,DRIVER_SSN,DRIVER_SEX,           
 DRIVER_DRIV_LIC,DRIVER_LIC_STATE,DRIVER_COST_GAURAD_AUX,IS_ACTIVE,CREATED_BY,CREATED_DATETIME,EXPERIENCE_CREDIT, PERCENT_DRIVEN,WAT_SAFETY_COURSE,CERT_COAST_GUARD,MARITAL_STATUS,DATE_ORDERED,MVR_ORDERED,VIOLATIONS,            
 --Added by Sibin on 16 Jan 09 for Itrack Issue 5304                  
 MVR_STATUS,MVR_REMARKS                       
 )                        
 SELECT                        
 @TO_CUSTOMER_ID,@TO_APP_ID,@TO_APP_VERSION_ID,@TO_DRIVER_ID,DRIVER_FNAME,DRIVER_MNAME,DRIVER_LNAME,@DRIVERCODE,           
 DRIVER_SUFFIX,DRIVER_ADD1,DRIVER_ADD2,DRIVER_CITY,DRIVER_STATE,DRIVER_ZIP,DRIVER_COUNTRY,DRIVER_DOB,DRIVER_SSN,           
 DRIVER_SEX,DRIVER_DRIV_LIC,DRIVER_LIC_STATE,DRIVER_COST_GAURAD_AUX,'Y', @CREATED_BY_USER_ID,GETDATE(),          
 EXPERIENCE_CREDIT,PERCENT_DRIVEN,WAT_SAFETY_COURSE,CERT_COAST_GUARD,MARITAL_STATUS,DATE_ORDERED,MVR_ORDERED,           
 VIOLATIONS,            
 --Added by Sibin on 16 Jan 09 for Itrack Issue 5304                  
 MVR_STATUS,MVR_REMARKS                             
 FROM    APP_WATERCRAFT_DRIVER_DETAILS                        
 WHERE   CUSTOMER_ID=@FROM_CUSTOMER_ID                        
 AND         APP_ID=@FROM_APP_ID                        
 AND         APP_VERSION_ID=@FROM_APP_VERSION_ID                        
 AND         DRIVER_ID= @FROM_DRIVER_ID                        
                   
 INSERT INTO APP_OPERATOR_ASSIGNED_BOAT              
  (                                
  CUSTOMER_ID,APP_ID,APP_VERSION_ID,DRIVER_ID,BOAT_ID,APP_VEHICLE_PRIN_OCC_ID                   
  )                                                    
  SELECT               
  @TO_CUSTOMER_ID,@TO_APP_ID,@TO_APP_VERSION_ID,@To_Driver_Id,BOAT_ID,11936--'Principal'              
  FROM    APP_WATERCRAFT_INFO                                 
  WHERE   CUSTOMER_ID=@TO_CUSTOMER_ID                                                    
  AND         APP_ID=@TO_APP_ID                                                    
  AND         APP_VERSION_ID=@TO_APP_VERSION_ID                  
                         
                   
                   
 END            
                                        
   INSERT INTO APP_WATER_MVR_INFORMATION                   
 (                                    
  APP_WATER_MVR_ID,CUSTOMER_ID,APP_ID, APP_VERSION_ID,VIOLATION_ID,DRIVER_ID,                       
  MVR_AMOUNT, MVR_DEATH,MVR_DATE,IS_ACTIVE,VERIFIED,VIOLATION_TYPE,                  
  CREATED_BY,CREATED_DATETIME,                   
  --Added by Sibin on 16 Jan 09 for Itrack Issue 5304                  
  OCCURENCE_DATE,DETAILS,POINTS_ASSIGNED,ADJUST_VIOLATION_POINTS                         
 )                
   SELECT             
  APP_WATER_MVR_ID,@TO_CUSTOMER_ID,@TO_APP_ID,@TO_APP_VERSION_ID,VIOLATION_ID,@TO_DRIVER_ID,MVR_AMOUNT,MVR_DEATH,  MVR_DATE,IS_ACTIVE,VERIFIED,VIOLATION_TYPE,                  
   CREATED_BY,GETDATE(),                   
  --Added by Sibin on 16 Jan 09 for Itrack Issue 5304                  
  OCCURENCE_DATE,DETAILS,POINTS_ASSIGNED,ADJUST_VIOLATION_POINTS                   
                 
  FROM  APP_WATER_MVR_INFORMATION                
  WHERE CUSTOMER_ID=@FROM_CUSTOMER_ID                 
   AND APP_ID=@FROM_APP_ID                 
   AND APP_VERSION_ID=@FROM_APP_VERSION_ID                 
   AND DRIVER_ID=@FROM_DRIVER_ID                                                       
                                  
                                    
                                  
 END                                           
ELSE                                               
                 
  IF (@TEMP_FROM_LOB_ID=2 OR @TEMP_FROM_LOB_ID=3)                
  BEGIN                            
   SET @DRIVERCODE=CONVERT(INT,RAND()*50)          
   SELECT @FIRSTCHAR=DRIVER_FNAME, @LASTCHAR=DRIVER_LNAME  FROM APP_WATERCRAFT_DRIVER_DETAILS                                                   
   WHERE   CUSTOMER_ID=@FROM_CUSTOMER_ID                                                        
    AND         APP_ID=@FROM_APP_ID                                       
    AND         APP_VERSION_ID=@FROM_APP_VERSION_ID                                              
    AND         DRIVER_ID= @FROM_DRIVER_ID                                                       
 SET @DRIVERCODE=(SUBSTRING(@FIRSTCHAR,1,3) + SUBSTRING(@LASTCHAR,1,4) + @DRIVERCODE)                            
                         
 --SELECT @DRIVER_TYPE=ISNULL(DRIVER_DRIV_TYPE,0) FROM APP_DRIVER_DETAILS                         
 --  WHERE CUSTOMER_ID=@TO_CUSTOMER_ID AND APP_ID=@TO_APP_ID AND APP_VERSION_ID=@TO_APP_VERSION_ID                        
                                                               
                                                 
 IF (@TO_CUSTOMER_ID=@FROM_CUSTOMER_ID and @TO_APP_ID=@FROM_APP_ID and @TO_APP_VERSION_ID=@FROM_APP_VERSION_ID)                                                
 BEGIN                                               
 INSERT INTO APP_WATERCRAFT_DRIVER_DETAILS                                                              
 (            
  CUSTOMER_ID,APP_ID,APP_VERSION_ID,DRIVER_ID,DRIVER_FNAME,DRIVER_MNAME,DRIVER_LNAME,DRIVER_CODE,DRIVER_SUFFIX,           
  DRIVER_ADD1,DRIVER_ADD2,DRIVER_CITY,DRIVER_STATE,DRIVER_ZIP,DRIVER_COUNTRY,DRIVER_DOB,DRIVER_SSN,DRIVER_SEX,           
  MARITAL_STATUS,DRIVER_DRIV_LIC,DRIVER_LIC_STATE,DRIVER_DRIV_TYPE,IS_ACTIVE,CREATED_BY,CREATED_DATETIME,          
  DATE_ORDERED,MVR_ORDERED,VIOLATIONS,            
  --Added by Sibin on 16 Jan 09 for Itrack Issue 5304                  
  MVR_STATUS,MVR_REMARKS                          
 )                
            
 SELECT                 
  @TO_CUSTOMER_ID,@TO_APP_ID,@TO_APP_VERSION_ID,@TO_DRIVER_ID,DRIVER_FNAME,DRIVER_MNAME,DRIVER_LNAME,DRIVER_CODE,DRIVER_SUFFIX,  DRIVER_ADD1,DRIVER_ADD2,DRIVER_CITY,DRIVER_STATE,DRIVER_ZIP,DRIVER_COUNTRY,DRIVER_DOB,DRIVER_SSN,            
 DRIVER_SEX,DRIVER_MART_STAT,DRIVER_DRIV_LIC,DRIVER_LIC_STATE,DRIVER_DRIV_TYPE,IS_ACTIVE,CREATED_BY,              
 CREATED_DATETIME,DATE_ORDERED,MVR_ORDERED,VIOLATIONS,                      
 --Added by Sibin on 16 Jan 09 for Itrack Issue 5304                      
 MVR_STATUS,MVR_REMARKS           
                             
 FROM    APP_DRIVER_DETAILS                                                               
 WHERE   CUSTOMER_ID=@FROM_CUSTOMER_ID                                                              
 AND     APP_ID=@FROM_APP_ID                                                              
 AND     APP_VERSION_ID=@FROM_APP_VERSION_ID                                                              
 AND     DRIVER_ID= @FROM_DRIVER_ID                                   
                                           
                                 
 INSERT INTO APP_DRIVER_ASSIGNED_VEHICLE                                                              
 (                                                      
 CUSTOMER_ID,APP_ID,APP_VERSION_ID,DRIVER_ID,VEHICLE_ID,APP_VEHICLE_PRIN_OCC_ID                             
 )                                                      
 SELECT                                                         
 @TO_CUSTOMER_ID,@TO_APP_ID,@TO_APP_VERSION_ID,@To_Driver_Id,VEHICLE_ID,11931--'Not Rated'                                     
 FROM    APP_VEHICLES                          
 WHERE   CUSTOMER_ID=@TO_CUSTOMER_ID                                                              
 AND         APP_ID=@TO_APP_ID                                                              
 AND         APP_VERSION_ID=@TO_APP_VERSION_ID                             
 ---             
                                 
 END                                           
 ELSE                                           
 BEGIN                                              
 INSERT INTO APP_WATERCRAFT_DRIVER_DETAILS                                                              
 (            
  CUSTOMER_ID,APP_ID,APP_VERSION_ID,DRIVER_ID,DRIVER_FNAME,DRIVER_MNAME,DRIVER_LNAME,DRIVER_CODE,DRIVER_SUFFIX,           
  DRIVER_ADD1,DRIVER_ADD2,DRIVER_CITY,DRIVER_STATE,DRIVER_ZIP,DRIVER_COUNTRY,DRIVER_DOB,DRIVER_SSN,DRIVER_SEX,           
  MARITAL_STATUS,DRIVER_DRIV_LIC,DRIVER_LIC_STATE,DRIVER_DRIV_TYPE,IS_ACTIVE,CREATED_BY,CREATED_DATETIME,          
  DATE_ORDERED,MVR_ORDERED,VIOLATIONS,            
  --Added by Sibin on 16 Jan 09 for Itrack Issue 5304                  
  MVR_STATUS,MVR_REMARKS                          
 )                
            
 SELECT                 
  @TO_CUSTOMER_ID,@TO_APP_ID,@TO_APP_VERSION_ID,@TO_DRIVER_ID,DRIVER_FNAME,DRIVER_MNAME,DRIVER_LNAME,DRIVER_CODE,DRIVER_SUFFIX,  DRIVER_ADD1,DRIVER_ADD2,DRIVER_CITY,DRIVER_STATE,DRIVER_ZIP,DRIVER_COUNTRY,DRIVER_DOB,DRIVER_SSN,            
 DRIVER_SEX,DRIVER_MART_STAT,DRIVER_DRIV_LIC,DRIVER_LIC_STATE,DRIVER_DRIV_TYPE,IS_ACTIVE,CREATED_BY,              
 CREATED_DATETIME,DATE_ORDERED,MVR_ORDERED,VIOLATIONS,                      
 --Added by Sibin on 16 Jan 09 for Itrack Issue 5304                      
 MVR_STATUS,MVR_REMARKS           
                             
 FROM    APP_DRIVER_DETAILS                                                               
 WHERE   CUSTOMER_ID=@FROM_CUSTOMER_ID                                                              
 AND     APP_ID=@FROM_APP_ID                                                              
 AND     APP_VERSION_ID=@FROM_APP_VERSION_ID                                                              
 AND     DRIVER_ID= @FROM_DRIVER_ID           
                         
 --If the driver copied belongs to application different from current, add default values at driver-vehicle assign table                        
                         
 INSERT INTO APP_DRIVER_ASSIGNED_VEHICLE                                                              
 (                                              
 CUSTOMER_ID,APP_ID,APP_VERSION_ID,DRIVER_ID,VEHICLE_ID,APP_VEHICLE_PRIN_OCC_ID                             
 )                                                              
 SELECT                                                         
 @TO_CUSTOMER_ID,@TO_APP_ID,@TO_APP_VERSION_ID,@To_Driver_Id,VEHICLE_ID,11931--'Not Rated'                                     
 FROM    APP_VEHICLES                                                              
 WHERE   CUSTOMER_ID=@TO_CUSTOMER_ID                                                              
 AND         APP_ID=@TO_APP_ID                                                              
 AND         APP_VERSION_ID=@TO_APP_VERSION_ID                                         
 --AND         DRIVER_ID= @FROM_DRIVER_ID                                
                         
   END             
            
   DECLARE @MVR_ID int,@OLD_VIOLATION_ID int,@VIOLATION_TYPE INT                
   DECLARE CurMVR_INSERT CURSOR                
   FOR SELECT APP_MVR_ID,VIOLATION_ID,VIOLATION_TYPE FROM APP_MVR_INFORMATION                 
   WHERE CUSTOMER_ID=@FROM_CUSTOMER_ID AND APP_ID=@FROM_APP_ID AND APP_VERSION_ID=@FROM_APP_VERSION_ID AND DRIVER_ID=@FROM_DRIVER_ID                   
                 
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
                  
  INSERT INTO APP_WATER_MVR_INFORMATION                           
  (                                            
  APP_WATER_MVR_ID,CUSTOMER_ID,APP_ID, APP_VERSION_ID,VIOLATION_ID,DRIVER_ID,                                                  
  MVR_AMOUNT, MVR_DEATH,MVR_DATE,IS_ACTIVE,VERIFIED,VIOLATION_TYPE,                      
  --Added by Sibin on 16 Jan 09 for Itrack Issue 5304                      
  OCCURENCE_DATE,POINTS_ASSIGNED,ADJUST_VIOLATION_POINTS,DETAILS                      
  --,CREATED_BY,CREATED_DATETIME                          
  )                        
  SELECT                                                
  APP_MVR_ID,@TO_CUSTOMER_ID,@TO_APP_ID,@TO_APP_VERSION_ID,0,@TO_DRIVER_ID,MVR_AMOUNT,MVR_DEATH,    
  MVR_DATE,IS_ACTIVE,VERIFIED,@TEMP_VIOLATION_TYPE,                       
  --Added by Sibin on 16 Jan 09 for Itrack Issue 5304                      
  OCCURENCE_DATE,POINTS_ASSIGNED,ADJUST_VIOLATION_POINTS,DETAILS                       
  --,CREATED_BY,GETDATE()                        
  FROM  APP_MVR_INFORMATION                   
                  
  WHERE CUSTOMER_ID=@FROM_CUSTOMER_ID                   
  AND APP_ID=@FROM_APP_ID                   
  AND APP_VERSION_ID=@FROM_APP_VERSION_ID                   
  AND DRIVER_ID=@FROM_DRIVER_ID                                                                   
  AND APP_MVR_ID  = @MVR_ID                
                     
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
                  
  INSERT INTO APP_WATER_MVR_INFORMATION                           
  (                                            
  APP_WATER_MVR_ID,CUSTOMER_ID,APP_ID, APP_VERSION_ID,VIOLATION_ID,DRIVER_ID,                                                  
  MVR_AMOUNT, MVR_DEATH,MVR_DATE,IS_ACTIVE,VERIFIED,VIOLATION_TYPE,                      
  --Added by Sibin on 16 Jan 09 for Itrack Issue 5304                      
  OCCURENCE_DATE,POINTS_ASSIGNED,ADJUST_VIOLATION_POINTS,DETAILS                      
  --,CREATED_BY,CREATED_DATETIME                          
  )                        
  SELECT                                                
  APP_MVR_ID,@TO_CUSTOMER_ID,@TO_APP_ID,@TO_APP_VERSION_ID,@TEMP_VIOLATION_ID,@TO_DRIVER_ID,                           MVR_AMOUNT,MVR_DEATH,MVR_DATE,IS_ACTIVE,VERIFIED,@TEMP_VIOLATION_TYPE,                       
  --Added by Sibin on 16 Jan 09 for Itrack Issue 5304                      
  OCCURENCE_DATE,POINTS_ASSIGNED,ADJUST_VIOLATION_POINTS,DETAILS                       
  --,CREATED_BY,GETDATE()                        
  FROM  APP_MVR_INFORMATION                   
                  
  WHERE CUSTOMER_ID=@FROM_CUSTOMER_ID                   
  AND APP_ID=@FROM_APP_ID                   
  AND APP_VERSION_ID=@FROM_APP_VERSION_ID                   
  AND DRIVER_ID=@FROM_DRIVER_ID                                                                   
  AND APP_MVR_ID  = @MVR_ID                
                     
  FETCH NEXT FROM CurMVR_INSERT INTO @MVR_ID,@OLD_VIOLATION_ID,@VIOLATION_TYPE                     
                
  END                
   CLOSE CurMVR_INSERT                
   DEALLOCATE CurMVR_INSERT                
   END     
  END                
END      
  
--go  
--  
--exec Proc_InsertExistingWatercraftDriver 1552,11,1,1552,12,1,4,334  
--rollback tran      
--              
GO

