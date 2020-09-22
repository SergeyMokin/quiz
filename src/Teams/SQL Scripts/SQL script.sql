USE [aspNet]
GO
/****** Object:  Table [dbo].[OpenAnswerQuestions]    Script Date: 21.09.2020 16:04:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
if OBJECT_ID('[database]..OpenAnswerQuestions') IS NULL
CREATE TABLE [dbo].[OpenAnswerQuestions](
	[Id] [uniqueidentifier] NOT NULL,
	[TextQuestion] [nvarchar](max) NULL,
	[TextAnswer] [nvarchar](max) NULL,
 CONSTRAINT [PK_OpenAnswerQuestions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT INTO OpenAnswerQuestions(Id,TextQuestion,TextAnswer)	
		VALUES('2384b3e2-00f0-4915-bd13-e49abd464b89','What is the oldest public school in England?','Eton')
INSERT INTO OpenAnswerQuestions(Id,TextQuestion,TextAnswer)
		VALUES('a3da24da-966e-44ca-89ca-0b11885a2159','Who is the author of the gravitation theory?','Isaac Newton')
INSERT INTO OpenAnswerQuestions(Id,TextQuestion,TextAnswer)
		VALUES('dcd46233-12e8-4aaa-9ef9-07fe07c6f15e','When did Queen Elizabeth II become the queen of Britain?','1952')