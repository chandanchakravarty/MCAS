IF  EXISTS (SELECT * FROM SYS.OBJECTS WHERE OBJECT_ID = OBJECT_ID(N'[DBO].[PROC_GET_POL_MARINECARGO_AIRCRAFT]') AND TYPE IN (N'P', N'PC'))
DROP PROCEDURE DBO.PROC_GET_POL_MARINECARGO_AIRCRAFT
GO
SET ANSI_NULLS ON 
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------      
--Proc Name          : dbo.[Proc_Get_POL_MARINECARGO_AIRCRAFT]      
--Created by         : Avijit Goswami           
--Date               : 21-Mar-2012             
--------------------------------------------------------      
--Date     Review By          Comments              
------   ------------       -------------------------*/             
-- DROP PROC DBO.[PROC_GET_POL_MARINECARGO_AIRCRAFT]
CREATE  PROCEDURE [DBO].[PROC_GET_POL_MARINECARGO_AIRCRAFT]            
 (  
@AIRCRAFT_ID VARCHAR(10)  
)           
AS             
BEGIN            
SELECT   
AIRCRAFT_ID,  
AIRCRAFT_NUMBER,  
AIRLINE,  
AIRCRAFT_FROM,  
AIRCRAFT_TO,  
AIRWAY_BILL
      
 FROM POL_MARINECARGO_AIRCRAFT  WHERE AIRCRAFT_ID=@AIRCRAFT_ID     
END  
            
  