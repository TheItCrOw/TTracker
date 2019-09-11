# TTracker
This is the repository for my TTracker - a tool that structures and organizes your personal workflow with different functions.

# Ticket Management

![TicketManagement](https://user-images.githubusercontent.com/49918134/64677437-d5857200-d477-11e9-9121-6897089cddd7.png)

TTracker has a buildin local Ticket Management section. There you can create Tickets/Tasks choosing individual Properties for each of them to suit the content of the Ticket. (Ticket-Description, Expected worktime of the ticket, Priority, Status and more).

You may not only create new tickets, but delete them as well. Personally, I do not want to delete every "finished" ticket. But keeping them in the programm may eventually cause long loading times. As a consequence, I built in an Import/Export logic that exports tickets that are "finished" and also imports them from the disc. Currently exported tickets are Base64 encoded, but I'd like to change that in the future and rather zip them.

The "Progress" bar on the right shows you how much time you have already spent on a ticket and what time you expected it to cost.

Every ticket must be subordinated to a project it belongs. Creating such projects may be done in the

# Project Management

![ProjectView](https://user-images.githubusercontent.com/49918134/64677434-d4ecdb80-d477-11e9-8a72-4bb89eab021b.png)

Here you can create new projects which you then subordinate new tickets to. The structure goes as follows: You first create a new "Root" Project. That may be a very general topic such as "IT Projects". After this project is created, you then proceed to fill this very generel project with "Sub" Projects. In my case, I created the project "TTracker" as a sub project of "IT Projects", since that is a specific project and falls under the theme of IT projects. There is not an option to subordinate a sub project to another sub project. I do believe, that this structure would end in a total mess of never understandable projects and connections.

After a root & sub project is created, you can now create your first ticket in the afore described Ticket Management section, choosing which project the created ticket should belong to.

# Time Engine

![TimeEngine](https://user-images.githubusercontent.com/49918134/64677438-d5857200-d477-11e9-8329-5d7826b6f894.png)

In the Time Engine you track your time of the day. It is up to you, to what extense you do that. I use it to track all of my time - the time I work, the time I study, the time I play, the time I do sports and so forth. You essecntially "book" your time on the tickets that you have created. At the end of the day you then have a clear list of what you did with your time today and you can also generate a "Day Summary" as seen in the screenshot, which lists all your doings, generates a statistic out of it and saves it as a PDF file if you want to.

For everyone that wants to track all of his/her time, I implemented the "Static" tickets. These tickets do not have a time limit, all they do is represent an activity that is ongoing. E.g. my "Go to the gym" ticket. I do NOT create a new ticket each time I go to the gym. Usually, there are no specific tasks while I'm doing sports - I just go work out. For that, I created a static ticket. So now I can go into the Time Engine and book the time of my workout on that ticket, so I can track all of my time and make statistics out of it, without creating dozens of tickets for the same activity I do regulary.

# Statistics View

![StatisticsView](https://user-images.githubusercontent.com/49918134/64677435-d5857200-d477-11e9-8377-59c5a8636778.png)

One of the most interesting features of the TTracker are the statistics. The whole concept of the program bundles in this very section. All you do is create Projects, Tickets, book your time, organize your workflow and all of this results in statistics. Here you can undoublty see, how much time you spend on what activity. Are you happy with that? Do you work too much, do you study too less...these are all questions that you have probably asked yourself many times, but never answered undoubtly. Let the TTracker caluclate any timerange statistic you want - a day, week, month, year, all time. It is a greate way of visualising your efforts and seeing, in what sections you need to do more and in which you need to do less. 

# Calendar

![CalendarView](https://user-images.githubusercontent.com/49918134/64677432-d4ecdb80-d477-11e9-9706-b9dd2670c686.png)

The most recent feature implemented is a custom-made Calendar. It works just like every other Calendar: You create Date-Tickets and they are shown in the Calendar. You can switch between Daily-, Weekly-, Monthly- & Yearly View (Yearly not yet implemented). The height of each ticket depends on the time that ticket has been appointed with. So a doctors appointment from 1pm - 3pm is smaller in Height than "Going out with friends" ticket from 7pm to 11pm.

# Home

![HomeView](https://user-images.githubusercontent.com/49918134/64677433-d4ecdb80-d477-11e9-96d7-217f8d0bb6dd.png)

To wrap up your duties for the day, the Home View shows everything you need to know. It shows all tickets that are set on "Working", every Appointment you have created in the Calendar for today, shows a little statstic of the running week and lets you create Micro Tasks, which are essentially tasks that are not worthy of making a ticket for, but still musn't be forgotten.

# Recap the project

I have learned a lot about WPF, C#, Visual Studio & Git in the proccess of making this software. I decided to go for an XML-Database, since I wanted the software to run offline as well and also wanted to learn more about XML. I plan on making an Online-Version of this as well, integrated into the program, that allows you to manage projects tasks and tickets across several people much like Jira.
Since this was one of my first projects with WPF technology, my former Code does have some sections I would definitly change looking back at it now and there are still some things that aren't implemented yet.
