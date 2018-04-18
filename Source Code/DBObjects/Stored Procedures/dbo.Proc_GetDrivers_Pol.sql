IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetDrivers_Pol]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetDrivers_Pol]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




/* ----------------------------------------------------------              
Proc Name            : Dbo.Proc_GetDrivers_Pol              
Created by           : Ashwani             
Date                 : 01 Mar 2006  
Purpose              : To get the drivers for the PPA policy rule implementation            
Revison History      :              
Used In              :   Wolverine                
------------------------------------------------------------              
Date     Review By          Comments              
------   ------------       ------------------------- */              
            
create proc dbo.Proc_GetDrivers_Pol                
 @CUSTOMER_ID  int,              
 @POLICY_ID  int,              
 @POLICY_VERSION_ID int             
as              
begin              
 select DRIVER_ID from POL_DRIVER_DETAILS              
 where  CUSTOMER_ID = @CUSTOMER_ID   and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID           
  and IS_ACTIVE='Y'  and isnull(DRIVER_DRIV_TYPE,11603)in (11603,3477,11941,11942,'')      
   order by DRIVER_ID          
  -- Driver Type is Licensed & value="11603"           
end            


GO

