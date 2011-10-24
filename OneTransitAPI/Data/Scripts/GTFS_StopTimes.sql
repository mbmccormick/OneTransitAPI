USE [OneTransitAPI]
GO

/****** Object:  Table [dbo].[GTFS_StopTimes]    Script Date: 10/24/2011 16:29:40 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[GTFS_StopTimes](
	[RowKey] [uniqueidentifier] NOT NULL,
	[PartitionKey] [uniqueidentifier] NOT NULL,
	[StopID] [varchar](50) NOT NULL,
	[TripID] [varchar](50) NOT NULL,
	[ArrivalTime] [smalldatetime] NOT NULL,
	[DepartureTime] [smalldatetime] NOT NULL,
	[StopSequence] [int] NOT NULL,
 CONSTRAINT [PrimaryKey_6007bf3a-c0d0-4bfd-9844-9ea4daefece6] PRIMARY KEY CLUSTERED 
(
	[RowKey] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)

GO

SET ANSI_PADDING OFF
GO

