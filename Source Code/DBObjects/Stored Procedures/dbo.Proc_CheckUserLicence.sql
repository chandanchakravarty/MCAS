IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CheckUserLicence]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CheckUserLicence]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                                            
Proc Name       : dbo.Proc_CheckUserLicence
Created by      : Swarup                                                         
Date            : 23-07-2007                                                      
                                                      
------   ------------       -------------------------*/                                                            
--DROP PROC dbo.Proc_CheckUserLicence                                                       
CREATE PROC dbo.Proc_CheckUserLicence                                                            
(
  @User_System_Id  varchar(8),
  @User_Id varchar(8) out 
) 
as 
begin                
	 declare @AGENCY_LIC_NUM INT                  
	 declare @USER_LIC_NUM INT
	-- declare @User_Id int                  
                                       
	begin                
		  ---Check that the number of licensed users do not exceed the number of licences assigned to current agency                  
		 SELECT @AGENCY_LIC_NUM=ISNULL(AGENCY_LIC_NUM,0) FROM MNT_AGENCY_LIST WHERE AGENCY_CODE=@User_System_Id                  
		 SELECT @USER_LIC_NUM=ISNULL(COUNT(USER_ID),0)+1 FROM MNT_USER_LIST                   
		  WHERE USER_SYSTEM_ID=@User_System_Id AND LIC_BRICS_USER=10963 AND IS_ACTIVE='Y'                  
		                  
 		IF(@USER_LIC_NUM > @AGENCY_LIC_NUM)                  
 		begin                
  			SELECT @User_Id = -3                  
  			return                
 		end 
		else
		begin
			SELECT @User_Id = -2                  
  			return  
		end   
	End 
end 



GO

