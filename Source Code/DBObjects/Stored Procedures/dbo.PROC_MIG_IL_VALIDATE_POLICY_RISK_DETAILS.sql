/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_VALIDATE_POLICY_RISK_DETAILS]    Script Date: 12/02/2011 17:54:04 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_MIG_IL_VALIDATE_POLICY_RISK_DETAILS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_MIG_IL_VALIDATE_POLICY_RISK_DETAILS]
GO



/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_VALIDATE_POLICY_RISK_DETAILS]    Script Date: 12/02/2011 17:54:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                                
Proc Name             : Dbo.[[PROC_MIG_IL_VALIDATE_POLICY_RISK_DETAILS]]                                                            
Created by            : Pradeep Kushwaha                                                            
Date                  : 11 oct 2011                                                          
Purpose               : Validate risk details 
Revison History       :                                                                
Used In               : INITIAL LOAD                   
------------------------------------------------------------                                                                
Date     Review By          Comments                                   
                          
drop Proc [PROC_MIG_IL_VALIDATE_POLICY_RISK_DETAILS]  1419    
------   ------------       -------------------------*/       
CREATE PROCEDURE [dbo].[PROC_MIG_IL_VALIDATE_POLICY_RISK_DETAILS]     
    
--------------------------------- INPUT PARAMETER    
@IMPORT_REQUEST_ID  INT    
-------------------------------------------------    
      
AS    
BEGIN    
     
-------------------------------- DECLARATION PART  
----------------------------------------------------------------------------------------  
DECLARE @ERROR_NUMBER    INT    
DECLARE @ERROR_SEVERITY  INT    
DECLARE @ERROR_STATE     INT    
DECLARE @ERROR_PROCEDURE VARCHAR(512)    
DECLARE @ERROR_LINE    INT    
DECLARE @ERROR_MESSAGE   NVARCHAR(MAX)    
  
  
DECLARE @LOOP_POLICY_NUMBER NVARCHAR(50)       
DECLARE @LOOP_POLICY_SEQUANCE_NO INT      
DECLARE @LOOP_END_SEQUANCE_NO INT      
DECLARE @LOOP_RISK_SERIAL_NO INT      
DECLARE @COUNTER INT  =1    
DECLARE @MAX_RECORD_COUNT INT     
DECLARE @ERROR_NO INT=0    
DECLARE @IMPORT_SERIAL_NO INT   
        
  
DECLARE @IMPORT_FILE_NAME NVARCHAR(50)             
DECLARE @IMPORT_FILE_TYPE INT = 15008 -- FOR POLICY RISK FILE TYPE  
DECLARE @SUSEP_LOB_CODE NVARCHAR(10)
DECLARE @LOB_ID INT
  
DECLARE @ACTIVITY_TYPE INT 
DECLARE @OCCUPIED_AS INT 
DECLARE @CONSTRUCTION INT 
DECLARE @CONSTRUCTION_TEXT NVARCHAR(250)
DECLARE @STATE_ID INT   
DECLARE @POSITION_ID INT 
DECLARE @MARITAL_STATUS INT
DECLARE @FIPE_CODE NVARCHAR(10)
DECLARE @CATEGORY INT
DECLARE @CAPACITY INT
DECLARE @MAKE_MODEL  NVARCHAR(50)
DECLARE @ORIGIN_COUNTRY INT
DECLARE @ORIGIN_STATE INT
DECLARE @DESTINATION_COUNTRY INT
DECLARE @DESTINATION_STATE INT
DECLARE @CONVEYANCE_TYPE INT
DECLARE @MODE INT
DECLARE @PROPERTY INT
DECLARE @CULTIVATION INT
DECLARE @SUBSIDY_STATE INT
DECLARE @YEAR INT
DECLARE @GENDER INT
BEGIN TRY    
-------------------------------- CREATE TEMP TABLE FOR THOSE RECORDS NEEDS TO BE PROCESSED    
-----------------------------------------------------------------------------------------    
      
    -------------------------------- CREATE TEMP TABLE FOR THOSE RECORDS NEEDS TO BE PROCESSED  
