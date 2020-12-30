# Marketing Tool

A catchy name is all you need to be a success. The purpose of the project is to design, build and deploy a tool that individuals/businesses can use to make email based marketing more easy. Development will be iterative and features will be light at first with possibly more added as development continues. The initial vision is that a user will be able to import a company database of mailinglist recipients as well as having live adding by hooking their online mailinglist signup into our API.

Once they have a database of contacts, they can create lists which will typically be a subset of their mailinglist with a common relationship i.e. they are all VIP customers.

From there they can create and manage campaigns that make use of these lists by designing attractive and exciting rich email content. These campaigns can be automated to get at regular intervals such as daily, weekly, fortnightly or monthly as well being able to send out ad hoc campaigns that just want one email to go at a certain time, say the day before Black Friday.

Of course a marketing tool is nothing without analytics to see how the campaign is performing so things such as bounce rate etc will be viewable within the tool.

Possible future features may include the management of a twitter, facebook and instagram feed within the tool.

## Technical Stuff

The plan is to use Azure for hosting of the app and the automation of the email sending i.e. the automation will utilise Azure functions and make use of Azure email servers whilst the app will be hosted in a VM so they can take care of all the load balancing etc.

The stack is C#/.Net 5 for backend based on an Ubuntu 18 platform with Apache acting as a reverse proxy, frontend will be built in React, dev environment is Ubuntu based in a Vagrant VM.

Initially the frontend will be a mock up just to test features i.e. the login screen will not actually do any login logic at the beginning but just take you to the dashboard of the test account.

## Planning

[Entity diagram](https://drive.google.com/file/d/1Cv-6_tlSqsJOHNXnE-5vJpTMmRlgHa8D/view?usp=sharing)

Dev will be in sprints and these will be planned here


