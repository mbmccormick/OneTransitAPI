USE [OneTransitAPI]
GO

/****** Object:  Table [dbo].[GTFS_Calendar]    Script Date: 10/24/2011 16:29:01 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[GTFS_Calendar](
	[RowKey] [uniqueidentifier] NOT NULL,
	[PartitionKey] [uniqueidentifier] NOT NULL,
	[ServiceID] [varchar](50) NOT NULL,
	[Monday] [bit] NOT NULL,
	[Tuesday] [bit] NOT NULL,
	[Wednesday] [bit] NOT NULL,
	[Thursday] [bit] NOT NULL,
	[Friday] [bit] NOT NULL,
	[Saturday] [bit] NOT NULL,
	[Sunday] [bit] NOT NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date] NOT NULL,
 CONSTRAINT [PrimaryKey_abdabc02-c099-4559-bf09-e1fd8161c809] PRIMARY KEY CLUSTERED 
(
	[RowKey] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO

SET ANSI_PADDING OFF
GO

