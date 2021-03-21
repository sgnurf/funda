# Funda Assignment

## Running the solution

Prerequistes :
* Dotnet5 runtime

Opena command line prompt in the `FundaAssignment` subfolder and execute `dotnet run`

### ApiKey

Add the api key to the appsettings.json file under the key `FundaApi:Key` 
or use dotnet secrets in a console in the `FundaAssignment` subfolder
`dotnet user-secrets set "FundaApi:Key" "myKey"`

## Project structure

To get quickly started and focus on the business logic I used a modified version of the ASPNet Core + React/Redux template.

In the client most of the logic lives in FundaAssignment\FundaAssignment\ClientApp\src\features

## Points fo interests

* To mitigate the throttling on the FundaApi, I have chosen to throttle outgoing requests as well as caching the responses. Caching and Throttling are added as decorator so neither the consumer nor the service need to know about it
* The `FundaOfferHttpClient`, its decorators and `PaginatedDataCollator` have been designed to be reusable to make calls to other Apis with the same query/response format with Objects and Pages.
* Filters on the server side: The filter processors on the server-side are easily extendable. Currently there is a city and a withgarden filter, but adding a bew one is limited to creating a new implementation of `ISearchQueryFilterProcessor` and wiring it in the DI container
* Filters on the client side: Likewise on the client side the filtering is quite dsecoupled, and adding a new filter only requires to create a new component and add it to teh child of FilterPanel

## Potential improvements

* Use CancellationToken Throughout to cancel the long running requests
* In the client cache result requests individually instead of clearing all items when a result comes in.
* Instead of the client requesting for the data, it could post a job and then poll (or use a websocket) to see progress of the job. Could also show partial results

## Conventions

### Unit tests

Unit tests are name with the following pattern
`MethodUnderTest_PreconditionOrScenario_ExpectedResult`