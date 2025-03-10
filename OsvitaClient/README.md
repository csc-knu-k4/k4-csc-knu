My confing for SQL Connection

{
"ConnectionStrings": {
"OsvitaDbConnection": "Server=VimeRSPC\\SQLEXPRESS;Database=OsvitaDB;TrustServerCertificate=True;Trusted_Connection=True;",
"IdentityOsvitaDbConnection": "Server=VimeRSPC\\SQLEXPRESS;Database=IdentityOsvitaDb;TrustServerCertificate=True;Trusted_Connection=True;MultipleActiveResultSets=true"
},
"Jwt": {
"Issuer": "OsvitaWebApi",
"Key": "JlTlu2GusoVhrkK96tgoCTEy6hLsQ71f",
"Lifetime": 15,
"Audience": "Api"
},
"AdminSettings": {
"Email": "admin@gmail.com",
"Password": "Qwerty_1"
},
"Logging": {
"LogLevel": {
"Default": "Information",
"Microsoft.AspNetCore": "Warning"
}
},
"AllowedHosts": "\*"
}
