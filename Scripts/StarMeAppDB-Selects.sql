SELECT * FROM Star;
SELECT * FROM Users;
SELECT * FROM Tag;
SELECT * FROM Tag_Star;

SELECT s.Title, u.Username, t.name as 'Tag'
FROM Star s INNER JOIN Users u ON s.IdUser = u.IdUser
			LEFT JOIN Tag_Star ts ON ts.IdStar = s.IdStar
			LEFT JOIN Tag t ON t.IdTag = ts.IdTag;




SELECT * FROM StarsHeaderInfo;

SELECT        TOP (10) dbo.Star.Title, dbo.Star.Summary, dbo.Users.Username,
				(SELECT STRING_AGG(t.Name, ',')
				FROM dbo.Tag t INNER JOIN
                    dbo.Tag_Star ON t.IdTag = dbo.Tag_Star.IdTag 
				WHERE dbo.Star.IdStar = dbo.Tag_Star.IdStar) AS Tags
FROM            dbo.Users INNER JOIN
                         dbo.Star ON dbo.Users.IdUser = dbo.Star.IdUser                         
ORDER BY dbo.Star.CreateTime DESC