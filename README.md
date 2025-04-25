# Repleet Backend

Hi There, Welcome to Repleet, the spaced repetition app for Data Structures and Algorithmn Problems. Visit the app [HERE](https://repleet-frontend.vercel.app/)

# How it Works
1) Fill out a quick survey about your current skill level in 18 popular categories, ranging from Linked Lists to Dynamic Programming.

  ![SurveyScreenshot](https://github.com/user-attachments/assets/512b1ca5-5840-4b72-bc9c-0f87418c2447)
  
3) Create and Account to persist your progress
4) Get personalized recommendation on which problem to tackle next based on your evolving skill level in each category and problem as well as your past history, and get a visual representation of your overall progress.
  ![CardScreenshot](https://github.com/user-attachments/assets/24217f2c-7b90-4dd8-b79e-3ca500372958)


  ![PercentScreenshot](https://github.com/user-attachments/assets/7a7d949d-293b-4fed-9e15-28c82606aa44)

5) After you attempt a problem, submit how well you did to update your progress and get a new reccomendation.
  ![image](https://github.com/user-attachments/assets/4acecee4-4706-40f7-bdc8-2fdd1c0b1b17)




# Tech Stack and Architecture Details
The Back end of Repleet uses ASP .Net Core in C# for the backend, XUnit for Unit and Integration Tests, ASP .Net Core Identity Framework for Authentication and User Management, and Microsoft SQL Server for the Database. As of Now, it's deployed with Azure but I'm looking at using Docker Compose to deploy elsewhere. The architecture follows MVC, such that you have your database entities in the "Models" folder, the controllers in the "controllers" folder, and the views are in a separate front end project which is [HERE](https://github.com/GonzaloAllenPerez444/Repleet-Front-End-Public).
