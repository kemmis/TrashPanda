﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PandaPress.Core.Models.Request
{
    public class ProfileSettingsUpdateRequest
    {
        public string DisplayName { get; set; }
        public string About { get; set; }
    }
}
