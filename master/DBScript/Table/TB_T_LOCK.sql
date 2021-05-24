CREATE TABLE [dbo].[TB_T_LOCK](
	[PROCESS_ID] [bigint] NOT NULL,
	[FUNCTION_ID] [varchar](10) NULL,
	[LOCK_REFF] [varchar](255) NULL,
	[PROCESSED_BY] [varchar](20) NULL,
	[START_TIME] [datetime] NULL
) ON [PRIMARY]
GO