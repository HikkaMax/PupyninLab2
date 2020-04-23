using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using PupyninLab2.Entity;

namespace PupyninLab2.Entity
{
    public class ExampleContext : DbContext
    {
        public ExampleContext(DbContextOptions<ExampleContext> options) : base(options)
        {
        }
        public DbSet<StudentEntity> Students {get; set;}
        public DbSet<GroupEntity> Groups { get; set; }
    }
}
