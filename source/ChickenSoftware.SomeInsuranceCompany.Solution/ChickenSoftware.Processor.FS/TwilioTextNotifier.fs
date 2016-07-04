namespace ChickenSoftware.Processor.FS

open ChickenSoftware.Core
open Twilio

type TwilioTextNotifier(accountId,authToken,from,recipiants) =
    interface INotifier with
        member this.Notify message = 
            let twilio = new TwilioRestClient(accountId, authToken)
            recipiants 
            |> Seq.iter(fun r -> twilio.SendMessage(from,r,message) |> ignore)
