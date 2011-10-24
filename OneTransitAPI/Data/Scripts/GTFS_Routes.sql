USE [OneTransitAPI]
GO

/****** Object:  Table [dbo].[GTFS_Routes]    Script Date: 10/24/2011 16:29:16 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[GTFS_Routes](
	[RowKey] [uniqueidentifier] NOT NULL,
	[PartitionKey] [uniqueidentifier] NOT NULL,
	[ID] [varchar](50) NOT NULL,
	[LongName] [varchar](100) NOT NULL,
	[ShortName] [varchar](100) NOT NULL,
	[Type] [tinyint] NOT NULL,
 CONSTRAINT [PrimaryKey_d8955b7d-d32a-4ec2-8612-5169fcbc8d18] PRIMARY KEY CLUSTERED 
(
	[RowKey] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO

SET ANSI_PADDING OFF
GO

