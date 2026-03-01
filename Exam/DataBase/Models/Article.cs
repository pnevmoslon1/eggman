using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DataBase.Models;

public class Article
{
    public string Id { get; set; }
    public string Name { get; set; }

    public string Thematic { get;  set; }
    public DateTime Date { get;  set; }

    [ForeignKey("ArticleId")]
    public List<Author> Authors { get; set; }
}
