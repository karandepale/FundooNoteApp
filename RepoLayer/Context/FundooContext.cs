using Microsoft.EntityFrameworkCore;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Context
{
    public class FundooContext :DbContext
    {
        public FundooContext(DbContextOptions option) : base(option)
        {
            
        }
        public DbSet<UserEntity> User { get; set; }
        public DbSet<NoteEntity> Note { get; set; } 
        public DbSet<CollabEntity> Collab { get; set; }
    }
}
