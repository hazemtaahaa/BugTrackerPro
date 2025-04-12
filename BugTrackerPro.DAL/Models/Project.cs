﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerPro.DAL;

public class Project 
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ICollection<Bug> Bugs { get; set; }
}
