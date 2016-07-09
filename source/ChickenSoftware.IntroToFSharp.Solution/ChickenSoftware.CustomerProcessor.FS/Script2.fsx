
type Customer = {CustomerId:int; FirstName:string; LastName:string; Address:string; 
                City:string; State:string; ZipCode:string}

type Result<'TEntity> =
    | Success of 'TEntity
    | Failure of string

let lastNameNotBlank customer =
    if customer.LastName = "" then
        Failure "Last name must not be blank."
    else
        Success customer

let lastNameGreaterThanTwo customer =
    if customer.LastName.Length < 2 then
        Failure "Last name must not be less than two."
    else
        Success customer

let bind switchFunction =
    fun resultInput ->
        match resultInput with
        | Success s -> switchFunction s
        | Failure f -> Failure f

let validateCustomer =
    lastNameNotBlank
    >> bind lastNameGreaterThanTwo

open System
open System.IO
open System.Data
open System.Data.SqlClient
#r "System.Configuration"
open System.Configuration

let insertCustomer customer =
    try
        let connectionString = ConfigurationManager.ConnectionStrings.[0].ConnectionString
        use sqlConnection = new SqlConnection(connectionString)
        let commandText = "usp_InsertCustomer"
        use command = new SqlCommand(commandText)
        command.CommandType <- CommandType.StoredProcedure
        let parameter = new SqlParameter()
        parameter.ParameterName <- "customer"
        parameter.Value <- customer
        command.Parameters.Add(parameter) |> ignore
        sqlConnection.Open()
        let customerId = command.ExecuteNonQuery()
        let customer' = {customer with CustomerId = customerId}
        Success customer'
    with
        | :? ConfigurationErrorsException -> Failure "Configuration file read failed."
        | :? InvalidCastException -> Failure "Could not execute command successfully."
        | :? SqlException -> Failure "Could not execute command successfully."
        | :? IOException -> Failure "Could not execute command successfully."
        | :? InvalidOperationException -> Failure "Could not execute command successfully."


open System.Text
open System.Net.Mail

let emailCustomerProcessedNotification customer =
    try
        let emailReceipiant = "test"
        let subject = "Customer " + customer.CustomerId.ToString() + "was added";
        let message = new StringBuilder()
        message.Append(customer.FirstName + " " + customer.LastName) |> ignore
        message.Append(" from " + customer.Address + " " + customer.City + ", " + customer.State) |> ignore
        message.Append(" was added to our system.") |> ignore
        let hostName = ConfigurationManager.AppSettings.["smtpAddress"]
        let client = new SmtpClient(hostName)
        let mailMessage = new MailMessage("postmaster@newco.com",emailReceipiant,subject,message.ToString())
        client.Send(mailMessage)
        Success customer
    with
        | :? ConfigurationErrorsException -> Failure "Configuration file read failed."
        | :? ArgumentNullException -> Failure "Could not execute email successfully."
        | :? InvalidOperationException -> Failure "Could not execute email successfully."
        | :? SmtpException -> Failure "Could not execute email successfully."


let RegisterNewCustomer =
    validateCustomer
    >> bind insertCustomer
    >> bind emailCustomerProcessedNotification

let customer = {CustomerId=0; FirstName = "Jamie"; LastName="Dixon"; Address="Test Address";
                City="Raleigh"; State="NC"; ZipCode="27519"}

let testResult = RegisterNewCustomer customer

match testResult with 
        | Success c -> "Customer %A was added" + c.CustomerId.ToString()
        | Failure f -> f
    

