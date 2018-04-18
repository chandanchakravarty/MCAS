  
  
    /*----------------------------------------------------------                                  
Proc Name        :            dbo.Proc_GetMNT_VIOLATIONS_TYPE                                   
Created by         :           Sumit Chhabra                                  
Date                :           01/03/2005                                  
Purpose           :           Get the violation_types (parent) information for mnt_violations                      
Revison History  :                                  
Used In             :           Wolverine                                  
------------------------------------------------------------                                  
Date     Review By          Comments       
drop PROC DBO.Proc_GetMNT_VIOLATIONS_TYPE_POL 1250,5,1                              
------   ------------       -------------------------*/                                  
ALTER PROC [dbo].[Proc_GetMNT_VIOLATIONS_TYPE_POL]                        
(                                         
@CUSTOMER_ID INT,                    
@POLICY_ID INT,                    
@POLICY_VERSION_ID INT              
              
)                                 
AS                                  
BEGIN                                  
DECLARE @STATE_ID int                    
DECLARE @POLICY_LOB INT              
SELECT  @STATE_ID=STATE_ID,@POLICY_LOB=POLICY_LOB FROM POL_CUSTOMER_POLICY_LIST             
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                    
          
--For Homeowner-Watercraft/Umbrella-Watercraft display the records for Watercraft LOB          
--1- Homeowner, 4-Watercraft, 5-Umbrella      
IF(@POLICY_LOB=1 or @POLICY_LOB=5)          
 SET @POLICY_LOB=4          
IF(@POLICY_LOB = 38)
BEGIN
SET @POLICY_LOB = 2
SET @STATE_ID = 14
eND
                    
 SELECT VIOLATION_ID, VIOLATION_DES                                   
 FROM  MNT_VIOLATIONS  WHERE STATE=@STATE_ID AND LOB=@POLICY_LOB     
AND VIOLATION_PARENT=0 AND VIOLATION_DES != '--ADJUST POINTS' ORDER BY VIOLATION_DES                
END 


--SELECT * FROM MNT_VIOLATIONS  WHERE LOB = 2

--select * from MNT_LOB_MASTER                               
              
          
          
        
      
    
    
    