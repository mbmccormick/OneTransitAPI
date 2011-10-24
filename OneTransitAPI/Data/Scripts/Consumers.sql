USE [OneTransitAPI]
GO

/****** Object:  Table [dbo].[Consumers]    Script Date: 10/24/2011 16:28:17 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Consumers](
	[ConsumerKey] [uniqueidentifier] NOT NULL,
	[EmailAddress] [varchar](50) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PrimaryKey_125b978f-0747-4c25-b70e-6beb5e335b6c] PRIMARY KEY CLUSTERED 
(
	[ConsumerKey] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO

SET ANSI_PADDING OFF
GO

