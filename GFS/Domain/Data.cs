using GFS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GFS.Domain
{
    public class Data
    {
        public IEnumerable<Navbar> navbarItems()
        {
            //var menu = new List<Navbar>();
            //menu.Add(new Navbar { Id = 1, nameOption = "Dashboard", controller = "Home", action = "Index", imageClass = "fa fa-dashboard fa-fw", estatus = true, isParent = false, parentId = 0 });
            //menu.Add(new Navbar { Id = 2, nameOption = "Charts", imageClass = "fa fa-bar-chart-o fa-fw", estatus = true, isParent = true, parentId = 0 });
            //menu.Add(new Navbar { Id = 3, nameOption = "Flot Charts", controller = "Home", action = "FlotCharts", estatus = true, isParent = false, parentId = 2 });
            //menu.Add(new Navbar { Id = 4, nameOption = "Morris.js Charts", controller = "Home", action = "MorrisCharts", estatus = true, isParent = false, parentId = 2 });
            //menu.Add(new Navbar { Id = 5, nameOption = "Tables", controller = "Home", action = "Tables", imageClass = "fa fa-table fa-fw", estatus = true, isParent = false, parentId = 0 });
            //menu.Add(new Navbar { Id = 6, nameOption = "Forms", controller = "Home", action = "Forms", imageClass = "fa fa-edit fa-fw", estatus = true, isParent = false, parentId = 0 });
            //menu.Add(new Navbar { Id = 7, nameOption = "UI Elements", imageClass = "fa fa-wrench fa-fw", estatus = true, isParent = true, parentId = 0 });
            //menu.Add(new Navbar { Id = 8, nameOption = "Panels and Wells", controller = "Home", action = "Panels", estatus = true, isParent = false, parentId = 7 });
            //menu.Add(new Navbar { Id = 9, nameOption = "Buttons", controller = "Home", action = "Buttons", estatus = true, isParent = false, parentId = 7 });
            //menu.Add(new Navbar { Id = 10, nameOption = "Notifications", controller = "Home", action = "Notifications", estatus = true, isParent = false, parentId = 7 });
            //menu.Add(new Navbar { Id = 11, nameOption = "Typography", controller = "Home", action = "Typography", estatus = true, isParent = false, parentId = 7 });
            //menu.Add(new Navbar { Id = 12, nameOption = "Icons", controller = "Home", action = "Icons", estatus = true, isParent = false, parentId = 7 });
            //menu.Add(new Navbar { Id = 13, nameOption = "Grid", controller = "Home", action = "Grid", estatus = true, isParent = false, parentId = 7 });
            //menu.Add(new Navbar { Id = 14, nameOption = "Multi-Level Dropdown", imageClass = "fa fa-sitemap fa-fw", estatus = true, isParent = true, parentId = 0 });
            //menu.Add(new Navbar { Id = 15, nameOption = "Second Level Item", estatus = true, isParent = false, parentId = 14 });
            //menu.Add(new Navbar { Id = 16, nameOption = "Sample Pages", imageClass = "fa fa-files-o fa-fw", estatus = true, isParent = true, parentId = 0 });
            //menu.Add(new Navbar { Id = 17, nameOption = "Blank Page", controller = "Home", action = "Blank", estatus = true, isParent = false, parentId = 16 });
            //menu.Add(new Navbar { Id = 18, nameOption = "Login Page", controller = "Home", action = "Login", estatus = true, isParent = false, parentId = 16 });

            var menu = new List<Navbar>();
            menu.Add(new Navbar { Id = 1, nameOption = "Policy Plans", controller = "PolicyPlans", action = "Index", imageClass = "fa fa-sitemap fa-fw", estatus = true, isParent = false, parentId = 0 });
            menu.Add(new Navbar { Id = 18, nameOption = "Create New Policy", controller = "NewMembers", action = "Create", imageClass = "fa fa-files-o fa-fw", estatus = true, isParent = false, parentId = 0 });
            menu.Add(new Navbar { Id = 2, nameOption = "Clients", imageClass = "fa fa-table fa-fw", estatus = true, isParent = true, parentId = 0 });
            menu.Add(new Navbar { Id = 3, nameOption = "Policy Holders", controller = "NewMembers", action = "Index", estatus = true, isParent = false, parentId = 2 });
            menu.Add(new Navbar { Id = 4, nameOption = "Dependants", controller = "Dependants", action = "Index", estatus = true, isParent = false, parentId = 2 });
            menu.Add(new Navbar { Id = 5, nameOption = "Payers", controller = "Payers", action = "Index", estatus = true, isParent = false, parentId = 2 });
            menu.Add(new Navbar { Id = 18, nameOption = "Beneficiaries", controller = "Beneficiaries", action = "Index", estatus = true, isParent = false, parentId = 2 });
            menu.Add(new Navbar { Id = 19, nameOption = "Authorizations", controller = "DebitOrderAuthorizations", action = "Index", estatus = true, isParent = false, parentId = 2 });
            menu.Add(new Navbar { Id = 6, nameOption = "Stock File", controller = "StockFiles", action = "Index", imageClass = "fa fa-edit fa-fw", estatus = true, isParent = false, parentId = 0 });
            menu.Add(new Navbar { Id = 7, nameOption = "Manage Your Data", imageClass = "fa fa-wrench fa-fw", estatus = true, isParent = true, parentId = 0 });
            menu.Add(new Navbar { Id = 8, nameOption = "Add Banks", controller = "Banks", action = "Create", estatus = true, isParent = false, parentId = 7 });
            menu.Add(new Navbar { Id = 9, nameOption = "Add Account Types", controller = "AccTypes", action = "Create", estatus = true, isParent = false, parentId = 7 });
            menu.Add(new Navbar { Id = 10, nameOption = "Add Relationships", controller = "Relations", action = "Create", estatus = true, isParent = false, parentId = 7 });
            menu.Add(new Navbar { Id = 11, nameOption = "Add Your Stock Categories", controller = "Stockcategories", action = "Create", estatus = true, isParent = false, parentId = 7 });
            menu.Add(new Navbar { Id = 12, nameOption = "Other Clients", imageClass = "fa fa-table fa-fw", estatus = true, isParent = true, parentId = 0 });
            menu.Add(new Navbar { Id = 13, nameOption = "Deceased Table", controller = "Deceaseds", action = "Index", imageClass = "fa fa-fw fa-bar-chart-o", estatus = true, isParent = false, parentId = 12 });
            menu.Add(new Navbar { Id = 14, nameOption = "Archived Members' Table", controller = "ArchivedMembers", action = "Index", imageClass = "fa fa-fw fa-bar-chart-o", estatus = true, isParent = false, parentId = 12 });
            menu.Add(new Navbar { Id = 15, nameOption = "Login Page", controller = "Home", action = "Login", estatus = true, isParent = false, parentId = 12 });
            menu.Add(new Navbar { Id = 16, nameOption = "Capture Payment", controller = "Payments", action = "Search", imageClass = "fa fa-edit fa-fw", estatus = true, isParent = false, parentId = 0 });
            menu.Add(new Navbar { Id = 17, nameOption = "Payments", controller = "Payments", action = "Index", imageClass = "fa fa-files-o fa-fw", estatus = true, isParent = false, parentId = 0 });



            //menu.Add(new Navbar { Id = 9, nameOption = "Policy Plans", controller = "PolicyPlans", action = "Index", imageClass = "fa fa-fw fa-bar-chart-o", estatus = true });

            //menu.Add(new Navbar { Id = 1, nameOption = "Applicants", controller = "Members", action = "Index", imageClass = "fa fa-fw fa-dashboard", estatus = true });
            //menu.Add(new Navbar { Id = 2, nameOption = "Policy Holders", controller = "NewMembers", action = "Index", imageClass = "fa fa-fw fa-bar-chart-o", estatus = true });
            //menu.Add(new Navbar { Id = 14, nameOption = "Dependants", controller = "Dependants", action = "Index", imageClass = "fa fa-fw fa-table", estatus = true });
            //menu.Add(new Navbar { Id = 15, nameOption = "Beneficiaries", controller = "Beneficiary", action = "Index", imageClass = "fa fa-fw fa-table", estatus = true });
            //menu.Add(new Navbar { Id = 8, nameOption = "Payers", controller = "Payers", action = "Index", imageClass = "fa fa-fw fa-table", estatus = true });
            //menu.Add(new Navbar { Id = 16, nameOption = "Authorizations", controller = "DebitOrderAuthorization", action = "Index", imageClass = "fa fa-fw fa-table", estatus = true });
            //menu.Add(new Navbar { Id = 5, nameOption = "Deceased", controller = "Deceaseds", action = "Index", imageClass = "fa fa-fw fa-table", estatus = true });
            //menu.Add(new Navbar { Id = 13, nameOption = "Achirved Members", controller = "AchirvedMembers", action = "Index", imageClass = "fa fa-fw fa-edit", estatus = true });
            //menu.Add(new Navbar { Id = 4, nameOption = "Inventory", controller = "StockFiles", action = "Index", imageClass = "fa fa-fw fa-edit", estatus = true });
            //menu.Add(new Navbar { Id = 6, nameOption = "Capture Payment", controller = "Payments", action = "Create", imageClass = "fa fa-fw fa-file", estatus = true });
            //menu.Add(new Navbar { Id = 7, nameOption = "Payments", controller = "Payments", action = "Index", imageClass = "fa fa-fw fa-table", estatus = true });
            //menu.Add(new Navbar { Id = 3, nameOption = "Employees", controller = "Employees", action = "Index", imageClass = "fa fa-fw fa-table", estatus = true });
            //menu.Add(new Navbar { Id = 10, nameOption = "Relationship", controller = "Relations", action = "Create", imageClass = "fa fa-fw fa-edit", estatus = true });
            //menu.Add(new Navbar { Id = 11, nameOption = "Banks", controller = "Banks", action = "Create", imageClass = "fa fa-fw fa-edit", estatus = true });
            //menu.Add(new Navbar { Id = 12, nameOption = "Account Types", controller = "AccTypes", action = "Create", imageClass = "fa fa-fw fa-edit", estatus = true });
            //menu.Add(new Navbar { Id = 17, nameOption = "Stock Categories", controller = "StockCategories", action = "Create", imageClass = "fa fa-fw fa-edit", estatus = true });


            return menu.ToList();
        }
        public IEnumerable<User> users()
        {
            var users = new List<User>();
            users.Add(new User { Id = 1, user = "Admin", password = "12345", estatus = true, RememberMe = false });
            users.Add(new User { Id = 2, user = "Data Capturer", password = "54321", estatus = true, RememberMe = true });
            users.Add(new User { Id = 3, user = "Cashier", password = "678910", estatus = true, RememberMe = true });
            //users.Add(new User { Id = 4, user = "invite", password = "12345", estatus = false, RememberMe = false });

            return users.ToList();
        }
        public IEnumerable<Roles> roles()
        {
            var roles = new List<Roles>();
            roles.Add(new Roles { rowid = 1, idUser = 2, idMenu = 1, status = true });
            roles.Add(new Roles { rowid = 2, idUser = 2, idMenu = 2, status = true });
            roles.Add(new Roles { rowid = 3, idUser = 1, idMenu = 3, status = true });
            roles.Add(new Roles { rowid = 4, idUser = 2, idMenu = 4, status = true });
            roles.Add(new Roles { rowid = 5, idUser = 2, idMenu = 5, status = true });
            roles.Add(new Roles { rowid = 6, idUser = 3, idMenu = 6, status = true });
            roles.Add(new Roles { rowid = 1, idUser = 1, idMenu = 1, status = true });
            roles.Add(new Roles { rowid = 2, idUser = 1, idMenu = 2, status = true });
            roles.Add(new Roles { rowid = 4, idUser = 1, idMenu = 4, status = true });
            roles.Add(new Roles { rowid = 5, idUser = 1, idMenu = 5, status = true });
            roles.Add(new Roles { rowid = 6, idUser = 1, idMenu = 6, status = true });
            roles.Add(new Roles { rowid = 7, idUser = 1, idMenu = 7, status = true });
            roles.Add(new Roles { rowid = 8, idUser = 1, idMenu = 8, status = true });
            roles.Add(new Roles { rowid = 9, idUser = 1, idMenu = 9, status = true });
            roles.Add(new Roles { rowid = 10, idUser = 1, idMenu = 10, status = true });
            roles.Add(new Roles { rowid = 11, idUser = 1, idMenu = 11, status = true });
            roles.Add(new Roles { rowid = 12, idUser = 1, idMenu = 12, status = true });
            roles.Add(new Roles { rowid = 13, idUser = 1, idMenu = 13, status = true });
            roles.Add(new Roles { rowid = 14, idUser = 1, idMenu = 14, status = true });
            roles.Add(new Roles { rowid = 15, idUser = 1, idMenu = 15, status = true });
            roles.Add(new Roles { rowid = 16, idUser = 1, idMenu = 16, status = true });
            roles.Add(new Roles { rowid = 17, idUser = 1, idMenu = 17, status = true });
            return roles.ToList();
        }
        public IEnumerable<Navbar> itemsPerUser(string controller, string action, string userName)
        {

            IEnumerable<Navbar> items = navbarItems();
            IEnumerable<Roles> rolesNav = roles();
            IEnumerable<User> usersNav = users();

            var navbar = items.Where(p => p.controller == controller && p.action == action).Select(c => { c.activeli = "active"; return c; }).ToList();

            navbar = (from nav in items
                      join rol in rolesNav on nav.Id equals rol.idMenu
                      join user in usersNav on rol.idUser equals user.Id
                      where user.user == userName
                      select new Navbar
                      {
                          Id = nav.Id,
                          nameOption = nav.nameOption,
                          controller = nav.controller,
                          action = nav.action,
                          imageClass = nav.imageClass,
                          estatus = nav.estatus,
                          activeli = nav.activeli
                      }).ToList();

            return navbar.ToList();
        }
    }
}