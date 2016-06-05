# LevisBot
A Bot that provides university information about it's students. The information currently includes:
-	Course Grades
-	Grades of Assignments / Tests / Exams
It is implemented using the [BotBuilder SDK](https://github.com/Microsoft/BotBuilder) and can thereby be registered on various supported chat clients (skype, sms, slack, ...).

# Requirements
-	[Luis AI Project](https://www.luis.ai/) - for language recognition
-	[Bot Framework Bot](https://dev.botframework.com) - Bot Connector for registering the bot to various channels/clients
-	[Bot Framework Emulator](http://docs.botframework.com/connector/tools/bot-framework-emulator/)  **(for testing only)**

# Setup
* Add a `secrets.config` file containing the following information:

	```
	<?xml version="1.0"?>
	<appSettings>
		<add key="AppId" value="<bot-app-id>" />
		<add key="AppSecret" value="<bot-app-secret" />
		<add key="LuisModelId" value="<luis-model-id>"/>
		<add key="LuisSubscriptionKey" value="<luis-subscription-key>"/>
	</appSettings>
	```
	
* Train your LUIS application:

 `CourseGradeQuery`: Query about the overall grade of a course (described by the entity `Course`).

 `GradeQuery`:  Query about the grade of a specific gradeable type (e.g. assignment, test, exam; descriped by the entity `Type` and the entity `Ordinal` for numbering) in the course (described by the entity `Course`).
* Deploy

# Example
![Chat Example](http://i.imgur.com/rEW6yDu.gif)

# Motivation
This is the result of a university assignment regarding `Service Engineering`, during my master studies at the [University of Applied Sciences Hagenberg](https://www.fh-ooe.at/en/hagenberg-campus/).
