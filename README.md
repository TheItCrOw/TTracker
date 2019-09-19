# TTracker
This is the repository for my TTracker - a tool that structures and organizes your personal workflow with different functions.

# Ticket Management

![TicketManagement](https://user-images.githubusercontent.com/49918134/64677437-d5857200-d477-11e9-9121-6897089cddd7.png)

TTracker has a build in local ticket-management-section. There you can create tickets/tasks choosing individual properties for each of them according to the content of the ticket. (Ticket-description, expected worktime of the ticket, priority, status and more).

You are not only able to create new tickets, but to delete them as well. Personally, I do not want to delete every "finished" ticket. But keeping them in the program may eventually cause long loading times. As a consequence, I built in an import/export logic that exports tickets that are "finished" and also imports them from the disc. Right now, exported tickets are Base64 encoded, but I'd like to change that in the future and rather zip them.

The "Progress" bar on the right shows you how much time you have already spent on a ticket and what time you expected it to take beforehand.

Every ticket must be subordinated to a project. Creating such projects must be done in the

# Project Management

![ProjectView](https://user-images.githubusercontent.com/49918134/64677434-d4ecdb80-d477-11e9-8a72-4bb89eab021b.png)

Here you can create new projects which tickets are subordinated to. The structure works as follows: You first create a new "Root" project. That may be a very general topic such as "IT-Projects". After the project has been created, you then proceed to fill this very general project with "Sub" projects. In my case, I created the project "TTracker" as a sub-project of "IT-Projects", since that is a specific project and falls under the theme of IT-projects. There is no option to subordinate a sub-project to another sub-project. I do believe that this structure would cause a row of hardly understandable projects and connections.

After a root - & sub-project has been created, you can now create your first ticket in the afore described ticket-management-section, choosing which project the created ticket should belong to.

# Time Engine

![TimeEngine](https://user-images.githubusercontent.com/49918134/64677438-d5857200-d477-11e9-8329-5d7826b6f894.png)

In the time engine you track your daily activities. It is up to you to decide to what extent you do that. Personally, I use it to track all of my time - the time I work, the time I study, the time I relax, the time I do sports and so forth. You essentially "book" your time on the tickets that you have created. At the end of the day you then have a clear list of what you did with your time and you can also generate a "Day Summary" as seen in the screenshot, which lists all your activities, generates a statistic out of it and saves it as a PDF file if you’d like to.

For everyone who wants to track all of his/her time, I implemented the "Static" tickets. These tickets do not have a time-limit, all they do is to represent an activity that is ongoing, e.g. my "Go to the gym" ticket. I do NOT create a new ticket each time I go to the gym. Usually there are no specific tasks while I'm doing sports – I‘m just working out. For that, I created a static ticket. Consequently, I can switch to the time engine and book the time of my workout on that ticket, so that I can track all of my time and make statistics out of it, without creating dozens of tickets for the same activity I do regularly.

# Statistics View

![StatisticsView](https://user-images.githubusercontent.com/49918134/64677435-d5857200-d477-11e9-8377-59c5a8636778.png)

One of the most interesting features of the TTracker is the statistics-generator. The whole concept of the program bundles in this very section. Everything you do, such as creating projects and tickets, booking your time, organizing your workflow, results in statistics. Here you can undoubtedly see how much time you spend on which activity. Are you happy with that? Do you work too much, do you study too less...these are all questions that you have probably asked yourself many times, but never really answered. Let the TTracker calculate any time-range-statistic you want: a day, a week, a month, a year, an endless period of time. It is a great way of visualising your efforts and seeing in what sections you need to do more and in which you could do less. 

# Calendar

![CalendarView](https://user-images.githubusercontent.com/49918134/64677432-d4ecdb80-d477-11e9-9706-b9dd2670c686.png)

The most recent feature implemented is a custom-made calendar. It works just like every other calendar: You create date-tickets and they are shown in the Calendar. You can switch between daily-, weekly-, monthly- & yearly View (Yearly not yet implemented). The vertical height of each ticket in the UI depends on the time that ticket has been appointed with. So a doctor’s appointment from 1pm - 3pm is smaller in height than a "Going out with friends" ticket from 7pm to 11pm.

# Home

![HomeView](https://user-images.githubusercontent.com/49918134/64677433-d4ecdb80-d477-11e9-96d7-217f8d0bb6dd.png)

To wrap up your duties for the day, the home view shows everything you need to know. It lists all tickets that are set on "Working", every appointment you have created in the calendar for today, a little statistic of the running week and lets you create micro tasks. These are essentially tasks that are not worthy of making a ticket for, but still mustn't be forgotten.

# Recap the project

I have learned a lot about WPF, C#, Visual Studio & Git in the process of making this software. I decided to go for an XML-Database, as I wanted the software to run offline as well and learn more about XML. I plan on adding an online-version integrated into the program that allows you to manage projects, tasks and tickets between several people much like Jira.
As this was one of my first projects with WPF technology, my former code does have some sections I would definitely change looking back at it now and there are still some things that aren't implemented yet. However, I am generally satisfied with the current state of the program and I do use it every day.

Things I would change today:
- Logging.
- XMLDataCache base works really fine, but it could be more generic and stable. (Try, Catch)
- UI is very primitive - I am not a designer, but I can do better.
- Data is currently stored plain, not encoded - but that is how I wanted it to be for better debugging. For a release, I would definitly change that.
- Passwords aren't hashed, due to the fact that I am not sure, whether I should keep the User logic. I do know, that you never store serious passwords without protection.
- ...
