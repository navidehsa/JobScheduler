# Job Scheuler Service

This solution proposes using C#,.NET Core6 to build a web API that allows clients to enqueue an array of numbers to be sorted in the background and query the state of any previously enqueued job. The solution includes a background job processing system(using hangfire), a message queue, an in-memory data store, logging functionality, and a suite of unit tests. The solution is self-contained, and all data can be stored in memory for the sake of simplicity. No external infrastructure is needed to run the solution.

## Concepts

'Job' : 
A job takes an unsorted array of numbers as input, sorts the input, and
outputs the sorted array. Apart from the input and output arrays a job should include the following metadata:
- ID - a unique ID assigned by the application
- timestamp - when was the job enqueued?
- duration - how much time did it take to execute the job?
- status - for example "pending" or "completed"

All jobs should be processed in the background and clients should not be forced to wait for jobs to complete.
To view the output of a job (the sorted array), the client must query a previously enqueued job.

## Endpoint

The JobScheduler API Contain three Endpoints with different functonalities:

'POST /jobs' - Enqueue a new job by providing an unsorted array of numbers as input.

'GET /jobs' - Retrieve an overview of all jobs (both pending and completed).

'GET /jobs/{id}' - Retrieve a specific job by its ID, including the output (sorted array) if the job has completed.

## Dashboard

you can monitor the jobs processing in a dashboard  with url :~/hangfire 


## Step to run

Clone the repository to your local machine.

Navigate to the project directory and open a terminal or command prompt.

Run the command "dotnet restore" to restore the necessary packages.

Run the command "dotnet build" to build the solution.

Run the command "dotnet test" to run the unit tests.

Run the command "dotnet run" to start the web API.

Use a tool such as Postman or curl to interact with the API endpoints.

## TODO list


