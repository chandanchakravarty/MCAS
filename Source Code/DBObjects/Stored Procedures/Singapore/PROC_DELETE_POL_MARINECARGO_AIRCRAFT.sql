IF  EXISTS (SELECT * FROM SYS.OBJECTS WHERE OBJECT_ID = OBJECT_ID(N'[DBO].[PROC_DELETE_POL_MARINECARGO_AIRCRAFT]') AND TYPE IN (N'P', N'PC'))
DROP PROCEDURE DBO.PROC_DELETE_POL_MARINECARGO_AIRCRAFT
GO
SET ANSI_NULLS ON 
GO
SET QUOTED_IDENTIFIER ON
GO 
 /*----------------------------------------------------------                      
 Proc Name       : dbo.[PROC_DELETE_POL_MARINECARGO_AIRCRAFT]          
 Created by      : Avijit Goswami  
 Date            : 22/03/2012  
 Purpose         :   
 Revison History :                      
 Used In   : Ebix Advantage                      
------------------------------------------------------------                      
Date     Review By          Comments                      
------   ------------       -------------------------      
DROP PROC PROC_DELETE_POL_MARINECARGO_AIRCRAFT*/      
CREATE PROC [DBO].[PROC_DELETE_POL_MARINECARGO_AIRCRAFT]       
      (      
      @AIRCRAFT_ID VARCHAR(10)
      )AS      
     BEGIN  
     DELETE FROM POL_MARINECARGO_AIRCRAFT
         
     WHERE AIRCRAFT_ID = @AIRCRAFT_ID  
    IF(@@ERROR<>0)      
    RETURN -1 
END 