using ExamContractModel.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamContractModel.BusinessLogicContracts;

public interface IAuthorBusinessLogicContract
{
    List<AuthorDataModel> GetAll();

    AuthorDataModel GetElementByData(string data);

    void InsertElement(AuthorDataModel authorDataModel);

    void UpdateElement(AuthorDataModel authorDataModel);

    void DeleteElement(string id);
}
