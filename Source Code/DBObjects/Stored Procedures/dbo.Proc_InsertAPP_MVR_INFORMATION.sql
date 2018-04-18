IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertAPP_MVR_INFORMATION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertAPP_MVR_INFORMATION]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

  
/*----------------------------------------------------------                                                    
                                                    
Proc Name       : Proc_InsertAPP_MVR_INFORMATION                                                    
Created by      : Sumit Chhabra                                                    
Date            : 9/27/2005                                                    
Purpose         :Insert of Driver MVR Information                                                    
Revison History :              
Modified by  : Praveen kasana  
Date   : 21 feb 2008   
Purpose   : @OCCURENCE_DATE  check.                               
drop proc dbo.Proc_InsertAPP_MVR_INFORMATION                          
Used In                   : Wolverine                                                    
------------------------------------------------------------                                                    
Date     Review By          Comments                                                    
------   ------------       -------------------------                
drop proc dbo.Proc_InsertAPP_MVR_INFORMATION                          
*/                                                    
CREATE PROC dbo.Proc_InsertAPP_MVR_INFORMATION                                                    
(                                                    
 @APP_MVR_ID  int OUTPUT,                                                    
 @CUSTOMER_ID  int,                                                    
 @APP_ID  int,                                                    
 @APP_VERSION_ID int,                                                    
 @VIOLATION_ID  int,                                                    
 @DRIVER_ID  int,                                                    
 @MVR_AMOUNT  decimal(20,0),                                          
 @MVR_DEATH  nvarchar(2),                                                    
 @MVR_DATE  datetime,                                                
 @IS_ACTIVE  nvarchar(2),                                  
 @CALLED_FROM VARCHAR(10)=null,                              
 @VERIFIED smallint,                          
 @VIOLATION_TYPE int ,      
 @OCCURENCE_DATE DateTime,      
 @DETAILS varchar(500),    
 @POINTS_ASSIGNED int,    
 @ADJUST_VIOLATION_POINTS int,            
 @RET_VIOLATION_TYPE int  =null  output,          
 @RET_VERIFIED smallint = null output                                                   
            
)                                                    
AS                                                    
BEGIN                                                    
 /*GENERATING THE NEW MVR ID*/                                                    
 SELECT @APP_MVR_ID=ISNULL(MAX(APP_MVR_ID),0)+1 FROM APP_MVR_INFORMATION                                    
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID AND DRIVER_ID=@DRIVER_ID                                    
--ADDED BY PRAVESH                
DECLARE @TEMP_VIOLATION_TYPE INT                
DECLARE @TEMP_STATE_ID INT                
DECLARE @TEMP_LOB_ID INT                
                
SELECT @TEMP_STATE_ID=STATE_ID,@TEMP_LOB_ID=APP_LOB FROM APP_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID                
set  @TEMP_VIOLATION_TYPE=@VIOLATION_TYPE                
if  (@VIOLATION_TYPE = 0 or @VIOLATION_TYPE is null)                                      
 begin                
 if (@VIOLATION_ID>25000)                
 set @TEMP_VIOLATION_TYPE=13220;                
 else                
 SELECT  @TEMP_VIOLATION_TYPE=VIOLATION_ID FROM MNT_VIOLATIONS WHERE VIOLATION_GROUP=(SELECT VIOLATION_PARENT FROM MNT_VIOLATIONS WHERE VIOLATION_ID=@VIOLATION_ID) AND  STATE=@TEMP_STATE_ID AND LOB=@TEMP_LOB_ID                
 end                     
-----END HERE                  
set @RET_VIOLATION_TYPE= @TEMP_VIOLATION_TYPE  
SET @RET_VERIFIED = 0;                
     
IF NOT EXISTS(SELECT APP_MVR_ID FROM  APP_MVR_INFORMATION WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID =@APP_ID AND APP_VERSION_ID =@APP_VERSION_ID  
 AND DRIVER_ID=@DRIVER_ID AND VIOLATION_ID =@VIOLATION_ID AND VIOLATION_TYPE=@TEMP_VIOLATION_TYPE and OCCURENCE_DATE = @OCCURENCE_DATE)               
  
    
 BEGIN   
              
 --ADDED BY SWARUP              
 --Commented by praveen kumar(12-02-2009):itrack 5434 as discussed with parvesh sir        
-- IF (@VERIFIED = 0)        
-- BEGIN         
--  SET @VERIFIED = 1;          
-- END    
 --End comment praveen kumar            
 --END                
               
    INSERT INTO APP_MVR_INFORMATION   (                                                    
     APP_MVR_ID, CUSTOMER_ID, APP_ID, APP_VERSION_ID, VIOLATION_ID,DRIVER_ID,                                                    
     MVR_AMOUNT, MVR_DEATH, MVR_DATE, IS_ACTIVE,VERIFIED, VIOLATION_TYPE, OCCURENCE_DATE, DETAILS,    
 POINTS_ASSIGNED, ADJUST_VIOLATION_POINTS )                                                    
    VALUES                                                    
     (@APP_MVR_ID,@CUSTOMER_ID,@APP_ID,@APP_VERSION_ID,@VIOLATION_ID,@DRIVER_ID,                                            
     @MVR_AMOUNT,@MVR_DEATH,@MVR_DATE,@IS_ACTIVE,@VERIFIED,                
 -- @VIOLATION_TYPE                
  @TEMP_VIOLATION_TYPE, @OCCURENCE_DATE, @DETAILS, @POINTS_ASSIGNED, @ADJUST_VIOLATION_POINTS                
  )                                          
                          
                       
--  IF (UPPER(@CALLED_FROM)='PPA')                                  
--  EXEC PROC_SETVEHICLECLASSRULE @CUSTOMER_ID,@APP_ID,@APP_VERSION_ID,@DRIVER_ID                                
                            
  IF (UPPER(@CALLED_FROM)='MOT')                                  
   EXEC Proc_SetPreferredRiskDiscount @CUSTOMER_ID,@APP_ID,@APP_VERSION_ID,@DRIVER_ID                                
                
 --update "MVR ordered" and "Date MVR ordered" fields on Driver tab                 
 Update APP_DRIVER_DETAILS set DATE_ORDERED = getdate(),MVR_ORDERED=10963   --yes                
 where CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and aPP_VERSION_ID=@APP_VERSION_ID and DRIVER_ID=@DRIVER_ID                
                
                
 SET @RET_VERIFIED = 1;                
 END                           
                                             
END                                            
                                  
                                        
                                        
                                      
                
                                    
                                  
                                
                              
                            
                          
                        
                      
                    
                  
                
                
                
                
                
                
                
              
            
          
        
      
    
  
  
  
  
  
GO

