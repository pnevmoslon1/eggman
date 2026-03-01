using ExamContractModel.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamContractModel.BusinessLogicContracts;

public interface IArticleBusinessLogicContract
{
    List<ArticleDataModel> GetAll();

    ArticleDataModel GetElementByData(string data);

    void InsertElement(ArticleDataModel articleDataModel);

    void UpdateElement(ArticleDataModel articleDataModel);

    void DeleteElement(string id);
}
