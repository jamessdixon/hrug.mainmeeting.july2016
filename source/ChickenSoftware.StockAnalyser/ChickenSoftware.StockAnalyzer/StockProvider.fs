namespace ChickenSoftware.StockAnalyzer

open Accord
open System.Web
open FSharp.Data
open System.Net.Http
open Accord.Statistics
open Accord.Statistics.Models.Regression.Linear

type YahooContext = CsvProvider<"http://ichart.finance.yahoo.com/table.csv?s=MSFT">
type LuisContext = JsonProvider<"../data/Luis.json">

type StockProvider() = 
    member this.GetMostRecentClose(ticker) =
        try
            let stockInfo = YahooContext.Load("http://ichart.finance.yahoo.com/table.csv?s=" + ticker)
            let mostRecent = stockInfo.Rows |> Seq.head
            (float)mostRecent.``Adj Close``
        with
        | :?  System.Net.WebException -> -1.0
            
    member this.PredictStockPrice(ticker, nextDate:System.DateTime) =
        try
            let stockInfo = YahooContext.Load("http://ichart.finance.yahoo.com/table.csv?s=" + ticker)
            let rows = stockInfo.Rows |> Seq.take(20)
            let x = 
                rows
                |> Seq.map(fun r -> r.Date.ToOADate()) 
                |> Seq.toArray
            let y = 
                rows 
                |> Seq.map(fun r -> (float)r.``Adj Close``) 
                |> Seq.toArray

            let regression = SimpleLinearRegression()
            regression.Regress(x,y) |> ignore
            let nextDate' = [|nextDate.ToOADate()|]
            regression.Compute(nextDate') |> Seq.head
        with
        | :?  System.Net.WebException -> -1.0
        
