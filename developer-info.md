# Developer info

## BE Aplikace

**DataSource:** Providers/DataSourceProvider.cs - načtení json a handlování cache  
**BL:** Queries - získání dat, validace, práce s modelem  
**Výstup do controlleru:** RequestHandlers - získání dat z Queries a mapování na Contract  
**AOP logování:** Pokus o implementaci v Attributes/Log* - nemám zkušenosti  
**Unit testy:**  ToDo (do 18.11.)  
**Nugety:**
 - Serilog
 - System.Text.Json
 - SimpleProxy - AOP pro logování volání metod repositářů [github](https://github.com/f135ta/SimpleProxy)
- Swashbuckle.AspNetCore - default [http://localhost:5000/swagger](http://localhost:5000/swagger)

#### Struktura
 - IoC - default microsoft
 - Models - business objekty
 - Contracts - výstupní objekty
 - Mappers - jednoduché mappery pro transformaci Model <> Contract
 - Middlewares/ExceptionProblemDetailHandler.cs - transformace exceptions do ProblemDetails
 - Middlewares/ResponseChacheMiddleware.cs - HTTP304 handler podle LastModified dat (LastModified *.json souborů v bin adresáři aplikace)
 - Validators - jednoduchá validace dat - výstup se plní do modelu v query
#### Nugety


## FE Aplikace
**ToDo (do 18.11.)** - Angular  
**Unit testy/E2E testy:** nebudou - neumím/nemám zkušenosti
