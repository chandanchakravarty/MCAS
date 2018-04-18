IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetLOBInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetLOBInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

  /*----------------------------------------------------------      
Proc Name          : Dbo.Proc_GetLOBInfo      
Created by         : Pradeep      
Date               : Sep 21, 2005      
Purpose            : Gets the LOB dependent information for the App Gen Info page      
Revison History    :      
Used In            :   Wolverine      
------------------------------------------------------------      
Date     Review By          Comments     
drop proc Proc_GetLOBInfo 
------   ------------       -------------------------*/      
CREATE  PROC [dbo].[Proc_GetLOBInfo]      
(      
  @LOB_ID Int       
)      
      
AS      
      
--Get the Terms for this LOB      
SELECT     MNT_LOB_MASTER.MAPPING_LOOKUP_ID,       
 MNT_LOOKUP_VALUES.LOOKUP_VALUE_DESC,       
        MNT_LOOKUP_VALUES.LOOKUP_VALUE_CODE,       
 MNT_LOOKUP_TABLES.LOOKUP_NAME,       
 MNT_LOB_MASTER.LOB_DESC ,
  MNT_LOB_MASTER.PRODUCT_TYPE     ---Added By Lalit dec 04,2011  
FROM         MNT_LOOKUP_VALUES       
 INNER JOIN MNT_LOOKUP_TABLES ON       
  MNT_LOOKUP_VALUES.LOOKUP_ID = MNT_LOOKUP_TABLES.LOOKUP_ID       
 INNER JOIN MNT_LOB_MASTER ON       
  MNT_LOOKUP_TABLES.LOOKUP_ID = MNT_LOB_MASTER.MAPPING_LOOKUP_ID      
WHERE     (MNT_LOB_MASTER.LOB_ID =@LOB_ID)      
      
--Get the Bill type for the LOB      
IF  (@LOB_ID = 1 OR @LOB_ID =6)      
BEGIN      
SELECT MLV.LOOKUP_UNIQUE_ID,      
   MLV.LOOKUP_VALUE_DESC,      
   MLV.LOOKUP_VALUE_CODE      
  FROM MNT_LOOKUP_VALUES MLV      
  INNER JOIN MNT_LOOKUP_TABLES MLT ON       
   MLV.LOOKUP_ID = MLT.LOOKUP_ID      
  WHERE MLT.LOOKUP_NAME = 'BLCODE' AND       
  MLV.Lookup_unique_id in(8459,11150,11191,11276,11277,11278)      
  ORDER BY MLV.LOOKUP_VALUE_DESC ASC      
      
      
END      
ELSE      
BEGIN      
SELECT MLV.LOOKUP_UNIQUE_ID,      
   MLV.LOOKUP_VALUE_DESC,      
   MLV.LOOKUP_VALUE_CODE      
  FROM MNT_LOOKUP_VALUES MLV      
  INNER JOIN MNT_LOOKUP_TABLES MLT ON       
   MLV.LOOKUP_ID = MLT.LOOKUP_ID      
  WHERE MLT.LOOKUP_NAME = 'BLCODE' AND       
  MLV.Lookup_unique_id in(8459,8460,11191)      
  ORDER BY MLV.LOOKUP_VALUE_DESC ASC      
END      
     
  
    
--Get SubLOBs for this LOB  
exec Proc_GetSubLOBs @LOB_ID  
      
    
--If LOB is Homeowners, get the Policy type    
IF ( @LOB_ID = 1 )    
BEGIN    
 exec Proc_GetLookupValues @LookupCode = N'HOPTYP'     
END    
  
GO

