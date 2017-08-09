﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PandaPress.Core.Models.Request
{
    public class PasswordChangeRequest
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string NewPasswordConfirm { get; set; }
    }

    public class PasswordChangeResult
    {
        public bool Succeeded { get; set; }
        public List<string> Errors { get; set; }
    }
}
