using ExamContractModel.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamContractModel.StorageContract;

public interface IAuthorStorageContract
{
    public List<AuthorDataModel> GetList();

    public AuthorDataModel GetById(string id);
    public AuthorDataModel GetByFIO(string name);
    void Create(AuthorDataModel author);

    void Update(AuthorDataModel author);

    void Delete(string id);
}
