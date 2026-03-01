using ExamContractModel.Extensions;
using ExamContractModel.Infrastructure;
using ExamContractModel.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamContractModel.DataModels;

public class ArticleDataModel(string id, string name, string thematics, DateTime date) : IValidation
{
    public string Id { get; private set; } = id;
    public string Name { get; private set; } = name;
    public string Thematic { get; private set;} = thematics;
    public DateTime Date { get; private set;} = date;

    public void Validation()
    {
        if (Id.IsEmpty())
            throw new ValidationException("Field Id is empty");
        if (!Id.IsGuid())
            throw new ValidationException("Field Id is not id");
        if (Name.IsEmpty())
            throw new ValidationException("Field Name is empty");
        if (Thematic.IsEmpty())
            throw new ValidationException("Field Thematic is empty");
        if (Date > DateTime.UtcNow)
            throw new ValidationException("Date is invalid");
    }
}
