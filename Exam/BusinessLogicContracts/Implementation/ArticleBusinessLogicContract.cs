using ExamContractModel.BusinessLogicContracts;
using ExamContractModel.DataModels;
using ExamContractModel.Exceptions;
using ExamContractModel.Extensions;
using ExamContractModel.StorageContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicContracts.Implementation;

public class ArticleBusinessLogicContract(IArticleStorageContract articleStorageContract) : IArticleBusinessLogicContract
{
    private readonly IArticleStorageContract _articleStorageContract = articleStorageContract;
    public List<ArticleDataModel> GetAll()
    {
        return _articleStorageContract.GetList();
    }

    public ArticleDataModel GetElementByData(string data)
    {
        if (data.IsEmpty())
            throw new ArgumentNullException(nameof(data));
        if (data.IsGuid())
            return _articleStorageContract.GetById(data) ?? throw new ElementNotFoundException(data);
        return _articleStorageContract.GetByName(data) ?? throw new ElementNotFoundException(data);
    }

    public void InsertElement(ArticleDataModel articleDataModel)
    {
        ArgumentNullException.ThrowIfNull(articleDataModel);

        articleDataModel.Validation();

        _articleStorageContract.Create(articleDataModel);
    }

    public void UpdateElement(ArticleDataModel articleDataModel)
    {
        ArgumentNullException.ThrowIfNull(articleDataModel);
        articleDataModel.Validation();
        _articleStorageContract.Update(articleDataModel);
    }

    public void DeleteElement(string id)
    {
        if (id.IsEmpty())
            throw new ArgumentNullException(nameof(id));
        if (!id.IsGuid())
            throw new ArgumentException(null, nameof(id));
        _articleStorageContract.Delete(id);
    }

}