-----------------------------------------------------------------------------------------  
    
     CREATE TABLE #TEMP_POLICY_RISK    
     (    
	  ID INT IDENTITY(1,1),    
      IMPORT_SERIAL_NO BIGINT,    
      POLICY_SEQUENCE_NO INT,  
	  END_SEQUENCE_NO INT,  
	  RISK_SEQUENCE_NO  INT ,
	  ACTIVITY_TYPE INT,
	  OCCUPIED_AS INT,
	  CONSTRUCTION NVARCHAR(250),
	  [STATE] INT,
	  POSITION INT,
	  MARITAL_STATUS  INT,
	  FIPE_CODE NVARCHAR(10),
	  CATEGORY INT,
      CAPACITY INT,
      [YEAR] INT ,
      GENDER INT
     )    

	 INSERT INTO #TEMP_POLICY_RISK    
     (    
		  	IMPORT_SERIAL_NO,  
		  	POLICY_SEQUENCE_NO,
		  	END_SEQUENCE_NO,
		  	RISK_SEQUENCE_NO,
			ACTIVITY_TYPE,
			OCCUPIED_AS,
			CONSTRUCTION,
			[STATE],
			POSITION,
			MARITAL_STATUS, 
		  	FIPE_CODE , 
		  	CATEGORY ,
			CAPACITY ,
			[YEAR],
			GENDER
     )
      SELECT     
		    IMPORT_SERIAL_NO  ,
			POLICY_SEQUENCE_NO,
			END_SEQUENCE_NO,
			RISK_SEQUENCE_NO,
			ACTIVITY_TYPE,
			OCCUPIED_AS,
			CONSTRUCTION,
			[STATE],
			POSITION,
			MARITAL_STATUS,
			FIPE_CODE,
			CATEGORY ,
			CAPACITY ,
			[YEAR],
			GENDER
      FROM MIG_IL_POLICY_RISK_DETAILS WITH(NOLOCK)    
      WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS = 0    
     
        
	------------------------------------            
   -- GET MAX RECOUNT COUNT      
   ------------------------------------         
	SELECT @MAX_RECORD_COUNT = COUNT(ID)       
    FROM   #TEMP_POLICY_RISK       
    
   ------------------------------------                
   -- GET IMPORT FILE NAME          
   ------------------------------------             
    IF(@MAX_RECORD_COUNT>0)          
    BEGIN          
              
      SELECT  @IMPORT_FILE_NAME=SUBSTRING(ISNULL(DISPLAY_FILE_NAME,''),1,9)          
      FROM MIG_IL_IMPORT_REQUEST_FILES WITH(NOLOCK)          
      WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND           
            IMPORT_FILE_TYPE  = @IMPORT_FILE_TYPE          
      SET @SUSEP_LOB_CODE=  SUBSTRING(@IMPORT_FILE_NAME,1,4)        
    END               
    ---------------------------------------------
    
	WHILE (@COUNTER<=@MAX_RECORD_COUNT)
		BEGIN 
		SET @ERROR_NO=0          
		SET @ACTIVITY_TYPE=0
		SET @OCCUPIED_AS =0
		SET @CONSTRUCTION=0
		SET @STATE_ID=0
		SET @POSITION_ID=0
		SET @MARITAL_STATUS =0
		SET @FIPE_CODE =null
		SET @CATEGORY =0
		SET @CAPACITY =0
		SET @MAKE_MODEL  =null
		SET @MODE=0
		SET @PROPERTY=0
		SET @CULTIVATION=0
		SET @SUBSIDY_STATE=0
		SET @YEAR =0 
		SET @GENDER=0
		SELECT     
        @IMPORT_SERIAL_NO			= IMPORT_SERIAL_NO,          
	 	@LOOP_POLICY_SEQUANCE_NO	= POLICY_SEQUENCE_NO,     
        @LOOP_END_SEQUANCE_NO       = END_SEQUENCE_NO,   
		@LOOP_RISK_SERIAL_NO		= RISK_SEQUENCE_NO , 
		@ACTIVITY_TYPE				= ACTIVITY_TYPE,
		@OCCUPIED_AS				= OCCUPIED_AS,
		@CONSTRUCTION_TEXT				= CONSTRUCTION,
		@STATE_ID					= [STATE],
		@POSITION_ID				= POSITION,
		@MARITAL_STATUS 			= MARITAL_STATUS  ,
		@FIPE_CODE					= FIPE_CODE,
		@CATEGORY					= CATEGORY,
		@CAPACITY					= CAPACITY,
		@YEAR						= [YEAR],
		@GENDER						= GENDER
        FROM   #TEMP_POLICY_RISK (NOLOCK) WHERE ID   = @COUNTER   
      
		 
		 ----------------------------  VALIDATION OF ALREADY PROCESSED RECORD  -----------------------------------------  
		IF EXISTS (SELECT 1 FROM MIG_IL_IMPORT_SUMMARY (NOLOCK) WHERE [FILE_NAME]=@IMPORT_FILE_NAME AND FILE_TYPE=@IMPORT_FILE_TYPE   
                        AND POLICY_SEQUENTIAL= @LOOP_POLICY_SEQUANCE_NO  AND   
                            ENDORSEMENT_SEQUENTIAL= @LOOP_END_SEQUANCE_NO AND  
                            IMPORT_SEQUENTIAL=@LOOP_RISK_SERIAL_NO AND --CHANGE  
                            IS_ACTIVE='Y'   
                     )            
		 SET @ERROR_NO=66      -- This record already processed.    
      
		SELECT 
		@LOB_ID=LOB_ID 
		
		FROM MNT_LOB_MASTER NOLOCK 
		WHERE SUSEP_LOB_CODE= CASE WHEN @SUSEP_LOB_CODE='9820' THEN '0982'
								   WHEN @SUSEP_LOB_CODE='9821' THEN '0520' 
								   ELSE @SUSEP_LOB_CODE 
						   END and IS_ACTIVE='Y'
	 
		IF(@LOB_ID IN(9,26))
		BEGIN 
			SET @CONSTRUCTION=@CONSTRUCTION_TEXT
			IF(@OCCUPIED_AS IS NOT NULL AND @OCCUPIED_AS >0 and NOT EXISTS(SELECT 1 FROM MNT_OCCUPIED_MASTER NOLOCK WHERE OCCUPIED_ID=@OCCUPIED_AS))
					SET @ERROR_NO =111 --  Occupied as does not exists.
				ELSE IF(@ACTIVITY_TYPE IS NOT NULL AND @ACTIVITY_TYPE >0 and NOT EXISTS(SELECT 1 FROM MNT_ACTIVITY_MASTER NOLOCK WHERE ACTIVITY_ID=@ACTIVITY_TYPE))
					SET @ERROR_NO =112 --  Activity type  does not exists.
				ELSE IF(@CONSTRUCTION IS NOT NULL AND @CONSTRUCTION >0 and NOT EXISTS(SELECT 1 FROM MNT_LOOKUP_VALUES NOLOCK WHERE LOOKUP_UNIQUE_ID =@CONSTRUCTION AND IS_ACTIVE='Y'))
					SET @ERROR_NO =113 --  Construction does not exists.
				
			select @ERROR_NO	
				 
		END
		--10	0116	Comprehensive Condominium
		--11	0118	Comprehensive Company
		--12	0351	General Civil Liability
		--14	0171	Diversified Risks
		--16	0115	Robbery
		--19	0114	Dwelling
		--25	0111	Traditional Fire
		--27	0173	Global of Bank
		--32	0750	Judicial Guarantee
		ELSE IF(@LOB_ID IN (10,11,12,14,16,19,25,27,32))
		BEGIN 
				SET @CONSTRUCTION=@CONSTRUCTION_TEXT
				IF(@OCCUPIED_AS IS NOT NULL AND @OCCUPIED_AS >0 and NOT EXISTS(SELECT 1 FROM MNT_OCCUPIED_MASTER NOLOCK WHERE OCCUPIED_ID=@OCCUPIED_AS))
					SET @ERROR_NO =111 --  Occupied as does not exists.
				ELSE IF(@ACTIVITY_TYPE IS NOT NULL AND @ACTIVITY_TYPE >0 and NOT EXISTS(SELECT 1 FROM MNT_ACTIVITY_MASTER NOLOCK WHERE ACTIVITY_ID=@ACTIVITY_TYPE))
					SET @ERROR_NO =112 --  Activity type  does not exists.
				ELSE IF(@CONSTRUCTION IS NOT NULL AND @CONSTRUCTION >0 and NOT EXISTS(SELECT 1 FROM MNT_LOOKUP_VALUES NOLOCK WHERE LOOKUP_UNIQUE_ID =@CONSTRUCTION AND IS_ACTIVE='Y'))
						SET @ERROR_NO =113 --  Construction does not exists.
						
			 
				 
		END
		--15	0981	Individual Personal Accident
		--21	0982	Group Personal Accident for Passengers
		--33	0977	Mortgage
		--34	0993	Group Life
		ELSE IF(@LOB_ID IN( 15,21,33,34))
		BEGIN 
	 
				IF (@STATE_ID IS NOT NULL AND @STATE_ID >0 and NOT EXISTS( SELECT 1 FROM MNT_COUNTRY_STATE_LIST (NOLOCK) WHERE COUNTRY_ID=5 AND STATE_ID=@STATE_ID AND IS_ACTIVE='Y')    )
					SET @ERROR_NO=92      -- WRONG STATE ID   
				ELSE IF(@POSITION_ID IS NOT NULL AND @POSITION_ID >0 and  NOT EXISTS(SELECT 1 FROM MNT_ACTIVITY_MASTER NOLOCK WHERE ACTIVITY_ID=@POSITION_ID))
					SET @ERROR_NO =114 --  Position  does not exists.
				ELSE IF(@MARITAL_STATUS IS NOT NULL AND @MARITAL_STATUS >0 AND NOT EXISTS(SELECT 1 FROM MNT_LOOKUP_VALUES NOLOCK WHERE LOOKUP_UNIQUE_ID=@MARITAL_STATUS AND IS_ACTIVE='Y'))
					SET @ERROR_NO =115 --  Marital status does not exists.	
				ELSE if(NOT EXISTS(SELECT 1 FROM MNT_LOOKUP_VALUES NOLOCK WHERE LOOKUP_UNIQUE_ID=@GENDER AND IS_ACTIVE='Y'))
					SET @ERROR_NO =139	---Please enter valid Gender
				
		END 
		--17	0553	Facultative Liability
		--18	0523	Civil Liability Transportation
		--28	0435	Aeronautic
		--29	0531	Motor
		--30	0589	Dpvat(Cat. 3 e 4)
		--31	0654	Cargo Transportation Civil Liability
		--36	0588	DPVAT(Cat.1,2,9 e 10)
		ELSE IF(@LOB_ID IN(28,17,18,29,31,30,36)) --DONE
		BEGIN 
			 
			IF(@LOB_ID IN(36,30))
			BEGIN
				IF (@STATE_ID IS NOT NULL AND @STATE_ID >0 AND NOT EXISTS( SELECT 1 FROM MNT_COUNTRY_STATE_LIST (NOLOCK) WHERE COUNTRY_ID=5 AND STATE_ID=@STATE_ID AND IS_ACTIVE='Y')    )
					SET @ERROR_NO=92      -- WRONG STATE ID   
				ELSE IF (@LOB_ID IN(36,30) AND @CATEGORY IS NOT NULL AND @CATEGORY >0 AND NOT EXISTS( SELECT 1 FROM MNT_LOOKUP_VALUES WITH(NOLOCK) WHERE LOOKUP_ID=1406 AND  CAST(LOOKUP_VALUE_CODE AS INT)=@CATEGORY AND IS_ACTIVE='Y'))
							SET @ERROR_NO=117      -- Category does not exists		
				
			END
			ELSE
			BEGIN
				IF (@FIPE_CODE IS NOT NULL AND @FIPE_CODE <>'' AND NOT EXISTS( SELECT 1 FROM MNT_FIPE_CODE_MASTER WITH(NOLOCK) WHERE FIPE_CODE=@FIPE_CODE )    )
					SET @ERROR_NO=116      -- Fipe code does not exists
				ELSE IF (@CATEGORY IS NOT NULL AND @CATEGORY >0 AND NOT EXISTS( SELECT 1 FROM MNT_FIPE_CODE_MASTER WITH(NOLOCK) WHERE CATEGORY_AUTO=@CATEGORY )    )
						SET @ERROR_NO=117      -- Category does not exists		
				ELSE IF (@CAPACITY  IS NOT NULL AND @CAPACITY >0 AND NOT EXISTS( SELECT 1 FROM MNT_FIPE_CODE_MASTER WITH(NOLOCK) WHERE CAPACITY=CAPACITY )    )
						SET @ERROR_NO=117      -- Category does not exists		
				ELSE IF( LEN(@YEAR)<4 OR LEN(@YEAR)<>4 OR @YEAR=0 OR @YEAR<1900 OR @YEAR>2100)		
						SET @ERROR_NO= 138  ---Please enter valid Year 
				
				
			END		

		END 
		--20	0621	National Cargo Transport
		--23	0622	International Cargo Transport
		ELSE IF(@LOB_ID IN(23,20))
		 BEGIN
		 
		 SELECT 
					 @ORIGIN_COUNTRY		=ORIGIN_COUNTRY,
					 @ORIGIN_STATE			=ORIGIN_STATE,
					 @DESTINATION_COUNTRY	=DESTINATION_COUNTRY,
					 @DESTINATION_STATE		=DESTINATION_STATE,
					 @CONVEYANCE_TYPE		=CONVEYANCE_TYPE
		 FROM MIG_IL_POLICY_RISK_DETAILS WITH(NOLOCK)  
		 WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS = 0     
		 
		 IF (@ORIGIN_COUNTRY IS NOT NULL AND @ORIGIN_COUNTRY >0 AND @ORIGIN_COUNTRY<>5)    
				SET @ERROR_NO=118      -- Origin country does not exists
		 ELSE IF (@ORIGIN_STATE IS NOT NULL AND @ORIGIN_STATE >0 )
			 BEGIN
			 IF NOT EXISTS( SELECT 1 FROM MNT_COUNTRY_STATE_LIST (NOLOCK) WHERE COUNTRY_ID=5 AND STATE_ID=@ORIGIN_STATE AND IS_ACTIVE='Y')    
					SET @ERROR_NO=119      -- Origin State does not exists
			 END		
		 ELSE IF (@DESTINATION_COUNTRY IS NOT NULL AND @DESTINATION_COUNTRY >0 AND @DESTINATION_COUNTRY=5)    
				SET @ERROR_NO=120      -- Destination country does not exists				
		 ELSE IF (@DESTINATION_STATE IS NOT NULL AND @DESTINATION_STATE >0)
		 BEGIN
			 IF NOT EXISTS( SELECT 1 FROM MNT_COUNTRY_STATE_LIST (NOLOCK) WHERE COUNTRY_ID=5 AND STATE_ID=@DESTINATION_STATE AND IS_ACTIVE='Y')    
					SET @ERROR_NO=121      -- Destination State does not exists		
			 END		
		 ELSE IF (@CONVEYANCE_TYPE IS NOT NULL AND @CONVEYANCE_TYPE >0 )
			 BEGIN
			 IF NOT EXISTS( SELECT 1 FROM MNT_LOOKUP_VALUES (NOLOCK) WHERE LOOKUP_UNIQUE_ID=@CONVEYANCE_TYPE AND IS_ACTIVE='Y')    
					SET @ERROR_NO=122      -- Conveyance Type does not exists		
			 END		
		 END
		--35	1163	Rural Lien
		--37	0746	Rental Surety -- NO VALIDATION FOR THIS PRODUCT
		ELSE IF(@LOB_ID IN(35))
		BEGIN
		
		 SELECT 
					 @MODE					=MODE,
					 @PROPERTY				=PROPERTY,
					 @CULTIVATION			=CULTIVATION,
					 @SUBSIDY_STATE			=SUBSIDY_STATE
		 FROM MIG_IL_POLICY_RISK_DETAILS WITH(NOLOCK)  
		 WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS = 0     
		 
		 IF (@MODE IS NOT NULL AND @MODE >0 AND NOT EXISTS( SELECT 1 FROM MNT_LOOKUP_VALUES (NOLOCK) WHERE  LOOKUP_ID=1407 and CAST(LOOKUP_VALUE_CODE AS INT)=@MODE  AND IS_ACTIVE='Y')    )
			SET @ERROR_NO=123      -- Mode does not exists		
			
		 ELSE IF (@PROPERTY IS NOT NULL AND @PROPERTY >0 AND NOT EXISTS( SELECT 1 FROM MNT_LOOKUP_VALUES (NOLOCK) WHERE  LOOKUP_ID=1408 and CAST(LOOKUP_VALUE_CODE AS INT)=@PROPERTY AND IS_ACTIVE='Y')    )
			SET @ERROR_NO=124      -- Property does not exists		
			
		 ELSE IF (@CULTIVATION IS NOT NULL AND @CULTIVATION >0 AND NOT EXISTS( SELECT 1 FROM MNT_LOOKUP_VALUES (NOLOCK) WHERE LOOKUP_ID=1409 and CAST(LOOKUP_VALUE_CODE AS INT)=@CULTIVATION AND IS_ACTIVE='Y') )
			SET @ERROR_NO=125      -- Cultivation does not exists		
			
		 ELSE IF (@SUBSIDY_STATE IS NOT NULL AND @SUBSIDY_STATE >0 AND NOT EXISTS( SELECT 1 FROM MNT_COUNTRY_STATE_LIST (NOLOCK) WHERE COUNTRY_ID=5 AND STATE_ID=@SUBSIDY_STATE AND IS_ACTIVE='Y')    )
			SET @ERROR_NO=126      -- Subsidy State does not exists
			
		 ELSE IF (@STATE_ID IS NOT NULL AND @STATE_ID >0 AND NOT EXISTS( SELECT 1 FROM MNT_COUNTRY_STATE_LIST (NOLOCK) WHERE COUNTRY_ID=5 AND STATE_ID=@STATE_ID AND IS_ACTIVE='Y') )
					SET  @ERROR_NO=92      -- WRONG STATE ID  
				  
		 END
		 
		
		/*--13	0433	Maritime THERE IS NO VALIDATION FOR THIS PRODUCT
		ELSE IF(@LOB_ID IN(13))
		 BEGIN
		 END
		 
		--22	0520	Personal Accident for Passengers -0520 would be 9821
		--THERE IS NO VALIDATION FOR THIS PRODUCT
		ELSE IF(@LOB_ID IN(22))
		BEGIN 
			 
		END
		*/
		 IF(@ERROR_NO>0)      
			 BEGIN 	
				 -----------------------------------------------------------            
				 -- INSERT ERROR DETAILS      
				 -----------------------------------------------------------       
				 UPDATE MIG_IL_POLICY_RISK_DETAILS    
				 SET    HAS_ERRORS=1            
				 WHERE  IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND       
						IMPORT_SERIAL_NO  = @IMPORT_SERIAL_NO            
			            
				 EXEC  [PROC_MIG_IL_INSERT_IMPORT_ERROR_DETAILS]                       
				 @IMPORT_REQUEST_ID     = @IMPORT_REQUEST_ID,            
				 @IMPORT_SERIAL_NO      = @IMPORT_SERIAL_NO  ,                
				 @ERROR_SOURCE_FILE     = ''     ,            
				 @ERROR_SOURCE_COLUMN   = ''     ,            
				 @ERROR_SOURCE_COLUMN_VALUE= '' ,            
				 @ERROR_ROW_NUMBER      = @IMPORT_SERIAL_NO   ,              
				 @ERROR_TYPES           = @ERROR_NO,         
				 @ACTUAL_RECORD_DATA    = '' ,            
				 @ERROR_MODE            = 'VE',  -- VALIDATION ERROR            
				 @ERROR_SOURCE_TYPE     = 'PRSK'         
		           
			 END    
        
        
		
		SET @COUNTER += 1
	 
	END	--end while  
    
