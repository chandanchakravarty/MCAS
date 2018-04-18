IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_SendDiaryForMVRService]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_SendDiaryForMVRService]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------      
Proc Name            	: Dbo.Proc_SendDiaryForMVRService  
Created by             : Vijay Arora    
Date                    : 21-03-2006  
Purpose                 :       
Revison History   :      
Used In                 : Wolverine        
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/      
CREATE PROCEDURE Proc_SendDiaryForMVRService      
@CUSTOMER_ID INT,  
@POLICY_ID INT,  
@POLICY_VERSION_ID INT,  
@DRIVER_ID INT,  
@LOB_ID INT  
AS    
BEGIN  
/*    
1 HOME Homeowners   
2 AUTOP Automobile   
3 CYCL Motorcycle   
4 BOAT Watercraft   
5 UMB Umbrella   
6 REDW Rental   
7 GENL General Liability   
*/  
  
DECLARE @intSD_PONITS INT                                          
DECLARE @SD_POINTS CHAR   
SET  @SD_POINTS = 'N'                                       
  
IF (@LOB_ID = 2)  
BEGIN  
                                          
  SELECT @intSD_PONITS=SUM(WOLVERINE_VIOLATIONS)                              
  FROM POL_MVR_INFORMATION                              
  INNER JOIN  VIW_DRIVER_VIOLATIONS ON POL_MVR_INFORMATION.VIOLATION_ID = VIW_DRIVER_VIOLATIONS.VIOLATION_ID                              
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID AND  POLICY_VERSION_ID=@POLICY_VERSION_ID   
         AND DRIVER_ID=@DRIVER_ID  AND (YEAR(GETDATE())- YEAR(MVR_DATE))<3 AND POL_MVR_INFORMATION.IS_ACTIVE='Y'                                                
                                       
  IF (@intSD_PONITS > 5)                                          
     SET @SD_POINTS='Y'                                          
  ELSE                                          
    SET @SD_POINTS='N'  


PRINT @SD_POINTS
END                    
  
  
IF (@LOB_ID = 4)  
BEGIN  
                                       
  SELECT @intSD_PONITS=SUM(WOLVERINE_VIOLATIONS)                    
  FROM POL_WATERCRAFT_MVR_INFORMATION                    
  INNER JOIN VIW_DRIVER_VIOLATIONS  ON POL_WATERCRAFT_MVR_INFORMATION.VIOLATION_ID = VIW_DRIVER_VIOLATIONS.VIOLATION_ID                    
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID   
  AND DRIVER_ID=@DRIVER_ID AND (YEAR(GETDATE())- YEAR(MVR_DATE))<3 AND POL_WATERCRAFT_MVR_INFORMATION.IS_ACTIVE='Y'  
 -- AND  @INT_CONVICTED_ACCIDENT=1                                                    
  IF (@intSD_PONITS > 5)                                        
    SET @SD_POINTS='Y'                                        
  ELSE                                        
    SET @SD_POINTS='N'                                         
  
END  
  
  
IF (@LOB_ID = 3)  
BEGIN  
          
  DECLARE @DATE_DATE_LICENSED DATETIME,           
  @PREFERRED_RISK CHAR,    
  @INTDATE INT,  
  @PREFERRED_DISC  CHAR                      
   
    
  SELECT @intSD_PONITS=ISNULL(SUM(WOLVERINE_VIOLATIONS),0) FROM VIW_DRIVER_VIOLATIONS WHERE VIOLATION_ID IN                
  (                
    SELECT VIOLATION_ID                      
    FROM  POL_MVR_INFORMATION
    WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID   
    AND DRIVER_ID=@DRIVER_ID          
    AND IS_ACTIVE='Y'              
  )          
           
  SELECT @DATE_DATE_LICENSED=DATE_LICENSED ,@PREFERRED_RISK=PREFERRED_RISK                                
  FROM POL_DRIVER_DETAILS                                                                  
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID   
  AND DRIVER_ID=@DRIVER_ID          
   
  SELECT @INTDATE=DATEDIFF(MONTH,@DATE_DATE_LICENSED,GETDATE())          
   
  IF (@PREFERRED_RISK ='1')      
    BEGIN  
     IF (@INTDATE<36 or @intSD_PONITS>2)    
                          BEGIN        
      SET @SD_POINTS='Y'          
      
    --UPDATE THE PREFFERED RISK DISCOUNT.  
    IF EXISTS (SELECT DRIVER_ID FROM POL_DRIVER_DETAILS WHERE CUSTOMER_ID = @CUSTOMER_ID  
        AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID   
        AND DRIVER_ID = @DRIVER_ID)  
    BEGIN  
     UPDATE POL_DRIVER_DETAILS SET PREFERRED_RISK = 0 WHERE CUSTOMER_ID = @CUSTOMER_ID  
        AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID   
        AND DRIVER_ID = @DRIVER_ID  
    END  
     END  
     ELSE          
     SET @SD_POINTS='N'          
   END       
  ELSE      
   SET @SD_POINTS='N'          
  
PRINT @SD_POINTS

END    

PRINT 'before'
  
IF (@SD_POINTS = 'Y')  
 BEGIN       
  DECLARE @UNDERWRITER INT  
  DECLARE @LIST_ID INT   
           
  SELECT @UNDERWRITER = UNDERWRITER FROM POL_CUSTOMER_POLICY_LIST WHERE CUSTOMER_ID = @CUSTOMER_ID  
  AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID  
  

   print  @UNDERWRITER

  --INSERT THE DIARY ENTRY.  
   INSERT INTO TODOLIST      
   (      
     RECDATE,      
     SUBJECTLINE,      
     LISTOPEN,      
     TOUSERID,      
     STARTTIME,      
     ENDTIME,      
     CUSTOMER_ID,      
     POLICY_ID,      
     POLICY_VERSION_ID      
   )      
   VALUES      
  (      
   GETDATE(),  
   'MVR VOILATIONS RETRIVED',  
          'Y',  
   @UNDERWRITER,  
   GETDATE(),  
   GETDATE(),  
          @CUSTOMER_ID,   
   @POLICY_ID,   
   @POLICY_VERSION_ID  
   )      
 END  
  
END  
  
  
  
  
  
  
  
  
  
  







GO

