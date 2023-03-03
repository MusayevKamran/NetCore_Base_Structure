## Установка проекта в контейнере Docker


1. Откройте PowerShell
2. Перейдите в корневую папку проекта, где лежит файл **docker-compose.yml**
3. Выполнить команду
   ``` powershell
   docker-compose --env-file ./config/docker.development.env up -d
   ```
4. В Docker Desktop должен появиться новый контейнер с названием **audit-service-p2**, он будет включать в себя все необходимые сервисы для работы, например: ElasticSearch, Kibana, Redis и тд.
5. По умолчанию AuditService размещается на порту 3000, но его можно изменить в **docker-compose.yml**