# Event-Stream-Processing

This is the main repository of the bachelor thesis for Hampus Nilsson and Kalle Elmdahl.

There are 2 main folders within the repository: ESP & Webserver.

* ESP contains everything that has to do with the processing such as
  * Classifier (Maps event types to handlers)
  * Handlers (Handles and processes all of the data)
  * Various event types

 * Webserver contains
    *  Middleware connecting the main system with the ESP system
    *  Minimum requirements for an HTTP webserver

There are a three main branches:
1. The main code structure can be browsed in the "main" branch. It utilises HTTP as communication protocol and has been used as the standard code during the project.
2. The second branch is "NATS-branch". It utilises NATS as communication protocol.
3. The third branch is "non-functional testing". It utilises HTTP as communication protocol and was used during load-, stress- and endurance-testing.


<details>
  <summary>Log</summary>
  
  ### 2023-02-01
  Setup: Getting to know the office, installing visual studio. Getting familiar with environment
  Getting to know c#
  Started implementing some basic functions and middlewares with a webserver. Middlewares include: Logging and basic analytics such as saving the amount of times the endpoint was requested.
  Played around with tests, TestHost and TestServer following this: https://www.roundthecode.com/dotnet/asp-net-core-web-api/asp-net-core-testserver-xunit-test-web-api-endpoints. Had a problem but eventually did Assembly.Load(“TestAPI”) and then it worked.
  
  ### 2023-02-02
  
  Had a quick meeting with external supervisor talking about the next step. Our plan is to create a Miro board so that everyone involved will have the same expectations of what the result will look like.

  Created a Miro board with a simple representation of what the ESP will consist of. ![Miro Board](./documenting-resources/ESP-Unit1.png?raw=true "=)")

  Spend most of the day researching event streams and processing. Wrote about 1/3 of the individual plan.


  ### 2023-02-06

  Mostly spent out time working on our individual report. Eventually we started writing the start of the ESP. By the end of friday, we had a parser that could handle both JSON and HTTP requests and in turn transform them into a special type.


  ### 2023-02-07

  Finished our individual plan and sent it our supervisor for a quick review. Looked into a lot of sources and references for our pre-study. Development wise, we started to implement our classifiers along with the filters. We also visualized how the relation between classifiers and filters will be done. For the next time, we will start implementing testing for our parsers to make sure that they will be able to handle eventual complications (Error handling). We will also start working on our memory store and the actual functionality of our classifiers. 


  ### 2023-02-20

  Wrote some tests and started working on some of the specifics of the parsers. We havent started working on our classifiers yet. The next step is to start working on the classifiers along with setting everything up with a kestal webserver in order to get more of a flow in order. We're gonna check out redis as well with faker.net in order to generate and persist some data in accordance with the classifiers.


  ### 2023-02-22

  Today we focused mostly on implementing the functionality of the Classify function of the AnomalyClassifier. We finally installed Rider which improved effieciency by infinite %. We built our in-memory store with autoclear functionality such as a rolling window, removing the oldest stored event and interval based clearing. We started cleaning up with some util functions, separating, and de-coupling them from other functions.
  


  ### 2023-03-15

  Today we focused on getting our TestHost for the webserver up and running. Our intention was to test using generated data from Faker.Net Github Repo. When that was done we rewrote and restructured some of our functions along with implemented a basic trendingHandler. We also wrote a script which sent requests using curl with about 0.5 seconds delay on a loop as to simulate an event stream. 


  ### 2023-03-17
  We started looking into redis and set up a quick server for it. We also talked with out external supervisor about the next step and we will start looking inot apache kafka, redis, NATs for comparing the persisting of data in the event stream. We also started to read about attributes.


  ### 2023-03-20
  We successed in writing a config for dependency injection following this example: https://www.azureblue.io/writing-factory-based-asp-net-core-middleware-part-4/
  
  ### 2023-03-28
  Started working with Redis. Preparing for refactors
  
  ### 2023-03-30
  Refactored project, added threading
  
  ### 2023-04-25
  Replaced String array with HashSet. Created new branch, implemented NATS. Prepared for measureing
  
  ### 2023-04-26
  Wrote request script for non-functional tests
  
  ### 2023-05-16
  Implemented stress and load tests.
  
  ### 2023-05-17
  Implemented functionality for endurance testing
</details>
