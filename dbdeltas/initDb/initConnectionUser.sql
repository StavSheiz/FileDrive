USE [master]
GO

/* For security reasons the login is created disabled and with a random password. */
/****** Object:  Login [filedriveadmin]    Script Date: 29/12/2020 15:57:29 ******/
CREATE LOGIN [filedriveadmin] WITH PASSWORD=N'R9x6d0dj1n1ikvNL3JmBGPQLImWCBMAyxJ3a/2VCVr0=', DEFAULT_DATABASE=[FileDriveDB], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=ON
GO

ALTER LOGIN [filedriveadmin] ENABLE
GO

GRANT CREATE TABLE TO filedriveadmin;
