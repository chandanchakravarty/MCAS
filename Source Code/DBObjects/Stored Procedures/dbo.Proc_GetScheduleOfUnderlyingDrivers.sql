IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetScheduleOfUnderlyingDrivers]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetScheduleOfUnderlyingDrivers]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Proc_GetScheduleOfUnderlyingDrivers]             
(            
 @CUSTOMERID INT = null       
 )            
AS            
BEGIN            
 SELECT  D.CUSTOMER_ID,D.DRIVER_CODE,(D.DRIVER_FNAME + ' ' + D.DRIVER_MNAME + ' ' + D.DRIVER_LNAME ) AS DRIVER_NAME,    
    A.APP_NUMBER,A.APP_ID,A.APP_VERSION_ID,D.DRIVER_ID,P.POLICY_LOB  
 FROM APP_LIST A (NOLOCK)  
  left JOIN APP_UMBRELLA_UNDERLYING_POLICIES P 
  ON A.APP_NUMBER = LTRIM(SUBSTRING(P.POLICY_NUMBER,0,CHARINDEX('-',P.POLICY_NUMBER)))    
  JOIN APP_DRIVER_DETAILS D ON A.CUSTOMER_ID = D.CUSTOMER_ID AND A.APP_ID = D.APP_ID     
   AND A.APP_VERSION_ID = D.APP_VERSION_ID    
 WHERE    
A.CUSTOMER_ID=@CUSTOMERID     
AND ISNULL(D.IS_ACTIVE,'Y')='Y'  
         
    
END            


GO

