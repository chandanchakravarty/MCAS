IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Deleteunassigned]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Deleteunassigned]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE    PROC [dbo].[Proc_Deleteunassigned]  
(  
 @intLobId  smallint,         
 @strUnderWriters varchar(200),     
 @intAgencyId  int   
)  
AS  
BEGIN  
 if exists(select ASSIGN_ID from MNT_AGENCY_UNDERWRITERS where AGENCY_ID=@intAgencyId and LOB_ID =@intLobId)    
BEGIN  
return -1  
END  
IF EXISTS ( SELECT AGENCY_ID FROM POL_CUSTOMER_POLICY_LIST WHERE AGENCY_ID=@intAgencyId AND UNDERWRITER=@strUnderWriters )   
BEGIN  
return -1  
END  

    
END  
GO

