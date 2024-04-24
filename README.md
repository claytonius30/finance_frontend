# Financial Tracker Frontend

Provides client-side UI for my Financial Tracking mobile app.

Challenge: Design a mobile/web/desktop app using .NET MAUI.

Context: For the final project in my .NET Applications class, I originally decided to design the frontend for a mobile application that tracks a user's expenses and incomes, providing a running balance and the ability to specify date ranges to retrieve financial info at specific points in time. After reasearching the best approach to create a mobile app using .NET, I discovered .NET MAUI (Multi-platform App User Interface) which provides a single code base that can be deployed on multiple patforms-desktop/mobile/web. .NET MAUI was introduced in 2020 as an evolution of Xamarin.Forms.

Action: The best resource I found to learn about the .NET MAUI framework was watching a PluralSight course by Lindsey Broos called "Building .NET MAUI Applications with MVVM". Following the design principles from the course, I was relatively easily able to create a UserDetail page that displayed a user's name, date and current balance. From this page, a user can click to view all of their incomes in another page that uses a collection view and be able to select individual incomes to either edit or delete. A user can also add Incomes and Expenses from the user detail page. Once I had this functionality set up and working with the backend API, I created login and register pages for authentication. An issue I encountered was using the int Id I originally created for a user.. Once I implemented Identity in the backend, I needed to switch all my int Ids to Guids.

It took some time for me to become familiar with the Model View Viewmodel design pattern as well as using xaml. A draw back of using MAUI is that you end up with lots of code.. this is primarily because in order to maintain the single codebase, there needs to be a strong separation of concerns between all the components in order to be instantly deployable on different platforms. Using the repository pattern and dependency injection must be strongly understood as well.

Result: Once I was able to successfully implement authentication for users to register and log in, finishing the remainder of the app went smoothly. I used the Android emulator for testing because it's free.. although I will likely subscribe to Apple Developer to be able to test and deploy on my iPhone.

Reflection: Through the long process of learning this framework, I am very excited to have the knowledge to be able to create many more mobile/desktop/web apps in the future. I will be continuously making improvements to this app as it is one I am most proud of.
