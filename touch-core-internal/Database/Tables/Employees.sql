USE [TC_Internal]
GO

/****** Object:  Table [dbo].[Employees]    Script Date: 12/31/2019 7:24:12 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Employees](
	[Id] [uniqueidentifier] NOT NULL,
	[Identifier] [nvarchar](30) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Photo] [varbinary](max) NULL,
	[Email] [nvarchar](max) NULL,
	[Designation] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Employees] ADD  DEFAULT (newid()) FOR [Id]
GO


