USE [Gratification]
GO

/****** Object:  Table [dbo].[Badges]    Script Date: 12/31/2019 7:27:05 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Badges](
	[Id] [uniqueidentifier] NOT NULL,
	[BadgeName] [nchar](100) NOT NULL,
	[Category] [nchar](50) NOT NULL,
	[Score] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Badges] ADD  DEFAULT (newid()) FOR [Id]
GO


