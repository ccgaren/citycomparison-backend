CREATE TABLE [dbo].[WeatherCity](
	[Id] uniqueidentifier NOT NULL,
	[CityA] [nvarchar](60) NOT NULL,
	[CityB] [nvarchar](60) NULL,
	[UserId] uniqueidentifier NOT NULL,
 CONSTRAINT [PK_WeatherCity] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[WeatherCity]  WITH CHECK ADD  CONSTRAINT [FK_WeatherCity_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO



