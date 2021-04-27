
```
 __        ___  ___       __             __  
/  \ |  | |__  |__  |\ | /__` |     /\  |__) 
\__X \__/ |___ |___ | \| .__/ |___ /~~\ |__) 
```
# Evaluation Assignment - LiA Backend

## Scenario
En butik behöver hålla koll på antal besökare, både i butiken som helhet samt på varje avdelning.
Det finns sensorer i butiken som registrerar när besökare kommer in och lämnar avdelningar.
Denna data skickas till ett web API i form av ett "ENTER" eller "EXIT"-anrop tillsammans med info om avdelning och timestamp.

## Uppgift
Skapa det web API som håller koll på hur många besökare som finns i butiken.

**API:et ska ha stöd för att:**
 - Ta emot info om besökare som kommer in och som lämnar en zon.
 - Lista alla zoner och antal besökare i varje zon.
 - Nollställa en zon.
 
**Tekniker:** dotnet (.net core, .net5), python, node js

**Bonus:** Bygg en web-sida som visar upp aktuella stats besökare (valfri teknik)

**Tips:**
Keep it simple.
Ha i åtanke testbarhet samt att kunna utöka protokoll för ingående trafik.
Patterns: Hexagonal, Ports and adapters



Allowed technologies: .NET Core, Python, NodeJS, React, Vue.js, Angular 2+,
SCSS, SASS, LESS, PostCSS, CSS, HTML5, Vanilla JS.

**Part 2 - Pricing calculator [Backend focus]**
------

**Background**

Company X provides a service,  _The Super Awesome Service_ to other companies in the region. Your job is to develop a Web API within a micro-services solution that is solely responsible for calculating prices and should only be called by other services, not humans. There are three types of services; _Service A_, _Service B_ and _Service C_. These services have different prices and they also depend on the customer, the time period for which they are charged, possible discount (Percentage of total price), free days (a type of discount based on number of free days). Customers can choose which service they want to use independently.

The user story is: **_As a calling service it should be possible to call an endpoint with customerId, start and end in PricingService to know what to charge for a specific customer_**

Build this Web API (PricingService) with appropriate endpoints and implement the following requirements:

- Base costs are as follows:
    - _Service A_ = € 0,2 / working day (monday-friday)
    - _Service B_ = € 0,24 / working day (monday-friday)
    - _Service C_ = € 0,4 / day (monday-sunday)

- Make sure it is possible to override base prices per customer (e.g. _Customer A_ only pays € 0,15 per working day for _Service A_ but pays € 0,25 per working day for _Service B_)

- Make sure it is possible to store discount per Customer and service
- Make sure it is possible to store Customer start date per service 
- Make sure it is possible to store number of free days per Customer (affects all services)

Lycka till!
<¤.,¤>
