# ASP.NET Core WellBeingDiary Web API Project
As the name sugetsts, it is a Web API for the WellBeingDiary. Purpose of this project is to make life better.

## WellBeing Diary - What is it?
Well, it is just a Diary, but what makes it special is the the fact, that it is WellBeing - more of that later.

## What is it for?
With the rise of civilization diseases like **Deppresion**, **Anxiety** and **Naurosis** It becomes harder and harder to live a life.
In Poland alone, nearly ***8 million*** people suffer from neurosis.
That is a lot of suffering people due to stress...

**So the main goal is to reduce stress.**

## How is a diary supposed to reduce stress?
You can hear a lot of coaches telling you to start journaling, that's one of key concepts in psychotherapy. It is documented that writing about your day
can help you clean up your mind, reduce stress, and live a happier life.

## What are the key concepts of WellBeing Diary?
Main features of this Diary are:
  - Rating your days, so you are able to see that it is not the end of the word when you have a bad day and feeling down.
  - later I will also add the required input of positive things that happened each day. This will help you see the light in the darkest moments.
  - Authorization using JWT Tokens. For security.

## What is inside the project:

### File Structure:

Well organized files following Repository pattern with Dto's, Mappers and Token Service.

![image](https://github.com/Kuruburu/WellBeingDiary/assets/135137385/df572e86-3202-4cdd-8816-9532d25a9ba8)

There are two controllers in this project:
  - Account
  ![image](https://github.com/Kuruburu/WellBeingDiary/assets/135137385/0ba5772b-3221-4516-998b-273b00bc005a)

    - PUT Account/register(all), lets you register with
      - username
      - email
      - passowrd, which must contain 8 letters, at least 1 digit, 1 lowercase letter, 1 uppercase letter, and 1 non alphanumeric
            
    returns your username, email and your JWT Token
      - ![image](https://github.com/Kuruburu/WellBeingDiary/assets/135137385/dba73228-a1fe-4607-b5a4-327a5cefee18)

    - PUT Account/login(all), let's you log in with 
      - username
      - password
        
    returns your username, email and your JWT Token
      - ![image](https://github.com/Kuruburu/WellBeingDiary/assets/135137385/a8f01ff6-9729-4f4f-b28b-bb0b5085a2b5)

    - GET Account/me(user)
      
    returns information about your account based on JWT Claims
      - ![image](https://github.com/Kuruburu/WellBeingDiary/assets/135137385/ddeae29e-d3c8-436b-9680-6d7acce52a2a)
     
    - GET Account/me/id(user)
      
    returns your id as a string based on JWT Claims
      - ![image](https://github.com/Kuruburu/WellBeingDiary/assets/135137385/84c90c36-b9b3-4cc5-bf64-888e71460e39)

    - GET Account(admin)
      
    returns all accounts created
     -  ![image](https://github.com/Kuruburu/WellBeingDiary/assets/135137385/9d1861b6-bc15-4c15-9a79-c3f7d893d57e)

    - GET Account/{id}(admin)
      
    returns account of id that is declared in path
      - ![image](https://github.com/Kuruburu/WellBeingDiary/assets/135137385/ff103169-b83b-46b1-aec8-91c5e773c18e)

    - DELETE Account/{id}(admin)
      
    Deletes user based of id that is declared in path with and returns 204
      -  ![image](https://github.com/Kuruburu/WellBeingDiary/assets/135137385/44afa22e-cfe6-4d4c-84bc-460497e557c5)

  - DiaryNotes
  ![image](https://github.com/Kuruburu/WellBeingDiary/assets/135137385/43f411f6-7caf-4eec-aeb5-56fd780d6f5f)

    - GET DiaryNotes(admin)

    returns all diary notes
     - ![image](https://github.com/Kuruburu/WellBeingDiary/assets/135137385/aa9628ab-af3b-4812-972b-b52003f9dc6a)

    - GET DiaryNotes/{id}(admin)

    returns diary note that is delcared in path
      - ![image](https://github.com/Kuruburu/WellBeingDiary/assets/135137385/78c1e7c8-1fd8-4f19-a07f-7675e6dfcd58)

    - GET DiaryNotes/me(user)
      
    return all your user diary notes
      - ![image](https://github.com/Kuruburu/WellBeingDiary/assets/135137385/367c6f56-d836-4a53-ae9a-4828a867db69)

    - POST DiaryNotes(user)

    Creates DiaryNote, assigns your id to it based on JWT Claims and returns created DiaryNote
     - ![image](https://github.com/Kuruburu/WellBeingDiary/assets/135137385/52236d69-2809-4f65-bf5d-24654a5370a3)
   
    - PUT DiaryNotes{id{(user)

    Updates DiaryNote and returns updated diary note
      - ![image](https://github.com/Kuruburu/WellBeingDiary/assets/135137385/80e25beb-9c71-442a-b8cb-3c25ba645640)

    -Delete DiaryNotes(admin)

    Deletes DiaryNote based on id that is declared in path and returns 204
      - ![image](https://github.com/Kuruburu/WellBeingDiary/assets/135137385/24df1fcd-e11a-436b-b583-ff0328604d16)

    




