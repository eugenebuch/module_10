# Система учёта посещаемости и успеваемости студентов

## Концепция

Данный проект представляет собой WEB API приложение, состоящие из 4 сущностей: Студент, Лектор, Лекция, Домашняя работа. Работает это так, что если студент посетил лекцию, он получает балл за посещение. Если он посетил лекцию и принёс домашнее задание, то это домашнее задание оценивается. Можно получить подробный отчёт о посещаемости по конкретному студенту (и всем его предметам) или по конкретному предмету (т.е. по всем студентам этого предмета). Так же в системе присутствует функционал, который отправляет емейл на почту студента и лектора, если балл студента по предмету ниже 3, но этот функционал не сделан в силу отсутствия доступа к коммуникационным каналам.

## Структура проекта

Проект представляет собой N-Layer архитектуру, где:

1. DAL - слой доступа к данным (подключение к БД, контекст данных, модели, репозитории, миграции)
2. BLL - слой бизнес логики (сервисы, функционал, логика взаимодействия, DTO объекты)
3. WEB (PL) - слой презентации (web api, интерфейс)

Помимо этого в проекте существуют тесты (Tests), которые покрывают весь функционал проекта (слои BLL, WEB целиком), а также проект полностью покрыт логгами и имеет самодельные exceptions и middlewares.
Слой ConsolePL нужен лишь для того, чтобы показать, что БД действительно есть и в нём хранятся данные.
Если БД не существует, программа автоматически её создаст и заполнит начальными данными.