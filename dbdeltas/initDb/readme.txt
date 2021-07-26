steps to init db:

1. run initDb.sql
2. enable sql authentication on the sql server
3. enable tcp/ip connection to sql server
4. run initConnectionUser.sql


insert [Tree_Entities] ([Id],[Name],[ParentId])
select 1,'Photos',NULL UNION ALL
select 2,'Documents',NULL UNION ALL
select 3,'Pdf Docs',2 UNION ALL
select 4,'Word Docs',2 UNION ALL
select 5,'Wedding',1 UNION ALL
select 6,'Birthday',1 UNION ALL
select 7,'Bar Mitzvah',1;
