
# Awesome Bank API
A simple banking systems implemented using ASP.Net Core 3.1

## Prerequisite
- OpenSsl

## To run the project
 1. Generate private key using OpenSsl by following command:
	```
	openssl genrsa 2048 > private.key
	```

 2. Create certificate file using private key generated from pervious step:
	```
	openssl req -new -x509 -nodes -sha1 -days 1000 -key private.key > public.cer
	```
	*This certificate is use to verify token.*

 3. Create Personal Information Exchange File .p12 file. :
	```
	openssl pkcs12 -export -in public.cer -inkey private.key -out cert_key.p12
	```
	*This file contain private key and certificate, It using for generate and sign a token. So Issuer of token can be verify using the certificate from step 2*
 4. Setup certificate infomation in appsettings.json file:
	```json
	  "Certificate": {
	    "PublicCertificate": "C:\\KEY\\public.cer",
	    "CertificateKey": "C:\\KEY\\cert_key.p12",
	    "Password": "Pass@word1"
	  }
	```
	*PublicCertificate = Location of certificate file created in step 2.
	CertificateKey = Location of .p12 file created in step 3.
	Password = the password from step 3.*

 5. From root directory execute following command to restore nuget package using dotnet cli:
	```
	  dotnet restore
	```	

 6. Create Migrations using dotnet entitiy framework cli:
	```
	dotnet ef migrations add initMigration
	```	
 7. Push migrations to SQL Server:
	```
	dotnet ef database update
	```	
	The migration will take database connection from ConnectionStrings to access database and generate table.
	

 8. Run project:
	```
	dotnet run
	```	
	You can go to https://localhost:5001/swagger to check it out.
	

### To test the project
 1. checkout ```develop-test``` branch.
 2. From root directory of project AwesomeBankAPI.Test Run command:
	```
	dotnet test
	```
