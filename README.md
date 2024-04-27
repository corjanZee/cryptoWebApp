# Crypto web app
The following application is used to create, update, search and delete cypto currencies. The technology stacks used are as follows

- Mongo Db to store the data
- Entity framework as the ORM 
- asp.net core 8


The operations which can be conducted are listed below 

| Operation | HttpType   | Endpoint                                                  |
|-----------|------------|-----------------------------------------------------------|
| Get All   | HttpGet    | http://localhost:{hostedport}/                            |
| Search    | HttpGet    | http://localhost:{hostedport}/search?code=code&&name=name |
| Delete    | HttpDelete | http://localhost:{hostedport}/{id}                        |

### Create
- HttpPost
- endpoint http://localhost:{hostedport}/
#### body
```json
{
	"Code":"Code",
	"Name": "Name",
	"Description":"description"
}
```
### update
- HttpPatch
- endpoint http://localhost:{hostedport}/{id}
#### body
```json
{
	"Code":"Code",
	"Name": "Name",
	"Description":"description"
}
```

### update
- HttpPatch
- endpoint http://localhost:{hostedport}/{id}
#### body
```json
{
	"Code":"Code",
	"Name": "Name",
	"Description":"description"
}
```

To add the database connection string this can be done using the appsettings.json in CryptoWebApp. Currently the value is set to 
````
"DbConnection":"mongodb://root:test@localhost:27017/"
````

### Tip

If you have docker and wish to quickly get a mogoDb quickly up and running. You can use docker compose in the root of this project

````
docker compose up -d
````
