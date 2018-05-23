using _4TierProject.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _4TierProject.WEB.UI.Models
{
    public class RoleViewModel
    {
        public RoleViewModel() { }

        public RoleViewModel(Role role)
        {
            Id = role.Id;
            Name = role.Name;
            IsActive = role.IsActive;
            IsDeleted = role.IsDeleted;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}