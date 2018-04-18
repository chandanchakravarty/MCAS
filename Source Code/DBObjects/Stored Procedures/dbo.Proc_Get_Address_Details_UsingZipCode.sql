IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Get_Address_Details_UsingZipCode]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Get_Address_Details_UsingZipCode]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
/*----------------------------------------------------------                          
PROC NAME      :  dbo.Proc_Get_Address_Details_UsingZipCode                     
CREATED BY     : PRADEEP KUSHWAHA                        
DATE           : 31-05-2010                          
PURPOSE        : To get the customer addresss details based on the Country_id and Zipe code 
REVISON HISTORY:                 
MODIFY BY      :                          
DATE           :                          
PURPOSE        :         
                       
USED IN        : EBIX ADVANTAGE                      
------------------------------------------------------------                          
DATE     REVIEW BY          COMMENTS                          
------   ------------       -------------------------*/                          
--DROP PROC dbo.Proc_Get_Address_Details_UsingZipCode              
CREATE PROC [dbo].[Proc_Get_Address_Details_UsingZipCode]  
@ZIPCODE NVARCHAR(10),  
@COUNTRYID INT 
AS                          
                          
BEGIN

SELECT   + '^'  + CONVERT(varchar(10), mnt_c.STATE_ID) + '^' + mnt_z.ZIP_CODE + '^' +ISNULL(mnt_z.STREET_TYPE,'')+ '^' +ISNULL(mnt_z.STREET_NAME,'')+ '^' +ISNULL(mnt_z.DISTRICT,'')+ '^' + ISNULL(mnt_z.CITY ,'') + '^' + ISNULL(mnt_c.STATE_NAME,'')  + '^' 
FROM MNT_ZIP_CODES as mnt_z with(nolock)
INNER JOIN MNT_COUNTRY_STATE_LIST as mnt_c with(nolock) ON mnt_z.STATE = mnt_c.STATE_CODE and mnt_c.COUNTRY_ID=@COUNTRYID

WHERE mnt_z.ZIP_CODE=@ZIPCODE
     
END 


GO

