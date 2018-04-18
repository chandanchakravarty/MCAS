IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchMotorGreaterAmount]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchMotorGreaterAmount]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                
Proc Name       : dbo.Proc_FetchMotorGreaterAmount               
Created by      : Swastika      
Date            : 28th Apr'06      
Purpose         : To Find the Motor Which Has Amount Greater Than 40000      
------------------------------------------------------------                
Date     Review By          Comments                
------   ------------       -------------------------*/      
--drop proc  Proc_FetchMotorGreaterAmount       
CREATE PROC dbo.Proc_FetchMotorGreaterAmount                
(                
@CUSTOMER_ID    int,                
@APP_ID          int,                
@APP_VERSION_ID  int                
)                
AS                
BEGIN                
   
  SELECT * FROM APP_VEHICLES WHERE             
	  CUSTOMER_ID  =@CUSTOMER_ID AND                
	  APP_ID   =@APP_ID AND                
	  APP_VERSION_ID =@APP_VERSION_ID AND                
	  AMOUNT > 40000 and IS_ACTIVE ='Y'  
END    


GO

