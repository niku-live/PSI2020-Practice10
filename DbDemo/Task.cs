using System;
using System.Collections.Generic;
using System.Text;

namespace DbDemo
{
    public class Task
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public int EmployeeEmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
