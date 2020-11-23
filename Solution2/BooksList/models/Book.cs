using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookList.models
{
    public class Book
    {
        
         [Key]
         public int Book_id { get; set; }
         public string Book_Name { get; set; }
         public double Cost { get; set; }

      
    }
}
