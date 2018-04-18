IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GET_MENU_LIST]') AND type in (N'P', N'PC'))

DROP PROC dbo.[SP_GET_MENU_LIST]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                                  
 Proc Name       : [dbo].[SP_GET_MENU_LIST]                           
 Created by      : praveer panghal         
 Date            : 2 SEP 2011                                 
 Purpose         : Stored procedure that returns the  data of menu list                            
 Revison History :                                                  
 Used In       : EbixAdvanatge         
         
 drop proc [dbo].[SP_GET_MENU_LIST]
------   ------------       -------------------------*/             
        

CREATE PROC dbo.[SP_GET_MENU_LIST]    
(    
@MODULE INT=0,    
@SUB_MODULE INT=0,    
@CARRIER_ID INT =NULL    
)    
AS    
BEGIN     
DECLARE @database nvarchar(50),@QUERY NVARCHAR(MAX)
		
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
		
		


SET @QUERY='    SELECT     '+
' A.MENU_ID AS level1_id    '+
' ,A.MENU_NAME AS level1    '+
    
' ,B.MENU_ID AS level2_id    '+
' ,B.MENU_NAME AS level2     '+ 
    
' ,C.MENU_ID AS level3_id     '+
' ,C.MENU_NAME AS level3      '+
    
' , CASE WHEN C.IS_ACTIVE IS NOT NULL THEN C.IS_ACTIVE     '+
' WHEN B.IS_ACTIVE IS NOT NULL THEN B.IS_ACTIVE     '+
' WHEN A.IS_ACTIVE IS NOT NULL THEN A.IS_ACTIVE END is_active     '+
    
' , CASE WHEN C.MENU_ID IS NOT NULL THEN C.MENU_ID     '+
' WHEN B.MENU_ID IS NOT NULL THEN B.MENU_ID     '+
' WHEN A.MENU_ID IS NOT NULL THEN A.MENU_ID END screen_id     '+
    
' , CASE WHEN C.MENU_NAME IS NOT NULL THEN C.MENU_NAME   '+  
' WHEN B.MENU_NAME IS NOT NULL THEN B.MENU_NAME     '+
' WHEN A.MENU_NAME IS NOT NULL THEN A.MENU_NAME END screen_desc   '+
    
' FROM '+@database+'.[DBO]. MNT_MENU_LIST A WITH(NOLOCK)    '+ 
' LEFT OUTER JOIN '+@database+'.[DBO].MNT_MENU_LIST B WITH(NOLOCK)ON  B.PARENT_ID=A.MENU_ID   '+  
' LEFT OUTER JOIN '+@database+'.[DBO]. MNT_MENU_LIST C WITH(NOLOCK) ON C.PARENT_ID=B.MENU_ID   '+
' WHERE (' + CAST (@SUB_MODULE AS VARCHAR(20))+ ' = CASE WHEN ISNULL(' + CAST (@SUB_MODULE AS VARCHAR(20))+ ',0)=0 THEN ISNULL(' + CAST (@SUB_MODULE AS VARCHAR(20))+ ',0)   '+
  '                 ELSE A.MENU_ID END) AND    '+
   '     ' + CAST (@MODULE AS VARCHAR(20))+ '=(CASE WHEN ISNULL(' + CAST (@MODULE AS VARCHAR(20))+ ',0)=1 THEN  A.MENU_ID    '+
    '                WHEN ISNULL(' + CAST (@MODULE AS VARCHAR(20))+ ',0)=0 THEN ISNULL(' + CAST (@MODULE AS VARCHAR(20))+ ',0)   '+
     '              ELSE A.PARENT_ID END) '+
      '  ORDER BY      '+                   
   ' A.NESTLEVEL ASC , A.MENU_ORDER ASC,   '+                      
   ' B.NESTLEVEL ASC , B.MENU_ORDER ASC  ,     '+
   ' C.NESTLEVEL ASC , C.MENU_ORDER ASC       '+
       
       
  ' SELECT MENU_ID AS MODULE_ID,MENU_NAME AS MODULE_NAME FROM '+@database+'.[DBO].MNT_MENU_LIST MENU  WITH(NOLOCK)      '+ 
   ' WHERE MENU.MENU_ID BETWEEN 1 AND 8 ORDER BY MENU_NAME   '+
     
    ' SELECT  MENU.PARENT_ID as PARENT_ID , MENU_ID AS SUB_MODULE_ID   ,MENU_NAME AS SUB_MODULE_NAME    '+
    ' FROM '+@database+'.[DBO].MNT_MENU_LIST MENU  WITH(NOLOCK)       '+
    ' WHERE (MENU.PARENT_ID BETWEEN 2 AND 8 )  ORDER BY MENU_NAME   '        


EXEC (@QUERY)
 END     
