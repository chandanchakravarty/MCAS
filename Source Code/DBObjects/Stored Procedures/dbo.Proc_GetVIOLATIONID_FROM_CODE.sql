IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetVIOLATIONID_FROM_CODE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetVIOLATIONID_FROM_CODE]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------    
Proc Name          : Dbo.Proc_GetVIOLATIONID_FROM_CODE    
Created by          : Pradeep    
Date                : 01/07/2005    
Purpose             : Returns the ViolationID for   
   a particular Violation code  
Revison History :    
Used In             :   Wolverine    
    
------------------------------------------------------------    

Modified :  Praveen Kasana : 15 feb 2006  : Added LOb Id paramater 
   if attached watercraft Attached to HomePolicy
Date     Review By          Comments  
  
------   ------------       -------------------------*/    
Create  PROCEDURE Proc_GetVIOLATIONID_FROM_CODE  
(  
 @VIOLATION_CODE VarChar(10),  
 @CUSTOMER_ID int,            
  @APP_ID int,            
  @APP_VERSION_ID smallint ,
  @LOB_ID smallint =null 
)      
      
AS      
BEGIN      
 DECLARE @VIOLATION_ID Int      
 DECLARE @STATEID SmallInt            
DECLARE @LOBID NVarCHar(5)            


      
            
SELECT @STATEID = STATE_ID,     
 @LOBID = APP_LOB            
FROM APP_LIST            
WHERE CUSTOMER_ID = @CUSTOMER_ID AND            
 APP_ID = @APP_ID AND            
 APP_VERSION_ID = @APP_VERSION_ID            


IF (@LOB_ID is not null)
begin 
	set @LOBID = @LOB_ID
end

  
 SELECT @VIOLATION_ID = VIOLATION_ID  
 FROM MNT_VIOLATIONS  
 WHERE VIOLATION_CODE = @VIOLATION_CODE AND  
 STATE = @STATEID AND  
 LOB =  @LOBID  
     
 RETURN @VIOLATION_ID  
END      
      
      
      
    
  
  



GO

