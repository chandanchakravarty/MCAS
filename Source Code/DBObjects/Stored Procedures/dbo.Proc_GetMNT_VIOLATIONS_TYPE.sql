IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetMNT_VIOLATIONS_TYPE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetMNT_VIOLATIONS_TYPE]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                            
Proc Name        :            dbo.Proc_GetMNT_VIOLATIONS_TYPE                             
Created by         :           Sumit Chhabra                            
Date                :           01/03/2005                            
Purpose           :           Get the violation_types (parent) information for mnt_violations                
Revison History  :                            
modified by  :Pravesh Chandel  
Dated 14 dec 2006  
Used In             :           Wolverine                            
------------------------------------------------------------                            
Date     Review By          Comments                            
drop proc dbo.Proc_GetMNT_VIOLATIONS_TYPE 1250,8,1  
------   ------------       -------------------------*/                            
create PROC dbo.Proc_GetMNT_VIOLATIONS_TYPE                  
(                                   
	@CUSTOMER_ID INT,              
	@APP_ID INT,              
	@APP_VERSION_ID INT        
)                           
AS                            
BEGIN                            
DECLARE @STATE_ID int              
DECLARE @APP_LOB INT        
SELECT  @STATE_ID=STATE_ID,@APP_LOB=APP_LOB FROM APP_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID              
  
--For Homeowner-Watercraft or Umbrella-Watercraft display the records for Watercraft LOB      
--1 - HOMEOWNER     5 - UMBRELLA   4--WATERCRAFT  
IF(@APP_LOB=1 or @APP_LOB=5)      
 SET @APP_LOB=4      
     
              
 SELECT VIOLATION_ID, VIOLATION_DES                             
 FROM  MNT_VIOLATIONS  WHERE (STATE=@STATE_ID) AND (LOB=@APP_LOB) 
AND VIOLATION_PARENT=0 AND VIOLATION_DES != '--ADJUST POINTS' order by violation_des          
END                            
      
    
  
  
  





GO

