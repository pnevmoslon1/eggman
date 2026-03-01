

using ExamContractModel.BusinessLogicContracts;
using ExamContractModel.DataModels;
using ExamContractModel.Exceptions;
using ExamContractModel.Extensions;
using ExamContractModel.StorageContract;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BusinessLogicContracts.Implementation;

public class AuthorBusinessLogicContract(IAuthorStorageContract authorStorageContract) : IAuthorBusinessLogicContract
{
    private readonly IAuthorStorageContract _authorStorageContract = authorStorageContract;
    public List<AuthorDataModel> GetAll()
    {
        return _authorStorageContract.GetList();
    }

    public AuthorDataModel GetElementByData(string data)
    {
        if (data.IsEmpty())
            throw new ArgumentNullException(nameof(data));
        if (data.IsGuid())
            return _authorStorageContract.GetByFIO(data) ?? throw new ElementNotFoundException(data);
        return _authorStorageContract.GetById(data) ?? throw new ElementNotFoundException(data);
    }

    public void InsertElement(AuthorDataModel authorDataModel)
    {
        ArgumentNullException.ThrowIfNull(authorDataModel);

        authorDataModel.Validation();

        _authorStorageContract.Create(authorDataModel);
    }

    public void UpdateElement(AuthorDataModel authorDataModel)
    {
        ArgumentNullException.ThrowIfNull(authorDataModel);
        authorDataModel.Validation();
        _authorStorageContract.Update(authorDataModel);
    }

    public void DeleteElement(string id)
    {
        if (id.IsEmpty())
            throw new ArgumentNullException(nameof(id));
        if (!id.IsGuid())
            throw new ArgumentException(null, nameof(id));
        _authorStorageContract.Delete(id);
    }

}
