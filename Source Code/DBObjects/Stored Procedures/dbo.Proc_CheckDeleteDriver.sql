IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CheckDeleteDriver]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CheckDeleteDriver]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                                            
Proc Name            : dbo.Proc_CheckDeletePolicyDriver                                                                            
Created by           : Sibin Philip                                                                           
Date                 : 13 April 2008                                                                            
Purpose              : To check whether there are associated any Prior Loss against the driver. If so don't allow        to delete the driver                                                               
Revison History   :                                                                            
Used In              :   Wolverine                                                                              
------------------------------------------------------------                                                                            
Date     Review By          Comments                                                                            
------   ------------       -------------------------*/                                     
-- DROP PROC Proc_CheckDeleteDriver                                      
CREATE PROCEDURE [dbo].[Proc_CheckDeleteDriver]                                          
(                                                   
 @CUSTOMER_ID int,                                    
 @APP_ID int,                                    
 @VERSION_ID int,                                    
 @DRIVER_ID int,                                    
 @CALLED_FROM varchar(10)                                                        
)                                                
AS                                           
 DECLARE @PRIOR_LOSS_DETAILS varchar(30)                                    
 DECLARE @CUSTOMER_DETAILS varchar(20)                      
 DECLARE @DRIVER_DETAILS VARCHAR(50)                      
 DECLARE @DRIVER_INFO VARCHAR(50)                      
 DECLARE @APPLICATION_ID INT  -- Done for Itrack Issue 5457 on 24 June 2009                                 
