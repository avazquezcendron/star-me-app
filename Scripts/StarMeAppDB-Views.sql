USE [StarMeAppDB]
GO

/****** Object:  View [dbo].[StarsHeaderInfo]    Script Date: 8/23/2020 3:44:36 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[StarsHeaderInfo]
AS
SELECT        TOP (10) dbo.Star.Title, dbo.Star.Summary, dbo.Users.Username,
				(SELECT STRING_AGG(t.Name, ',')
				FROM dbo.Tag t INNER JOIN
                    dbo.Tag_Star ON t.IdTag = dbo.Tag_Star.IdTag 
				WHERE dbo.Star.IdStar = dbo.Tag_Star.IdStar) AS Tags
FROM            dbo.Users INNER JOIN
                         dbo.Star ON dbo.Users.IdUser = dbo.Star.IdUser                         
ORDER BY dbo.Star.CreateTime DESC
GO


