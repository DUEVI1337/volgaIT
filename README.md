# volgaIT
### Прикладное программирование (Java/C#)
    
# Развертывание проекта.
    
    1. После скачивания и распаковки архива с проектом необходимо открыть командную строку и перейти
       в папку с проектом до .sln файла включительно (выполнить команду "cd <пути_к_папке_с_проектом>").
![image](https://user-images.githubusercontent.com/100204371/165747267-6cfc7066-6043-4f14-b7cb-5d53ce5c3807.png)

![image](https://user-images.githubusercontent.com/100204371/165747842-e5e70e3d-a1b7-4900-91d9-291428e26676.png)

    2. Запустите Docker desktop, если он еще не запущен.
    
    3. После запуска docker необходимо прописать следующие команды в командной строке
            docker-compose build
            ... //сборка
            docker-compose up
            ...//установка образов
            
![image](https://user-images.githubusercontent.com/100204371/165748249-16f132b8-a8dc-45ae-bb55-e09e6f833aa4.png)

![image](https://user-images.githubusercontent.com/100204371/165748660-2f643876-f4cb-4d34-941e-1a6e46b14ce2.png)

     Если мы перейдем в Docker, то увидим наши запущенные контейнеры с приложение и СУБД. 
    
![image](https://user-images.githubusercontent.com/100204371/165749369-0694f4ee-20a6-45de-85df-f02743f4eed0.png)


    4. Наш проект развернут и готов к работе, осталось посмотреть порт нашего приложения
    в Docker и перейти по следующему URL: https://localhost:<порт_приложения>/
    
![image](https://user-images.githubusercontent.com/100204371/165749917-d61029b1-d511-4523-a848-1a677c231473.png)

    5. Готово!

![image](https://user-images.githubusercontent.com/100204371/165750365-da5530c2-9347-49e5-b702-97c5ee1122fd.png)


# Реализованный функционал.

    При переходе на сайт нас будет встречать главная страница, из нее мы можем попасть на страницу регистрации и авторизации.
    
    На странице авторизации была реализован дополнительный функционал по восстановлению пароля
    (вы можете указать при регистрации действующую почту и
    на нее придет письмо с ссылкой на форму создания нового пароля).
    
    После успешного вход в систему перед нами будет главная страница пользователя, в меню сверху мы можем выйти из аккаунта,
    создать новое приложение, просмотреть наши приложения.
    
    Из страницы со списком наших приложений мы можем просматривать статистику каждого отдельного приложения.
    
    Также на странице просмотра наших приложений был реализован следующей дополнительный функционал:
    изменение наименование приложения, удаление приложения.
    
    Дополнительно была реализована страница профиля пользователя, на которой мы можем изменить электронную почту.
    
    На сайте реализован метод POST принятия запросов от пользователя, для сбора статистики событий происходящих 
    в приложение пользователя (инструкция по отправке запросов к данному методу описан далее).
    
# Инструкция по отправке запросов к сервису для отслеживания статистики.
    
    Для имитации работающего приложения мы будем использовать сервис по тестированию API - "Postman", из этого сервиса будем отправлять
    запросы к нашему сайту.
    
    Для тестирования нашего метода необходимо:
    
    1. Запустить сервис "Postman".
    
 ![image](https://user-images.githubusercontent.com/100204371/165756167-9532ee5c-e36e-4bb4-abf8-e0b2e6abe6f8.png)
 
    2. В указанной области выбрать из выпадающего списка "POST"
    
![image](https://user-images.githubusercontent.com/100204371/165756467-2f8a2e39-9d18-480b-822e-b18d5fcf4d70.png)

    3. В следующее указанной области нужно написать URL нашего запущенного сайта, дополнительно указать имя контроллера,
    в котором реализован нужный нам метод (App),
    название самого метода (CreateRequest) и указать параметры метода (id, nameEvent, bonusInfo) где,
    "id" - id нашего приложения, "nameEvent" - имя события, "bonusInfo" - дополнительная информация.
    (https://localhost:<порт>/App/CreateRequest?id=<id_приложения>&nameEvent=<имя_события>&bonusInfo=<дополнительная_информация>)
    
![image](https://user-images.githubusercontent.com/100204371/165756794-284776de-486d-4554-b7fe-0121e9ee5b1b.png)
    
        Доступные имена событий для использования: "View", "SignIn", "Register", "Click".

    4. Нажать кнопку "Send".
    
![image](https://user-images.githubusercontent.com/100204371/165759004-bf37bbeb-72a2-40cb-9b6f-ed69a2cbbd95.png)

    5. Если все прошло удачно, то Postman вернут нам код 200.
    
![image](https://user-images.githubusercontent.com/100204371/165759855-48de8e39-42f0-4c7c-bd87-3220aaafa63e.png)
    
    6. Просмотреть статистику мы можем на странице со списком наших приложений и выбрав соответсвующее приложение перейти на страницу "статистика"
    (ссылка https://localhost:<порт>/AppsActions/StatisticsApp/<id приложения>)


