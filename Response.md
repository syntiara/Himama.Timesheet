# Building A Timesheet Application

## Process
As a user on the application, show me a search field so that I can easily search for my name (either first or last name). If I can’t find my name, show me a button to create my profile on the system. Else, display table with a list of name matches.

1. **Profile creation on the system** -  As a user on the profile creation page, display a form to fill my details. On successful submission, route me to the attendance sheet page. 

2. **Table Display of name matches** - As a user, when I click on my name, route me to the attendance sheet page.

3. **View list of time entries** - As a user, when I am on the attendance sheet page, I should see a link to view attendance entries. 

4. **Attendance entries page** - As a user when I click on the link to view entries, I should see a table display of attendance entries, and I should be able to edit an entry.

For someone with SPA experience, I chose .NetCore MVC as my MVC framework because I was already familiar with .netcore.

During development, I noticed it required a page for every method call, and reloads the pages on every server call. I wasn’t comfortable with it so I mixed it up with a couple of javascript.


## Database Schema Consideration
1. **User Table**: should have fields firstName, lastName and email. They are required fields because that’s needed to display user information. 
2. **UserAttendance Table**: should have fields userId, clockIn and clockOut time. These fields link a user to a time entry. I considered having boolean fields isClockIn and isClockOut, just to make it easy to know when someone is clocked in/out (this helps determine the necessary button to display). That changed during development as the last entered entry was enough to handle it. 

### If I had more time, I would consider
- adding an auth middleware to avoid attendance fraud
- making mvc play nicely with javascript, espcially in terms of displaying errors to the user  