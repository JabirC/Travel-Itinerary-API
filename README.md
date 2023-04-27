# Travel-Itinerary-API

## API Idea

* Lets you search for all the tourist attractions in a given city or country
* Add to the database of tourist attractions
* Keep track of tourist attractions a user wants to visit next


## Database Schema

<img width="609" alt="Screenshot 2023-04-27 at 6 48 54 AM" src="https://user-images.githubusercontent.com/27122290/234840757-df7e9830-52c2-4bf5-99e3-d16c044e54d5.png">


<img width="703" alt="Screenshot 2023-04-27 at 7 02 07 AM" src="https://user-images.githubusercontent.com/27122290/234843673-8f00c805-1515-43be-a22b-3c9558ad0eb9.png">


I decided to make the Itinerary table a little different from my API idea by adding an ItineraryId column to make it easier for the model to recognize that it was the primary key. In addition, I decided to make the table only have max 4 attractions in the itinerary instead of making it expand when a user ran out of columns. Making it expandable would have caused a headache in terms of structuring the model for a table thats dynamic.


## Endpoints

### GET

<br/>

```
GET /API/attractions
```
* Get all the attractions in the database

<br/>

```
GET /API/attractions/country/{country}
```
* Get all the attractions in a given country

<br/>

```
GET /API/attractions/city/{city}
```
* Get all the attractions in a given city

<br/>

```
GET /API/Itinerary/{username}
```
* Get the itinerary of the given user

<br/>

Sample response body:
~~~~
{
    "statusCode": 200,
    "statusDescription": "Success",
    "data": [
        {
            "attractionsId": 5,
            "attractionName": "Summit One Vanderbilt",
            "country": "USA",
            "city": "NYC",
            "address": "45 E 42nd St New York NY 10017"
        },
        {
            "attractionsId": 9,
            "attractionName": "Little Island",
            "country": "USA",
            "city": "NYC",
            "address": "Pier55 W 13th St New York NY 10014"
        },
        {
            "attractionsId": 11,
            "attractionName": "The Vessel",
            "country": "USA",
            "city": "NYC",
            "address": "20 Hudson Yards New York NY 10001"
        },
        {
            "attractionsId": 16,
            "attractionName": "Musee d'Orsay",
            "country": "France",
            "city": "Paris",
            "address": "1 Rue de la Légion d'Honneur 75007 Paris"
        }
    ]
}
~~~~

<br/>

### POST

<br/>

```
POST /API/attractions/AddAttraction
```
* Add a row to the attractions table

<br/>

Sample request body:

~~~~
{
    "AttractionName": "Sainte-Chapelle",
    "Country": "France",
    "City": "Paris",
    "Address": "10 Bd du Palais 75001 Paris"
}
~~~~

<br/>

```
POST /API/Itinerary
```

* Initalize a new user in the Itinerary table

<br/>

Sample request body:

~~~~
{
    "UserName": "JabirChowdhury"
}
~~~~

### DELETE

<br/>

```
DELETE  /API/Itinerary/ClearItinerary/{userName}
```
* Clears the itinerary for the given user
* Decided to make this endpoint a DELETE method instead of the PUT method I initially planned in the API dea presentation because PUT usually requires a request body but in this endpoint there is no need for a request body

<br/>

### PATCH

<br/>

```
PATCH  /API/Itinerary/addItinerary/{userName}
```
* Adds an attraction to the itinerary of the given user
* Decided to make this endpoint a PATCH method instead of the PUT method I initially planned in the API dea presentation because PUT usually means many field will be replaced but in this endpoint there will only be a change in one column max

<br/>

Sample request body:

~~~~
{
    "AttractionName": "Sainte-Chapelle",
    "Country": "France",
    "City": "Paris",
    "Address": "10 Bd du Palais 75001 Paris"
}
~~~~







