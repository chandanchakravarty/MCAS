IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertAgecnyUnderwriter]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertAgecnyUnderwriter]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================  
--Proc Name       : dbo.Proc_InsertAgecnyUnderwriter   
-- Author:  Sonal  
-- Create date: 7/5/2010  
-- Description: Assign agencies UW/Marketing to LOB  
-- =============================================  
-- drop procedure Proc_InsertAgecnyUnderwriter
CREATE PROCEDURE [dbo].[Proc_InsertAgecnyUnderwriter]  
 @intLobId  smallint,         
 @strUnderWriters varchar(8000),      
 @strMarketeer varchar(8000),    
 @intAgencyId  int   
AS  
BEGIN 

   if (@strMarketeer <> '' or @strUnderWriters <> '') 
   begin
    if exists(select ASSIGN_ID from MNT_AGENCY_UNDERWRITERS where AGENCY_ID=@intAgencyId and LOB_ID =@intLobId)  
        begin   
           /* first delete the existing records in transaction table then insert new records*/  
            delete from MNT_AGENCY_UNDERWRITERS where AGENCY_ID=@intAgencyId and LOB_ID=@intLobId  
        end 
        insert into MNT_AGENCY_UNDERWRITERS (AGENCY_ID,LOB_ID,UNDERWRITERS,MARKETERS)   
		values (@intAgencyId,@intLobId,@strUnderWriters,@strMarketeer)  
   end   
   
END  
GO

