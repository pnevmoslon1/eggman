using ExamContractModel.Extensions;
using ExamContractModel.Exceptions;
using System.Text.RegularExpressions;

namespace ExamContractModel.DataModels;

public class AuthorDataModel(string id, string articleId, string fio, string email, DateTime birthDay, string workPlace)
{
    public string Id { get; private set; } = id;

    public string ArticleId { get; private set; } = articleId;

    public string FIO { get; private set; } = fio;

    public string Email { get; private set; }  = email;

    public DateTime BirthDay { get; private set; } = birthDay;

    public string WorkPlace { get; private set; } = workPlace;

    public void Validation()
    {
        if (Id.IsEmpty())
            throw new ValidationException("Field Id is empty");
        if (!Id.IsGuid())
            throw new ValidationException("Field Id is not id");
        if (ArticleId.IsEmpty())
            throw new ValidationException("Field ArticleId is empty");
        if (!ArticleId.IsGuid())
            throw new ValidationException("Field ArticleId is not id");
        if (FIO.IsEmpty())
            throw new ValidationException("Field FIO is empty");
        if (Email.IsEmpty())
            throw new ValidationException("Field Email is empty");
        if (!Regex.IsMatch(Email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
            throw new ValidationException("Field Email is not email");
        if (BirthDay > DateTime.UtcNow)
            throw new ValidationException("BirthDay is invalid");
        if (BirthDay > DateTime.UtcNow.AddYears(-18))
            throw new ValidationException("Author is too young");
        if (WorkPlace.IsEmpty())
            throw new ValidationException("Field WorkPlace is empty");
    }


}
