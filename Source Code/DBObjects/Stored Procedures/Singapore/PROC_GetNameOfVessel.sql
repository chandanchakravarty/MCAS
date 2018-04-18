                
Alter PROC [dbo].[PROC_GetNameOfVessel]          
                          
AS                                
                                
BEGIN           
SELECT VESSEL_ID,VESSEL_NAME FROM MNT_VESSEL_MASTER  where IS_ACTIVE = 'Y'                 
END 