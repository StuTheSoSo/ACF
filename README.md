# ACF
Thank you for giving me the opportunity to present this demo. I wasn't exactly sure how deep to get into this - hopefully there's enough here to show that I can hit the ground running with this project... More on this at the end of this document.

I've been working with .Net since .Net Framework 2, and working with Angular since version 1 (when it was "angularjs").

The Design Architecture, Technology Decisions and Security Considerations are below - but if you have any questions, or would like to discuss anything, I would LOVE the opportunity.

Thanks,

Stu

     

## Design Architecture
    The files presented here are in 3 sections:
    1. Front End
    2. Back End
    3. SQL

### Front End
The front end is written in Angular version 20, and created/scaffolded by the dotnet cli tool.

In the "Front End\src\app" directory - the elements are broken up by type:
- components:

    All components (views) to be displayed in the application.
- guard:

    The "auth-guard" used to protect routes. The guard checks the Auth Service for the existence/validity of the JWT in localstorage, and prevents access to routes based on that fact.
- interceptors:

    the "auth-interceptor" is responsible for adding the JWT to outgoing http calls. It adds it to the "Authorization" key as a bearer token to be read and used by the back end for it's authorization (see "Back End")
- models:

    data models to be used by the front end. Objects such as "case" and "client" are passed to the back end, and used for data-binding the UI.
- services:

    services used by the application - an "auth" service to handle authentication/authorization and a "data" service to handle http calls to the back end. These services are registered, and can be injected anywhere in the app for extremely easy use.


### Back End



### SQL


## Technology Decisions

## Security Considerations