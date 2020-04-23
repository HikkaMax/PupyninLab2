using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PupyninLab2.Entity
{
    public class GroupEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<StudentEntity>Students { get; set; }
    }
}
