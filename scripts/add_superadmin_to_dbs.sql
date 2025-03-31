-- Переходим на базу mg_products и создаем пользователя, связанного с логином
USE mg_products;
CREATE USER dockeruser FOR LOGIN dockeruser;
ALTER ROLE db_owner ADD MEMBER dockeruser;

-- Переходим на базу mg_reviews и создаем пользователя, связанного с логином
USE mg_reviews;
CREATE USER dockeruser FOR LOGIN dockeruser;
ALTER ROLE db_owner ADD MEMBER dockeruser;

/* ============================================================================= */
USE master;
GO

CREATE LOGIN dockeruser WITH PASSWORD = 'YourStrongPassword!';
GO

CREATE USER dockeruser FOR LOGIN dockeruser;
GO

ALTER SERVER ROLE sysadmin ADD MEMBER dockeruser;
GO


USE mg_identity;
GO

CREATE USER dockeruser FOR LOGIN dockeruser;
GO

ALTER ROLE db_owner ADD MEMBER dockeruser;
GO

USE mg_products;
GO

CREATE USER dockeruser FOR LOGIN dockeruser;
GO

ALTER ROLE db_owner ADD MEMBER dockeruser;
GO

USE mg_reviews;
GO

CREATE USER dockeruser FOR LOGIN dockeruser;
GO

ALTER ROLE db_owner ADD MEMBER dockeruser;
GO