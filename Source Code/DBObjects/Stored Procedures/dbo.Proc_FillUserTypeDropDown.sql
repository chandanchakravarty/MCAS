IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FillUserTypeDropDown]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FillUserTypeDropDown]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------    
Proc Name       : dbo.Proc_FillUserTypeDropDown    
Created by      : Gaurav    
Date            : 4/15/2005    
Purpose         : To select  record in MNT_USER_TYPE    
Revison History :    
Used In         :   Wolverine    
------------------------------------------------------------    
Date     : 19 July, 2005     
Modified By : Anshuman    
Comments : Select user type base on agency user or carrier user    
    
    
------   ------------       -------------------------*/    
--drop procedure Dbo.Proc_FillUserTypeDropDown    
CREATE   PROC [dbo].[Proc_FillUserTypeDropDown]     
(    
 @AGENCY  nchar(1) = null    ,
 @LANG_ID int =1
)    
AS    
    
BEGIN    
    
 -- ===============================================================================
 -- MODIFIED BY SANTOSH KUMAR GAUTAM ON 28 APRIL 2011 FOR ITRACK :1133   
 -- ===============================================================================
 IF((@AGENCY is null) or(@AGENCY = 'N'))    
 BEGIN    
	  SELECT MUT.USER_TYPE_ID,ISNULL(MUTM.USER_TYPE_DESC ,MUT.USER_TYPE_DESC) AS USER_TYPE_DESC     
	  FROM   MNT_USER_TYPES AS MUT   LEFT OUTER JOIN
		     MNT_USER_TYPES_MULTILINGUAL AS MUTM ON MUT.USER_TYPE_ID=MUTM.USER_TYPE_ID AND MUTM.LANG_ID=@LANG_ID
	  WHERE  IS_ACTIVE='Y'     
	  ORDER BY USER_TYPE_DESC ASC    
 END    
 ELSE    
 BEGIN    
 
      SELECT MUT.USER_TYPE_ID,ISNULL(MUTM.USER_TYPE_DESC ,MUT.USER_TYPE_DESC) AS USER_TYPE_DESC     
	  FROM   MNT_USER_TYPES AS MUT   LEFT OUTER JOIN
		     MNT_USER_TYPES_MULTILINGUAL AS MUTM ON MUT.USER_TYPE_ID=MUTM.USER_TYPE_ID AND MUTM.LANG_ID=@LANG_ID
	  WHERE  IS_ACTIVE='Y' AND  USER_TYPE_FOR_CARRIER = 2   
	  ORDER BY USER_TYPE_DESC ASC  
	    
	
 END    
END    
  
  

GO

