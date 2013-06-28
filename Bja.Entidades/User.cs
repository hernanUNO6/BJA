﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bja.Entidades
{
    public class User
    {
        public long Id { get; set; }
        public long IdInstance { get; set; }
        public long IdUserRelation { get; set; }
        public String UserName { get; set; }
        public String CompleteName { get; set; }
        public String Password { get; set; }
        public UserStatus Status { get; set; }

        public List<Role> Roles { get; set; }
    }
}