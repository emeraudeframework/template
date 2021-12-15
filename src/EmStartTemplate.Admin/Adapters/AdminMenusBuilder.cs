﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Emeraude.Application.Admin.Adapters;
using Emeraude.Application.Admin.Models;

namespace EmStartTemplate.Admin.Adapters;

public class AdminMenusBuilder : IAdminMenusBuilder
{
    public async Task<SidebarSchema> BuildAsync()
        => await Task.FromResult(new SidebarSchema
        {
            ShortcutsLinks = new List<SidebarShortcutLink>
            {
                new ()
                {
                    Title = "Emeraude Framework",
                    Route = "https://emeraude.dev/",
                },
                new ()
                {
                    Title = "Emeraude Portal",
                    Route = "https://emeraude.io/",
                },
            },
            Sections = new List<SidebarMenuSection>
            {
                new ()
                {
                    Title = "Dashboard",
                    Icon = "mdi mdi-television",
                    Children = new List<SidebarMenuLink>
                    {
                        new ("Index", "/admin"),
                        new ("Insights", "/admin/insights"),
                    },
                },
                new ()
                {
                    Title = "Users",
                    Icon = "mdi mdi-account",
                    Children = new List<SidebarMenuLink>
                    {
                        new ("List", "/admin/users/", "/admin/users/{*}"),
                    },
                },
            },
        });
}