IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetMaxLocationNumber]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetMaxLocationNumber]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                              
PROC NAME      : DBO.[Proc_GetMaxLocationNumber]                              
CREATED BY     : Pradeep Kushwaha                 
DATE           : 02-07-2010                              
PURPOSE        : Get the Max Location Number 
REVISON HISTORY:                     
MODIFY BY      :                              
DATE           :                              
PURPOSE        :             
USED IN        : EBIX ADVANTAGE                          
------------------------------------------------------------                              
DATE     REVIEW BY          COMMENTS                              
------   ------------       -------------------------*/                              
--Proc_GetMaxLocationNumber 2154,2,32
 --DROP PROC dbo.Proc_GetMaxLocationNumber  

CREATE PROC [dbo].[Proc_GetMaxLocationNumber] 
 @CUSTOMER_ID  INT ,      
 @FLAG INT,  
 @LOCATION_NUMBER NUMERIC OUT
AS                              
                              
BEGIN  
IF(@FLAG=1)--Get the max Location Number if the pageload call (flag==1)  
 BEGIN  
	SELECT  @LOCATION_NUMBER=ISNULL(MAX(LOC_NUM),0)+1 FROM POL_LOCATIONS  WITH (NOLOCK)  WHERE CUSTOMER_ID=@CUSTOMER_ID 
	RETURN 1  
 END  
ELSE IF (@FLAG=2)--To check whether the  Location Number exists or not  
 BEGIN  
 IF EXISTS (SELECT DISTINCT(LOC_NUM) FROM POL_LOCATIONS  WITH (NOLOCK)  WHERE CUSTOMER_ID=@CUSTOMER_ID AND LOC_NUM=@LOCATION_NUMBER )
  BEGIN					
    RETURN -5 --if LOC_NUM exists then return -5  
  END
 ELSE
 BEGIN  
  RETURN -6 --if LOC_NUM does not exists then return -6  
 END
END  
END

GO

