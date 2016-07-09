
open System

//Syntax
let foo = "Hello"
let foo = "World"

type Customer = {Id:Guid;LastName:string;FirstName:string;Age:float}
let customers = [|{Id=Guid.NewGuid();LastName="Brady";FirstName="Mike";Age=42.5};
                  {Id=Guid.NewGuid();LastName="Brady";FirstName="Carol";Age=41.3}
                  {Id=Guid.NewGuid();LastName="Brady";FirstName="Greg";Age=16.1}
                  {Id=Guid.NewGuid();LastName="Brady";FirstName="Peter";Age=13.1}
                  {Id=Guid.NewGuid();LastName="Brady";FirstName="Bobby";Age=8.3}
                  {Id=Guid.NewGuid();LastName="Brady";FirstName="Jan";Age=15.4}
                  {Id=Guid.NewGuid();LastName="Brady";FirstName="Marcia";Age=9.0}
                  {Id=Guid.NewGuid();LastName="Brady";FirstName="Cindy";Age=7.2}
                  {Id=Guid.NewGuid();LastName="Alice";FirstName="Nelson";Age=58.9}
                  {Id=Guid.NewGuid();LastName="Sam";FirstName="Franklin";Age=62.6}
                  |]

//Language lying
let divide(x:float, y:float) =
    x / y

divide (6.0,2.0)
divide (6.0,0.0)

let divideBetter(x:float, y:float) =
    if(y = 0.0) then
        failwith("y cannot be zero")
    else
        x / y

divide (6.0,2.0)
try
    divide (6.0,0.0)
with
    | exn -> 0.0

let divideBest(x:float, y:float) =
    match y with
    | 0.0 -> None
    | _ -> Some (x / y)

divideBest (6.0,3.0)
divideBest (6.0,0.0)

//Piping Thoughts
customers
|> Seq.filter(fun c -> c.LastName = "Brady")
|> Seq.sortBy(fun c -> c.FirstName)
|> Seq.map(fun c -> c.FirstName)
|> Seq.toArray

//Pipe and Option
let totalCustomerCount = customers |> Seq.length |> float
let targetCustomerCount = customers |> Seq.filter(fun c -> c.LastName = "Brady") |> Seq.length |> float
let targetPercent = divideBest (targetCustomerCount,totalCustomerCount)




