IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_DEACTIVATE_MENU_LIST]') AND type in (N'P', N'PC'))

DROP PROC dbo.[SP_DEACTIVATE_MENU_LIST]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                                  
 Proc Name       : [dbo].[SP_DEACTIVATE_MENU_LIST]                          
 Created by      : praveer panghal         
 Date            : 5 SEP 2011                                 
 Purpose         : Stored procedure that DE-ACTIVATE the menu list                            
 Revison History :                                                  
 Used In       : EbixAdvanatge         
         
 drop proc [dbo].[SP_DEACTIVATE_MENU_LIST]'10-Y',1
------   ------------       -------------------------*/             
        

CREATE PROC dbo.[SP_DEACTIVATE_MENU_LIST]
(
@MENU_ISACTIVE NVARCHAR(50)=NULL,
@CARRIER_ID INT =1
)
AS

DECLARE @MENU_ID NVARCHAR(20) =null,
@IS_ACTIVE CHAR(2)=null
, @database nvarchar(50),@QUERY NVARCHAR(MAX)
		
    IF (@CARRIER_ID =1)
		Set @database = '[EBIX-ADVANTAGE-DEV-NEW]'
	ELSE IF (@CARRIER_ID =2)
		Set @database = '[EBIX-ADVANTAGE-LOCAL]'
	ELSE IF (@CARRIER_ID =3)
		Set @database = '[EBX-ADV-DEV-SINGAPORE]'
	ELSE IF (@CARRIER_ID =4)
		Set @database = '[ALBA-UAT]'
	ELSE IF (@CARRIER_ID =5)
		Set @database = '[EBIX-ADV-ALBA-UAT]'
	ELSE IF (@CARRIER_ID =6)
		Set @database = '[EBX-ALBA-INITIAL-LOAD]'
	ELSE IF (@CARRIER_ID =7)
		Set @database = '[EBIX-ADV-ALBA-UAT-06092011]'
	ELSE IF (@CARRIER_ID =8)
		Set @database = '[EBIX-ADV-SING-UAT]'
	ELSE
		Set @database = '[EBIX-ADVANTAGE-DEV-NEW]'
		

  SELECT @MENU_ID= RTRIM(LTRIM(SUBSTRING(@MENU_ISACTIVE,1,CHARINDEX('-',@MENU_ISACTIVE,0)-1)))--find menu id
  SELECT @IS_ACTIVE= RTRIM(LTRIM(SUBSTRING(@MENU_ISACTIVE,CHARINDEX('-',@MENU_ISACTIVE)+1, LEN(@MENU_ISACTIVE))))--find is-active
  
BEGIN

SET @QUERY='UPDATE '+@database+'.[DBO].MNT_MENU_LIST SET IS_ACTIVE='''+@IS_ACTIVE+''' WHERE MENU_ID='+@MENU_ID

EXEC(@QUERY)
END




