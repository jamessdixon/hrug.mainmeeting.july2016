
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


let customer = {CustomerId=0; FirstName = "Jamie"; LastName=""; Address="Test Address";
                City="Raleigh"; State="NC"; ZipCode="27519"}

let testResult = validateCustomer customer





