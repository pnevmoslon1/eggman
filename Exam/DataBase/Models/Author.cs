using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Models;

public class Author
{
    public string Id { get;  set; }

    public string ArticleId { get;  set; }

    public string FIO { get;  set; } 

    public string Email { get;  set; }

    public DateTime BirthDay { get;  set; } 

    public string WorkPlace { get;  set; } 

    public Article Article { get; set; }  
}
