
# E-Commerce using .Net

This repository was created to fulfill college assignment

## Authors

- [@TitoJhay](https://github.com/TitoJhay)


## Requirement of assignment
the project should add the feature of 
- MDI Parent
- Simple CRUD 
- Login Form


## Run Locally

Clone the project

```bash
  git clone https://github.com/TitoJhay/E-Commerce-using-.Net
```
Set Up Database using Mysql laragon

```bash
  Go to the laragon.org website then click Download

  Select Laragon - Full

  Follow the existing installation from the installer
  
  Run Laragon
  
  Click Start All

  Click database

  Create New Database "ecommerce"

  Open directory "../E-Commerce-using-.Net/Database"

  Import "ecommerce.sql" to the Mysql
```

Install dependencies

```bash
  Go to the https://dev.mysql.com/downloads/connector/net/ then click Download

  then open and install the "mysql-connector-net-9.2.0.msi" file
  next until the last page

  put the "MySql.Data.dll" to your local repository my reference "C:\Program Files (x86)\MySQL\MySQL Connector NET 9.2"

  click finish 
```

Go to the project directory 

```bash
  cd "../E-Commerce-using-.Net/14522712_MuhammadTitoJayaKusuma"

  open "14522712_MuhammadTitoJayaKusuma.sln" using Visual Studio 2022 

  in solution explorer or click (Ctrl+;), right click the ecommerce file -> click ADD -> click Project Reference 

  click Browse.. go to ""C:\Program Files (x86)\MySQL\MySQL Connector NET 9.2"

  click "MySql.Data.dll" then click Add
```

Install feature mysqlconnector in Visual Studio

```bash
  go to search or click(ctrl+Q) in Visual Studio 2022

  search and click "Manage NuGet Packages.."

  Click browse and search "MySql.Data" (by MySql) and Install
```

Run the program

```bash
  Re-open the visual studio 2022

  click button run E-Commerce
```



## Running Program as User

To run Program of User, run the following command 

```bash
  in login page
  username : user
  password : user
```

Select the product and Buy


## Running Program as Admin

To run Program as Admin, run the following command 

```bash
  in login page
  username : admin
  password : admin
```

-> You can add the products and upload the image of the product

-> You can create another user

