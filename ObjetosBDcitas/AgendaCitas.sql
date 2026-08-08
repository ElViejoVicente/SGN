USE [Notaria01]
GO

/****** Object:  Table [dbo].[AgendaCitas]    Script Date: 08/08/2026 10:40:05 a. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AgendaCitas](
	[IdCita] [int] IDENTITY(1,1) NOT NULL,
	[FechaInicio] [datetime] NOT NULL,
	[FechaFin] [datetime] NOT NULL,
	[TodoDia] [bit] NOT NULL,
	[Asunto] [nvarchar](200) NOT NULL,
	[Descripcion] [nvarchar](max) NULL,
	[Ubicacion] [nvarchar](200) NULL,
	[Estatus] [int] NULL,
	[Etiqueta] [int] NULL,
	[Tipo] [int] NOT NULL,
	[RecurrenceInfo] [nvarchar](max) NULL,
	[ReminderInfo] [nvarchar](max) NULL,
	[IdRecurso] [int] NULL,
	[IdExpediente] [varchar](50) NULL,
	[IdTipoCita] [int] NULL,
	[UsuarioCrea] [varchar](50) NULL,
	[FechaCrea] [datetime] NOT NULL,
	[UsuarioMod] [varchar](50) NULL,
	[FechaMod] [datetime] NULL,
 CONSTRAINT [PK_AgendaCitas] PRIMARY KEY CLUSTERED 
(
	[IdCita] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[AgendaCitas] ADD  CONSTRAINT [DF_AgendaCitas_TodoDia]  DEFAULT ((0)) FOR [TodoDia]
GO

ALTER TABLE [dbo].[AgendaCitas] ADD  CONSTRAINT [DF_AgendaCitas_Tipo]  DEFAULT ((0)) FOR [Tipo]
GO

ALTER TABLE [dbo].[AgendaCitas] ADD  CONSTRAINT [DF_AgendaCitas_FechaCrea]  DEFAULT (getdate()) FOR [FechaCrea]
GO

ALTER TABLE [dbo].[AgendaCitas]  WITH CHECK ADD  CONSTRAINT [FK_AgendaCitas_Etiqueta] FOREIGN KEY([Etiqueta])
REFERENCES [dbo].[Cat_AgendaEtiqueta] ([IdEtiqueta])
GO

ALTER TABLE [dbo].[AgendaCitas] CHECK CONSTRAINT [FK_AgendaCitas_Etiqueta]
GO

ALTER TABLE [dbo].[AgendaCitas]  WITH CHECK ADD  CONSTRAINT [FK_AgendaCitas_Recurso] FOREIGN KEY([IdRecurso])
REFERENCES [dbo].[Cat_AgendaRecurso] ([IdRecurso])
GO

ALTER TABLE [dbo].[AgendaCitas] CHECK CONSTRAINT [FK_AgendaCitas_Recurso]
GO


