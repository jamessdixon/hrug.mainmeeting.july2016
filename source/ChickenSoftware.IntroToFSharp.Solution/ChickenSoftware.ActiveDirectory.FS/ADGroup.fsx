
#r "System.DirectoryServices.AccountManagement"

open System;
open System.Collections.ObjectModel
open System.DirectoryServices.AccountManagement

type ADGroupInfo = {Name :string; Description : string; UserId : string }

let userId = "jamie@newco.com"
let context = new PrincipalContext(ContextType.Domain, "NEWCO")
let user = UserPrincipal.FindByIdentity(context, IdentityType.SamAccountName, userId)
user.GetGroups()
|> Seq.filter(fun g -> g :? GroupPrincipal)
|> Seq.sortBy(fun g -> g.Description)
|> Seq.toArray
|> ReadOnlyCollection<_>




