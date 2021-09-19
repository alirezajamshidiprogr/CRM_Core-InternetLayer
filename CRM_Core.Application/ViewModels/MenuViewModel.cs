using System;
using System.Collections.Generic;
using System.Text;

namespace CRM_Core.Application.ViewModels
{
    public class MenuViewModel
    {
        public string MenuName { get; set; }
        public string Title { get; set; }
        public int MenuId { get; set; }
        public int Order { get; set; }
        public int? ParentMenuId { get; set; }
        public string Event { get; set; }
        public bool IsActive { get; set; }
        public bool IsButton { get; set; }
        public bool IsVisible { get; set; }
        public int hasAccess { get; set; }
        public string IconClass { get; set; }
    }

    public class UserMenuViewModel
    {
        public int MenuId { get; set; }
        public int? ParentMenuId { get; set; }
        public string MenuTitle { get; set; }
        public string IdentityUserId { get; set; }
        public int? MenuState { get; set; }
    }
}