END TRY    
BEGIN CATCH      
       
 SELECT       
    @ERROR_NUMBER    = ERROR_NUMBER(),      
    @ERROR_SEVERITY  = ERROR_SEVERITY(),      
    @ERROR_STATE     = ERROR_STATE(),      
    @ERROR_PROCEDURE = ERROR_PROCEDURE(),      
    @ERROR_LINE   = ERROR_LINE(),      
    @ERROR_MESSAGE   = ERROR_MESSAGE()      
           
  -- CREATING LOG OF EXCEPTION       
  EXEC [PROC_MIG_INSERT_ERROR_LOG]        
  @IMPORT_REQUEST_ID    = @IMPORT_REQUEST_ID      
 ,@IMPORT_SERIAL_NO  = 0      
 ,@ERROR_NUMBER      = @ERROR_NUMBER      
 ,@ERROR_SEVERITY    = @ERROR_SEVERITY      
 ,@ERROR_STATE          = @ERROR_STATE      
 ,@ERROR_PROCEDURE   = @ERROR_PROCEDURE      
 ,@ERROR_LINE      = @ERROR_LINE      
 ,@ERROR_MESSAGE        = @ERROR_MESSAGE      
 ,@INITIAL_LOAD_FLAG    = 'Y'      
        
       
END CATCH      
    
END     
    
 
GO