using ExamContractModel.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamContractModel.StorageContract;

public interface IArticleStorageContract
{
    public List<ArticleDataModel> GetList();

    public ArticleDataModel GetById(string id);
    public ArticleDataModel GetByName(string fio);
    void Create(ArticleDataModel article);

    void Update(ArticleDataModel article);

    void Delete(string id);
}
