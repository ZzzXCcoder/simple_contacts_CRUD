# Развертывание с помощью Nginx на Ubuntu

## Требования

- .NET SDK 8
- .NET Runtime 8
- ASP.NET Core Runtime
- Nginx
- SQLite

## Настройка Nginx

1. Открой файл конфигурации Nginx:

   ```bash
   sudo nano /etc/nginx/sites-available/default

   и измените location
   location    /{
       proxy_pass         http://127.0.0.1:5000;
       proxy_http_version 1.1;
       proxy_set_header   Upgrade $http_upgrade;
       proxy_set_header   Connection keep-alive;
       proxy_set_header   Host $host;
       proxy_cache_bypass $http_upgrade;
       proxy_set_header   X-Forwarded-For $proxy_add_x_forwarded_for;
       proxy_set_header   X-Forwarded-Proto $scheme;   
   }
Перезапусти Nginx для применения изменений:
sudo systemctl restart nginx
Для запуска опубликованного приложения используй команду:
dotnet contacts_CRUD.dll

Запуск через Docker
В папке с опубликованным приложением создайте файл Dockerfile с содержимым:
   FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

   WORKDIR /app
   
   COPY . .
   
   ENV ConnectionStrings__DefaultConnection="Data Source=/app/Contacts.db"
   
   EXPOSE 5000
   
   ENTRYPOINT ["dotnet", "contacts_CRUD.dll"]

Соберите образ Docker:
sudo docker build -t <названиербраза> .


Запустите контейнер:
sudo docker run -d -p 8000:80 --name contacts-api-container <названиербраза>
