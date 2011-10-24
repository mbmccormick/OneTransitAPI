USE [OneTransitAPI]
GO

/****** Object:  Table [dbo].[GTFS_Trips]    Script Date: 10/24/2011 16:29:48 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[GTFS_Trips](
	[RowKey] [uniqueidentifier] NOT NULL,
	[PartitionKey] [uniqueidentifier] NOT NULL,
	[ID] [varchar](50) NOT NULL,
	[RouteID] [varchar](50) NOT NULL,
	[ServiceID] [varchar](50) NOT NULL,
 CONSTRAINT [PrimaryKey_89da1300-27fc-4530-a589-021d496c68df] PRIMARY KEY CLUSTERED 
(
	[RowKey] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO

SET ANSI_PADDING OFF
GO


