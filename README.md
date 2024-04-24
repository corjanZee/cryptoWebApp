# Crypto web app
Unfortunately the following applications is not complete and the connection to the database does not quite function. 
I believe this might be due to the fact that I have not applied a schema to my MongoDb. Application can be reached on hosted 
````
http://localhost:{hostedport}/api
````
To add the database connection string this can be done using the appsettings.json in CryptoWebApp. Currently the value is set to 
````
"DbConnection":"mongodb://root:test@localhost:27017/"
````

If you have docker and wish to quickly get a mogoDb quickly up and running. You can use docker compose in the root of this project

````
docker compose up -d
````
