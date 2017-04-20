# Jeopardy

A web-based Jeopardy game for CS690 written in ASP.NET and C#.NET

## Projects in the Solution
- Jeopardy_Game - This project contains web forms 
- Database_Connection - A library of classes that interact with a MySQL Database
 
## How to run this project
1. Install [.NET MySQL Connector](https://dev.mysql.com/downloads/connector/net/)
1. Clone this repository
1. Copy the Example_Web.config file to Web.config
1. Add your mysql connection info in the appSettings node of Web.config and remove the html comment notation
1. Open the solution with Visual Studio
1. Right click the Jeopardy_Game project in the solution explorer and choose "Set As StartUp Project"
1. Click the green triangle button to run the project. This will start a web server inside of Visual Studio and open a web browser to show the web pages.
1. A 403 message in the browser indicates that the URL in the browser is probably wrong.

## Note on Database_Connection
This library expects to read mysql server connection from the config file of the project using the library.

## How to setup a database for the first time
This application relies on an external mysql database for storing its questions and high scores. The following explains how to prepare a database for use by this application.
1. Prepare a mysql database and user for the application to use.
1. Add the connection info to the Web.config as described in 'How to run this project'
1. Create the high scores table by running the createTable method in HighScoresTable.cs
1. Create the questions table by running the createTable method in QuestionsTable.cs
1. Populate the questions table with values. One way to do this is to create a csv file and use QuestionsTable.importRegularQuestionsFromCSV. CSV columns are as follows:
	1. question-the text of the question
	1. correct-the correct answer
	1. wrong1-the first wrong answer
	1. wrong2-the second wrong answer
	1. wrong3-the third wrong answer
	1. category-The category for the question. Categories that are the same must match exactly including case
	1. level-the difficulty of the question. integers 1-5
	1. reference-Where the question came from.
1. Three database fields are not in this csv format: ID because it uses auto-increment, used because this is initialized to false for all questions, and image_file because image questions cannot be added by this method.Iman