CREATE TABLE GPPS_U_DEV.TB_R_TNQC_RESULT_AB
(
VER						VARCHAR2(1 BYTE)           NOT NULL,
COMPANY_CD              VARCHAR2(4 BYTE)		   NOT NULL,
PLANT_CD                VARCHAR2(1 BYTE)                 ,
DOCK_CD                 VARCHAR2(2 BYTE)                 ,
SUPPLIER_CD             VARCHAR2(4 BYTE)           NOT NULL,
SUPPLIER_PLANT_CD       VARCHAR2(1 BYTE)                ,
SHIP_DOC_CD             VARCHAR2(3 BYTE)				,
FIRM_PACK_MONTH         VARCHAR2(6 BYTE)           NOT NULL,
CFC                     VARCHAR2(4 BYTE)           NOT NULL,
PART_NO                 VARCHAR2(12 BYTE)                ,
LOT_CD                  VARCHAR2(2 BYTE)                ,
EXT_CD                  VARCHAR2(4 BYTE)                ,
INT_CD                  VARCHAR2(4 BYTE)                ,
CTL_KATA                VARCHAR2(20 BYTE)             ,
ORD_LOT_SIZE            VARCHAR2(5 BYTE)           NOT NULL,
KANBAN_NO               VARCHAR2(4 BYTE)               ,
PART_MATCH_KEY          VARCHAR2(6 BYTE)               ,
TNQC_OUT_CYCLE          VARCHAR2(1 BYTE)           NOT NULL,
DATA_ID                 VARCHAR2(4 BYTE)           NOT NULL,
REV                     VARCHAR2(4 BYTE)           NOT NULL,
ORD_TYPE                VARCHAR2(1 BYTE)              ,
PAMS_CFC                VARCHAR2(4 BYTE)              ,
RE_EXPORT_CD            VARCHAR2(1 BYTE)               ,
AICO_FLAG               VARCHAR2(1 BYTE)               ,
LOT_PXP_FLAG            VARCHAR2(1 BYTE)            ,
SOURCE_CD               VARCHAR2(1 BYTE)              ,
IMPORTER_NAME           VARCHAR2(3 BYTE)                ,
EXPORTER_NAME           VARCHAR2(3 BYTE)                ,
SS_NO                   VARCHAR2(2 BYTE)            NOT NULL,
PAMS_SS_NO              VARCHAR2(2 BYTE)                ,
LINE_CD                 VARCHAR2(1 BYTE)                ,
DISPLAY_KATA            VARCHAR2(20 BYTE)                ,
N_MONTH_ORD_VOL         NUMERIC(8,0)	            NOT NULL,
N1_MONTH_ORD_VOL        NUMERIC(8,0)	            NOT NULL,
N2_MONTH_ORD_VOL        NUMERIC(8,0)	            NOT NULL,
N3_MONTH_ORD_VOL        NUMERIC(8,0)	            NOT NULL,
N_DAY_VOL_01            NUMERIC(7,0)	            NOT NULL,
N_DAY_VOL_02            NUMERIC(7,0)	            NOT NULL,
N_DAY_VOL_03            NUMERIC(7,0)	            NOT NULL,
N_DAY_VOL_04            NUMERIC(7,0)	            NOT NULL,
N_DAY_VOL_05            NUMERIC(7,0)	            NOT NULL,
N_DAY_VOL_06            NUMERIC(7,0)	            NOT NULL,
N_DAY_VOL_07            NUMERIC(7,0)	            NOT NULL,
N_DAY_VOL_08            NUMERIC(7,0)	            NOT NULL,
N_DAY_VOL_09            NUMERIC(7,0)	            NOT NULL,
N_DAY_VOL_10            NUMERIC(7,0)	            NOT NULL,
N_DAY_VOL_11            NUMERIC(7,0)	            NOT NULL,
N_DAY_VOL_12            NUMERIC(7,0)	            NOT NULL,
N_DAY_VOL_13            NUMERIC(7,0)	            NOT NULL,
N_DAY_VOL_14            NUMERIC(7,0)	            NOT NULL,
N_DAY_VOL_15            NUMERIC(7,0)	            NOT NULL,
N_DAY_VOL_16            NUMERIC(7,0)	            NOT NULL,
N_DAY_VOL_17            NUMERIC(7,0)	            NOT NULL,
N_DAY_VOL_18            NUMERIC(7,0)	            NOT NULL,
N_DAY_VOL_19            NUMERIC(7,0)	            NOT NULL,
N_DAY_VOL_20            NUMERIC(7,0)	            NOT NULL,
N_DAY_VOL_21            NUMERIC(7,0)	            NOT NULL,
N_DAY_VOL_22            NUMERIC(7,0)	            NOT NULL,
N_DAY_VOL_23            NUMERIC(7,0)	            NOT NULL,
N_DAY_VOL_24            NUMERIC(7,0)	            NOT NULL,
N_DAY_VOL_25            NUMERIC(7,0)	            NOT NULL,
N_DAY_VOL_26            NUMERIC(7,0)	            NOT NULL,
N_DAY_VOL_27            NUMERIC(7,0)	            NOT NULL,
N_DAY_VOL_28            NUMERIC(7,0)	            NOT NULL,
N_DAY_VOL_29            NUMERIC(7,0)	            NOT NULL,
N_DAY_VOL_30            NUMERIC(7,0)	            NOT NULL,
N_DAY_VOL_31            NUMERIC(7,0)	            NOT NULL,
N1_DAY_VOL_01           NUMERIC(7,0)	            NOT NULL,
N1_DAY_VOL_02           NUMERIC(7,0)	            NOT NULL,
N1_DAY_VOL_03           NUMERIC(7,0)	            NOT NULL,
N1_DAY_VOL_04           NUMERIC(7,0)	            NOT NULL,
N1_DAY_VOL_05           NUMERIC(7,0)	            NOT NULL,
N1_DAY_VOL_06           NUMERIC(7,0)	            NOT NULL,
N1_DAY_VOL_07           NUMERIC(7,0)	            NOT NULL,
N1_DAY_VOL_08           NUMERIC(7,0)	            NOT NULL,
N1_DAY_VOL_09           NUMERIC(7,0)	            NOT NULL,
N1_DAY_VOL_10           NUMERIC(7,0)	            NOT NULL,
N1_DAY_VOL_11           NUMERIC(7,0)	            NOT NULL,
N1_DAY_VOL_12           NUMERIC(7,0)	            NOT NULL,
N1_DAY_VOL_13           NUMERIC(7,0)	            NOT NULL,
N1_DAY_VOL_14           NUMERIC(7,0)	            NOT NULL,
N1_DAY_VOL_15           NUMERIC(7,0)	            NOT NULL,
N1_DAY_VOL_16           NUMERIC(7,0)	            NOT NULL,
N1_DAY_VOL_17           NUMERIC(7,0)	            NOT NULL,
N1_DAY_VOL_18           NUMERIC(7,0)	            NOT NULL,
N1_DAY_VOL_19           NUMERIC(7,0)	            NOT NULL,
N1_DAY_VOL_20           NUMERIC(7,0)	            NOT NULL,
N1_DAY_VOL_21           NUMERIC(7,0)	            NOT NULL,
N1_DAY_VOL_22           NUMERIC(7,0)	            NOT NULL,
N1_DAY_VOL_23           NUMERIC(7,0)	            NOT NULL,
N1_DAY_VOL_24           NUMERIC(7,0)	            NOT NULL,
N1_DAY_VOL_25           NUMERIC(7,0)	            NOT NULL,
N1_DAY_VOL_26           NUMERIC(7,0)	            NOT NULL,
N1_DAY_VOL_27           NUMERIC(7,0)	            NOT NULL,
N1_DAY_VOL_28           NUMERIC(7,0)	            NOT NULL,
N1_DAY_VOL_29           NUMERIC(7,0)	            NOT NULL,
N1_DAY_VOL_30           NUMERIC(7,0)	            NOT NULL,
N1_DAY_VOL_31           NUMERIC(7,0)	            NOT NULL,
SYS_DT                  VARCHAR2(8 BYTE)		         ,
SERIES_NAME             VARCHAR2(20 BYTE)                ,
LIFE_CYCLE_CD           VARCHAR2(1 BYTE) 	               ,
NMIN1_MONTH_VOL         NUMERIC(8,0)	            NOT NULL,
N_MONTH_VOL             NUMERIC(8,0)	            NOT NULL,
N1_MONTH_VOL            NUMERIC(8,0)	            NOT NULL,
N2_MONTH_VOL            NUMERIC(8,0)	            NOT NULL,
BACK_ADJUST_SIGN        VARCHAR2(8 BYTE)            NOT NULL,
BACK_ADJUST_QTY         NUMERIC(8,0)	            NOT NULL,
RATIO_LAST_MONTH        VARCHAR2(3 BYTE)            NOT NULL,
N_MONTH_DAY_VOL_MAX     NUMERIC(7,0)                NOT NULL,
N_MONTH_DAY_VOL_MIN     NUMERIC(7,0)                NOT NULL,
N_MONTH_DAY_VOL_VAR     NUMERIC(3,0)                NOT NULL,
SUPPLIER_NAME           VARCHAR2(40 BYTE)                ,
PART_NAME               VARCHAR2(40 BYTE)                ,
JCFD                    VARCHAR2(5 BYTE)               ,
CONTA_CLASS             VARCHAR2(1 BYTE)                ,
BULK_LOT_CD             VARCHAR2(2 BYTE)                ,
TERMINATE_CD            VARCHAR2(1 BYTE)            NOT NULL,
CREATED_DT              DATE                        NOT NULL,
CHANGED_DT              DATE                        NOT NULL,
CREATED_BY              VARCHAR2(25 BYTE),
CHANGED_BY              VARCHAR2(25 BYTE)
)
TABLESPACE GPPS_U_DEV
PCTUSED    0
PCTFREE    10
INITRANS   1
MAXTRANS   255
STORAGE    (
            INITIAL          128K
            NEXT             1M
            MINEXTENTS       1
            MAXEXTENTS       UNLIMITED
            PCTINCREASE      0
            BUFFER_POOL      DEFAULT
           )
