# Fishfry Tours BoatTracker service

# Description
A service that is used as a back-end system for a boat tracking application. 
This service can create, update or get boats and their states.

# Database

This service uses MS SQL database that is hosted in Azure. 

It contains two tables 

1. Boats: 
   Columns : HIN(PK) (Identification of a boat), 
             Name (Human readable name of a boat),
             State(FK) (State of a boat)
2. States: 
   Columns : Id(PK) (Numeric identification of a boat state), 
             Description (Boat state in text format)
             
# ORM

It uses Asp.Net Core Entitity framework to map tables to our domain models. EF is an over kill for such a small database and I wish I used Dapper or other light weight tools instead.

# Projects

1. BoatTrackerDomain: A project for handling most of the business logic.
2. BoatTracker.Domain: Asp.Net Core Web API project that provides enpoints.
3. UnitTests: A testing project for the unit test.

# How to use the API

The live version of this service is hosted live at https://fishfrytoursboattrackerservice.azurewebsites.net. You can also download the git repository and run it locally.

1. Get all Boats

 url: https://fishfrytoursboattrackerservice.azurewebsites.net/api/boats
 Method: Get
 
 2. Get a Boat

 url: https://fishfrytoursboattrackerservice.azurewebsites.net/api/boats/{HIN}
 Method: Get
 Example: https://fishfrytoursboattrackerservice.azurewebsites.net/api/boats/ABC123asd45D404
 
  ABC123asd45D404 refers to the HIN.

2. Add a boat

 url: https://fishfrytoursboattrackerservice.azurewebsites.net/api/boats
 Method: Post
 sample Body:   {
                  "hin": "XWE12345765",
                  "name": "Tooth Ferry"
                 }
 Note: When a new boat is created, it's state will be 0(Docked), by default.
   
 Requirement: HIN can not be empty. You can not create duplicate boats with same HIN.
 
 2. Update a boat

 url: https://fishfrytoursboattrackerservice.azurewebsites.net/api/boats
 Method: Put
 sample Body:  {
                    "hin": "ABC12345D404",
                    "name": "Last Dance",
                    "boatState": {
                        "id": 1,
                        "description": "Docked"
                    }
                }
  Requirement: boatState id can only be a number 0 upto 3.  
  
  #Testing
  
  Unit tests and integration test are included but they are incomplete. There is an automated functional tests written in Postman
