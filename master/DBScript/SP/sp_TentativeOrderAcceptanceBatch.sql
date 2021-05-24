
-- =============================================
-- Author:		wot) Mahfudin
-- Create date: 15/03/2021
-- Description:	Get Tentative Order From T NQC To GPPS U DB
-- =============================================
ALTER PROCEDURE [dbo].[sp_TentativeOrderAcceptanceBatch] 
	-- Add the parameters for the stored procedure here
	@v_companyCode			varchar(4),
	@v_version				varchar(2),
	@v_userId				varchar(50)
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	BEGIN TRY 
	-- for test
	--DECLARE
	--	@v_companyCode			varchar(4)='807B',
	--	@v_version				varchar(2)='TP',
	--	@v_userId				varchar(50)='dummy'

	DECLARE
		@l_n_process_status		smallint = 0,
		@FUNCTION_ID			varchar(100) = 'U01-004',
		@return_message			VARCHAR(255),
		@DATE DATE =			getdate(),
		@SERVERLINK				varchar(100),
		@openquery				varchar(max),
		@TableName				varchar(100),
		@N1_MONTH_Date			Date,
		@N1_MONTH				varchar(10),
		@NR_ERR					INT,
		@process_flag			INT = 0, -- 0 Success 1 Error 2 Warning
		@PROCESS_NAME			varchar(max) = 'Tentative Order Acceptance Process',
		@PROCESS_ID				bigint,
		@FUNCTION_LOCKED		BIT,
		@RUN_PROCESS_ID			VARCHAR(MAX),
		@existing				INT,
		@MSG_TXT				varchar(max),
		@car_pant_cd_string		varchar(2000)

	DECLARE @TB_T_COMPANY TABLE(
		COMPANY_CD varchar(4),
		CAR_PLANT_CD varchar(4)
	);

	DECLARE @o [TentativeOrderTable];

	IF OBJECT_ID('tempdb..##TB_T_TNQC_RESULT_FO') IS NOT NULL
	DROP TABLE dbo.##TB_T_TNQC_RESULT_FO;

	IF OBJECT_ID('tempdb..##TB_T_TNQC_RESULT_ABF') IS NOT NULL
	DROP TABLE dbo.##TB_T_TNQC_RESULT_ABF;

	IF OBJECT_ID('tempdb..##TB_T_TNQC_RESULT_AB') IS NOT NULL
	DROP TABLE dbo.##TB_T_TNQC_RESULT_AB;	

	IF OBJECT_ID('tempdb..##TB_T_TNQC_RESULT_EO') IS NOT NULL
	DROP TABLE dbo.##TB_T_TNQC_RESULT_EO;
	
	IF OBJECT_ID('tempdb..##TB_T_TNQC_LOT_FO') IS NOT NULL
	DROP TABLE dbo.##TB_T_TNQC_LOT_FO;

	IF OBJECT_ID('tempdb..##TB_T_TNQC_LOT_ABF') IS NOT NULL
	DROP TABLE dbo.##TB_T_TNQC_LOT_ABF;

	IF OBJECT_ID('tempdb..##TB_T_TNQC_LOT_AB') IS NOT NULL
	DROP TABLE dbo.##TB_T_TNQC_LOT_AB;
	
	IF OBJECT_ID('tempdb..##TB_T_TNQC_LOT_EO') IS NOT NULL
	DROP TABLE dbo.##TB_T_TNQC_LOT_EO;

	IF OBJECT_ID('tempdb..#TB_TEMP_WARNING_LIST_31') IS NOT NULL
	DROP TABLE dbo.#TB_TEMP_WARNING_LIST_31;

	IF OBJECT_ID('tempdb..#TB_TEMP_WARNING_LIST_31_ALL') IS NOT NULL
	DROP TABLE dbo.#TB_TEMP_WARNING_LIST_31_ALL;	
	
	CREATE TABLE ##TB_T_TNQC_RESULT_FO
		(
			VER						VARCHAR(1),  
			COMPANY_CD              VARCHAR(4),	
			PLANT_CD        		VARCHAR(1),  
			DOCK_CD                 VARCHAR(2),  
			SUPPLIER_CD             VARCHAR(4),  
			SUPPLIER_PLANT_CD       VARCHAR(1),  
			SHIP_DOC_CD             VARCHAR(3),	
			FIRM_PACK_MONTH         VARCHAR(6),  
			CFC                     VARCHAR(4),  
			PART_NO                 VARCHAR(12), 
			LOT_CD                  VARCHAR(2),  
			EXT_CD                  VARCHAR(4),  
			INT_CD                  VARCHAR(4),  
			CTL_KATA                VARCHAR(20), 
			ORD_LOT_SIZE            VARCHAR(5),  
			KANBAN_NO               VARCHAR(4),  
			PART_MATCH_KEY          VARCHAR(6),  
			TNQC_OUT_CYCLE          VARCHAR(1),  
			DATA_ID                 VARCHAR(4),  
			REV		                VARCHAR(4),  
			ORD_TYPE                VARCHAR(1),  
			PAMS_CFC                VARCHAR(4),  
			RE_EXPORT_CD            VARCHAR(1),  
			AICO_FLAG               VARCHAR(1),  
			LOT_PXP_FLAG            VARCHAR(1),  
			SOURCE_CD               VARCHAR(1),  
			IMPORTER_NAME           VARCHAR(3),  
			EXPORTER_NAME           VARCHAR(3),  
			SS_NO                   VARCHAR(2),  
			PAMS_SS_NO              VARCHAR(2),  
			LINE_CD                 VARCHAR(1),  
			DISPLAY_KATA            VARCHAR(20), 
			N_MONTH_ORD_VOL         VARCHAR(8),	
			N1_MONTH_ORD_VOL        VARCHAR(8),	
			N2_MONTH_ORD_VOL        VARCHAR(8),	
			N3_MONTH_ORD_VOL        VARCHAR(8),	
			N_DAY_VOL_01            VARCHAR(7),	
			N_DAY_VOL_02            VARCHAR(7),	
			N_DAY_VOL_03            VARCHAR(7),	
			N_DAY_VOL_04            VARCHAR(7),	
			N_DAY_VOL_05            VARCHAR(7),	
			N_DAY_VOL_06            VARCHAR(7),	
			N_DAY_VOL_07            VARCHAR(7),	
			N_DAY_VOL_08            VARCHAR(7),	
			N_DAY_VOL_09            VARCHAR(7),	
			N_DAY_VOL_10            VARCHAR(7),	
			N_DAY_VOL_11            VARCHAR(7),	
			N_DAY_VOL_12            VARCHAR(7),	
			N_DAY_VOL_13            VARCHAR(7),	
			N_DAY_VOL_14            VARCHAR(7),	
			N_DAY_VOL_15            VARCHAR(7),	
			N_DAY_VOL_16            VARCHAR(7),	
			N_DAY_VOL_17            VARCHAR(7),	
			N_DAY_VOL_18            VARCHAR(7),	
			N_DAY_VOL_19            VARCHAR(7),	
			N_DAY_VOL_20            VARCHAR(7),	
			N_DAY_VOL_21            VARCHAR(7),	
			N_DAY_VOL_22            VARCHAR(7),	
			N_DAY_VOL_23            VARCHAR(7),	
			N_DAY_VOL_24            VARCHAR(7),	
			N_DAY_VOL_25            VARCHAR(7),	
			N_DAY_VOL_26            VARCHAR(7),	
			N_DAY_VOL_27            VARCHAR(7),	
			N_DAY_VOL_28            VARCHAR(7),	
			N_DAY_VOL_29            VARCHAR(7),	
			N_DAY_VOL_30            VARCHAR(7),	
			N_DAY_VOL_31            VARCHAR(7),	
			N1_DAY_VOL_01           VARCHAR(7),	
			N1_DAY_VOL_02           VARCHAR(7),	
			N1_DAY_VOL_03           VARCHAR(7),	
			N1_DAY_VOL_04           VARCHAR(7),	
			N1_DAY_VOL_05           VARCHAR(7),	
			N1_DAY_VOL_06           VARCHAR(7),	
			N1_DAY_VOL_07           VARCHAR(7),	
			N1_DAY_VOL_08           VARCHAR(7),	
			N1_DAY_VOL_09           VARCHAR(7),	
			N1_DAY_VOL_10           VARCHAR(7),	
			N1_DAY_VOL_11           VARCHAR(7),	
			N1_DAY_VOL_12           VARCHAR(7),	
			N1_DAY_VOL_13           VARCHAR(7),	
			N1_DAY_VOL_14           VARCHAR(7),	
			N1_DAY_VOL_15           VARCHAR(7),	
			N1_DAY_VOL_16           VARCHAR(7),	
			N1_DAY_VOL_17           VARCHAR(7),	
			N1_DAY_VOL_18           VARCHAR(7),	
			N1_DAY_VOL_19           VARCHAR(7),	
			N1_DAY_VOL_20           VARCHAR(7),	
			N1_DAY_VOL_21           VARCHAR(7),	
			N1_DAY_VOL_22           VARCHAR(7),	
			N1_DAY_VOL_23           VARCHAR(7),	
			N1_DAY_VOL_24           VARCHAR(7),	
			N1_DAY_VOL_25           VARCHAR(7),	
			N1_DAY_VOL_26           VARCHAR(7),	
			N1_DAY_VOL_27           VARCHAR(7),	
			N1_DAY_VOL_28           VARCHAR(7),	
			N1_DAY_VOL_29           VARCHAR(7),	
			N1_DAY_VOL_30           VARCHAR(7),	
			N1_DAY_VOL_31           VARCHAR(7),	
			SYS_DT                   VARCHAR(8),
			SERIES_NAME              VARCHAR(20),
			LIFE_CYCLE_CD            VARCHAR(1), 
			NMIN1_MONTH_VOL          VARCHAR(8),
			N_MONTH_VOL              VARCHAR(8),
			N1_MONTH_VOL             VARCHAR(8),
			N2_MONTH_VOL             VARCHAR(8),
			BACK_ADJUST_SIGN         VARCHAR(8), 
			BACK_ADJUST_QTY          VARCHAR(8),
			RATIO_LAST_MONTH         VARCHAR(3), 
			N_MONTH_DAY_VOL_MAX      VARCHAR(7), 
			N_MONTH_DAY_VOL_MIN      VARCHAR(7), 
			N_MONTH_DAY_VOL_VAR      VARCHAR(3), 
			SUPPLIER_NAME            VARCHAR(40),
			PART_NAME                VARCHAR(40),
			JCFD                     VARCHAR(5), 
			CONTA_CLASS              VARCHAR(1), 
			BULK_LOT_CD              VARCHAR(2), 
			TERMINATE_CD             VARCHAR(1), 
			CREATED_DT               DATE    ,         
			CHANGED_DT               DATE         ,    
			CREATED_BY               VARCHAR(25),
			CHANGED_BY               VARCHAR(25)
		)

	CREATE TABLE ##TB_T_TNQC_RESULT_ABF
		(
			VER						VARCHAR(1),  
			COMPANY_CD              VARCHAR(4),	
			PLANT_CD                VARCHAR(1),  
			DOCK_CD                 VARCHAR(2),  
			SUPPLIER_CD             VARCHAR(4),  
			SUPPLIER_PLANT_CD       VARCHAR(1),  
			SHIP_DOC_CD             VARCHAR(3),	
			FIRM_PACK_MONTH         VARCHAR(6),  
			CFC                     VARCHAR(4),  
			PART_NO                 VARCHAR(12), 
			LOT_CD                  VARCHAR(2),  
			EXT_CD                  VARCHAR(4),  
			INT_CD                  VARCHAR(4),  
			CTL_KATA                VARCHAR(20), 
			ORD_LOT_SIZE            VARCHAR(5),  
			KANBAN_NO               VARCHAR(4),  
			PART_MATCH_KEY          VARCHAR(6),  
			TNQC_OUT_CYCLE          VARCHAR(1),  
			DATA_ID                 VARCHAR(4),  
			REV                     VARCHAR(4),  
			ORD_TYPE                VARCHAR(1),  
			PAMS_CFC                VARCHAR(4),  
			RE_EXPORT_CD            VARCHAR(1),  
			AICO_FLAG               VARCHAR(1),  
			LOT_PXP_FLAG            VARCHAR(1),  
			SOURCE_CD               VARCHAR(1),  
			IMPORTER_NAME           VARCHAR(3),  
			EXPORTER_NAME           VARCHAR(3),  
			SS_NO                   VARCHAR(2),  
			PAMS_SS_NO              VARCHAR(2),  
			LINE_CD                 VARCHAR(1),  
			DISPLAY_KATA            VARCHAR(20), 
			N_MONTH_ORD_VOL         NUMERIC(8,0),	  
			N1_MONTH_ORD_VOL        NUMERIC(8,0),	  
			N2_MONTH_ORD_VOL        NUMERIC(8,0),	  
			N3_MONTH_ORD_VOL        NUMERIC(8,0),	  
			N_DAY_VOL_01            NUMERIC(7,0),	  
			N_DAY_VOL_02            NUMERIC(7,0),	  
			N_DAY_VOL_03            NUMERIC(7,0),	  
			N_DAY_VOL_04            NUMERIC(7,0),	  
			N_DAY_VOL_05            NUMERIC(7,0),	  
			N_DAY_VOL_06            NUMERIC(7,0),	  
			N_DAY_VOL_07            NUMERIC(7,0),	  
			N_DAY_VOL_08            NUMERIC(7,0),	  
			N_DAY_VOL_09            NUMERIC(7,0),	  
			N_DAY_VOL_10            NUMERIC(7,0),	  
			N_DAY_VOL_11            NUMERIC(7,0),	  
			N_DAY_VOL_12            NUMERIC(7,0),	  
			N_DAY_VOL_13            NUMERIC(7,0),	  
			N_DAY_VOL_14            NUMERIC(7,0),	  
			N_DAY_VOL_15            NUMERIC(7,0),	  
			N_DAY_VOL_16            NUMERIC(7,0),	  
			N_DAY_VOL_17            NUMERIC(7,0),	  
			N_DAY_VOL_18            NUMERIC(7,0),	  
			N_DAY_VOL_19            NUMERIC(7,0),	  
			N_DAY_VOL_20            NUMERIC(7,0),	  
			N_DAY_VOL_21            NUMERIC(7,0),	  
			N_DAY_VOL_22            NUMERIC(7,0),	  
			N_DAY_VOL_23            NUMERIC(7,0),	  
			N_DAY_VOL_24            NUMERIC(7,0),	  
			N_DAY_VOL_25            NUMERIC(7,0),	  
			N_DAY_VOL_26            NUMERIC(7,0),	  
			N_DAY_VOL_27            NUMERIC(7,0),	  
			N_DAY_VOL_28            NUMERIC(7,0),	  
			N_DAY_VOL_29            NUMERIC(7,0),	  
			N_DAY_VOL_30            NUMERIC(7,0),	  
			N_DAY_VOL_31            NUMERIC(7,0),	  
			N1_DAY_VOL_01           NUMERIC(7,0),	  
			N1_DAY_VOL_02           NUMERIC(7,0),	  
			N1_DAY_VOL_03           NUMERIC(7,0),	  
			N1_DAY_VOL_04           NUMERIC(7,0),	  
			N1_DAY_VOL_05           NUMERIC(7,0),	  
			N1_DAY_VOL_06           NUMERIC(7,0),	  
			N1_DAY_VOL_07           NUMERIC(7,0),	  
			N1_DAY_VOL_08           NUMERIC(7,0),	  
			N1_DAY_VOL_09           NUMERIC(7,0),	  
			N1_DAY_VOL_10           NUMERIC(7,0),	  
			N1_DAY_VOL_11           NUMERIC(7,0),	  
			N1_DAY_VOL_12           NUMERIC(7,0),	  
			N1_DAY_VOL_13           NUMERIC(7,0),	  
			N1_DAY_VOL_14           NUMERIC(7,0),	  
			N1_DAY_VOL_15           NUMERIC(7,0),	  
			N1_DAY_VOL_16           NUMERIC(7,0),	  
			N1_DAY_VOL_17           NUMERIC(7,0),	  
			N1_DAY_VOL_18           NUMERIC(7,0),	  
			N1_DAY_VOL_19           NUMERIC(7,0),	  
			N1_DAY_VOL_20           NUMERIC(7,0),	  
			N1_DAY_VOL_21           NUMERIC(7,0),	  
			N1_DAY_VOL_22           NUMERIC(7,0),	  
			N1_DAY_VOL_23           NUMERIC(7,0),	  
			N1_DAY_VOL_24           NUMERIC(7,0),	  
			N1_DAY_VOL_25           NUMERIC(7,0),	  
			N1_DAY_VOL_26           NUMERIC(7,0),	  
			N1_DAY_VOL_27           NUMERIC(7,0),	  
			N1_DAY_VOL_28           NUMERIC(7,0),	  
			N1_DAY_VOL_29           NUMERIC(7,0),	  
			N1_DAY_VOL_30           NUMERIC(7,0),	  
			N1_DAY_VOL_31           NUMERIC(7,0),	  
			SYS_DT                  VARCHAR(8),	
			SERIES_NAME             VARCHAR(20), 
			LIFE_CYCLE_CD           VARCHAR(1), 
			NMIN1_MONTH_VOL         NUMERIC(8,0),	  
			N_MONTH_VOL             NUMERIC(8,0),	  
			N1_MONTH_VOL            NUMERIC(8,0),	  
			N2_MONTH_VOL            NUMERIC(8,0),	  
			BACK_ADJUST_SIGN        VARCHAR(8),  
			BACK_ADJUST_QTY         NUMERIC(8,0),	  
			RATION                  VARCHAR(3),  
			N_MONTH_DAY_VOL_MAX     NUMERIC(7,0),      
			N_MONTH_DAY_VOL_MIN     NUMERIC(7,0),      
			N_MONTH_DAY_VOL_VAR     NUMERIC(3,0),      
			SUPPLIER_NAME           VARCHAR(40), 
			PART_NAME               VARCHAR(40), 
			JCFD                    VARCHAR(5),  
			CONTA_CLASS             VARCHAR(1),  
			BULK_LOT_CD             VARCHAR(2),  
			TERMINATE_CD            VARCHAR(1),  
			CREATED_DT              DATE,              
			CHANGED_DT              DATE  ,            
			CREATED_BY              VARCHAR(25),
			CHANGED_BY              VARCHAR(25)
		)

	CREATE TABLE ##TB_T_TNQC_RESULT_AB
		(
			VER						VARCHAR(1),
			COMPANY_CD              VARCHAR(4),	
			PLANT_CD                VARCHAR(1),
			DOCK_CD                 VARCHAR(2),
			SUPPLIER_CD             VARCHAR(4),
			SUPPLIER_PLANT_CD       VARCHAR(1),
			SHIP_DOC_CD             VARCHAR(3),	
			FIRM_PACK_MONTH         VARCHAR(6),
			CFC                     VARCHAR(4),
			PART_NO                 VARCHAR(12),    
			LOT_CD                  VARCHAR(2),
			EXT_CD                  VARCHAR(4),
			INT_CD                  VARCHAR(4),
			CTL_KATA                VARCHAR(20),    
			ORD_LOT_SIZE            VARCHAR(5),
			KANBAN_NO               VARCHAR(4),
			PART_MATCH_KEY          VARCHAR(6),
			TNQC_OUT_CYCLE          VARCHAR(1),
			DATA_ID                 VARCHAR(4),
			REV                     VARCHAR(4),
			ORD_TYPE                VARCHAR(1),
			PAMS_CFC                VARCHAR(4),
			RE_EXPORT_CD            VARCHAR(1),
			AICO_FLAG               VARCHAR(1),
			LOT_PXP_FLAG            VARCHAR(1),
			SOURCE_CD               VARCHAR(1),
			IMPORTER_NAME           VARCHAR(3),
			EXPORTER_NAME           VARCHAR(3),
			SS_NO                   VARCHAR(2),
			PAMS_SS_NO              VARCHAR(2),
			LINE_CD                 VARCHAR(1),
			DISPLAY_KATA            VARCHAR(20),
			N_MONTH_ORD_VOL         NUMERIC(8,0),	      
			N1_MONTH_ORD_VOL        NUMERIC(8,0),	      
			N2_MONTH_ORD_VOL        NUMERIC(8,0),	      
			N3_MONTH_ORD_VOL        NUMERIC(8,0),	      
			N_DAY_VOL_01            NUMERIC(7,0),	      
			N_DAY_VOL_02            NUMERIC(7,0),	      
			N_DAY_VOL_03            NUMERIC(7,0),	      
			N_DAY_VOL_04            NUMERIC(7,0),	      
			N_DAY_VOL_05            NUMERIC(7,0),	      
			N_DAY_VOL_06            NUMERIC(7,0),	      
			N_DAY_VOL_07            NUMERIC(7,0),	      
			N_DAY_VOL_08            NUMERIC(7,0),	      
			N_DAY_VOL_09            NUMERIC(7,0),	      
			N_DAY_VOL_10            NUMERIC(7,0),	      
			N_DAY_VOL_11            NUMERIC(7,0),	      
			N_DAY_VOL_12            NUMERIC(7,0),	      
			N_DAY_VOL_13            NUMERIC(7,0),	      
			N_DAY_VOL_14            NUMERIC(7,0),	      
			N_DAY_VOL_15            NUMERIC(7,0),	      
			N_DAY_VOL_16            NUMERIC(7,0),	      
			N_DAY_VOL_17            NUMERIC(7,0),	      
			N_DAY_VOL_18            NUMERIC(7,0),	      
			N_DAY_VOL_19            NUMERIC(7,0),	      
			N_DAY_VOL_20            NUMERIC(7,0),	      
			N_DAY_VOL_21            NUMERIC(7,0),	      
			N_DAY_VOL_22            NUMERIC(7,0),	      
			N_DAY_VOL_23            NUMERIC(7,0),	      
			N_DAY_VOL_24            NUMERIC(7,0),	      
			N_DAY_VOL_25            NUMERIC(7,0),	      
			N_DAY_VOL_26            NUMERIC(7,0),	      
			N_DAY_VOL_27            NUMERIC(7,0),	      
			N_DAY_VOL_28            NUMERIC(7,0),	      
			N_DAY_VOL_29            NUMERIC(7,0),	      
			N_DAY_VOL_30            NUMERIC(7,0),	      
			N_DAY_VOL_31            NUMERIC(7,0),	      
			N1_DAY_VOL_01           NUMERIC(7,0),	      
			N1_DAY_VOL_02           NUMERIC(7,0),	      
			N1_DAY_VOL_03           NUMERIC(7,0),	      
			N1_DAY_VOL_04           NUMERIC(7,0),	      
			N1_DAY_VOL_05           NUMERIC(7,0),	      
			N1_DAY_VOL_06           NUMERIC(7,0),	      
			N1_DAY_VOL_07           NUMERIC(7,0),	      
			N1_DAY_VOL_08           NUMERIC(7,0),	      
			N1_DAY_VOL_09           NUMERIC(7,0),	      
			N1_DAY_VOL_10           NUMERIC(7,0),	      
			N1_DAY_VOL_11           NUMERIC(7,0),	      
			N1_DAY_VOL_12           NUMERIC(7,0),	      
			N1_DAY_VOL_13           NUMERIC(7,0),	      
			N1_DAY_VOL_14           NUMERIC(7,0),	      
			N1_DAY_VOL_15           NUMERIC(7,0),	      
			N1_DAY_VOL_16           NUMERIC(7,0),	      
			N1_DAY_VOL_17           NUMERIC(7,0),	      
			N1_DAY_VOL_18           NUMERIC(7,0),	      
			N1_DAY_VOL_19           NUMERIC(7,0),	      
			N1_DAY_VOL_20           NUMERIC(7,0),	      
			N1_DAY_VOL_21           NUMERIC(7,0),	      
			N1_DAY_VOL_22           NUMERIC(7,0),	      
			N1_DAY_VOL_23           NUMERIC(7,0),	      
			N1_DAY_VOL_24           NUMERIC(7,0),	      
			N1_DAY_VOL_25           NUMERIC(7,0),	      
			N1_DAY_VOL_26           NUMERIC(7,0),	      
			N1_DAY_VOL_27           NUMERIC(7,0),	      
			N1_DAY_VOL_28           NUMERIC(7,0),	      
			N1_DAY_VOL_29           NUMERIC(7,0),	      
			N1_DAY_VOL_30           NUMERIC(7,0),	      
			N1_DAY_VOL_31           NUMERIC(7,0),	      
			SYS_DT                  VARCHAR(8),	
			SERIES_NAME             VARCHAR(20),    
			LIFE_CYCLE_CD           VARCHAR(1),	  
			NMIN1_MONTH_VOL         NUMERIC(8,0),	      
			N_MONTH_VOL             NUMERIC(8,0),	      
			N1_MONTH_VOL            NUMERIC(8,0),	      
			N2_MONTH_VOL            NUMERIC(8,0),	      
			BACK_ADJUST_SIGN        VARCHAR(8),
			BACK_ADJUST_QTY         NUMERIC(8,0),	      
			RATIO_LAST_MONTH        VARCHAR(3),
			N_MONTH_DAY_VOL_MAX     NUMERIC(7,0),          
			N_MONTH_DAY_VOL_MIN     NUMERIC(7,0),          
			N_MONTH_DAY_VOL_VAR     NUMERIC(3,0),          
			SUPPLIER_NAME           VARCHAR(40),    
			PART_NAME               VARCHAR(40),    
			JCFD                    VARCHAR(5),
			CONTA_CLASS             VARCHAR(1),
			BULK_LOT_CD             VARCHAR(2),
			TERMINATE_CD            VARCHAR(1),
			CREATED_DT              DATE    ,              
			CHANGED_DT              DATE     ,             
			CREATED_BY              VARCHAR(25),
			CHANGED_BY              VARCHAR(25)
		)

	CREATE TABLE ##TB_T_TNQC_RESULT_EO
		(
			VER						VARCHAR(1),       
			COMPANY_CD              VARCHAR(4),		
			PLANT_CD                VARCHAR(1),       
			DOCK_CD                 VARCHAR(2),       
			SUPPLIER_CD             VARCHAR(4),       
			SUPPLIER_PLANT_CD       VARCHAR(1),       
			SHIP_DOC_CD             VARCHAR(3),		
			FIRM_PACK_MONTH         VARCHAR(6),       
			CFC                     VARCHAR(4),       
			PART_NO                 VARCHAR(12),      
			LOT_CD                  VARCHAR(2),       
			EXT_CD                  VARCHAR(4),       
			INT_CD                  VARCHAR(4),       
			CTL_KATA                VARCHAR(20),      
			ORD_LOT_SIZE            VARCHAR(5),       
			KANBAN_NO               VARCHAR(4),       
			PART_MATCH_KEY          VARCHAR(6),       
			TNQC_OUT_CYCLE          VARCHAR(1),       
			DATA_ID                 VARCHAR(4),       
			REV                     VARCHAR(4),       
			ORD_TYPE                VARCHAR(1),       
			PAMS_CFC                VARCHAR(4),       
			RE_EXPORT_CD            VARCHAR(1),       
			AICO_FLAG               VARCHAR(1),       
			LOT_PXP_FLAG            VARCHAR(1),       
			SOURCE_CD               VARCHAR(1),       
			IMPORTER_NAME           VARCHAR(3),       
			EXPORTER_NAME           VARCHAR(3),       
			SS_NO                   VARCHAR(2),       
			PAMS_SS_NO              VARCHAR(2),       
			LINE_CD                 VARCHAR(1),       
			DISPLAY_KATA            VARCHAR(20),      
			N_MONTH_ORD_VOL         NUMERIC(8,0),	       
			N1_MONTH_ORD_VOL        NUMERIC(8,0),	       
			N2_MONTH_ORD_VOL        NUMERIC(8,0),	       
			N3_MONTH_ORD_VOL        NUMERIC(8,0),	       
			N_DAY_VOL_01            NUMERIC(7,0),	       
			N_DAY_VOL_02            NUMERIC(7,0),	       
			N_DAY_VOL_03            NUMERIC(7,0),	       
			N_DAY_VOL_04            NUMERIC(7,0),	       
			N_DAY_VOL_05            NUMERIC(7,0),	       
			N_DAY_VOL_06            NUMERIC(7,0),	       
			N_DAY_VOL_07            NUMERIC(7,0),	       
			N_DAY_VOL_08            NUMERIC(7,0),	       
			N_DAY_VOL_09            NUMERIC(7,0),	       
			N_DAY_VOL_10            NUMERIC(7,0),	       
			N_DAY_VOL_11            NUMERIC(7,0),	       
			N_DAY_VOL_12            NUMERIC(7,0),	       
			N_DAY_VOL_13            NUMERIC(7,0),	       
			N_DAY_VOL_14            NUMERIC(7,0),	       
			N_DAY_VOL_15            NUMERIC(7,0),	       
			N_DAY_VOL_16            NUMERIC(7,0),	       
			N_DAY_VOL_17            NUMERIC(7,0),	       
			N_DAY_VOL_18            NUMERIC(7,0),	       
			N_DAY_VOL_19            NUMERIC(7,0),	       
			N_DAY_VOL_20            NUMERIC(7,0),	       
			N_DAY_VOL_21            NUMERIC(7,0),	       
			N_DAY_VOL_22            NUMERIC(7,0),	       
			N_DAY_VOL_23            NUMERIC(7,0),	       
			N_DAY_VOL_24            NUMERIC(7,0),	       
			N_DAY_VOL_25            NUMERIC(7,0),	       
			N_DAY_VOL_26            NUMERIC(7,0),	       
			N_DAY_VOL_27            NUMERIC(7,0),	       
			N_DAY_VOL_28            NUMERIC(7,0),	       
			N_DAY_VOL_29            NUMERIC(7,0),	       
			N_DAY_VOL_30            NUMERIC(7,0),	       
			N_DAY_VOL_31            NUMERIC(7,0),	       
			N1_DAY_VOL_01           NUMERIC(7,0),	       
			N1_DAY_VOL_02           NUMERIC(7,0),	       
			N1_DAY_VOL_03           NUMERIC(7,0),	       
			N1_DAY_VOL_04           NUMERIC(7,0),	       
			N1_DAY_VOL_05           NUMERIC(7,0),	       
			N1_DAY_VOL_06           NUMERIC(7,0),	       
			N1_DAY_VOL_07           NUMERIC(7,0),	       
			N1_DAY_VOL_08           NUMERIC(7,0),	       
			N1_DAY_VOL_09           NUMERIC(7,0),	       
			N1_DAY_VOL_10           NUMERIC(7,0),	       
			N1_DAY_VOL_11           NUMERIC(7,0),	       
			N1_DAY_VOL_12           NUMERIC(7,0),	       
			N1_DAY_VOL_13           NUMERIC(7,0),	       
			N1_DAY_VOL_14           NUMERIC(7,0),	       
			N1_DAY_VOL_15           NUMERIC(7,0),	       
			N1_DAY_VOL_16           NUMERIC(7,0),	       
			N1_DAY_VOL_17           NUMERIC(7,0),	       
			N1_DAY_VOL_18           NUMERIC(7,0),	       
			N1_DAY_VOL_19           NUMERIC(7,0),	       
			N1_DAY_VOL_20           NUMERIC(7,0),	       
			N1_DAY_VOL_21           NUMERIC(7,0),	       
			N1_DAY_VOL_22           NUMERIC(7,0),	       
			N1_DAY_VOL_23           NUMERIC(7,0),	       
			N1_DAY_VOL_24           NUMERIC(7,0),	       
			N1_DAY_VOL_25           NUMERIC(7,0),	       
			N1_DAY_VOL_26           NUMERIC(7,0),	       
			N1_DAY_VOL_27           NUMERIC(7,0),	       
			N1_DAY_VOL_28           NUMERIC(7,0),	       
			N1_DAY_VOL_29           NUMERIC(7,0),	       
			N1_DAY_VOL_30           NUMERIC(7,0),	       
			N1_DAY_VOL_31           NUMERIC(7,0),	       
			SYS_DT                  VARCHAR(8),		
			SERIES_NAME             VARCHAR(20),      
			LIFE_CYCLE_CD           VARCHAR(1), 	   
			NMIN1_MONTH_VOL         NUMERIC(8,0),	       
			N_MONTH_VOL             NUMERIC(8,0),	       
			N1_MONTH_VOL            NUMERIC(8,0),	       
			N2_MONTH_VOL            NUMERIC(8,0),	       
			BACK_ADJUST_SIGN        VARCHAR(8),       
			BACK_ADJUST_QTY         NUMERIC(8,0),	       
			RATIO_LAST_MONTH        VARCHAR(3),       
			N_MONTH_DAY_VOL_MAX     NUMERIC(7,0),           
			N_MONTH_DAY_VOL_MIN     NUMERIC(7,0),           
			N_MONTH_DAY_VOL_VAR     NUMERIC(3,0),           
			SUPPLIER_NAME           VARCHAR(40),      
			PART_NAME               VARCHAR(40),      
			JCFD                    VARCHAR(5),       
			CONTA_CLASS             VARCHAR(1),       
			BULK_LOT_CD             VARCHAR(2),       
			TERMINATE_CD            VARCHAR(1),       
			CREATED_DT              DATE,                   
			CHANGED_DT              DATE,                   
			CREATED_BY              VARCHAR(25),
			CHANGED_BY              VARCHAR(25),
		)

	CREATE TABLE ##TB_T_TNQC_LOT_FO
		(
			VER						VARCHAR(1),         
			COMPANY_CD              VARCHAR(4),		 
			PLANT_CD                VARCHAR(1),         
			DOCK_CD                 VARCHAR(2),         
			SUPPLIER_CD             VARCHAR(4),         
			SUPPLIER_PLANT_CD       VARCHAR(1),         
			SHIP_DOC_CD             VARCHAR(3),		
			FIRM_PACK_MONTH         VARCHAR(6),         
			CFC                     VARCHAR(4),         
			PART_NO                 VARCHAR(12),        
			LOT_CD                  VARCHAR(2),         
			EXT_CD                  VARCHAR(4),         
			INT_CD                  VARCHAR(4),         
			CTL_KATA                VARCHAR(20),        
			ORD_LOT_SIZE            VARCHAR(5),         
			KANBAN_NO               VARCHAR(4),         
			PART_MATCH_KEY          VARCHAR(6),         
			TNQC_OUT_CYCLE          VARCHAR(1),         
			DATA_ID                 VARCHAR(4),         
			REV                     VARCHAR(4),         
			ORD_TYPE                VARCHAR(1),         
			PAMS_CFC                VARCHAR(4),         
			RE_EXPORT_CD            VARCHAR(1),         
			AICO_FLAG               VARCHAR(1),         
			LOT_PXP_FLAG            VARCHAR(1),         
			SOURCE_CD               VARCHAR(1),         
			IMPORTER_NAME           VARCHAR(3),         
			EXPORTER_NAME           VARCHAR(3),         
			SS_NO                   VARCHAR(2),         
			PAMS_SS_NO              VARCHAR(2),         
			LINE_CD                 VARCHAR(1),         
			DISPLAY_KATA            VARCHAR(20),        
			N_MONTH_ORD_VOL         NUMERIC(8,0),	         
			N1_MONTH_ORD_VOL        NUMERIC(8,0),	         
			N2_MONTH_ORD_VOL        NUMERIC(8,0),	         
			N3_MONTH_ORD_VOL        NUMERIC(8,0),	         
			N_DAY_VOL_01            NUMERIC(7,0),	         
			N_DAY_VOL_02            NUMERIC(7,0),	         
			N_DAY_VOL_03            NUMERIC(7,0),	         
			N_DAY_VOL_04            NUMERIC(7,0),	         
			N_DAY_VOL_05            NUMERIC(7,0),	         
			N_DAY_VOL_06            NUMERIC(7,0),	         
			N_DAY_VOL_07            NUMERIC(7,0),	         
			N_DAY_VOL_08            NUMERIC(7,0),	         
			N_DAY_VOL_09            NUMERIC(7,0),	         
			N_DAY_VOL_10            NUMERIC(7,0),	         
			N_DAY_VOL_11            NUMERIC(7,0),	         
			N_DAY_VOL_12            NUMERIC(7,0),	         
			N_DAY_VOL_13            NUMERIC(7,0),	         
			N_DAY_VOL_14            NUMERIC(7,0),	         
			N_DAY_VOL_15            NUMERIC(7,0),	         
			N_DAY_VOL_16            NUMERIC(7,0),	         
			N_DAY_VOL_17            NUMERIC(7,0),	         
			N_DAY_VOL_18            NUMERIC(7,0),	         
			N_DAY_VOL_19            NUMERIC(7,0),	         
			N_DAY_VOL_20            NUMERIC(7,0),	         
			N_DAY_VOL_21            NUMERIC(7,0),	         
			N_DAY_VOL_22            NUMERIC(7,0),	         
			N_DAY_VOL_23            NUMERIC(7,0),	         
			N_DAY_VOL_24            NUMERIC(7,0),	         
			N_DAY_VOL_25            NUMERIC(7,0),	         
			N_DAY_VOL_26            NUMERIC(7,0),	         
			N_DAY_VOL_27            NUMERIC(7,0),	         
			N_DAY_VOL_28            NUMERIC(7,0),	         
			N_DAY_VOL_29            NUMERIC(7,0),	         
			N_DAY_VOL_30            NUMERIC(7,0),	         
			N_DAY_VOL_31            NUMERIC(7,0),	         
			N1_DAY_VOL_01           NUMERIC(7,0),	         
			N1_DAY_VOL_02           NUMERIC(7,0),	         
			N1_DAY_VOL_03           NUMERIC(7,0),	         
			N1_DAY_VOL_04           NUMERIC(7,0),	         
			N1_DAY_VOL_05           NUMERIC(7,0),	         
			N1_DAY_VOL_06           NUMERIC(7,0),	         
			N1_DAY_VOL_07           NUMERIC(7,0),	         
			N1_DAY_VOL_08           NUMERIC(7,0),	         
			N1_DAY_VOL_09           NUMERIC(7,0),	         
			N1_DAY_VOL_10           NUMERIC(7,0),	         
			N1_DAY_VOL_11           NUMERIC(7,0),	         
			N1_DAY_VOL_12           NUMERIC(7,0),	         
			N1_DAY_VOL_13           NUMERIC(7,0),	         
			N1_DAY_VOL_14           NUMERIC(7,0),	         
			N1_DAY_VOL_15           NUMERIC(7,0),	         
			N1_DAY_VOL_16           NUMERIC(7,0),	         
			N1_DAY_VOL_17           NUMERIC(7,0),	         
			N1_DAY_VOL_18           NUMERIC(7,0),	         
			N1_DAY_VOL_19           NUMERIC(7,0),	         
			N1_DAY_VOL_20           NUMERIC(7,0),	         
			N1_DAY_VOL_21           NUMERIC(7,0),	         
			N1_DAY_VOL_22           NUMERIC(7,0),	         
			N1_DAY_VOL_23           NUMERIC(7,0),	         
			N1_DAY_VOL_24           NUMERIC(7,0),	         
			N1_DAY_VOL_25           NUMERIC(7,0),	         
			N1_DAY_VOL_26           NUMERIC(7,0),	         
			N1_DAY_VOL_27           NUMERIC(7,0),	         
			N1_DAY_VOL_28           NUMERIC(7,0),	         
			N1_DAY_VOL_29           NUMERIC(7,0),	         
			N1_DAY_VOL_30           NUMERIC(7,0),	         
			N1_DAY_VOL_31           NUMERIC(7,0),	         
			SYS_DT                  VARCHAR(8),		 
			SERIES_NAME             VARCHAR(20),        
			LIFE_CYCLE_CD           VARCHAR(1), 	     
			NMIN1_MONTH_VOL         NUMERIC(8,0),	         
			N_MONTH_VOL             NUMERIC(8,0),	         
			N1_MONTH_VOL            NUMERIC(8,0),	         
			N2_MONTH_VOL            NUMERIC(8,0),	         
			BACK_ADJUST_QTY         NUMERIC(8,0),	         
			RATIO_LAST_MONTH        VARCHAR(3),         
			N_MONTH_DAY_VOL_MAX     NUMERIC(7,0),             
			N_MONTH_DAY_VOL_MIN     NUMERIC(7,0),             
			N_MONTH_DAY_VOL_VAR     NUMERIC(3,0),             
			SUPPLIER_NAME           VARCHAR(40),        
			PART_NAME               VARCHAR(40),        
			JCFD                    VARCHAR(5),         
			CONTA_CLASS             VARCHAR(1),         
			BULK_LOT_CD             VARCHAR(2),         
			TERMINATE_CD            VARCHAR(1),         
			CREATED_DT              DATE ,                    
			CHANGED_DT              DATE   ,                  
			CREATED_BY              VARCHAR(25),
			CHANGED_BY              VARCHAR(25)
		)

	CREATE TABLE ##TB_T_TNQC_LOT_ABF
		(
			VER						VARCHAR(1),        
			COMPANY_CD              VARCHAR(4),		
			PLANT_CD                VARCHAR(1),        
			DOCK_CD                 VARCHAR(2),        
			SUPPLIER_CD             VARCHAR(4),        
			SUPPLIER_PLANT_CD       VARCHAR(1),        
			SHIP_DOC_CD             VARCHAR(3),		
			FIRM_PACK_MONTH         VARCHAR(6),        
			CFC                     VARCHAR(4),        
			PART_NO                 VARCHAR(12),       
			LOT_CD                  VARCHAR(2),        
			EXT_CD                  VARCHAR(4),        
			INT_CD                  VARCHAR(4),        
			CTL_KATA                VARCHAR(20),       
			ORD_LOT_SIZE            VARCHAR(5),        
			KANBAN_NO               VARCHAR(4),        
			PART_MATCH_KEY          VARCHAR(6),        
			TNQC_OUT_CYCLE          VARCHAR(1),        
			DATA_ID                 VARCHAR(4),        
			REV                     VARCHAR(4),        
			ORD_TYPE                VARCHAR(1),        
			PAMS_CFC                VARCHAR(4),        
			RE_EXPORT_CD            VARCHAR(1),        
			AICO_FLAG               VARCHAR(1),        
			LOT_PXP_FLAG            VARCHAR(1),        
			SOURCE_CD               VARCHAR(1),        
			IMPORTER_NAME           VARCHAR(3),        
			EXPORTER_NAME           VARCHAR(3),        
			SS_NO                   VARCHAR(2),        
			PAMS_SS_NO              VARCHAR(2),        
			LINE_CD                 VARCHAR(1),        
			DISPLAY_KATA            VARCHAR(20),       
			N_MONTH_ORD_VOL         NUMERIC(8,0),	        
			N1_MONTH_ORD_VOL        NUMERIC(8,0),	        
			N2_MONTH_ORD_VOL        NUMERIC(8,0),	        
			N3_MONTH_ORD_VOL        NUMERIC(8,0),	        
			N_DAY_VOL_01            NUMERIC(7,0),	        
			N_DAY_VOL_02            NUMERIC(7,0),	        
			N_DAY_VOL_03            NUMERIC(7,0),	        
			N_DAY_VOL_04            NUMERIC(7,0),	        
			N_DAY_VOL_05            NUMERIC(7,0),	        
			N_DAY_VOL_06            NUMERIC(7,0),	        
			N_DAY_VOL_07            NUMERIC(7,0),	        
			N_DAY_VOL_08            NUMERIC(7,0),	        
			N_DAY_VOL_09            NUMERIC(7,0),	        
			N_DAY_VOL_10            NUMERIC(7,0),	        
			N_DAY_VOL_11            NUMERIC(7,0),	        
			N_DAY_VOL_12            NUMERIC(7,0),	        
			N_DAY_VOL_13            NUMERIC(7,0),	        
			N_DAY_VOL_14            NUMERIC(7,0),	        
			N_DAY_VOL_15            NUMERIC(7,0),	        
			N_DAY_VOL_16            NUMERIC(7,0),	        
			N_DAY_VOL_17            NUMERIC(7,0),	        
			N_DAY_VOL_18            NUMERIC(7,0),	        
			N_DAY_VOL_19            NUMERIC(7,0),	        
			N_DAY_VOL_20            NUMERIC(7,0),	        
			N_DAY_VOL_21            NUMERIC(7,0),	        
			N_DAY_VOL_22            NUMERIC(7,0),	        
			N_DAY_VOL_23            NUMERIC(7,0),	        
			N_DAY_VOL_24            NUMERIC(7,0),	        
			N_DAY_VOL_25            NUMERIC(7,0),	        
			N_DAY_VOL_26            NUMERIC(7,0),	        
			N_DAY_VOL_27            NUMERIC(7,0),	        
			N_DAY_VOL_28            NUMERIC(7,0),	        
			N_DAY_VOL_29            NUMERIC(7,0),	        
			N_DAY_VOL_30            NUMERIC(7,0),	        
			N_DAY_VOL_31            NUMERIC(7,0),	        
			N1_DAY_VOL_01           NUMERIC(7,0),	        
			N1_DAY_VOL_02           NUMERIC(7,0),	        
			N1_DAY_VOL_03           NUMERIC(7,0),	        
			N1_DAY_VOL_04           NUMERIC(7,0),	        
			N1_DAY_VOL_05           NUMERIC(7,0),	        
			N1_DAY_VOL_06           NUMERIC(7,0),	        
			N1_DAY_VOL_07           NUMERIC(7,0),	        
			N1_DAY_VOL_08           NUMERIC(7,0),	        
			N1_DAY_VOL_09           NUMERIC(7,0),	        
			N1_DAY_VOL_10           NUMERIC(7,0),	        
			N1_DAY_VOL_11           NUMERIC(7,0),	        
			N1_DAY_VOL_12           NUMERIC(7,0),	        
			N1_DAY_VOL_13           NUMERIC(7,0),	        
			N1_DAY_VOL_14           NUMERIC(7,0),	        
			N1_DAY_VOL_15           NUMERIC(7,0),	        
			N1_DAY_VOL_16           NUMERIC(7,0),	        
			N1_DAY_VOL_17           NUMERIC(7,0),	        
			N1_DAY_VOL_18           NUMERIC(7,0),	        
			N1_DAY_VOL_19           NUMERIC(7,0),	        
			N1_DAY_VOL_20           NUMERIC(7,0),	        
			N1_DAY_VOL_21           NUMERIC(7,0),	        
			N1_DAY_VOL_22           NUMERIC(7,0),	        
			N1_DAY_VOL_23           NUMERIC(7,0),	        
			N1_DAY_VOL_24           NUMERIC(7,0),	        
			N1_DAY_VOL_25           NUMERIC(7,0),	        
			N1_DAY_VOL_26           NUMERIC(7,0),	        
			N1_DAY_VOL_27           NUMERIC(7,0),	        
			N1_DAY_VOL_28           NUMERIC(7,0),	        
			N1_DAY_VOL_29           NUMERIC(7,0),	        
			N1_DAY_VOL_30           NUMERIC(7,0),	        
			N1_DAY_VOL_31           NUMERIC(7,0),	        
			SYS_DT                  VARCHAR(8),		
			SERIES_NAME             VARCHAR(20),       
			LIFE_CYCLE_CD           VARCHAR(1), 	    
			NMIN1_MONTH_VOL         NUMERIC(8,0),	        
			N_MONTH_VOL             NUMERIC(8,0),	        
			N1_MONTH_VOL            NUMERIC(8,0),	        
			N2_MONTH_VOL            NUMERIC(8,0),	        
			BACK_ADJUST_QTY         NUMERIC(8,0),	        
			RATIO_LAST_MONTH        VARCHAR(3),        
			N_MONTH_DAY_VOL_MAX     NUMERIC(7,0),           
			N_MONTH_DAY_VOL_MIN     NUMERIC(7,0),           
			N_MONTH_DAY_VOL_VAR     NUMERIC(3,0),           
			SUPPLIER_NAME           VARCHAR(40),       
			PART_NAME               VARCHAR(40),       
			JCFD                    VARCHAR(5),        
			CONTA_CLASS             VARCHAR(1),        
			BULK_LOT_CD             VARCHAR(2),        
			TERMINATE_CD            VARCHAR(1),        
			CREATED_DT              DATE ,                   
			CHANGED_DT              DATE   ,                 
			CREATED_BY              VARCHAR(25),
			CHANGED_BY              VARCHAR(25)
		)
		
	CREATE TABLE ##TB_T_TNQC_LOT_AB 
		(
			VER					VARCHAR(1),
			COMPANY_CD			VARCHAR(4),
			PLANT_CD				VARCHAR(1),
			DOCK_CD				VARCHAR(2),
			SUPPLIER_CD			VARCHAR(4),
			SUPPLIER_PLANT_CD		VARCHAR(1),
			SHIP_DOC_CD			VARCHAR(3),
			FIRM_PACK_MONTH		VARCHAR(6),
			CFC					VARCHAR(4),
			PART_NO				VARCHAR(12),
			LOT_CD				VARCHAR(2),
			EXT_CD				VARCHAR(4),
			INT_CD				VARCHAR(4),
			CTL_KATA			VARCHAR(20),
			ORD_LOT_SIZE		VARCHAR(5),
			KANBAN_NO			VARCHAR(4),
			PART_MATCH_KEY		VARCHAR(6),
			TNQC_OUT_CYCLE		VARCHAR(1),
			DATA_ID				VARCHAR(4),
			REV			VARCHAR(4),
			ORD_TYPE			VARCHAR(1),
			PAMS_CFC			VARCHAR(4),
			RE_EXPORT_CD		VARCHAR(1),
			AICO_FLAG			VARCHAR(1),
			LOT_PXP_FLAG			VARCHAR(1),
			SOURCE_CD			VARCHAR(1),
			IMPORTER_NAME		VARCHAR(3),
			EXPORTER_NAME		VARCHAR(3),
			SS_NO				VARCHAR(2),
			PAMS_SS_NO			VARCHAR(2),
			LINE_CD				VARCHAR(1),
			DISPLAY_KATA			VARCHAR(20),
			N_MONTH_ORD_VOL		NUMERIC(8,0)	,
			N1_MONTH_ORD_VOL	NUMERIC(8,0)	,
			N2_MONTH_ORD_VOL	NUMERIC(8,0)	,
			N3_MONTH_ORD_VOL	NUMERIC(8,0)	,
			N_DAY_VOL_01		NUMERIC(7,0)	,
			N_DAY_VOL_02		NUMERIC(7,0)	,
			N_DAY_VOL_03		NUMERIC(7,0)	,
			N_DAY_VOL_04		NUMERIC(7,0)	,
			N_DAY_VOL_05		NUMERIC(7,0)	,
			N_DAY_VOL_06		NUMERIC(7,0)	,
			N_DAY_VOL_07		NUMERIC(7,0)	,
			N_DAY_VOL_08		NUMERIC(7,0)	,
			N_DAY_VOL_09		NUMERIC(7,0)	,
			N_DAY_VOL_10		NUMERIC(7,0)	,
			N_DAY_VOL_11		NUMERIC(7,0)	,
			N_DAY_VOL_12		NUMERIC(7,0)	,
			N_DAY_VOL_13		NUMERIC(7,0)	,
			N_DAY_VOL_14		NUMERIC(7,0)	,
			N_DAY_VOL_15		NUMERIC(7,0)	,
			N_DAY_VOL_16		NUMERIC(7,0)	,
			N_DAY_VOL_17		NUMERIC(7,0)	,
			N_DAY_VOL_18		NUMERIC(7,0)	,
			N_DAY_VOL_19		NUMERIC(7,0)	,
			N_DAY_VOL_20		NUMERIC(7,0)	,
			N_DAY_VOL_21		NUMERIC(7,0)	,
			N_DAY_VOL_22		NUMERIC(7,0)	,
			N_DAY_VOL_23		NUMERIC(7,0)	,
			N_DAY_VOL_24		NUMERIC(7,0)	,
			N_DAY_VOL_25		NUMERIC(7,0)	,
			N_DAY_VOL_26		NUMERIC(7,0)	,
			N_DAY_VOL_27		NUMERIC(7,0)	,
			N_DAY_VOL_28		NUMERIC(7,0)	,
			N_DAY_VOL_29		NUMERIC(7,0)	,
			N_DAY_VOL_30		NUMERIC(7,0)	,
			N_DAY_VOL_31		NUMERIC(7,0)	,
			N1_DAY_VOL_01		NUMERIC(7,0)	,
			N1_DAY_VOL_02		NUMERIC(7,0)	,
			N1_DAY_VOL_03		NUMERIC(7,0)	,
			N1_DAY_VOL_04		NUMERIC(7,0)	,
			N1_DAY_VOL_05		NUMERIC(7,0)	,
			N1_DAY_VOL_06		NUMERIC(7,0)	,
			N1_DAY_VOL_07		NUMERIC(7,0)	,
			N1_DAY_VOL_08		NUMERIC(7,0)	,
			N1_DAY_VOL_09		NUMERIC(7,0)	,
			N1_DAY_VOL_10		NUMERIC(7,0)	,
			N1_DAY_VOL_11		NUMERIC(7,0)	,
			N1_DAY_VOL_12		NUMERIC(7,0)	,
			N1_DAY_VOL_13		NUMERIC(7,0)	,
			N1_DAY_VOL_14		NUMERIC(7,0)	,
			N1_DAY_VOL_15		NUMERIC(7,0)	,
			N1_DAY_VOL_16		NUMERIC(7,0)	,
			N1_DAY_VOL_17		NUMERIC(7,0)	,
			N1_DAY_VOL_18		NUMERIC(7,0)	,
			N1_DAY_VOL_19		NUMERIC(7,0)	,
			N1_DAY_VOL_20		NUMERIC(7,0)	,
			N1_DAY_VOL_21		NUMERIC(7,0)	,
			N1_DAY_VOL_22		NUMERIC(7,0)	,
			N1_DAY_VOL_23		NUMERIC(7,0)	,
			N1_DAY_VOL_24		NUMERIC(7,0)	,
			N1_DAY_VOL_25		NUMERIC(7,0)	,
			N1_DAY_VOL_26		NUMERIC(7,0)	,
			N1_DAY_VOL_27		NUMERIC(7,0)	,
			N1_DAY_VOL_28		NUMERIC(7,0)	,
			N1_DAY_VOL_29		NUMERIC(7,0)	,
			N1_DAY_VOL_30		NUMERIC(7,0)	,
			N1_DAY_VOL_31		NUMERIC(7,0)	,
			SYS_DT				VARCHAR(8)	,
			SERIES_NAME			VARCHAR(20),
			LIFE_CYCLE_CD			VARCHAR(1),
			NMIN1_MONTH_VOL		NUMERIC(8,0)	,
			N_MONTH_VOL			NUMERIC(8,0)	,
			N1_MONTH_VOL		NUMERIC(8,0)	,
			N2_MONTH_VOL		NUMERIC(8,0)	,
			BACK_ADJUST_QTY		NUMERIC(8,0)	,
			RATIO_LAST_MONTH	VARCHAR(3),
			N_MONTH_DAY_VOL_MAX		NUMERIC(7,0),
			N_MONTH_DAY_VOL_MIN		NUMERIC(7,0),
			N_MONTH_DAY_VOL_VAR		NUMERIC(3,0),
			SUPPLIER_NAME			VARCHAR(40),
			PART_NAME				VARCHAR(40),
			JCFD				VARCHAR(5),
			CONTA_CLASS			VARCHAR(1),
			BULK_LOT_CD		VARCHAR(2),
			TERMINATE_CD		VARCHAR(1),
			CREATED_DT			DATE,
			CHANGED_DT			DATE,
			CREATED_BY			VARCHAR(25),
			CHANGED_BY			VARCHAR(25)
		)

	CREATE TABLE ##TB_T_TNQC_LOT_EO 
		(
			VER						VARCHAR(1),          
			COMPANY_CD              VARCHAR(4),		  
			PLANT_CD                VARCHAR(1),          
			DOCK_CD                 VARCHAR(2),          
			SUPPLIER_CD             VARCHAR(4),          
			SUPPLIER_PLANT_CD       VARCHAR(1),          
			SHIP_DOC_CD             VARCHAR(3),			
			FIRM_PACK_MONTH         VARCHAR(6),          
			CFC                     VARCHAR(4),          
			PART_NO                 VARCHAR(12),         
			LOT_CD                  VARCHAR(2),          
			EXT_CD                  VARCHAR(4),          
			INT_CD                  VARCHAR(4),          
			CTL_KATA                VARCHAR(20),         
			ORD_LOT_SIZE            VARCHAR(5),          
			KANBAN_NO               VARCHAR(4),          
			PART_MATCH_KEY          VARCHAR(6),          
			TNQC_OUT_CYCLE          VARCHAR(1),          
			DATA_ID                 VARCHAR(4),          
			REV                     VARCHAR(4),          
			ORD_TYPE                VARCHAR(1),          
			PAMS_CFC                VARCHAR(4),          
			RE_EXPORT_CD            VARCHAR(1),          
			AICO_FLAG               VARCHAR(1),          
			LOT_PXP_FLAG            VARCHAR(1),          
			SOURCE_CD               VARCHAR(1),          
			IMPORTER_NAME           VARCHAR(3),          
			EXPORTER_NAME           VARCHAR(3),          
			SS_NO                   VARCHAR(2),          
			PAMS_SS_NO              VARCHAR(2),          
			LINE_CD                 VARCHAR(1),          
			DISPLAY_KATA            VARCHAR(20),         
			N_MONTH_ORD_VOL         NUMERIC(8,0),	          
			N1_MONTH_ORD_VOL        NUMERIC(8,0),	          
			N2_MONTH_ORD_VOL        NUMERIC(8,0),	          
			N3_MONTH_ORD_VOL        NUMERIC(8,0),	          
			N_DAY_VOL_01            NUMERIC(7,0),	          
			N_DAY_VOL_02            NUMERIC(7,0),	          
			N_DAY_VOL_03            NUMERIC(7,0),	          
			N_DAY_VOL_04            NUMERIC(7,0),	          
			N_DAY_VOL_05            NUMERIC(7,0),	          
			N_DAY_VOL_06            NUMERIC(7,0),	          
			N_DAY_VOL_07            NUMERIC(7,0),	          
			N_DAY_VOL_08            NUMERIC(7,0),	          
			N_DAY_VOL_09            NUMERIC(7,0),	          
			N_DAY_VOL_10            NUMERIC(7,0),	          
			N_DAY_VOL_11            NUMERIC(7,0),	          
			N_DAY_VOL_12            NUMERIC(7,0),	          
			N_DAY_VOL_13            NUMERIC(7,0),	          
			N_DAY_VOL_14            NUMERIC(7,0),	          
			N_DAY_VOL_15            NUMERIC(7,0),	          
			N_DAY_VOL_16            NUMERIC(7,0),	          
			N_DAY_VOL_17            NUMERIC(7,0),	          
			N_DAY_VOL_18            NUMERIC(7,0),	          
			N_DAY_VOL_19            NUMERIC(7,0),	          
			N_DAY_VOL_20            NUMERIC(7,0),	          
			N_DAY_VOL_21            NUMERIC(7,0),	          
			N_DAY_VOL_22            NUMERIC(7,0),	          
			N_DAY_VOL_23            NUMERIC(7,0),	          
			N_DAY_VOL_24            NUMERIC(7,0),	          
			N_DAY_VOL_25            NUMERIC(7,0),	          
			N_DAY_VOL_26            NUMERIC(7,0),	          
			N_DAY_VOL_27            NUMERIC(7,0),	          
			N_DAY_VOL_28            NUMERIC(7,0),	          
			N_DAY_VOL_29            NUMERIC(7,0),	          
			N_DAY_VOL_30            NUMERIC(7,0),	          
			N_DAY_VOL_31            NUMERIC(7,0),	          
			N1_DAY_VOL_01           NUMERIC(7,0),	          
			N1_DAY_VOL_02           NUMERIC(7,0),	          
			N1_DAY_VOL_03           NUMERIC(7,0),	          
			N1_DAY_VOL_04           NUMERIC(7,0),	          
			N1_DAY_VOL_05           NUMERIC(7,0),	          
			N1_DAY_VOL_06           NUMERIC(7,0),	          
			N1_DAY_VOL_07           NUMERIC(7,0),	          
			N1_DAY_VOL_08           NUMERIC(7,0),	          
			N1_DAY_VOL_09           NUMERIC(7,0),	          
			N1_DAY_VOL_10           NUMERIC(7,0),	          
			N1_DAY_VOL_11           NUMERIC(7,0),	          
			N1_DAY_VOL_12           NUMERIC(7,0),	          
			N1_DAY_VOL_13           NUMERIC(7,0),	          
			N1_DAY_VOL_14           NUMERIC(7,0),	          
			N1_DAY_VOL_15           NUMERIC(7,0),	          
			N1_DAY_VOL_16           NUMERIC(7,0),	          
			N1_DAY_VOL_17           NUMERIC(7,0),	          
			N1_DAY_VOL_18           NUMERIC(7,0),	          
			N1_DAY_VOL_19           NUMERIC(7,0),	          
			N1_DAY_VOL_20           NUMERIC(7,0),	          
			N1_DAY_VOL_21           NUMERIC(7,0),	          
			N1_DAY_VOL_22           NUMERIC(7,0),	          
			N1_DAY_VOL_23           NUMERIC(7,0),	          
			N1_DAY_VOL_24           NUMERIC(7,0),	          
			N1_DAY_VOL_25           NUMERIC(7,0),	          
			N1_DAY_VOL_26           NUMERIC(7,0),	          
			N1_DAY_VOL_27           NUMERIC(7,0),	          
			N1_DAY_VOL_28           NUMERIC(7,0),	          
			N1_DAY_VOL_29           NUMERIC(7,0),	          
			N1_DAY_VOL_30           NUMERIC(7,0),	          
			N1_DAY_VOL_31           NUMERIC(7,0),	          
			SYS_DT                  VARCHAR(8),		  
			SERIES_NAME             VARCHAR(20),         
			LIFE_CYCLE_CD           VARCHAR(1), 	      
			NMIN1_MONTH_VOL         NUMERIC(8,0),	          
			N_MONTH_VOL             NUMERIC(8,0),	          
			N1_MONTH_VOL            NUMERIC(8,0),	          
			N2_MONTH_VOL            NUMERIC(8,0),	          
			BACK_ADJUST_QTY         NUMERIC(8,0),	          
			RATIO_LAST_MONTH        VARCHAR(3),          
			N_MONTH_DAY_VOL_MAX     NUMERIC(7,0),              
			N_MONTH_DAY_VOL_MIN     NUMERIC(7,0),              
			N_MONTH_DAY_VOL_VAR     NUMERIC(3,0),              
			SUPPLIER_NAME           VARCHAR(40),         
			PART_NAME               VARCHAR(40),         
			JCFD                    VARCHAR(5),          
			CONTA_CLASS             VARCHAR(1),          
			BULK_LOT_CD             VARCHAR(2),          
			TERMINATE_CD            VARCHAR(1),          
			CREATED_DT              DATE,                      
			CHANGED_DT              DATE,                      
			CREATED_BY              VARCHAR(25),
			CHANGED_BY              VARCHAR(25)
		)
		
	/* Create Log Header */
	EXEC	[dbo].[SP_LOG_WRITE_HEADER]
			@PROCESS_ID = @PROCESS_ID OUTPUT,
			@FUNCTION_ID = @FUNCTION_ID,
			@MODULE_ID = 'BATCH',
			@PROCESS_STS = '4', /*Inprogress*/
			@USER_ID = @v_userId,
			@READ_FLAG = NULL,
			@REMARK = 'Odr Rec. TEN-TMC'

	-- Lock Checking
	EXEC SP_IS_LOCK_FUNCTION @FUNCTION_ID,@FUNCTION_LOCKED OUTPUT, @RUN_PROCESS_ID OUTPUT
	IF @FUNCTION_LOCKED <> 0 AND @RUN_PROCESS_ID <> 0
	BEGIN
		--EXEC [SP_MESSAGE_GET] 'MCSTSTD118E', @MSG_TXT OUTPUT, @MSG_TYPE OUTPUT, @NR_ERR OUTPUT
		SET @MSG_TXT = REPLACE('Process is being locked by another user with process id = {0}','{0}',@RUN_PROCESS_ID)

		EXEC [SP_LOG_WRITE_DETAIL] @PROCESS_ID, @v_userId, 'Process is locked', 'MCSTSTD118E', @MSG_TXT, @NR_ERR OUTPUT

		SET @process_flag = 1
	END

	-- Lock process
	INSERT INTO TB_T_LOCK (PROCESS_ID,FUNCTION_ID,LOCK_REFF,PROCESSED_BY,START_TIME)
	VALUES(@PROCESS_ID,@FUNCTION_ID,NULL,@v_userId,GETDATE())

	IF @process_flag = 0
	BEGIN
		SET @N1_MONTH_Date = (select DATEADD(mm,1,getdate()))
		SET @N1_MONTH = (SELECT CAST(YEAR(@N1_MONTH_Date) AS VARCHAR) + ''+RIGHT('00' + CAST(DATEPART(mm, @N1_MONTH_Date) AS varchar(2)), 2) );
		SET @SERVERLINK = (select top 1 SYSTEM_VALUE_TXT  from TB_M_SYSTEM  where SYSTEM_TYPE = 'LINKED_SERVER' and SYSTEM_CD = 'GPPSU_DB')
		SET @TableName = (select top 1 SYSTEM_VALUE_TXT  from TB_M_SYSTEM  where SYSTEM_TYPE = 'ORACLE_DB' and SYSTEM_CD = 'DB_NAME')
	
		SET @MSG_TXT = 'Start Log '+' function id '+@FUNCTION_ID+' with Month : '+@N1_MONTH+ ' linkserver: '+@SERVERLINK+' table oracle: '+@TableName;
		EXEC [SP_LOG_WRITE_DETAIL] @PROCESS_ID, @v_userId, 'Start', '01I', @MSG_TXT, @NR_ERR OUTPUT

		-- [START] batch status
		EXEC  dbo.sp_Common_Batch_Status
					@company_cd = @v_companyCode,
					@batch_id = @FUNCTION_ID,
					@class = 'Odr Rec. TEN-TMC',
					@batch_name = 'Odr Rec. TEN-TMC',
					@status = 'EXE',
					@start_time = @DATE,
					@changed_dt = @DATE,
					@changed_by = @v_userId,
					@return_message = @return_message OUTPUT
		-- [END]

		-- [START] Get Car Plant Code from TB_M_COMPANY using UNPIVOT Table
		DELETE FROM @TB_T_COMPANY
		INSERT INTO @TB_T_COMPANY
		select COMPANY_CD , CPC AS CAR_PLANT_CD FROM(
			select  COMPANY_CD, CPC from 
			(
			SELECT COMPANY_CD, CAR_PLANT_CD_01,CAR_PLANT_CD_02, CAR_PLANT_CD_03, CAR_PLANT_CD_04,CAR_PLANT_CD_05,CAR_PLANT_CD_06, CAR_PLANT_CD_07, CAR_PLANT_CD_08, CAR_PLANT_CD_09, CAR_PLANT_CD_10
				FROM TB_M_COMPANY
				WHERE COMPANY_CD = @v_companyCode
			) p
			UNPIVOT
			(CPC  FOR CAR_PLANT_CD IN 
				(CAR_PLANT_CD_01,CAR_PLANT_CD_02, CAR_PLANT_CD_03, CAR_PLANT_CD_04,CAR_PLANT_CD_05,CAR_PLANT_CD_06, CAR_PLANT_CD_07, CAR_PLANT_CD_08, CAR_PLANT_CD_09, CAR_PLANT_CD_10)
			)AS unpvt
		) d
		WHERE NULLIF(d.CPC,'') is NOT NULL
		-- [END]

		-- [START] Cek & Set Car Plant Cd from Company Master
		IF ((SELECT COUNT(*) FROM @TB_T_COMPANY) > 0)
		BEGIN
			set @car_pant_cd_string = (SELECT STUFF((SELECT ','  +(CAR_PLANT_CD) FROM @TB_T_COMPANY FOR XML PATH(''),TYPE).value('.', 'VARCHAR(MAX)'),1,1,''))
		
			SET @MSG_TXT = 'Car Plant Code: '+@car_pant_cd_string;
			EXEC [SP_LOG_WRITE_DETAIL] @PROCESS_ID, @v_userId, 'Get Company Master', '02I', @MSG_TXT, @NR_ERR OUTPUT

			set @car_pant_cd_string =(select replace(@car_pant_cd_string,',',''''','''''))
			set @car_pant_cd_string = ' (''''' +@car_pant_cd_string + ''''')';
		END
		ELSE
		BEGIN -- Blank of Car Plant Cd
			SET @process_flag = 1
			--print 'Blank of Car Plant Cd of Company Master';
			SET @MSG_TXT = 'Blank of Car Plant Cd of Company Master';
			EXEC [SP_LOG_WRITE_DETAIL] @PROCESS_ID, @v_userId, 'Get Company Master', '03E', @MSG_TXT, @NR_ERR OUTPUT
		END
		-- [END]
	END

	IF @process_flag = 0
	BEGIN
		-- Get Data From T NQC with DBLink Oracle Database --
		/*  Get Data TB_R_TNQC_RESULT_FO [1]*/ 
		SET @openquery  =' 
			select * from openquery({0},''select * from {1}.TB_R_TNQC_RESULT_FO 
				WHERE DATA_ID =''''TNQC'''' AND SUPPLIER_CD IN {2} AND FIRM_PACK_MONTH = '''''+@N1_MONTH+''''' AND PART_NO is not null  AND TRIM(LOT_CD) IS NULL ''
				) '

		set @openquery = replace (@openquery, '{0}' , @SERVERLINK)
		set @openquery = replace (@openquery, '{1}' , @TableName)
		set @openquery = replace (@openquery, '{2}' , @car_pant_cd_string)

		--PRINT @openquery
		INSERT INTO ##TB_T_TNQC_RESULT_FO
		EXECUTE(@openquery)
		SET @existing = @@ROWCOUNT;

		SET @MSG_TXT = 'Get Data TB_R_TNQC_RESULT_FO : '+convert(VARCHAR(10),@existing);
		EXEC [SP_LOG_WRITE_DETAIL] @PROCESS_ID, @v_userId, 'Get Data From Oracle', '04I', @MSG_TXT, @NR_ERR OUTPUT
		--SELECT * FROM ##TB_T_TNQC_RESULT_FO 
		
		/*  Get Data TB_R_TNQC_RESULT_ABF [2]*/
		SET @openquery='';
		SET @openquery  =' 
			select * from openquery({0},''select * from {1}.TB_R_TNQC_RESULT_ABF 
				WHERE DATA_ID =''''TNQC'''' AND SUPPLIER_CD IN {2} AND FIRM_PACK_MONTH = '''''+@N1_MONTH+''''' AND PART_NO is not null  AND TRIM(LOT_CD) IS NULL ''
				) '

		set @openquery = replace (@openquery, '{0}' , @SERVERLINK)
		set @openquery = replace (@openquery, '{1}' , @TableName)
		set @openquery = replace (@openquery, '{2}' , @car_pant_cd_string)

		--PRINT @openquery
		INSERT INTO ##TB_T_TNQC_RESULT_ABF
		EXECUTE(@openquery)
		SET @existing = @@ROWCOUNT;

		SET @MSG_TXT = 'Get Data TB_R_TNQC_RESULT_ABF : '+convert(VARCHAR(10),@existing);
		EXEC [SP_LOG_WRITE_DETAIL] @PROCESS_ID, @v_userId, 'Get Data From Oracle', '05I', @MSG_TXT, @NR_ERR OUTPUT
		--SELECT * FROM ##TB_T_TNQC_RESULT_ABF

		/*  Get Data TB_R_TNQC_RESULT_AB [3]*/
		SET @openquery='';
		SET @openquery  =' 
			select * from openquery({0},''select * from {1}.TB_R_TNQC_RESULT_AB
				WHERE DATA_ID =''''TNQC'''' AND SUPPLIER_CD IN {2} AND FIRM_PACK_MONTH = '''''+@N1_MONTH+''''' AND PART_NO is not null  AND TRIM(LOT_CD) IS NULL ''
				) '

		set @openquery = replace (@openquery, '{0}' , @SERVERLINK)
		set @openquery = replace (@openquery, '{1}' , @TableName)
		set @openquery = replace (@openquery, '{2}' , @car_pant_cd_string)

		--PRINT @openquery
		INSERT INTO ##TB_T_TNQC_RESULT_AB
		EXECUTE(@openquery)
		SET @existing = @@ROWCOUNT;

		SET @MSG_TXT = 'Get Data TB_R_TNQC_RESULT_AB : '+convert(VARCHAR(10),@existing);
		EXEC [SP_LOG_WRITE_DETAIL] @PROCESS_ID, @v_userId, 'Get Data From Oracle', '06I', @MSG_TXT, @NR_ERR OUTPUT
		--SELECT * FROM ##TB_T_TNQC_RESULT_AB

		/*  Get Data TB_R_TNQC_RESULT_EO [4]*/
		SET @openquery='';
		SET @openquery  =' 
			select * from openquery({0},''select * from {1}.TB_R_TNQC_RESULT_EO
				WHERE DATA_ID =''''TNQC'''' AND SUPPLIER_CD IN {2} AND FIRM_PACK_MONTH = '''''+@N1_MONTH+''''' AND PART_NO is not null  AND TRIM(LOT_CD) IS NULL ''
				) '

		set @openquery = replace (@openquery, '{0}' , @SERVERLINK)
		set @openquery = replace (@openquery, '{1}' , @TableName)
		set @openquery = replace (@openquery, '{2}' , @car_pant_cd_string)

		--PRINT @openquery
		INSERT INTO ##TB_T_TNQC_RESULT_EO
		EXECUTE(@openquery)
		SET @existing = @@ROWCOUNT;

		SET @MSG_TXT = 'Get Data TB_R_TNQC_RESULT_EO : '+convert(VARCHAR(10),@existing);
		EXEC [SP_LOG_WRITE_DETAIL] @PROCESS_ID, @v_userId, 'Get Data From Oracle', '07I', @MSG_TXT, @NR_ERR OUTPUT
		--SELECT * FROM ##TB_T_TNQC_RESULT_EO

		/*  Get Data TB_R_TNQC_LOT_FO [5]*/
		SET @openquery='';
		SET @openquery  =' 
			select * from openquery({0},''select * from {1}.TB_R_TNQC_LOT_FO
				WHERE DATA_ID =''''TNQC'''' AND SUPPLIER_CD IN {2} AND FIRM_PACK_MONTH = '''''+@N1_MONTH+''''' AND TRIM(PART_NO) is null  AND LOT_CD IS NOT NULL ''
				) '

		set @openquery = replace (@openquery, '{0}' , @SERVERLINK)
		set @openquery = replace (@openquery, '{1}' , @TableName)
		set @openquery = replace (@openquery, '{2}' , @car_pant_cd_string)

		--PRINT @openquery
		INSERT INTO ##TB_T_TNQC_LOT_FO
		EXECUTE(@openquery)
		SET @existing = @@ROWCOUNT;

		SET @MSG_TXT = 'Get Data TB_R_TNQC_LOT_FO : '+convert(VARCHAR(10),@existing);
		EXEC [SP_LOG_WRITE_DETAIL] @PROCESS_ID, @v_userId, 'Get Data From Oracle', '08I', @MSG_TXT, @NR_ERR OUTPUT
		--SELECT * FROM ##TB_T_TNQC_LOT_FO

		/*  Get Data TB_R_TNQC_LOT_ABF [6]*/
		SET @openquery='';
		SET @openquery  =' 
			select * from openquery({0},''select * from {1}.TB_R_TNQC_LOT_ABF
				WHERE DATA_ID =''''TNQC'''' AND SUPPLIER_CD IN {2} AND FIRM_PACK_MONTH = '''''+@N1_MONTH+''''' AND TRIM(PART_NO) is null  AND LOT_CD IS NOT NULL ''
				) '

		set @openquery = replace (@openquery, '{0}' , @SERVERLINK)
		set @openquery = replace (@openquery, '{1}' , @TableName)
		set @openquery = replace (@openquery, '{2}' , @car_pant_cd_string)

		--PRINT @openquery
		INSERT INTO ##TB_T_TNQC_LOT_ABF
		EXECUTE(@openquery)
		SET @existing = @@ROWCOUNT;

		SET @MSG_TXT = 'Get Data TB_R_TNQC_LOT_ABF : '+convert(VARCHAR(10),@existing);
		EXEC [SP_LOG_WRITE_DETAIL] @PROCESS_ID, @v_userId, 'Get Data From Oracle', '09I', @MSG_TXT, @NR_ERR OUTPUT
		--SELECT * FROM ##TB_T_TNQC_LOT_ABF

		/*  Get Data TB_R_TNQC_LOT_AB [7]*/
		SET @openquery='';
		SET @openquery  =' 
			select * from openquery({0},''select * from {1}.TB_R_TNQC_LOT_AB
				WHERE DATA_ID =''''TNQC'''' AND SUPPLIER_CD IN {2} AND FIRM_PACK_MONTH = '''''+@N1_MONTH+''''' AND TRIM(PART_NO) is null  AND LOT_CD IS NOT NULL ''
				) '

		set @openquery = replace (@openquery, '{0}' , @SERVERLINK)
		set @openquery = replace (@openquery, '{1}' , @TableName)
		set @openquery = replace (@openquery, '{2}' , @car_pant_cd_string)

		--PRINT @openquery
		INSERT INTO ##TB_T_TNQC_LOT_AB
		EXECUTE(@openquery)
		SET @existing = @@ROWCOUNT;

		SET @MSG_TXT = 'Get Data TB_R_TNQC_LOT_AB : '+convert(VARCHAR(10),@existing);
		EXEC [SP_LOG_WRITE_DETAIL] @PROCESS_ID, @v_userId, 'Get Data From Oracle', '10I', @MSG_TXT, @NR_ERR OUTPUT
		--SELECT * FROM ##TB_T_TNQC_LOT_AB

		/*  Get Data TB_R_TNQC_LOT_EO [8]*/
		SET @openquery='';
		SET @openquery  =' 
			select * from openquery({0},''select * from {1}.TB_R_TNQC_LOT_EO
				WHERE DATA_ID =''''TNQC'''' AND SUPPLIER_CD IN {2} AND FIRM_PACK_MONTH = '''''+@N1_MONTH+''''' AND TRIM(PART_NO) is null  AND LOT_CD IS NOT NULL ''
				) '

		set @openquery = replace (@openquery, '{0}' , @SERVERLINK)
		set @openquery = replace (@openquery, '{1}' , @TableName)
		set @openquery = replace (@openquery, '{2}' , @car_pant_cd_string)

		--PRINT @openquery
		INSERT INTO ##TB_T_TNQC_LOT_EO
		EXECUTE(@openquery)
		SET @existing = @@ROWCOUNT;

		SET @MSG_TXT = 'Get Data TB_R_TNQC_LOT_EO : '+convert(VARCHAR(10),@existing);
		EXEC [SP_LOG_WRITE_DETAIL] @PROCESS_ID, @v_userId, 'Get Data From Oracle', '11I', @MSG_TXT, @NR_ERR OUTPUT
		--SELECT * FROM ##TB_T_TNQC_LOT_EO
		

		IF ((SELECT COUNT(*) FROM ##TB_T_TNQC_RESULT_FO) < 1 AND (SELECT COUNT(*) FROM ##TB_T_TNQC_RESULT_ABF) < 1 
				AND (SELECT COUNT(*) FROM ##TB_T_TNQC_RESULT_AB) < 1 AND (SELECT COUNT(*) FROM ##TB_T_TNQC_RESULT_EO) < 1)
		BEGIN -- Blank Data in T NQC
			SET @process_flag = 1
			--print 'Blank Data in T NQC';
			SET @MSG_TXT = 'Blank Data in T NQC ';
			EXEC [SP_LOG_WRITE_DETAIL] @PROCESS_ID, @v_userId, '0 row Data From Oracle', '12E', @MSG_TXT, @NR_ERR OUTPUT
		END
	END
	
	IF @process_flag = 0
	BEGIN
		INSERT @o
		SELECT VER,COMPANY_CD,PLANT_CD,DOCK_CD,SUPPLIER_CD,SUPPLIER_PLANT_CD,SHIP_DOC_CD,FIRM_PACK_MONTH,CFC,PART_NO,LOT_CD,EXT_CD,INT_CD,CTL_KATA,ORD_LOT_SIZE,KANBAN_NO,
		PART_MATCH_KEY,TNQC_OUT_CYCLE,DATA_ID,REV,ORD_TYPE,PAMS_CFC,RE_EXPORT_CD,AICO_FLAG,LOT_PXP_FLAG,SOURCE_CD,IMPORTER_NAME,EXPORTER_NAME,SS_NO,PAMS_SS_NO,LINE_CD,
		DISPLAY_KATA,N_MONTH_ORD_VOL,N1_MONTH_ORD_VOL,N2_MONTH_ORD_VOL,N3_MONTH_ORD_VOL,N_DAY_VOL_01,N_DAY_VOL_02,N_DAY_VOL_03,N_DAY_VOL_04,N_DAY_VOL_05,N_DAY_VOL_06,
		N_DAY_VOL_07,N_DAY_VOL_08,N_DAY_VOL_09,N_DAY_VOL_10,N_DAY_VOL_11,N_DAY_VOL_12,N_DAY_VOL_13,N_DAY_VOL_14,N_DAY_VOL_15,N_DAY_VOL_16,N_DAY_VOL_17,N_DAY_VOL_18,
		N_DAY_VOL_19,N_DAY_VOL_20,N_DAY_VOL_21,N_DAY_VOL_22,N_DAY_VOL_23,N_DAY_VOL_24,N_DAY_VOL_25,N_DAY_VOL_26,N_DAY_VOL_27,N_DAY_VOL_28,N_DAY_VOL_29,N_DAY_VOL_30,
		N_DAY_VOL_31,N1_DAY_VOL_01,N1_DAY_VOL_02,N1_DAY_VOL_03,N1_DAY_VOL_04,N1_DAY_VOL_05,N1_DAY_VOL_06,N1_DAY_VOL_07,N1_DAY_VOL_08,N1_DAY_VOL_09,N1_DAY_VOL_10,
		N1_DAY_VOL_11,N1_DAY_VOL_12,N1_DAY_VOL_13,N1_DAY_VOL_14,N1_DAY_VOL_15,N1_DAY_VOL_16,N1_DAY_VOL_17,N1_DAY_VOL_18,N1_DAY_VOL_19,N1_DAY_VOL_20,N1_DAY_VOL_21,
		N1_DAY_VOL_22,N1_DAY_VOL_23,N1_DAY_VOL_24,N1_DAY_VOL_25,N1_DAY_VOL_26,N1_DAY_VOL_27,N1_DAY_VOL_28,N1_DAY_VOL_29,N1_DAY_VOL_30,N1_DAY_VOL_31,SYS_DT,SERIES_NAME,
		LIFE_CYCLE_CD,NMIN1_MONTH_VOL,N_MONTH_VOL,N1_MONTH_VOL,N2_MONTH_VOL,BACK_ADJUST_SIGN,BACK_ADJUST_QTY,RATIO_LAST_MONTH,N_MONTH_DAY_VOL_MAX,N_MONTH_DAY_VOL_MIN,
		N_MONTH_DAY_VOL_VAR,SUPPLIER_NAME,PART_NAME,JCFD,CONTA_CLASS,BULK_LOT_CD,TERMINATE_CD,NULL AS LFCD FROM ##TB_T_TNQC_RESULT_FO --[1]
		UNION ALL
		SELECT VER,COMPANY_CD,PLANT_CD,DOCK_CD,SUPPLIER_CD,SUPPLIER_PLANT_CD,SHIP_DOC_CD,FIRM_PACK_MONTH,CFC,PART_NO,LOT_CD,EXT_CD,INT_CD,CTL_KATA,ORD_LOT_SIZE,KANBAN_NO,
		PART_MATCH_KEY,TNQC_OUT_CYCLE,DATA_ID,REV,ORD_TYPE,PAMS_CFC,RE_EXPORT_CD,AICO_FLAG,LOT_PXP_FLAG,SOURCE_CD,IMPORTER_NAME,EXPORTER_NAME,SS_NO,PAMS_SS_NO,LINE_CD,
		DISPLAY_KATA,N_MONTH_ORD_VOL,N1_MONTH_ORD_VOL,N2_MONTH_ORD_VOL,N3_MONTH_ORD_VOL,N_DAY_VOL_01,N_DAY_VOL_02,N_DAY_VOL_03,N_DAY_VOL_04,N_DAY_VOL_05,N_DAY_VOL_06,
		N_DAY_VOL_07,N_DAY_VOL_08,N_DAY_VOL_09,N_DAY_VOL_10,N_DAY_VOL_11,N_DAY_VOL_12,N_DAY_VOL_13,N_DAY_VOL_14,N_DAY_VOL_15,N_DAY_VOL_16,N_DAY_VOL_17,N_DAY_VOL_18,
		N_DAY_VOL_19,N_DAY_VOL_20,N_DAY_VOL_21,N_DAY_VOL_22,N_DAY_VOL_23,N_DAY_VOL_24,N_DAY_VOL_25,N_DAY_VOL_26,N_DAY_VOL_27,N_DAY_VOL_28,N_DAY_VOL_29,N_DAY_VOL_30,
		N_DAY_VOL_31,N1_DAY_VOL_01,N1_DAY_VOL_02,N1_DAY_VOL_03,N1_DAY_VOL_04,N1_DAY_VOL_05,N1_DAY_VOL_06,N1_DAY_VOL_07,N1_DAY_VOL_08,N1_DAY_VOL_09,N1_DAY_VOL_10,
		N1_DAY_VOL_11,N1_DAY_VOL_12,N1_DAY_VOL_13,N1_DAY_VOL_14,N1_DAY_VOL_15,N1_DAY_VOL_16,N1_DAY_VOL_17,N1_DAY_VOL_18,N1_DAY_VOL_19,N1_DAY_VOL_20,N1_DAY_VOL_21,
		N1_DAY_VOL_22,N1_DAY_VOL_23,N1_DAY_VOL_24,N1_DAY_VOL_25,N1_DAY_VOL_26,N1_DAY_VOL_27,N1_DAY_VOL_28,N1_DAY_VOL_29,N1_DAY_VOL_30,N1_DAY_VOL_31,SYS_DT,SERIES_NAME,
		LIFE_CYCLE_CD,NMIN1_MONTH_VOL,N_MONTH_VOL,N1_MONTH_VOL,N2_MONTH_VOL,BACK_ADJUST_SIGN,BACK_ADJUST_QTY,RATION AS RATIO_LAST_MONTH,N_MONTH_DAY_VOL_MAX,N_MONTH_DAY_VOL_MIN,
		N_MONTH_DAY_VOL_VAR,SUPPLIER_NAME,PART_NAME,JCFD,CONTA_CLASS,BULK_LOT_CD,TERMINATE_CD,NULL AS LFCD FROM ##TB_T_TNQC_RESULT_ABF --[2]
		UNION ALL
		SELECT VER,COMPANY_CD,PLANT_CD,DOCK_CD,SUPPLIER_CD,SUPPLIER_PLANT_CD,SHIP_DOC_CD,FIRM_PACK_MONTH,CFC,PART_NO,LOT_CD,EXT_CD,INT_CD,CTL_KATA,ORD_LOT_SIZE,KANBAN_NO,
		PART_MATCH_KEY,TNQC_OUT_CYCLE,DATA_ID,REV,ORD_TYPE,PAMS_CFC,RE_EXPORT_CD,AICO_FLAG,LOT_PXP_FLAG,SOURCE_CD,IMPORTER_NAME,EXPORTER_NAME,SS_NO,PAMS_SS_NO,LINE_CD,
		DISPLAY_KATA,N_MONTH_ORD_VOL,N1_MONTH_ORD_VOL,N2_MONTH_ORD_VOL,N3_MONTH_ORD_VOL,N_DAY_VOL_01,N_DAY_VOL_02,N_DAY_VOL_03,N_DAY_VOL_04,N_DAY_VOL_05,N_DAY_VOL_06,
		N_DAY_VOL_07,N_DAY_VOL_08,N_DAY_VOL_09,N_DAY_VOL_10,N_DAY_VOL_11,N_DAY_VOL_12,N_DAY_VOL_13,N_DAY_VOL_14,N_DAY_VOL_15,N_DAY_VOL_16,N_DAY_VOL_17,N_DAY_VOL_18,
		N_DAY_VOL_19,N_DAY_VOL_20,N_DAY_VOL_21,N_DAY_VOL_22,N_DAY_VOL_23,N_DAY_VOL_24,N_DAY_VOL_25,N_DAY_VOL_26,N_DAY_VOL_27,N_DAY_VOL_28,N_DAY_VOL_29,N_DAY_VOL_30,
		N_DAY_VOL_31,N1_DAY_VOL_01,N1_DAY_VOL_02,N1_DAY_VOL_03,N1_DAY_VOL_04,N1_DAY_VOL_05,N1_DAY_VOL_06,N1_DAY_VOL_07,N1_DAY_VOL_08,N1_DAY_VOL_09,N1_DAY_VOL_10,
		N1_DAY_VOL_11,N1_DAY_VOL_12,N1_DAY_VOL_13,N1_DAY_VOL_14,N1_DAY_VOL_15,N1_DAY_VOL_16,N1_DAY_VOL_17,N1_DAY_VOL_18,N1_DAY_VOL_19,N1_DAY_VOL_20,N1_DAY_VOL_21,
		N1_DAY_VOL_22,N1_DAY_VOL_23,N1_DAY_VOL_24,N1_DAY_VOL_25,N1_DAY_VOL_26,N1_DAY_VOL_27,N1_DAY_VOL_28,N1_DAY_VOL_29,N1_DAY_VOL_30,N1_DAY_VOL_31,SYS_DT,SERIES_NAME,
		LIFE_CYCLE_CD,NMIN1_MONTH_VOL,N_MONTH_VOL,N1_MONTH_VOL,N2_MONTH_VOL,BACK_ADJUST_SIGN,BACK_ADJUST_QTY,RATIO_LAST_MONTH,N_MONTH_DAY_VOL_MAX,N_MONTH_DAY_VOL_MIN,
		N_MONTH_DAY_VOL_VAR,SUPPLIER_NAME,PART_NAME,JCFD,CONTA_CLASS,BULK_LOT_CD,TERMINATE_CD,NULL AS LFCD FROM ##TB_T_TNQC_RESULT_AB --[3]
		UNION ALL
		SELECT VER,COMPANY_CD,PLANT_CD,DOCK_CD,SUPPLIER_CD,SUPPLIER_PLANT_CD,SHIP_DOC_CD,FIRM_PACK_MONTH,CFC,PART_NO,LOT_CD,EXT_CD,INT_CD,CTL_KATA,ORD_LOT_SIZE,KANBAN_NO,
		PART_MATCH_KEY,TNQC_OUT_CYCLE,DATA_ID,REV,ORD_TYPE,PAMS_CFC,RE_EXPORT_CD,AICO_FLAG,LOT_PXP_FLAG,SOURCE_CD,IMPORTER_NAME,EXPORTER_NAME,SS_NO,PAMS_SS_NO,LINE_CD,
		DISPLAY_KATA,N_MONTH_ORD_VOL,N1_MONTH_ORD_VOL,N2_MONTH_ORD_VOL,N3_MONTH_ORD_VOL,N_DAY_VOL_01,N_DAY_VOL_02,N_DAY_VOL_03,N_DAY_VOL_04,N_DAY_VOL_05,N_DAY_VOL_06,
		N_DAY_VOL_07,N_DAY_VOL_08,N_DAY_VOL_09,N_DAY_VOL_10,N_DAY_VOL_11,N_DAY_VOL_12,N_DAY_VOL_13,N_DAY_VOL_14,N_DAY_VOL_15,N_DAY_VOL_16,N_DAY_VOL_17,N_DAY_VOL_18,
		N_DAY_VOL_19,N_DAY_VOL_20,N_DAY_VOL_21,N_DAY_VOL_22,N_DAY_VOL_23,N_DAY_VOL_24,N_DAY_VOL_25,N_DAY_VOL_26,N_DAY_VOL_27,N_DAY_VOL_28,N_DAY_VOL_29,N_DAY_VOL_30,
		N_DAY_VOL_31,N1_DAY_VOL_01,N1_DAY_VOL_02,N1_DAY_VOL_03,N1_DAY_VOL_04,N1_DAY_VOL_05,N1_DAY_VOL_06,N1_DAY_VOL_07,N1_DAY_VOL_08,N1_DAY_VOL_09,N1_DAY_VOL_10,
		N1_DAY_VOL_11,N1_DAY_VOL_12,N1_DAY_VOL_13,N1_DAY_VOL_14,N1_DAY_VOL_15,N1_DAY_VOL_16,N1_DAY_VOL_17,N1_DAY_VOL_18,N1_DAY_VOL_19,N1_DAY_VOL_20,N1_DAY_VOL_21,
		N1_DAY_VOL_22,N1_DAY_VOL_23,N1_DAY_VOL_24,N1_DAY_VOL_25,N1_DAY_VOL_26,N1_DAY_VOL_27,N1_DAY_VOL_28,N1_DAY_VOL_29,N1_DAY_VOL_30,N1_DAY_VOL_31,SYS_DT,SERIES_NAME,
		LIFE_CYCLE_CD,NMIN1_MONTH_VOL,N_MONTH_VOL,N1_MONTH_VOL,N2_MONTH_VOL,BACK_ADJUST_SIGN,BACK_ADJUST_QTY,RATIO_LAST_MONTH,N_MONTH_DAY_VOL_MAX,N_MONTH_DAY_VOL_MIN,
		N_MONTH_DAY_VOL_VAR,SUPPLIER_NAME,PART_NAME,JCFD,CONTA_CLASS,BULK_LOT_CD,TERMINATE_CD,NULL AS LFCD FROM ##TB_T_TNQC_RESULT_EO --[4]
		UNION ALL
		SELECT VER,COMPANY_CD,PLANT_CD,DOCK_CD,SUPPLIER_CD,SUPPLIER_PLANT_CD,SHIP_DOC_CD,FIRM_PACK_MONTH,CFC,PART_NO,LOT_CD,EXT_CD,INT_CD,CTL_KATA,ORD_LOT_SIZE,KANBAN_NO,
		PART_MATCH_KEY,TNQC_OUT_CYCLE,DATA_ID,REV,ORD_TYPE,PAMS_CFC,RE_EXPORT_CD,AICO_FLAG,LOT_PXP_FLAG,SOURCE_CD,IMPORTER_NAME,EXPORTER_NAME,SS_NO,PAMS_SS_NO,LINE_CD,
		DISPLAY_KATA,N_MONTH_ORD_VOL,N1_MONTH_ORD_VOL,N2_MONTH_ORD_VOL,N3_MONTH_ORD_VOL,N_DAY_VOL_01,N_DAY_VOL_02,N_DAY_VOL_03,N_DAY_VOL_04,N_DAY_VOL_05,N_DAY_VOL_06,
		N_DAY_VOL_07,N_DAY_VOL_08,N_DAY_VOL_09,N_DAY_VOL_10,N_DAY_VOL_11,N_DAY_VOL_12,N_DAY_VOL_13,N_DAY_VOL_14,N_DAY_VOL_15,N_DAY_VOL_16,N_DAY_VOL_17,N_DAY_VOL_18,
		N_DAY_VOL_19,N_DAY_VOL_20,N_DAY_VOL_21,N_DAY_VOL_22,N_DAY_VOL_23,N_DAY_VOL_24,N_DAY_VOL_25,N_DAY_VOL_26,N_DAY_VOL_27,N_DAY_VOL_28,N_DAY_VOL_29,N_DAY_VOL_30,
		N_DAY_VOL_31,N1_DAY_VOL_01,N1_DAY_VOL_02,N1_DAY_VOL_03,N1_DAY_VOL_04,N1_DAY_VOL_05,N1_DAY_VOL_06,N1_DAY_VOL_07,N1_DAY_VOL_08,N1_DAY_VOL_09,N1_DAY_VOL_10,
		N1_DAY_VOL_11,N1_DAY_VOL_12,N1_DAY_VOL_13,N1_DAY_VOL_14,N1_DAY_VOL_15,N1_DAY_VOL_16,N1_DAY_VOL_17,N1_DAY_VOL_18,N1_DAY_VOL_19,N1_DAY_VOL_20,N1_DAY_VOL_21,
		N1_DAY_VOL_22,N1_DAY_VOL_23,N1_DAY_VOL_24,N1_DAY_VOL_25,N1_DAY_VOL_26,N1_DAY_VOL_27,N1_DAY_VOL_28,N1_DAY_VOL_29,N1_DAY_VOL_30,N1_DAY_VOL_31,SYS_DT,SERIES_NAME,
		LIFE_CYCLE_CD,NMIN1_MONTH_VOL,N_MONTH_VOL,N1_MONTH_VOL,N2_MONTH_VOL,NULL AS BACK_ADJUST_SIGN,BACK_ADJUST_QTY,RATIO_LAST_MONTH,N_MONTH_DAY_VOL_MAX,N_MONTH_DAY_VOL_MIN,
		N_MONTH_DAY_VOL_VAR,SUPPLIER_NAME,PART_NAME,JCFD,CONTA_CLASS,BULK_LOT_CD,TERMINATE_CD,NULL AS LFCD FROM ##TB_T_TNQC_LOT_FO --[5]
		UNION ALL
		SELECT VER,COMPANY_CD,PLANT_CD,DOCK_CD,SUPPLIER_CD,SUPPLIER_PLANT_CD,SHIP_DOC_CD,FIRM_PACK_MONTH,CFC,PART_NO,LOT_CD,EXT_CD,INT_CD,CTL_KATA,ORD_LOT_SIZE,KANBAN_NO,
		PART_MATCH_KEY,TNQC_OUT_CYCLE,DATA_ID,REV,ORD_TYPE,PAMS_CFC,RE_EXPORT_CD,AICO_FLAG,LOT_PXP_FLAG,SOURCE_CD,IMPORTER_NAME,EXPORTER_NAME,SS_NO,PAMS_SS_NO,LINE_CD,
		DISPLAY_KATA,N_MONTH_ORD_VOL,N1_MONTH_ORD_VOL,N2_MONTH_ORD_VOL,N3_MONTH_ORD_VOL,N_DAY_VOL_01,N_DAY_VOL_02,N_DAY_VOL_03,N_DAY_VOL_04,N_DAY_VOL_05,N_DAY_VOL_06,
		N_DAY_VOL_07,N_DAY_VOL_08,N_DAY_VOL_09,N_DAY_VOL_10,N_DAY_VOL_11,N_DAY_VOL_12,N_DAY_VOL_13,N_DAY_VOL_14,N_DAY_VOL_15,N_DAY_VOL_16,N_DAY_VOL_17,N_DAY_VOL_18,
		N_DAY_VOL_19,N_DAY_VOL_20,N_DAY_VOL_21,N_DAY_VOL_22,N_DAY_VOL_23,N_DAY_VOL_24,N_DAY_VOL_25,N_DAY_VOL_26,N_DAY_VOL_27,N_DAY_VOL_28,N_DAY_VOL_29,N_DAY_VOL_30,
		N_DAY_VOL_31,N1_DAY_VOL_01,N1_DAY_VOL_02,N1_DAY_VOL_03,N1_DAY_VOL_04,N1_DAY_VOL_05,N1_DAY_VOL_06,N1_DAY_VOL_07,N1_DAY_VOL_08,N1_DAY_VOL_09,N1_DAY_VOL_10,
		N1_DAY_VOL_11,N1_DAY_VOL_12,N1_DAY_VOL_13,N1_DAY_VOL_14,N1_DAY_VOL_15,N1_DAY_VOL_16,N1_DAY_VOL_17,N1_DAY_VOL_18,N1_DAY_VOL_19,N1_DAY_VOL_20,N1_DAY_VOL_21,
		N1_DAY_VOL_22,N1_DAY_VOL_23,N1_DAY_VOL_24,N1_DAY_VOL_25,N1_DAY_VOL_26,N1_DAY_VOL_27,N1_DAY_VOL_28,N1_DAY_VOL_29,N1_DAY_VOL_30,N1_DAY_VOL_31,SYS_DT,SERIES_NAME,
		LIFE_CYCLE_CD,NMIN1_MONTH_VOL,N_MONTH_VOL,N1_MONTH_VOL,N2_MONTH_VOL,NULL AS BACK_ADJUST_SIGN,BACK_ADJUST_QTY,RATIO_LAST_MONTH,N_MONTH_DAY_VOL_MAX,N_MONTH_DAY_VOL_MIN,
		N_MONTH_DAY_VOL_VAR,SUPPLIER_NAME,PART_NAME,JCFD,CONTA_CLASS,BULK_LOT_CD,TERMINATE_CD,NULL AS LFCD FROM ##TB_T_TNQC_LOT_ABF --[6]
		UNION ALL
		SELECT VER,COMPANY_CD,PLANT_CD,DOCK_CD,SUPPLIER_CD,SUPPLIER_PLANT_CD,SHIP_DOC_CD,FIRM_PACK_MONTH,CFC,PART_NO,LOT_CD,EXT_CD,INT_CD,CTL_KATA,ORD_LOT_SIZE,KANBAN_NO,
		PART_MATCH_KEY,TNQC_OUT_CYCLE,DATA_ID,REV,ORD_TYPE,PAMS_CFC,RE_EXPORT_CD,AICO_FLAG,LOT_PXP_FLAG,SOURCE_CD,IMPORTER_NAME,EXPORTER_NAME,SS_NO,PAMS_SS_NO,LINE_CD,
		DISPLAY_KATA,N_MONTH_ORD_VOL,N1_MONTH_ORD_VOL,N2_MONTH_ORD_VOL,N3_MONTH_ORD_VOL,N_DAY_VOL_01,N_DAY_VOL_02,N_DAY_VOL_03,N_DAY_VOL_04,N_DAY_VOL_05,N_DAY_VOL_06,
		N_DAY_VOL_07,N_DAY_VOL_08,N_DAY_VOL_09,N_DAY_VOL_10,N_DAY_VOL_11,N_DAY_VOL_12,N_DAY_VOL_13,N_DAY_VOL_14,N_DAY_VOL_15,N_DAY_VOL_16,N_DAY_VOL_17,N_DAY_VOL_18,
		N_DAY_VOL_19,N_DAY_VOL_20,N_DAY_VOL_21,N_DAY_VOL_22,N_DAY_VOL_23,N_DAY_VOL_24,N_DAY_VOL_25,N_DAY_VOL_26,N_DAY_VOL_27,N_DAY_VOL_28,N_DAY_VOL_29,N_DAY_VOL_30,
		N_DAY_VOL_31,N1_DAY_VOL_01,N1_DAY_VOL_02,N1_DAY_VOL_03,N1_DAY_VOL_04,N1_DAY_VOL_05,N1_DAY_VOL_06,N1_DAY_VOL_07,N1_DAY_VOL_08,N1_DAY_VOL_09,N1_DAY_VOL_10,
		N1_DAY_VOL_11,N1_DAY_VOL_12,N1_DAY_VOL_13,N1_DAY_VOL_14,N1_DAY_VOL_15,N1_DAY_VOL_16,N1_DAY_VOL_17,N1_DAY_VOL_18,N1_DAY_VOL_19,N1_DAY_VOL_20,N1_DAY_VOL_21,
		N1_DAY_VOL_22,N1_DAY_VOL_23,N1_DAY_VOL_24,N1_DAY_VOL_25,N1_DAY_VOL_26,N1_DAY_VOL_27,N1_DAY_VOL_28,N1_DAY_VOL_29,N1_DAY_VOL_30,N1_DAY_VOL_31,SYS_DT,SERIES_NAME,
		LIFE_CYCLE_CD,NMIN1_MONTH_VOL,N_MONTH_VOL,N1_MONTH_VOL,N2_MONTH_VOL,NULL AS BACK_ADJUST_SIGN,BACK_ADJUST_QTY,RATIO_LAST_MONTH,N_MONTH_DAY_VOL_MAX,N_MONTH_DAY_VOL_MIN,
		N_MONTH_DAY_VOL_VAR,SUPPLIER_NAME,PART_NAME,JCFD,CONTA_CLASS,BULK_LOT_CD,TERMINATE_CD,NULL AS LFCD FROM ##TB_T_TNQC_LOT_AB --[7]
		UNION ALL
		SELECT VER,COMPANY_CD,PLANT_CD,DOCK_CD,SUPPLIER_CD,SUPPLIER_PLANT_CD,SHIP_DOC_CD,FIRM_PACK_MONTH,CFC,PART_NO,LOT_CD,EXT_CD,INT_CD,CTL_KATA,ORD_LOT_SIZE,KANBAN_NO,
		PART_MATCH_KEY,TNQC_OUT_CYCLE,DATA_ID,REV,ORD_TYPE,PAMS_CFC,RE_EXPORT_CD,AICO_FLAG,LOT_PXP_FLAG,SOURCE_CD,IMPORTER_NAME,EXPORTER_NAME,SS_NO,PAMS_SS_NO,LINE_CD,
		DISPLAY_KATA,N_MONTH_ORD_VOL,N1_MONTH_ORD_VOL,N2_MONTH_ORD_VOL,N3_MONTH_ORD_VOL,N_DAY_VOL_01,N_DAY_VOL_02,N_DAY_VOL_03,N_DAY_VOL_04,N_DAY_VOL_05,N_DAY_VOL_06,
		N_DAY_VOL_07,N_DAY_VOL_08,N_DAY_VOL_09,N_DAY_VOL_10,N_DAY_VOL_11,N_DAY_VOL_12,N_DAY_VOL_13,N_DAY_VOL_14,N_DAY_VOL_15,N_DAY_VOL_16,N_DAY_VOL_17,N_DAY_VOL_18,
		N_DAY_VOL_19,N_DAY_VOL_20,N_DAY_VOL_21,N_DAY_VOL_22,N_DAY_VOL_23,N_DAY_VOL_24,N_DAY_VOL_25,N_DAY_VOL_26,N_DAY_VOL_27,N_DAY_VOL_28,N_DAY_VOL_29,N_DAY_VOL_30,
		N_DAY_VOL_31,N1_DAY_VOL_01,N1_DAY_VOL_02,N1_DAY_VOL_03,N1_DAY_VOL_04,N1_DAY_VOL_05,N1_DAY_VOL_06,N1_DAY_VOL_07,N1_DAY_VOL_08,N1_DAY_VOL_09,N1_DAY_VOL_10,
		N1_DAY_VOL_11,N1_DAY_VOL_12,N1_DAY_VOL_13,N1_DAY_VOL_14,N1_DAY_VOL_15,N1_DAY_VOL_16,N1_DAY_VOL_17,N1_DAY_VOL_18,N1_DAY_VOL_19,N1_DAY_VOL_20,N1_DAY_VOL_21,
		N1_DAY_VOL_22,N1_DAY_VOL_23,N1_DAY_VOL_24,N1_DAY_VOL_25,N1_DAY_VOL_26,N1_DAY_VOL_27,N1_DAY_VOL_28,N1_DAY_VOL_29,N1_DAY_VOL_30,N1_DAY_VOL_31,SYS_DT,SERIES_NAME,
		LIFE_CYCLE_CD,NMIN1_MONTH_VOL,N_MONTH_VOL,N1_MONTH_VOL,N2_MONTH_VOL,NULL AS BACK_ADJUST_SIGN,BACK_ADJUST_QTY,RATIO_LAST_MONTH,N_MONTH_DAY_VOL_MAX,N_MONTH_DAY_VOL_MIN,
		N_MONTH_DAY_VOL_VAR,SUPPLIER_NAME,PART_NAME,JCFD,CONTA_CLASS,BULK_LOT_CD,TERMINATE_CD,NULL AS LFCD FROM ##TB_T_TNQC_LOT_EO --[8]
		

		--Get Data Not Exists in TB_M_DLV_PACK_PART and then print to Warning Order List [3.1]			
		SELECT COMPANY_CD, SUPPLIER_CD, FIRM_PACK_MONTH, ORD_TYPE,CFC,PART_NO,ORD_LOT_SIZE,N_MONTH_ORD_VOL,N1_MONTH_ORD_VOL,N2_MONTH_ORD_VOL,N3_MONTH_ORD_VOL
		INTO #TB_TEMP_WARNING_LIST_31
		from @o t
		WHERE NOT EXISTS (
			select * from @o o
			LEFT JOIN TB_M_DLV_PACK_PART d on o.SUPPLIER_CD = d.COMPANY_CD
			AND o.COMPANY_CD = d.DEST_CD
			AND o.CFC = d.CFC
			AND o.PART_NO = d.PART_NO
			WHERE o.COMPANY_CD IN (SELECT CAR_PLANT_CD FROM @TB_T_COMPANY)
			AND TNQC_OUT_CYCLE = '1'
			AND t.COMPANY_CD = o.COMPANY_CD
			AND t.CFC = o.CFC
			AND t.PART_NO = o.PART_NO
			AND t.SUPPLIER_CD = o.SUPPLIER_CD
		)	

		INSERT INTO TB_T_ORDER_WARNING_LIST 
		([OCN],[CFC] ,[PART_NO] ,[ORD_LOT_SIZE] ,[STATUS_CD] ,[N] ,[N+1] ,[N+2] ,[N+3]  ,[ERR_MESG])
		SELECT CONCAT(COMPANY_CD,'-',SUPPLIER_CD,'/',LEFT(FIRM_PACK_MONTH,4),'/',RIGHT(FIRM_PACK_MONTH,2),'/',ORD_TYPE) AS OCN, CFC,PART_NO,ORD_LOT_SIZE,'1',
		N_MONTH_ORD_VOL,N1_MONTH_ORD_VOL,N2_MONTH_ORD_VOL,N3_MONTH_ORD_VOL,'No record was found in the Del/Pack Part No Master'  
		FROM #TB_TEMP_WARNING_LIST_31
		ORDER BY [CFC],[PART_NO]
		SET @existing = @@ROWCOUNT;

		SET @MSG_TXT = 'Total Warning List 3.1 : '+convert(VARCHAR(10),@existing);
		EXEC [SP_LOG_WRITE_DETAIL] @PROCESS_ID, @v_userId, 'Warning List [3.1]', '13I', @MSG_TXT, @NR_ERR OUTPUT

		--print to Warning Order List process cd <> K [3.2]	
		INSERT INTO TB_T_ORDER_WARNING_LIST 
		([OCN],[CFC] ,[PART_NO] ,[ORD_LOT_SIZE] ,[STATUS_CD] ,[N] ,[N+1] ,[N+2] ,[N+3]  ,[ERR_MESG])
		SELECT CONCAT(o.COMPANY_CD,'-',o.SUPPLIER_CD,'/',LEFT(o.FIRM_PACK_MONTH,4),'/',RIGHT(o.FIRM_PACK_MONTH,2),'/',o.ORD_TYPE) AS OCN, o.CFC,o.PART_NO,o.ORD_LOT_SIZE,'1',
		N_MONTH_ORD_VOL,N1_MONTH_ORD_VOL,N2_MONTH_ORD_VOL,N3_MONTH_ORD_VOL,'Error in Narrowing Part (In House) Data'  
		from @o o
		LEFT JOIN TB_M_DLV_PACK_PART d on o.SUPPLIER_CD = d.COMPANY_CD
		AND o.COMPANY_CD = d.DEST_CD
		AND o.CFC = d.CFC
		AND o.PART_NO = d.PART_NO
		WHERE 
		o.COMPANY_CD IN (SELECT CAR_PLANT_CD FROM @TB_T_COMPANY)
		AND TNQC_OUT_CYCLE = '1'
		AND d.PROCESS_CD <> 'K'
		SET @existing = @@ROWCOUNT;

		SET @MSG_TXT = 'Total Warning List 3.2 : '+convert(VARCHAR(10),@existing);
		EXEC [SP_LOG_WRITE_DETAIL] @PROCESS_ID, @v_userId, 'Warning List [3.2]', '14I', @MSG_TXT, @NR_ERR OUTPUT

		-- Cek Mandatory kolom [3.5]
		IF EXISTS(
			SELECT COUNT(*)
			from @o o
			WHERE NULLIF(COMPANY_CD,'') is NULL OR NULLIF(SUPPLIER_CD,'') is NULL OR NULLIF(TNQC_OUT_CYCLE,'') is NULL OR NULLIF(CFC,'') is NULL
		)
		BEGIN
			INSERT INTO TB_T_ORDER_WARNING_LIST 
			([OCN],[CFC] ,[PART_NO] ,[ORD_LOT_SIZE] ,[STATUS_CD] ,[N] ,[N+1] ,[N+2] ,[N+3]  ,[ERR_MESG])
			SELECT CONCAT(o.COMPANY_CD,'-',o.SUPPLIER_CD,'/',LEFT(o.FIRM_PACK_MONTH,4),'/',RIGHT(o.FIRM_PACK_MONTH,2),'/',o.ORD_TYPE) AS OCN, o.CFC,o.PART_NO,o.ORD_LOT_SIZE,'1',
			N_MONTH_ORD_VOL,N1_MONTH_ORD_VOL,N2_MONTH_ORD_VOL,N3_MONTH_ORD_VOL,'No Items found where Required to Input'  
			from @o o
			WHERE NULLIF(COMPANY_CD,'') is NULL OR NULLIF(SUPPLIER_CD,'') is NULL OR NULLIF(TNQC_OUT_CYCLE,'') is NULL OR NULLIF(CFC,'') is NULL
			SET @existing = @@ROWCOUNT;

			SET @MSG_TXT = 'Total Warning List 3.5 : '+convert(VARCHAR(10),@existing);
			EXEC [SP_LOG_WRITE_DETAIL] @PROCESS_ID, @v_userId, 'Warning List [3.5]', '15I', @MSG_TXT, @NR_ERR OUTPUT
		END

		SELECT * INTO #TB_TEMP_WARNING_LIST_31_ALL FROM TB_T_ORDER_WARNING_LIST
		DELETE FROM TB_T_ORDER_WARNING_LIST

		INSERT INTO TB_T_ORDER_WARNING_LIST
		SELECT DENSE_RANK() OVER ( ORDER BY OCN ASC) AS [NO], [OCN],[CFC] ,[PART_NO] ,[ORD_LOT_SIZE] ,[STATUS_CD] ,[N] ,[N+1] ,[N+2] ,[N+3]  ,[ERR_MESG]
		FROM #TB_TEMP_WARNING_LIST_31_ALL
	END

	SELECT 1 AS REPORT_ID, @PROCESS_ID AS PROCESS_ID, @v_companyCode AS COMPANY_CD

END TRY
BEGIN CATCH
	
DECLARE @ErrorMessage NVARCHAR(4000)
DECLARE @ErrorSeverity INT
DECLARE @ErrorState INT
DECLARE @ErrorLine INT
	
SELECT @ErrorMessage = ERROR_MESSAGE(),
		@ErrorSeverity = ERROR_SEVERITY(),
		@ErrorState = ERROR_STATE(),
		@ErrorLine = ERROR_LINE()

		SET @process_flag = 2
		EXEC [dbo].SP_LOG_WRITE_FINISH @PROCESS_ID, @v_userId, @PROCESS_NAME, @process_flag, @NR_ERR;

		SELECT @return_message = @ErrorMessage + ', ' + CAST(@ErrorSeverity AS VARCHAR) + ', ' + CAST(@ErrorState AS VARCHAR) + ', ' + CAST(@ErrorState AS VARCHAR)

END CATCH

	EXEC [dbo].SP_LOG_WRITE_FINISH @PROCESS_ID, @v_userId, @PROCESS_NAME, @process_flag, @NR_ERR
	EXEC [dbo].[SP_UNLOCK_FUNCTION] @PROCESS_ID,@FUNCTION_ID
END