namespace ChickenSoftware.ActiveDirectory
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.DirectoryServices.AccountManagement;

    public static class ADGroupList
    {
        public static ReadOnlyCollection<ADGroup> GetList(string userId)
        {
            try
            {
                List<ADGroup> result = new List<ADGroup>();

                using (PrincipalContext context = new PrincipalContext(ContextType.Domain, "NEWCO"))
                {
                    using (UserPrincipal user = UserPrincipal.FindByIdentity(context, IdentityType.SamAccountName, userId))
                    {
                        if (user == null)
                        {
                            return new ReadOnlyCollection<ADGroup>(result);
                        }

                        PrincipalSearchResult<Principal> groups = user.GetAuthorizationGroups();

                        var iterGroup = groups.GetEnumerator();

                        using (iterGroup)
                        {
                            while (iterGroup.MoveNext())
                            {
                                try
                                {
                                    Principal group = iterGroup.Current;

                                    if (group is GroupPrincipal)
                                    {
                                        result.Add(new ADGroup(group.SamAccountName, group.Description, userId));
                                    }
                                }
                                catch (NoMatchingPrincipalException)
                                {
                                    continue;
                                }
                            }
                        }
                    }
                }

                result.Sort((grp1, grp2) => string.Compare(grp1.Description, grp2.Description, StringComparison.CurrentCultureIgnoreCase));
                return new ReadOnlyCollection<ADGroup>(result);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}
