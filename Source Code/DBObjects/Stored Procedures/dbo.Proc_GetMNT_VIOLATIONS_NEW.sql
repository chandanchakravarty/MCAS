IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetMNT_VIOLATIONS_NEW]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetMNT_VIOLATIONS_NEW]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                              
Proc Name        :            dbo.Proc_GetMNT_VIOLATIONS_NEW                               
Created by         :           Sumit Chhabra                              
Date                :           01/03/2005                              
Purpose           :           Get the violation_types  information for mnt_violations                  
Revison History  :                              
Used In             :           Wolverine                              
------------------------------------------------------------                              
Date     Review By          Comments               
drop proc      dbo.Proc_GetMNT_VIOLATIONS_NEW  
Proc_GetMNT_VIOLATIONS_NEW 1081,136,1,1,'PPA'        
------   ------------       -------------------------*/                              
create PROC dbo.Proc_GetMNT_VIOLATIONS_NEW                    
(                                     
@CUSTOMER_ID INT,                
@APP_ID INT,                
@APP_VERSION_ID INT,          
@VIOLATION_ID INT,        
@CALLED_FROM varchar(10)          
)                             
AS                              
BEGIN                              
DECLARE @STATE_ID int                
DECLARE @APP_LOB INT          
          
IF(@CALLED_FROM IS NULL OR @CALLED_FROM='')        
 SELECT  @STATE_ID=STATE_ID,@APP_LOB=APP_LOB FROM APP_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID                
else if(upper(@CALLED_FROM)='POLICY')        
  SELECT  @STATE_ID=STATE_ID,@APP_LOB=POLICY_LOB FROM POL_CUSTOMER_POLICY_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@APP_ID AND POLICY_VERSION_ID=@APP_VERSION_ID                      
      
--When the state belongs to Homeowner-Watercraft or Umbrella-Watercraft then show the records for Watercraft LOB      
--1=HOMEOWNER          4=WATERCRAFT      5 - Umbrella
IF(@APP_LOB=1 or @APP_LOB=5)      
 SET @APP_LOB=4      
 
if (@VIOLATION_ID !=13220)  
SELECT VIOLATION_ID,                               
-- (isnull(VIOLATION_DES,'') + ' (' + CAST(isnull(MVR_POINTS,'') AS VARCHAR) +  '/' +  CAST(isnull(SD_POINTS,'') AS VARCHAR)  +  ')' )AS VIOLATION_DES                  
 (isnull(VIOLATION_DES,'') + ' (' + CAST(isnull(MVR_POINTS,'') AS VARCHAR)   
-- +  '/' +  CAST(isnull(SD_POINTS,'') AS VARCHAR)  
  +  ')' )AS VIOLATION_DES                  
 FROM  MNT_VIOLATIONS  WHERE STATE=@STATE_ID AND LOB=@APP_LOB AND VIOLATION_PARENT=(          
     SELECT VIOLATION_GROUP FROM MNT_VIOLATIONS WHERE VIOLATION_ID=@VIOLATION_ID)          
	ORDER BY VIOLATION_DES            
else

SELECT MVR.ID as VIOLATION_ID,                               
 (isnull(MVR_DESCRIPTION,'') + ' (' + CAST(isnull(MNT.MVR_POINTS,'') AS VARCHAR)   
  +  ')' )AS VIOLATION_DES                  
 FROM  MVR_EXCEPTION MVR, MNT_VIOLATIONS MNT WHERE  MNT.VIOLATION_PARENT=0          
     and mnt.VIOLATION_GROUP=14    and  
     CUSTOMER_ID =@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID      
	ORDER BY MVR_DESCRIPTION            
END                              
            
  


GO

