IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_GETFULLCUSTOMERXML_INFO]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_GETFULLCUSTOMERXML_INFO]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE PROC_GETFULLCUSTOMERXML_INFO 
             

            @CUSTOMER_ID varchar(10), 

            @POLICY_ID varchar(10), 

            @POLICY_VERSION_ID varchar(10), 

			@CALLED_FOR nvarchar(20),			

            @CUSTOMER_XML text --for xml

AS                                          

BEGIN     

INSERT INTO ACT_PREMIUM_NOTICE_PROCCESS_LOG

 

            (

                        CUSTOMER_ID ,

                        POLICY_ID ,

                        POLICY_VERSION_ID,
						
						CALLED_FOR,
						
                        DEC_CUSTOMERXML, 
						
						DATE_TIME

            )            

            VALUES            

            (       

                        @CUSTOMER_ID,

                        @POLICY_ID,

                        @POLICY_VERSION_ID,
						
						@CALLED_FOR,

                        @CUSTOMER_XML,

						GETUTCDATE()

            )

END







GO

