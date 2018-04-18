IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetMVRViolationDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetMVRViolationDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                        
Proc Name                : Dbo.Proc_GetMVRViolationDetails  
Created by               : 
Date                     : 
Purpose                  : Match the Violation codes.
Revison History          :                                 
Modified by		 : pravesh Chandel
Dated 			 : 18 dec 2006
Purpose                  : Match the Violation codes only from mnt_violations.		                       
Used In                  : Wolverine                                                        
------------------------------------------------------------                                                        
Date     Review By          Comments                                                        
------   ------------       -------------------------*/ 
--Proc_GetMVRViolationDetails "'26100','23300','28300','28300','26100'",2,22
-- DROP PROC Dbo.Proc_GetMVRViolationDetails                                                 
CREATE PROC Dbo.Proc_GetMVRViolationDetails                                                 
(                                                        
 	@MVR_Violation_Codes   VARCHAR(8000),                                       
 	@LobID INT,      
	@StateID INT      
)                                                        
AS    
BEGIN
DECLARE @strQuery VARCHAR(8000)    
    
/*	set @strQuery ='
			SELECT  dbo.MNT_MAPVIOLATION.SSV_CODE,dbo.MNT_VIOLATIONS.*      
			FROM  dbo.MNT_MAPVIOLATION INNER JOIN      
	                      dbo.MNT_VIOLATIONS ON dbo.MNT_MAPVIOLATION.VIOLATION_CODE = dbo.MNT_VIOLATIONS.VIOLATION_ID      
			WHERE (dbo.MNT_MAPVIOLATION.SSV_CODE IN ('+ @MVR_Violation_Codes +'))'    
*/

set @strQuery ='
			SELECT   dbo.MNT_VIOLATIONS.VIOLATION_CODE SSV_CODE,dbo.MNT_VIOLATIONS.*      
			FROM  dbo.MNT_VIOLATIONS      
			WHERE (dbo.MNT_VIOLATIONS.VIOLATION_CODE IN ('+ @MVR_Violation_Codes +') and LOB=' + convert(varchar,@LobID) + ' and state=' + convert(varchar,@StateID) + ')' + ' and VIOLATION_PARENT<>0'
--convert(varchar,state)=convert(varchar,' + @StateID + '))' 
    
EXEC (@strQuery);  
--select @strQuery

END
  
  
  





GO

