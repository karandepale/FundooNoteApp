# FundooNoteApp
FundooNoteApp API Documentation

Welcome to my FundooNoteApp API documentation.FundooNoteApp is a note-taking application that allows users to create, manage, and organize their notes effectively. The application provides features such as user registration, note creation, search, archiving, and more.

 
Table of Contents:-

(1) Introduction

(2) User API

(3) Note API

(4) Authentication
 
(5) Error Handling


---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


INTRODUCTION:-
FundooNoteApp is a web-based note-taking application designed to help users keep track of their tasks, ideas, and important information. It provides a range of features, including user registration, note creation, management, and collaboration. 


-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------



USER API:-
The User API allows users to perform actions related to user accounts, such as registration, login, password management, and more.

-> Registration: Create a new user account by providing registration details.


-> Login: Authenticate users and provide access to their accounts.


-> Forgot Password: Initiate the process of resetting a forgotten password.


-> Reset Password: Change the user's password after authentication.


--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------



NOTES API:-
The Note API enables users to manage their notes efficiently, including creating, retrieving, updating, and deleting notes. Users can also perform actions like archiving, pinning, and searching for notes.

-> Create Note: Add a new note with various attributes such as title, content, and color.


-> Get All Notes: Retrieve a list of all notes associated with the user.


-> Get Note by ID: Retrieve a specific note using its unique ID. 


-> Update Note: Modify the contents and attributes of an existing note.


-> Delete Note: Permanently delete a note from the user's account.


-> Search Note: Search for notes based on a query string.


-> Archive Note: Archive a note to keep it out of the main notes list.


-> Pin Note: Pin a note to keep it at the top of the notes list.


-> Trash Note: Move a note to the trash, marking it for potential deletion.


-> Upload Image: Attach an image to a note for visual representation.




---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------






AUTHENTICATION:-

For security reasons, many of the endpoints require authentication using JSON Web Tokens (JWT). When making requests, include the JWT token in the Authorization header.






