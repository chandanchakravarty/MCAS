IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateAPP_HOME_OWNER_CHIMNEY_STOVE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateAPP_HOME_OWNER_CHIMNEY_STOVE]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

  /*----------------------------------------------------------  
Proc Name       : dbo.Proc_insertAPP_HOME_OWNER_CHIMNEY_STOVE  
Created by      : Anshuman  
Date            : 5/20/2005  
Purpose     : Update record in APP_HOME_OWNER_CHIMNEY_STOVE  
Revison History :  
Used In  : BRICS  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
--drop proc Proc_UpdateAPP_HOME_OWNER_CHIMNEY_STOVE
CREATE PROC Dbo.Proc_UpdateAPP_HOME_OWNER_CHIMNEY_STOVE  
(  
 @CUSTOMER_ID     int,  
 @APP_ID     int,  
 @APP_VERSION_ID     smallint,  
 @FUEL_ID     smallint,  
 @IS_STOVE_VENTED     nchar(2),  
 @OTHER_DEVICES_ATTACHED     nvarchar(200),  
 @CHIMNEY_CONSTRUCTION     nvarchar(10),  
 @CONSTRUCT_OTHER_DESC     nvarchar(200),  
 @IS_TILE_FLUE_LINING     nchar(2),  
 @IS_CHIMNEY_GROUND_UP     nchar(2),  
 @CHIMNEY_INST_AFTER_HOUSE_BLT     nchar(2),  
 @IS_CHIMNEY_COVERED     nchar(2),  
 @DIST_FROM_SMOKE_PIPE     smallint,  
 @THIMBLE_OR_MATERIAL     nvarchar(200),  
 @STOVE_PIPE_IS     nvarchar(10),  
 @DOES_SMOKE_PIPE_FIT     nchar(2),  
 @SMOKE_PIPE_WASTE_HEAT     nchar(2),  
 @STOVE_CONN_SECURE     nchar(2),  
 @SMOKE_PIPE_PASS     nchar(2),  
 @SELECT_PASS     nvarchar(10),  
 @PASS_INCHES     decimal(25,2) --Changed from smallint by Charles on 21-Oct-09 for Itrack 6599  
)  
AS  
BEGIN  
 UPDATE APP_HOME_OWNER_CHIMNEY_STOVE set  
   
  IS_STOVE_VENTED = @IS_STOVE_VENTED,  
  OTHER_DEVICES_ATTACHED = @OTHER_DEVICES_ATTACHED,  
  CHIMNEY_CONSTRUCTION = @CHIMNEY_CONSTRUCTION,  
  CONSTRUCT_OTHER_DESC = @CONSTRUCT_OTHER_DESC,  
  IS_TILE_FLUE_LINING = @IS_TILE_FLUE_LINING,  
  IS_CHIMNEY_GROUND_UP = @IS_CHIMNEY_GROUND_UP,  
  CHIMNEY_INST_AFTER_HOUSE_BLT = @CHIMNEY_INST_AFTER_HOUSE_BLT,  
  IS_CHIMNEY_COVERED = @IS_CHIMNEY_COVERED,  
  DIST_FROM_SMOKE_PIPE = @DIST_FROM_SMOKE_PIPE,  
  THIMBLE_OR_MATERIAL = @THIMBLE_OR_MATERIAL,  
  STOVE_PIPE_IS = @STOVE_PIPE_IS,  
  DOES_SMOKE_PIPE_FIT = @DOES_SMOKE_PIPE_FIT,  
  SMOKE_PIPE_WASTE_HEAT = @SMOKE_PIPE_WASTE_HEAT,  
  STOVE_CONN_SECURE = @STOVE_CONN_SECURE,  
  SMOKE_PIPE_PASS = @SMOKE_PIPE_PASS,  
  SELECT_PASS = @SELECT_PASS,  
  PASS_INCHES = @PASS_INCHES  
   
 WHERE   
  CUSTOMER_ID  = @CUSTOMER_ID AND  
  APP_ID  = @APP_ID AND  
  APP_VERSION_ID = @APP_VERSION_ID AND  
  FUEL_ID  = @FUEL_ID  
END  
  
  
  
GO

