USE [OneTransitAPI]
GO

/****** Object:  Table [dbo].[GTFS_Stops]    Script Date: 10/24/2011 16:29:27 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[GTFS_Stops](
	[RowKey] [uniqueidentifier] NOT NULL,
	[PartitionKey] [uniqueidentifier] NOT NULL,
	[ID] [varchar](50) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Code] [varchar](50) NULL,
	[Latitude] [decimal](18, 15) NOT NULL,
	[Longitude] [decimal](18, 15) NOT NULL,
 CONSTRAINT [PrimaryKey_ffe92477-8076-471d-abe5-cabd46c6f417] PRIMARY KEY CLUSTERED 
(
	[RowKey] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO

SET ANSI_PADDING OFF
GO

