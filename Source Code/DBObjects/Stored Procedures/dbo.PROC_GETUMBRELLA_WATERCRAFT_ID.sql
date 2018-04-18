IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_GETUMBRELLA_WATERCRAFT_ID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_GETUMBRELLA_WATERCRAFT_ID]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO




/*----------------------------------------------------------                                                                  
Proc Name           : Proc_GetRatingInformationForUmbrella                                                        
Created by          : Neeraj singh                                                                 
Date                : 28-12-2006                                                                  
Purpose             : To get the information for creating the input xml                                                                   
Revison History     :                                                                  
Used In             : Wolverine                                                                  
------------------------------------------------------------                                                                  
Date     Review By          Comments                                                                  
------   ------------       -------------------------*/      
--  
       
CREATE PROC dbo.PROC_GETUMBRELLA_WATERCRAFT_ID    
 @CUSTOMER_ID      INT,                                                                      
 @ID         INT,                                                                      
 @VERSION_ID    INT, 
 @DATA_ACCESS_POINT INT
      
AS      
BEGIN      
declare @POLID int          
DECLARE @POLVERSIONID smallint          
DECLARE @POL_NUMBER NVARCHAR(20)          
DECLARE @POL_VERSION NVARCHAR(20)          
DECLARE @POL_lob NVARCHAR (20)               
DECLARE @STATEID SMALLINT      
DECLARE @BOATID INT            
DECLARE @APP_LOBOTHER INT       
DECLARE @WATERCRAFTBOATPOLNUMBER INT      
DECLARE @IS_POLICY bit      
DECLARE @WATERCRAFTBOATPOLVERSION SMALLINT            
DECLARE @POLICY int  
DECLARE @APPLICTION int  
DECLARE @OTHERS int 

SET @POLICY =1  
SET @APPLICTION =2  
SET @OTHERS =3          

IF ( @DATA_ACCESS_POINT = @POLICY)  
BEGIN  
      SELECT BOAT_ID      
       FROM POL_WATERCRAFT_INFO  WITH (NOLOCK) INNER JOIN MNT_LOOKUP_VALUES WITH (NOLOCK) ON TYPE_OF_WATERCRAFT = LOOKUP_UNIQUE_ID  
	WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID=@ID AND POLICY_VERSION_ID=@VERSION_ID and POL_WATERCRAFT_INFO.IS_ACTIVE='Y'             
       ORDER BY   BOAT_ID       
          
END      

IF ( @DATA_ACCESS_POINT = @APPLICTION)  
BEGIN  
       
    SELECT BOAT_ID                        
        FROM APP_WATERCRAFT_INFO WITH (NOLOCK) INNER JOIN MNT_LOOKUP_VALUES WITH (NOLOCK) ON TYPE_OF_WATERCRAFT = LOOKUP_UNIQUE_ID  
	WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID=@ID AND APP_VERSION_ID=@VERSION_ID and APP_WATERCRAFT_INFO.IS_ACTIVE='Y'   
        ORDER BY   BOAT_ID        
END      
IF ( @DATA_ACCESS_POINT = @OTHERS)  
BEGIN  
      SELECT BOAT_ID       
      FROM APP_UMBRELLA_WATERCRAFT_INFO WITH (NOLOCK) INNER JOIN MNT_LOOKUP_VALUES WITH (NOLOCK) ON TYPE_OF_WATERCRAFT = LOOKUP_UNIQUE_ID  WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID=@ID AND APP_VERSION_ID=@VERSION_ID and APP_UMBRELLA_WATERCRAFT_INFO.IS_ACTIVE='Y'              
         ORDER BY   BOAT_ID          
END      
      
      
      
END        
      



GO

