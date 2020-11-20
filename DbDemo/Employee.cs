using System;
using System.Collections.Generic;
using System.Text;

namespace DbDemo
{
    public partial class Employee
    {
        public Employee()
        {
            Tasks = new HashSet<Task>();
        }
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
