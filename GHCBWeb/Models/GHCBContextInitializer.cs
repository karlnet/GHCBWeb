using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace GHCBWeb.Models
{
    public class GHCBContextInitializer : DropCreateDatabaseIfModelChanges<ghcbDBEntities>
    {
        protected override void Seed(ghcbDBEntities context)
        {
            var interfacedescription = new List<interfaceDescription>()
            {
                new interfaceDescription() { },
                new interfaceDescription() { },
                new interfaceDescription() {  }
            };

            interfacedescription.ForEach(p => context.interfaceDescriptions.Add(p));
            context.SaveChanges();

          
        }
    }

}
