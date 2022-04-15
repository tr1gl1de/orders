Orders
=============================

Задача этого проекта: Реализовать простое веб приложение
для приемки заказа на доставку.



Необходимые зависимости
------------
Linux или Windows с установленным .NET SDK 6,
а также установленной PostgreSQL.

Запуск
-----------

1. Перед запуском внести изменения в файл appsettings.Development.json
 в соответствии с вашей конфигурацией PostgreSQL
 
```json
{
  "ConnectionStrings": {
    "PostgreSQL" : "Server=localhost;Port=5432;Database=your_name_db;User Id=db_user;Password=your_password"
  }
}
```
2. Откройте командную строку и выполните данную команду
```shell
dotnet tool install --global dotnet-ef
```
3. Откройте комнадную строку в директории проекта и выполните следующие команды:
```shell
dotnet restore
dotnet ef database update
```
4. Для запуска выполняет данную команду в директории проекта:
```shell
dotnet run
```

5. Реализовано
   - [ ] WebAPI:
     - [x] Rest CRUD API
     - [x] DTO
     - [x] Документация OpenAPI
     - [x] Валидация
     - [ ] Расширение API - пользователи*
     - [ ] Расширение API - JWT Auth*
   - [ ] UI:
     - [ ] Форма создания нового заказа
     - [ ] форма отображения списка заказов
     - [ ] Форма регистрации*
     - [ ] Форма аутентификации*