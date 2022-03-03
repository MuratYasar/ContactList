# My Contact List
This is my first time trying microservice design in a project to see how things are connected.

There are many things to consider such as checking required fields on UI screens.
I haven't paid much attention to this sort of thing, instead try to understand how things are interconnected in terms of microservices.

![MyContactList](https://user-images.githubusercontent.com/3499585/156147858-f956c37b-02c9-451e-8633-d011a875a650.png)

You can see the visual studio solution folder structure in below image.

![image](https://user-images.githubusercontent.com/3499585/156371042-e19fc19a-81a1-4d7b-85fe-cb8ebe4ebb03.png)

#

* Database: PostgreSQL
* Message Broker: RabbitMQ
* Logging: NLog
* Service Bus: MassTransit
* Tests: xUnit
#
* Api Gateway: Ocelot
* Contact Rest API: Contact Management Microservice
* Report Rest API: Report Management Microservice
* UI: Asp.NET Core MVC
#
* IDE: Visual Studio 2019
* Framework: .NET 5
* Language: C#
#

PostgreSQL is used as database and code first approach has been used in entity framework core. Unit of work in generic repository pattern is being used for database operations. NLog is used for logging and both file system and database are used to keep logs.

Ocelot is the only gate which open to world. All requests come to ocelot and it works as gateway to inner services. Ocelot has many capabilities which I used only small amount of them.

I did not do any work to prevent to meet outside requests in microservice api's. It can be prevented by using middleware or whitelist kind of things.

I only have 34% unit test code coverage at the time of writing these lines. It can be increased, as you can imagine it's just a matter of free time.

![15-CodeCoverege](https://user-images.githubusercontent.com/3499585/156454196-f2228672-8ab2-4395-8cdf-9e92cf07d5c3.png)
#
What else can be done?

As with most coding, it's up to your imagination.

More testing, including unit and integration.
Better design.
Better decoupling.
More control over methods and return values in the UI part too.

Better UI design of course.

I should mention about not forgetting dockerization as well.

And others that I'm not considering right now.
#
Below, I will try to explain how to run the application with screenshots and simple data entries to test its operation. I think that if it is applied sequentially, there should be no problem.
#
I installed the postgresql windows version on my computer.

The application communicates with the database using the following connection information. Therefore, my advice would be to be able to access postgresql with the following username and password. This will save you from extra replace operations as I have to write this information in different places throughout the application. But if you want, you can update your own information in the relevant files with a simple search and replace.

![01-ConnectionString](https://user-images.githubusercontent.com/3499585/156454645-d757be70-e044-480b-b114-7f3faef4d5d7.png)
#
After opening the application with visual studio, in the package manager console we type the relevant command after the selections has been made like in the screenshot below.

![02-AddMigration](https://user-images.githubusercontent.com/3499585/156454707-5b211826-5f5d-490c-880b-a39eb3295c5b.png)
#
Next step is the process of creating the classes in the application on the database. We do this by typing the relevant command as below.

![03-UpdateDatabase](https://user-images.githubusercontent.com/3499585/156454916-6d5b54df-cb88-4597-b152-ceac5d48edbd.png)
#
If everything went well, the database and tables created with pgAdmin can be viewed. This is shown below.

![04-DatabaseInPgAdmin](https://user-images.githubusercontent.com/3499585/156454975-7de1a777-a846-4372-9379-3407080bcebc.png)
#
Multiple apps should run while testing the app. The following screen shows the order and which ones they are.

![05-SettingProjectStartStatus](https://user-images.githubusercontent.com/3499585/156455029-d2d2808d-afaf-4aef-9f6a-ffe93cafb3ba.png)
#
When we run the application in debug mode, we see that the applications are running as in the screenshot below.

![06-ProjectsAreRunning](https://user-images.githubusercontent.com/3499585/156455079-28800fc4-d5ec-45f7-8f46-b9b8dd01496f.png)
![07-ProjectsAreRunning](https://user-images.githubusercontent.com/3499585/156455118-78414489-c2c8-42dc-8db4-279220e8a17b.png)
#
We will attempt data entry through our UI application. After providing an example data entry as below, I click the Add button.

![08-AddContact](https://user-images.githubusercontent.com/3499585/156455151-c726f009-7132-497b-beb7-069d2aaba439.png)
#
I am listing all registered contact information. I see a screen like the one below.

![09-AllContacts](https://user-images.githubusercontent.com/3499585/156455214-35ba3772-2d2d-470e-9376-ae11ec43d491.png)
#
By clicking the Add Contact Detail button, we can enter as many different phone, email and address information as we want for a person.

![13-AddContactDetail](https://user-images.githubusercontent.com/3499585/156455264-5162c478-abe0-46d8-a5a9-4da389760b73.png)
#
We register our request to receive a report from the Add Report section. When we click on the Add button, the request starts to move in the message broker and the consumer application reads the messages in the queue and generates the report in turn and gives a message on the console screen.

![10-AddReport](https://user-images.githubusercontent.com/3499585/156455318-b265c312-747e-4d07-be5c-90972fde361f.png)
![11-AddReportMessageQueue](https://user-images.githubusercontent.com/3499585/156455363-4e7bb513-732c-4a27-b505-529279b0c950.png)
#
By clicking on the report link in the UI application, we can view the all reports in the status of being prepared and completed generated as a result of report creation.

![14-NewReport](https://user-images.githubusercontent.com/3499585/156455420-ad04a4ce-02f0-476f-bcfe-7dff0ec02123.png)

## 
I hope, I was able to help those who want to review this type of application as a whole.

## Thank you.
