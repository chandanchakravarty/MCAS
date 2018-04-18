ALTER PROC [dbo].[PROC_GetDataForVesselatRisk]          
@VesselID int                        
AS                                
                                
BEGIN           
SELECT VESSEL_ID,VESSEL_NAME,FLAG,YEAR_BUILT  FROM MNT_VESSEL_MASTER  where VESSEL_ID = @VesselID and IS_ACTIVE = 'Y'          
END 