# Indy Value Golf
https://indyvaluegolf.azurewebsites.net/ <br><br>
Indy Value Golf is my final project as a student at Eleven Fifty Academy. It's a Blazor WebAssembly and is deployed to Azure Web Services. The project utilizes n-tier architecture with a data layer, a services layer, a controllers layer, and a presentation layer. Each of the 3 data tables have full CRUD capabilities. The code is written in C#, HTML, and CSS.

## Screenshot of the deployed site
![Screenshot 2022-05-05 142233](https://user-images.githubusercontent.com/98339001/167015822-a87e4a67-0de0-4d1e-a537-fce2ad96414d.png)

## Functionality
The idea of the project is for users to enter local golf course information, including prices they paid and reviews of their experiences. The site displays an average rating and average price for each course in the list. It also uses that information to calculate a "value" so users can see which courses provide the most bang for their buck.

## Extra Features
* The home page of the site contains a search bar that allows users to search for a course by name. A dropdown list of courses is displayed as soon as the user starts typing. They can then click the name of the course they want and be taken directly to that courses details page. 
* Courses are automatically sorted from highest to lowest value. Users can sort the list of courses ascending or descending by price, rating, or value by clicking on the appropriate table header.
* From any course details page, you can see all the ratings or prices for that individual course by clicking on the appropriate button.

### References
+ Search Bar - https://github.com/Blazored/Typeahead
+ Sorting Table - https://www.thecodehubs.com/sorting-table-in-blazor/comment-page-1/
+ Awesome Golf Background - https://clipground.com/free-golfing-clipart.html
