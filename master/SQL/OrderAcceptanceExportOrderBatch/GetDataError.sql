select
convert(varchar(max),getdate()) as LOCALDATE
,[OCN]
,[CFC]
,[PARTNO]
,[ORDERLOTSIZE]
,[STATUSCODE]
,[N]
,[N+1]   as [NPLUS1]
,[N+2]	 as [NPLUS2]
,[N+3]	 as [NPLUS3]
,[ERR_MESG]
from REPORT_ERROR