BEGIN                        
   SET @CUSTOMER_DETAILS = CONVERT( VARCHAR(10),@CUSTOMER_ID) + '^' + CONVERT(VARCHAR(10),@APP_ID)+ '^' +             CONVERT(VARCHAR(10),@VERSION_ID) + '^' + CONVERT(VARCHAR(10),@DRIVER_ID) + '^' + @CALLED_FROM                         
       
            
                
   IF(@CALLED_FROM = 'POL')                    
   BEGIN 
       
  -- Done for Itrack Issue 5457 on 24 June 09. @APPLICATION_ID is taken because when we convert a application to  policy, it is not necessary that policy_id  will be same as app_id.So we will take the app_id from POL_CUSTOMER_POLICY_LIST 
    SET @APPLICATION_ID = (SELECT APP_ID FROM POL_CUSTOMER_POLICY_LIST with(nolock)WHERE CUSTOMER_ID= @CUSTOMER_ID AND       
      POLICY_ID=@APP_ID AND POLICY_VERSION_ID =@VERSION_ID)   
             
    SELECT @DRIVER_DETAILS=DRIVER_FNAME + '^' + ISNULL(DRIVER_MNAME,'') + '^' + DRIVER_LNAME + '^' +                  CONVERT(VARCHAR(10),DRIVER_DOB,101) + '^' +  CONVERT(VARCHAR(10),@DRIVER_ID)                    
    FROM POL_DRIVER_DETAILS WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMER_ID                   
    AND POLICY_ID = @APP_ID AND                 
    POLICY_VERSION_ID=@VERSION_ID AND                   
    DRIVER_ID = @DRIVER_ID       
                     
   END                    
   ELSE                    
   BEGIN       
  
   -- Done for Itrack Issue 5457 on 24 June 09. @APPLICATION_ID is taken because when we convert a application to  policy, it is not necessary that policy_id  will be same as app_id.So we will take the app_id from POL_CUSTOMER_POLICY_LIST
 SET @APPLICATION_ID = (SELECT APP_ID FROM APP_LIST WHERE CUSTOMER_ID= @CUSTOMER_ID AND       
      APP_ID=@APP_ID AND APP_VERSION_ID =@VERSION_ID)  
               
    SELECT @DRIVER_DETAILS=DRIVER_FNAME + '^' + ISNULL(DRIVER_MNAME,'') + '^' + DRIVER_LNAME + '^' +                  CONVERT(VARCHAR(10),DRIVER_DOB,101) + '^' +  CONVERT(VARCHAR(10),@DRIVER_ID)                     
    FROM APP_DRIVER_DETAILS WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMER_ID                   
    AND APP_ID= @APPLICATION_ID AND                   
    APP_VERSION_ID=@VERSION_ID AND                   
    DRIVER_ID = @DRIVER_ID                      
   END                     
                            
  DECLARE @DRIVER_FNAME VARCHAR(100),@DRIVER_MNAME VARCHAR(100),@DRIVER_LNAME VARCHAR(100),              
  @DRIVER_DOB VARCHAR(20),@DRIVER_NO INT, @LOSS_ID INT                      
                 
    ----Declaring Cursor                      
  IF(@CALLED_FROM = 'POL')                    
  BEGIN                      
   DECLARE CurDRIVER_CHECK CURSOR                      
   FOR SELECT DISTINCT DRIVER_FNAME,DRIVER_MNAME,DRIVER_LNAME,CONVERT(VARCHAR(10),DRIVER_DOB,101),PDL.DRIVER_ID      FROM POL_DRIVER_DETAILS PDL WITH(NOLOCK) LEFT OUTER JOIN APP_PRIOR_LOSS_INFO APLI WITH(NOLOCK)                    ON PDL.CUSTOMER_ID = @CUSTOMER_ID AND            
   CONVERT(VARCHAR(10),PDL.POLICY_ID) = DBO.PIECE(DRIVER_NAME,'^',2) AND                      
   CONVERT(VARCHAR(10),PDL.DRIVER_ID) =  DBO.PIECE(DRIVER_NAME,'^',4)                       
   WHERE PDL.CUSTOMER_ID = @CUSTOMER_ID                      
  END                    
  ELSE                    
  BEGIN                
   DECLARE CurDRIVER_CHECK CURSOR                    
   FOR SELECT DISTINCT DRIVER_FNAME,DRIVER_MNAME,DRIVER_LNAME,CONVERT(VARCHAR(10),DRIVER_DOB,101),ADL.DRIVER_ID      FROM APP_DRIVER_DETAILS ADL WITH(NOLOCK) LEFT OUTER JOIN APP_PRIOR_LOSS_INFO APLI  WITH(NOLOCK)                   ON ADL.CUSTOMER_ID = @CUSTOMER_ID AND                      
   CONVERT(VARCHAR(10),ADL.APP_ID) = DBO.PIECE(DRIVER_NAME,'^',2) AND                      
   CONVERT(VARCHAR(10),ADL.DRIVER_ID)=  DBO.PIECE(DRIVER_NAME,'^',4)                       
   WHERE ADL.CUSTOMER_ID = @CUSTOMER_ID                    
  END                     
                 
  OPEN CurDRIVER_CHECK                                                        
                                        
  FETCH NEXT FROM CurDRIVER_CHECK INTO @DRIVER_FNAME,@DRIVER_MNAME,@DRIVER_LNAME,@DRIVER_DOB,@DRIVER_NO                                   
  WHILE (@@FETCH_STATUS=0)                      
   BEGIN                      
      SET @DRIVER_INFO = @DRIVER_FNAME + '^' + ISNULL(@DRIVER_MNAME,'') + '^' + @DRIVER_LNAME + '^' +                   @DRIVER_DOB  + '^' + CONVERT(VARCHAR(10),@DRIVER_NO)         
                      
      IF (@DRIVER_INFO = @DRIVER_DETAILS)                      
      BEGIN              
       IF EXISTS(SELECT 1 FROM APP_PRIOR_LOSS_INFO WHERE                             
       DBO.PIECE(DRIVER_NAME,'^',1)= CAST(@CUSTOMER_ID AS VARCHAR) AND                            
       DBO.PIECE(DRIVER_NAME,'^',2) IN (CAST(@APP_ID AS VARCHAR),CAST(@APPLICATION_ID AS VARCHAR))       
      AND DBO.PIECE(DRIVER_NAME,'^',4)=CAST(@DRIVER_ID AS VARCHAR)                                    
       AND @DRIVER_ID = @DRIVER_NO                         
      )                       
     BEGIN                  
       SELECT DISTINCT APP_NUMBER AS APP_NUM,CAST((ISNULL(APP_NUMBER,'') + ' - Ver ' +               
       CAST(APP_VERSION_ID AS VARCHAR) + '.0')AS VARCHAR(100)) AS DRIVER_DETAILS                               
       FROM APP_LIST AP LEFT OUTER JOIN APP_PRIOR_LOSS_INFO APLI                      
       ON AP.CUSTOMER_ID = APLI.CUSTOMER_ID                 
       AND DBO.PIECE(DRIVER_NAME,'^',2)=AP.APP_ID            
       AND DBO.PIECE(DRIVER_NAME,'^',3)=AP.APP_VERSION_ID                  
       AND  DBO.PIECE(DRIVER_NAME,'^',5)='APP'                      
       WHERE AP.CUSTOMER_ID = @CUSTOMER_ID AND                      
       AP.APP_ID = @APPLICATION_ID AND -- Done for Itrack Issue 5457 on 24 June 2009                   
       AP.IS_ACTIVE='Y' AND                                                    
       APLI.IS_ACTIVE='Y'                      
       AND DBO.PIECE(DRIVER_NAME,'^',4) = @DRIVER_NO      
                  
      UNION                       
      SELECT  POLICY_NUMBER AS APP_NUM,CAST((ISNULL(POLICY_NUMBER,'') + ' - Ver ' +                   
    CAST(POLICY_VERSION_ID  AS VARCHAR)+ '.0') AS VARCHAR(100)) AS DRIVER_DETAILS                               
      FROM POL_CUSTOMER_POLICY_LIST PCL WITH(NOLOCK)                 
      LEFT OUTER JOIN APP_PRIOR_LOSS_INFO APLI  WITH(NOLOCK)                    
      ON PCL.CUSTOMER_ID = APLI.CUSTOMER_ID             
      AND DBO.PIECE(DRIVER_NAME,'^',2)=PCL.POLICY_ID            
      AND DBO.PIECE(DRIVER_NAME,'^',3)=PCL.POLICY_VERSION_ID                  
      AND DBO.PIECE(DRIVER_NAME,'^',5)='POL'                
      WHERE PCL.CUSTOMER_ID = @CUSTOMER_ID AND                    
      PCL.POLICY_ID= @APP_ID AND                    
      PCL.IS_ACTIVE='Y' AND                                                    
      APLI.IS_ACTIVE='Y'                      
      AND DBO.PIECE(DRIVER_NAME,'^',4) = @DRIVER_NO                   
                  
   ORDER BY APP_NUM desc                  
  
   ---FILTER LOSS IDs
   SELECT LOSS_ID FROM APP_PRIOR_LOSS_INFO WITH(NOLOCK)  
   WHERE DBO.PIECE(DRIVER_NAME,'^',1)= CAST(@CUSTOMER_ID AS VARCHAR) AND         
   -- Done for Itrack Issue 5457 on 24 June 2009                           
   DBO.PIECE(DRIVER_NAME,'^',2) IN (CAST(@APPLICATION_ID AS VARCHAR))      
   AND DBO.PIECE(DRIVER_NAME,'^',4)=CAST(@DRIVER_ID AS VARCHAR)  
   AND DBO.PIECE(DRIVER_NAME,'^',5)= 'APP'    
  
   UNION  
  
  SELECT LOSS_ID FROM APP_PRIOR_LOSS_INFO WITH(NOLOCK)  
  WHERE DBO.PIECE(DRIVER_NAME,'^',1)= CAST(@CUSTOMER_ID AS VARCHAR) AND         
  -- Done for Itrack Issue 5457 on 24 June 2009                           
  DBO.PIECE(DRIVER_NAME,'^',2) IN (CAST(@APP_ID AS VARCHAR))      
  AND DBO.PIECE(DRIVER_NAME,'^',4)=CAST(@DRIVER_ID AS VARCHAR)  
  AND DBO.PIECE(DRIVER_NAME,'^',5)= 'POL'                          
                    
   ORDER BY LOSS_ID ASC   
                   
    END                  
    END                      
    FETCH NEXT FROM CurDRIVER_CHECK INTO @DRIVER_FNAME,@DRIVER_MNAME,@DRIVER_LNAME,@DRIVER_DOB,@DRIVER_NO             
    END                      
  CLOSE CurDRIVER_CHECK                                             
  DEALLOCATE CurDRIVER_CHECK    
                    
END
GO

