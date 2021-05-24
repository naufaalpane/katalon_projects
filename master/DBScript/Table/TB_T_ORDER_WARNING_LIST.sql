CREATE TABLE [dbo].TB_T_ORDER_WARNING_LIST(
	[NO] [smallint] NULL,
	[OCN] [varchar](max) NULL,
	[CFC] [varchar](4) NULL,
	[PART_NO] [varchar](12) NULL,
	[ORD_LOT_SIZE] [varchar](5) NULL,
	[STATUS_CD] [varchar](5) NULL,
	[N] NUMERIC(8,0) NULL,
	[N+1] NUMERIC(8,0) NULL,
	[N+2] NUMERIC(8,0) NULL,
	[N+3] NUMERIC(8,0) NULL,
	[ERR_MESG] [varchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO