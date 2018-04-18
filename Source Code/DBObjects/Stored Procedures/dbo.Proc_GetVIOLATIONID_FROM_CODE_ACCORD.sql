IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetVIOLATIONID_FROM_CODE_ACCORD]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetVIOLATIONID_FROM_CODE_ACCORD]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------      
Proc Name          : Dbo.Proc_GetVIOLATIONID_FROM_CODE      
Created by          : PRAVEEN KASANA      
Date                : 4/19/2006 
Purpose             : Returns the ViolationID for a particular Violation code    
Revison History :      
Used In             :   Wolverine      
      
------------------------------------------------------------      
  
Modified :  Praveen Kasana : 15 feb 2006  : Added LOb Id paramater   
   if attached watercraft Attached to HomePolicy  
Date     Review By          Comments    
 drop proc  Proc_GetVIOLATIONID_FROM_CODE_ACCORD  12622
------   ------------       -------------------------*/      
CREATE  PROCEDURE Proc_GetVIOLATIONID_FROM_CODE_ACCORD    
(    
 @VIOLATION_CODE VarChar(10),    
 @CUSTOMER_ID int,              
 @APP_ID int,              
 @APP_VERSION_ID smallint , 
 @VIOLATION_IDS varchar(100) out , 
 @LOB_ID smallint =null  

)        
        
AS        
BEGIN        
 DECLARE @VIOLATION_ID Int
 DECLARE @VIOLATION_TYPE Int         
 DECLARE @STATEID SmallInt              
 DECLARE @LOBID NVarCHar(5)              
 DECLARE @VIOLATIONPOINT INT
  
        
              
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
  
    
 /*SELECT @VIOLATION_ID = VIOLATION_ID    
 FROM MNT_VIOLATIONS    
 WHERE VIOLATION_CODE = @VIOLATION_CODE AND    
 STATE = @STATEID AND    
 LOB =  @LOBID    */


SELECT  @VIOLATION_ID = mv1.VIOLATION_ID , @VIOLATION_TYPE = mv2.VIOLATION_ID , @VIOLATIONPOINT = MV1.MVR_POINTS
FROM MNT_VIOLATIONS mv1 INNER join MNT_VIOLATIONS mv2 
ON  mv1.VIOLATION_PARENT=mv2.VIOLATION_GROUP and mv1.LOB=mv2.LOB and mv1.STATE= mv2.STATE
WHERE mv1.VIOLATION_CODE=@VIOLATION_CODE and mv1.LOB=@LOBID and mv1.STATE=@STATEID 


       
set @VIOLATION_IDS= CAST(ISNULL(@VIOLATION_ID,'') AS VARCHAR) + ':'+ CAST(ISNULL(@VIOLATION_TYPE,'') AS VARCHAR)+ ':'+ CAST(ISNULL(@VIOLATIONPOINT,'') AS VARCHAR)


END        
        
        
        
      
    
    
  






GO

