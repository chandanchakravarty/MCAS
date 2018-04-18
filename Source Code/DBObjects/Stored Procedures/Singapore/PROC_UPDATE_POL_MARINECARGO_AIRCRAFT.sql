IF  EXISTS (SELECT * FROM SYS.OBJECTS WHERE OBJECT_ID = OBJECT_ID(N'[DBO].[PROC_UPDATE_POL_MARINECARGO_AIRCRAFT]') AND TYPE IN (N'P', N'PC'))
DROP PROCEDURE DBO.PROC_UPDATE_POL_MARINECARGO_AIRCRAFT
GO
SET ANSI_NULLS ON 
GO
SET QUOTED_IDENTIFIER ON
GO 
 /*----------------------------------------------------------                      
 Proc Name       : dbo.[PROC_UPDATE_POL_MARINECARGO_AIRCRAFT]          
 Created by      : Avijit Goswami  
 Date            : 21/03/2012  
 Purpose         :   
 Revison History :                      
 Used In   : Ebix Advantage                      
------------------------------------------------------------                      
Date     Review By          Comments                      
------   ------------       -------------------------      
DROP PROC PROC_UPDATE_POL_MARINECARGO_AIRCRAFT*/      
Create Proc [dbo].[PROC_UPDATE_POL_MARINECARGO_AIRCRAFT]       
      (      
      @AIRCRAFT_ID VARCHAR(10),  
      @AIRCRAFT_NUMBER VARCHAR(10),  
      @AIRLINE VARCHAR(40),  
      @AIRCRAFT_FROM VARCHAR(40),  
      @AIRCRAFT_TO VARCHAR(40),  
      @AIRWAY_BILL VARCHAR(40),
      @IS_ACTIVE CHAR,
	  @MODIFIED_BY VARCHAR(4),
	  @LAST_UPDATED_DATETIME DATETIME
      )AS      
     BEGIN  
     UPDATE POL_MARINECARGO_AIRCRAFT SET 
     
     AIRCRAFT_NUMBER=@AIRCRAFT_NUMBER,  
     AIRLINE=@AIRLINE,  
     AIRCRAFT_FROM=@AIRCRAFT_FROM,  
     AIRCRAFT_TO=@AIRCRAFT_TO,  
     AIRWAY_BILL=@AIRWAY_BILL,
     MODIFIED_BY=@MODIFIED_BY,
     LAST_UPDATED_DATETIME=@LAST_UPDATED_DATETIME    
     WHERE AIRCRAFT_ID = @AIRCRAFT_ID  
    IF(@@ERROR<>0)      
    RETURN -1      
       
       
END 