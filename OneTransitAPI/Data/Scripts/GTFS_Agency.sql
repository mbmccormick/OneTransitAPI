USE [OneTransitAPI]
GO

/****** Object:  Table [dbo].[GTFS_Agency]    Script Date: 10/24/2011 16:28:32 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[GTFS_Agency](
	[RowKey] [uniqueidentifier] NOT NULL,
	[PartitionKey] [uniqueidentifier] NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[URL] [varchar](500) NULL,
	[TimeZone] [varchar](50) NOT NULL,
	[State] [varchar](50) NOT NULL,
	[ID] [varchar](50) NULL,
 CONSTRAINT [PrimaryKey_73d89562-b49f-40b1-a5f0-3a43a346fd46] PRIMARY KEY CLUSTERED 
(
	[RowKey] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO

SET ANSI_PADDING OFF
GO


