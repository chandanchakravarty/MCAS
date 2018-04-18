IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetHomeRatingLookups]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetHomeRatingLookups]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--DROP PROC Dbo.Proc_GetHomeRatingLookups    
  
CREATE PROC Dbo.Proc_GetHomeRatingLookups          
          
AS          
          
--Rating method lookup----------------          
exec Proc_GetLookupValues @LookupCode = N'RMCD'          
          
--Suare footage lookups----------------------------          
--Renovation lookup          
exec Proc_GetLookupValues @LookupCode = N'RENO'          
          
--Yes no lookup for Circuit breakers          
exec Proc_GetLookupValues @LookupCode = N'YESNO'          
          
--Construction lookups------------------------------          
          
--Foundation code          
exec Proc_GetLookupValues @LookupCode = N'FNDCD'--,@LookupParam = NULL,@OrderBy = N'1'      
          
--Construction type          
exec Proc_GetLookupValues @LookupCode = N'CONTYP'          
          
--Wiring          
exec Proc_GetLookupValues @LookupCode = N'WIRING'          
          
--Primary Heat and Sec heat          
exec Proc_GetLookupValues @LookupCode = N'PHEAT'          
          
--Roofing type          
exec Proc_GetLookupValues @LookupCode = N'RFTYP'          
          
--Prot devices--------------          
--Temperature type          
exec Proc_GetLookupValues @LookupCode = N'PVDCD'          
          
--Smoke type          
exec Proc_GetLookupValues @LookupCode = N'PDSCD'          
          
--Burglar type          
exec Proc_GetLookupValues @LookupCode = N'%BURG'          
          
--Swimming pool type          
exec Proc_GetLookupValues @LookupCode = 'SPLCD'         
        
--Costruction Code-HO        
exec Proc_GetLookupValues @LookupCode = 'HOCC'          
        
--Costruction Code-RD        
exec Proc_GetLookupValues @LookupCode = 'RDCC'          
    
--Sprinker Type    
exec Proc_GetLookupValues 'RISR',null,1    
  
-- Number of Amps   
  
EXEC Proc_GetLookupValues 'NOAMP',null,1  
          
          
        
      
    
    
    
    
    
    
    
  


GO

