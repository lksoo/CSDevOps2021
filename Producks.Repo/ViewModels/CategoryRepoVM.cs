using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Producks.Repo.ViewModels
{
    public class CategoryRepoVM
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
    }
}
