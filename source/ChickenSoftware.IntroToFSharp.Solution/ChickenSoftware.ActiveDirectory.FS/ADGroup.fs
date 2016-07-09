namespace ChickenSoftware.FSharpFeatures

open System;
open System.Collections.ObjectModel
open System.DirectoryServices.AccountManagement

type ADGroupInfo = {AccountName:string; Description:string; UserId:string }

type ADGroup() = 
    member this.GetList userId = 
        use context = new PrincipalContext(ContextType.Domain, "NEWCO")
        use user = UserPrincipal.FindByIdentity(context, IdentityType.SamAccountName, userId)
        try
            user.GetGroups()
            |> Seq.filter(fun g -> g :? GroupPrincipal)
            |> Seq.sortBy(fun g -> g.Description)
            |> Seq.toArray
            |> ReadOnlyCollection<_>
            |> Some
        with
        | :? NoMatchingPrincipalException -> None



