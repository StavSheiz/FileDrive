USE [master]
GO

/* For security reasons the login is created disabled and with a random password. */
/****** Object:  Login [filedriveadmin]    Script Date: 29/12/2020 15:57:29 ******/
CREATE LOGIN [filedriveadmin] WITH PASSWORD=N'Password123', DEFAULT_DATABASE=[FileDriveDB], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=ON
GO

ALTER LOGIN [filedriveadmin] ENABLE
GO

USE [FileDriveDB]
GO
CREATE USER [filedriveadmin] FOR LOGIN [filedriveadmin];
GRANT CREATE TABLE TO [filedriveadmin];
GRANT ALTER ON SCHEMA::dbo TO filedriveadmin;
GRANT REFERENCES ON SCHEMA::dbo TO filedriveadmin;
GRANT SELECT ON SCHEMA::dbo TO filedriveadmin;
GRANT INSERT ON SCHEMA::dbo TO filedriveadmin;
GRANT UPDATE ON SCHEMA::dbo TO filedriveadmin;
GRANT DELETE ON SCHEMA::dbo TO filedriveadmin;