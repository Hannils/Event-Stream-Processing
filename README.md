# Event-Stream-Processing

<details>
  <summary>Log</summary>
  
  ### Day 0
  Setup: Getting to know the office, installing visual studio. Getting familiar with environment
  Getting to know c#
  Started implementing some basic functions and middlewares with a webserver. Middlewares include: Logging and basic analytics such as saving the amount of times the endpoint was requested.
  Played around with tests, TestHost and TestServer following this: https://www.roundthecode.com/dotnet/asp-net-core-web-api/asp-net-core-testserver-xunit-test-web-api-endpoints. Had a problem but eventually did Assembly.Load(“TestAPI”) and then it worked.
  
  ### Day 1
  
  Had a quick meeting with external supervisor talking about the next step. Our plan is to create a Miro board so that everyone involved will have the same expectations of what the result will look like.

  Created a Miro board with a simple representation of what the ESP will consist of. ![Miro Board](./documenting-resources/ESP-Unit1.png?raw=true "=)")

  Spend most of the day researching event streams and processing. Wrote about 1/3 of the individual plan.


  ### Day 2

  Mostly spent out time working on our individual report. Eventually we started writing the start of the ESP. By the end of friday, we had a parser that could handle both JSON and HTTP requests and in turn transform them into a special type.


  ### Day 3

  Finished our individual plan and sent it our supervisor for a quick review. Looked into a lot of sources and references for our pre-study. Development wise, we started to implement our classifiers along with the filters. We also visualized how the relation between classifiers and filters will be done. For the next time, we will start implementing testing for our parsers to make sure that they will be able to handle eventual complications (Error handling). We will also start working on our memory store and the actual functionality of our classifiers. 
  
</details>