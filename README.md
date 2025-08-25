# ACF
Thank you for giving me the opportunity to present this demo. I wasn't exactly sure how deep to get into this - hopefully there's enough here to show that I can hit the ground running with this project... More on this at the end of this document.

I've been working with .Net since .Net Framework 2, and working with Angular since version 1 (when it was "angularjs").

The Design Architecture, Technology Decisions and Security Considerations are below - but if you have any questions, or would like to discuss anything, I would LOVE the opportunity.

Thanks,

Stu

## Highlights (What's in this demo)
This demo contains all of the pieces required to run the front end, back end and database of the application.

-Role based access controls (front end): an auth-guard ensures no route can be called without the JWT existing and being valid in localstorage. The auth service decodes the JWT to determine the users role - then ONLY displays the sidebar navigation items appropriate to that role.

-Role based access controls (back end)/Secure API Endpoints: Sensitive methods are decorated at "[Authorize]" to allow only requests with the correct Bearer token, and when appropriate, are further specified (for example: "[Authorize(Roles = "Admin, Officer")]") to restrict access by role(s).

-Audit Logs: All interesting methods (which are only a few at this point) include logging on success and failure. The logging is done in a database table called "AuditLogs" asynchronously.

-Ability to add Cases and Clients: Applicable only to Admin and Officers, so Auditors are resticted from this functionality.


## Design Architecture
The files presented here are in 3 sections:
1. Front End
2. Back End
3. SQL

### Front End
The front end is written in Angular version 16.

In the "Front End\src\app" directory - the elements are broken up by type:

- <u>components</u>: All components (views) to be displayed in the application.

- <u>guard</u>: The "auth-guard" used to protect routes. The guard checks the Auth Service for the existence/validity of the JWT in localstorage, and prevents access to routes based on that fact.

- <u>interceptors</u>: the "auth-interceptor" is responsible for adding the JWT to outgoing http calls. It adds it to the "Authorization" key as a bearer token to be read and used by the back end for it's authorization (see "Back End").

- <u>models</u>: data models to be used by the front end. Objects such as "case" and "client" are passed to the back end, and used for data-binding the UI.

- <u>services</u>: services used by the application - an "auth" service to handle authentication/authorization and a "data" service to handle http calls to the back end. These services are registered, and can be injected anywhere in the app for extremely easy use.


### Back End
The back end is a web api written in .Net Core 7. The structure is as follows, although if I were to continue work on this, I would add classes/layers for "Service/Business Logic" and "Data Access/Repository" functionality.

- <u>Controllers</u>: Responsible for all http functionality. Authorization, authentication and validation would be here - although in this simple demo, the controllers also contain data access functionality. That would NOT be the case in a production application.

- <u>Data</u>: The applications database context. The "Code" version of the database, meant for ease of access to and from the data.

- <u>Logger</u>: Custom logger to write to the database asyncronously from any controllers.

- <u>Models</u>: Similar to the front end models, these are the data models to and from the database, and to and from the front end.



### SQL
The database is (at this point) very simple, with relationships existing from the "Case" object to the Client and Officer... but other than that, this is a very simple setup at this point.


## Security Considerations
The scurity considerations are a bit difficult to speak to with my VERY limited knowledge of the product, and the data going into it. I see "SSN" which would obviously have to be encrypted at rest - but I don't see many other fields. I CAN however assume, given the subject matter, that the data will be of a sensitive nature, and must be encrypted at rest, with specified/mandatory data retention periods - and that logging must be complete including the date/time and acting user.

## Final Thoughts
I just wanted to thank you again for the opportunity to present this demo. I battled with adding functionality vs. getting it to you quickly. Hopefully I struck the correct balance.

Looking at the requirements sent, I have no doubt that I could hit the ground running on this project and contribute quickly to its success.

Thanks!

Stu