USE [prueba_tecnica]
GO
/****** Object:  Table [dbo].[zips]    Script Date: 6/06/2024 11:20:56 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[zips](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[archivo] [text] NOT NULL,
	[createdAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[zips] ADD  DEFAULT (getdate()) FOR [createdAt]
GO
