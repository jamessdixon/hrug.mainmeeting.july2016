
#r "../packages/FSharp.Data.2.3.0/lib/net40/FSharp.Data.dll"
open FSharp.Data

type YahooContext = CsvProvider<"http://ichart.finance.yahoo.com/table.csv?s=MSFT">
let stockInfo = YahooContext.Load("http://ichart.finance.yahoo.com/table.csv?s=MSFT")
let mostRecent = stockInfo.Rows |> Seq.head
(float)mostRecent.``Adj Close``

#load "../packages/FSharp.Charting.0.90.14/FSharp.Charting.fsx"
open System
open FSharp.Charting

let rows = stockInfo.Rows
rows 
|> Seq.map(fun r -> r.Date, (float)r.``Adj Close``)
|> Chart.FastLine


#r "../packages/Accord.3.0.2/lib/net40/Accord.dll"
#r "../packages/Accord.Statistics.3.0.2/lib/net40/Accord.Statistics.dll"
#r "../packages/Accord.Math.3.0.2/lib/net40/Accord.Math.dll"
open Accord
open Accord.Statistics
open Accord.Statistics.Models.Regression.Linear

let x = 
    rows
    |> Seq.take(20) 
    |> Seq.map(fun r -> r.Date.ToOADate()) 
    |> Seq.toArray
let y = 
    rows 
    |> Seq.take(20) 
    |> Seq.map(fun r -> (float)r.``Adj Close``) 
    |> Seq.toArray

let regression = SimpleLinearRegression()
let sse = regression.Regress(x,y)
let mse = sse/float x.Length 
let rmse = sqrt(mse)
let r2 = regression.CoefficientOfDetermination(x,y)

let nextDate = [|(new DateTime(2016,5,11)).ToOADate()|]
regression.Compute(nextDate)

