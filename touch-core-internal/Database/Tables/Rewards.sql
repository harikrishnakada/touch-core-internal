USE [Gratification]
GO

/****** Object:  Table [dbo].[Rewards]    Script Date: 12/31/2019 7:27:30 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Rewards](
	[Id] [uniqueidentifier] NOT NULL,
	[EmployeeId] [uniqueidentifier] NOT NULL,
	[BadgeId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Rewards] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Rewards]  WITH CHECK ADD  CONSTRAINT [FK_Rewards_BadgeId] FOREIGN KEY([BadgeId])
REFERENCES [dbo].[Badges] ([Id])
GO

ALTER TABLE [dbo].[Rewards] CHECK CONSTRAINT [FK_Rewards_BadgeId]
GO

ALTER TABLE [dbo].[Rewards]  WITH CHECK ADD  CONSTRAINT [FK_Rewards_EmployeeId] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employees] ([Id])
GO

ALTER TABLE [dbo].[Rewards] CHECK CONSTRAINT [FK_Rewards_EmployeeId]
GO


