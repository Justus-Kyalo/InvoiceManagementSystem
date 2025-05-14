-- Create the database if it doesn't exist
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'InvoiceManagementSystem')
BEGIN
    CREATE DATABASE [InvoiceManagementSystem];
END
GO

-- Restore the backup into the container
USE [master];
GO

RESTORE DATABASE [InvoiceManagementSystem]
FROM DISK = '/var/opt/mssql/backup/InvoiceManagementSystem.bak'
WITH
    MOVE 'InvoiceManagementSystem' TO '/var/opt/mssql/data/InvoiceManagementSystem.mdf',
    MOVE 'InvoiceManagementSystem_Log' TO '/var/opt/mssql/data/InvoiceManagementSystem.ldf',
    REPLACE;  -- Overwrite if the DB exists
GO


