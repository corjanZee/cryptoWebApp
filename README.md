# Crypto web app
The following are list of operations which can be done to crypto application

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

If you have docker and wish to quickly get a mogoDb quickly up and running. You can use docker compose in the root of this project

````
docker compose up -d
````