LOGGING 
NOCOMPRESS 
NOCACHE
MONITORING;

CREATE UNIQUE INDEX GPPS_U_DEV.PK_TB_R_TNQC_RESULT_AB ON GPPS_U_DEV.TB_R_TNQC_RESULT_AB
(	VER					,
    COMPANY_CD        	,
    PLANT_CD         	,
    DOCK_CD           	,
    SUPPLIER_CD       	,
    SUPPLIER_PLANT_CD	,
    SHIP_DOC_CD      	,
    FIRM_PACK_MONTH  	 ,
    CFC               	,
    PART_NO           	,
    LOT_CD            	,
    EXT_CD            	,
    INT_CD            	,
    CTL_KATA         	,
    ORD_LOT_SIZE     	,
    KANBAN_NO        	,
    PART_MATCH_KEY    	,
    TNQC_OUT_CYCLE    
)
LOGGING
TABLESPACE GPPS_U_DEV
PCTFREE    10
INITRANS   2
MAXTRANS   255
STORAGE    (
            INITIAL          128K
            NEXT             1M
            MINEXTENTS       1
            MAXEXTENTS       UNLIMITED
            PCTINCREASE      0
            BUFFER_POOL      DEFAULT
           );

ALTER TABLE GPPS_U_DEV.TB_R_TNQC_RESULT_AB ADD (
  CONSTRAINT PK_TB_R_TNQC_RESULT_AB
  PRIMARY KEY
  (	VER					,
    COMPANY_CD        	,
    PLANT_CD         	,
    DOCK_CD           	,
    SUPPLIER_CD       	,
    SUPPLIER_PLANT_CD	,
    SHIP_DOC_CD      	,
    FIRM_PACK_MONTH  	 ,
    CFC               	,
    PART_NO           	,
    LOT_CD            	,
    EXT_CD            	,
    INT_CD            	,
    CTL_KATA         	,
    ORD_LOT_SIZE     	,
    KANBAN_NO        	,
    PART_MATCH_KEY    	,
    TNQC_OUT_CYCLE    
  )
  USING INDEX GPPS_U_DEV.PK_TB_R_TNQC_RESULT_AB
  ENABLE VALIDATE);
