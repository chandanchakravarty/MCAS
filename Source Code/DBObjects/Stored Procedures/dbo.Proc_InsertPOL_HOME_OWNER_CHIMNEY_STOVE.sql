IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertPOL_HOME_OWNER_CHIMNEY_STOVE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertPOL_HOME_OWNER_CHIMNEY_STOVE]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------      
Proc Name       : dbo.Proc_InsertPOL_HOME_OWNER_CHIMNEY_STOVE      
Created by      : Vijay Arora  
Date            : 18-11-2005  
Purpose      : Insert record in POL_HOME_OWNER_CHIMNEY_STOVE      
Revison History :      
Used In   : Wolverine  
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/  
--drop proc Proc_InsertPOL_HOME_OWNER_CHIMNEY_STOVE    
CREATE PROC Dbo.Proc_InsertPOL_HOME_OWNER_CHIMNEY_STOVE      
(      
 @CUSTOMER_ID     int,      
 @POLICY_ID     int,      
 @POLICY_VERSION_ID     smallint,      
 @FUEL_ID     smallint,      
 @IS_STOVE_VENTED     nchar(2) = null,      
 @OTHER_DEVICES_ATTACHED     nvarchar(200) =null,      
 @CHIMNEY_CONSTRUCTION     nvarchar(10) =null,      
 @CONSTRUCT_OTHER_DESC     nvarchar(200)=null,      
 @IS_TILE_FLUE_LINING     nchar(2) =null,      
 @IS_CHIMNEY_GROUND_UP     nchar(2)=null,      
 @CHIMNEY_INST_AFTER_HOUSE_BLT     nchar(2)=null,      
 @IS_CHIMNEY_COVERED     nchar(2)=null,      
 @DIST_FROM_SMOKE_PIPE     smallint=null,      
 @THIMBLE_OR_MATERIAL     nvarchar(200)=null,      
 @STOVE_PIPE_IS     nvarchar(10)=null,      
 @DOES_SMOKE_PIPE_FIT     nchar(2)=null,      
 @SMOKE_PIPE_WASTE_HEAT     nchar(2)=null,      
 @STOVE_CONN_SECURE     nchar(2)=null,      
 @SMOKE_PIPE_PASS     nchar(2)=null,      
 @SELECT_PASS     nvarchar(10)=null,      
 @PASS_INCHES     decimal(25,2)=null  --Changed from smallint by Charles on 21-Oct-09 for Itrack 6599 
)      
AS      
BEGIN      
 INSERT INTO POL_HOME_OWNER_CHIMNEY_STOVE      
 (      
  CUSTOMER_ID,      
  POLICY_ID,      
  POLICY_VERSION_ID,      
  FUEL_ID,      
  IS_STOVE_VENTED,      
  OTHER_DEVICES_ATTACHED,      
  CHIMNEY_CONSTRUCTION,      
  CONSTRUCT_OTHER_DESC,      
  IS_TILE_FLUE_LINING,      
  IS_CHIMNEY_GROUND_UP,      
  CHIMNEY_INST_AFTER_HOUSE_BLT,      
  IS_CHIMNEY_COVERED,      
  DIST_FROM_SMOKE_PIPE,      
  THIMBLE_OR_MATERIAL,      
  STOVE_PIPE_IS,      
  DOES_SMOKE_PIPE_FIT,      
  SMOKE_PIPE_WASTE_HEAT,      
  STOVE_CONN_SECURE,      
  SMOKE_PIPE_PASS,      
  SELECT_PASS,      
  PASS_INCHES,    
  IS_ACTIVE      
 )      
 VALUES      
 (      
  @CUSTOMER_ID,      
  @POLICY_ID,      
  @POLICY_VERSION_ID,      
  @FUEL_ID,      
  @IS_STOVE_VENTED,      
  @OTHER_DEVICES_ATTACHED,      
  @CHIMNEY_CONSTRUCTION,      
  @CONSTRUCT_OTHER_DESC,      
  @IS_TILE_FLUE_LINING,      
  @IS_CHIMNEY_GROUND_UP,      
  @CHIMNEY_INST_AFTER_HOUSE_BLT,      
  @IS_CHIMNEY_COVERED,      
  @DIST_FROM_SMOKE_PIPE,      
  @THIMBLE_OR_MATERIAL,      
  @STOVE_PIPE_IS,      
  @DOES_SMOKE_PIPE_FIT,      
  @SMOKE_PIPE_WASTE_HEAT,      
  @STOVE_CONN_SECURE,      
  @SMOKE_PIPE_PASS,      
  @SELECT_PASS,      
  @PASS_INCHES,    
  'Y'  
 )      
END      
    
  
  
GO

