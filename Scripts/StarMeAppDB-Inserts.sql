INSERT INTO Users (Username, Password, Email)
VALUES ('tiki', 'abc1234', 'agustiki@hotmail.com');


INSERT INTO Star (Title, Summary, Content, IdUser)
VALUES (
	'Plugins',
	'A way to add custom client logic to a core funcionality.',
	'We had a lot of request from de clients, asking for add new funcionalities or replace core logic.',
	1
);

INSERT INTO Tag (Name)
VALUES ('Design Patterns'), ('ORM'), ('NHibernate'), ('SQL');

INSERT INTO Tag_Star (IdTag, IdStar)
VALUES (1, 2), (2, 2);