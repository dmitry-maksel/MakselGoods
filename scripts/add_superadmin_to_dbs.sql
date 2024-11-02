-- Переходим на базу mg_products и создаем пользователя, связанного с логином
USE mg_products;
CREATE USER dockeruser FOR LOGIN dockeruser;
ALTER ROLE db_owner ADD MEMBER dockeruser;

-- Переходим на базу mg_reviews и создаем пользователя, связанного с логином
USE mg_reviews;
CREATE USER dockeruser FOR LOGIN dockeruser;
ALTER ROLE db_owner ADD MEMBER dockeruser;