using ExamContractModel.DataModels;
using ExamContractModel.Exceptions;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamTests.DataModelsTests;

[TestFixture]
public class AuthorDataModelTest
{
    private static readonly string ValidId = Guid.NewGuid().ToString();
    private static readonly string ValidArticleId = Guid.NewGuid().ToString();
    private const string ValidFio = "Какое-то";
    private const string ValidWorkPlace = "Какое-то";
    private const string ValidEmail = "karimarlehinm@gmail.com";
    private readonly DateTime ValidBithDayDate = DateTime.UtcNow.AddYears(-19);


    [Test]
    public void Validation_WithValidData_NotThrowValidationException()
    {
        // Arrange
        var model = new AuthorDataModel
            (
                ValidId,
                ValidArticleId,
                ValidFio,
                ValidEmail,
                ValidBithDayDate,
                ValidWorkPlace
            );

        // Act & Assert
        Assert.DoesNotThrow(() => model.Validation());

    }

    [Test]
    public void Validation_NotGuidId_ThrowValidationException()
    {
        // Arrange
        var model = new AuthorDataModel
            (
                "ывфв32",
                ValidArticleId,
                ValidFio,
                ValidEmail,
                ValidBithDayDate,
                ValidWorkPlace
            );

        var ex = Assert.Throws<ValidationException>(() => model.Validation());
        Assert.That(ex.Message, Is.EqualTo("Field Id is not id"));
    }
    [Test]
    public void Validation_EmptyId_ThrowValidationException()
    {
        // Arrange
        var model = new AuthorDataModel
            (
                string.Empty,
                ValidArticleId,
                ValidFio,
                ValidEmail,
                ValidBithDayDate,
                ValidWorkPlace
            );

        var ex = Assert.Throws<ValidationException>(() => model.Validation());
        Assert.That(ex.Message, Is.EqualTo("Field Id is empty"));
    }
    [Test]
    public void Validation_NotGuidArticleId_ThrowValidationException()
    {
        // Arrange
        var model = new AuthorDataModel
            (
                ValidId,
                "ValidArticleId",
                ValidFio,
                ValidEmail,
                ValidBithDayDate,
                ValidWorkPlace
            );

        var ex = Assert.Throws<ValidationException>(() => model.Validation());
        Assert.That(ex.Message, Is.EqualTo("Field ArticleId is not id"));
    }
    [Test]
    public void Validation_EmptyArticleId_ThrowValidationException()
    {
        // Arrange
        var model = new AuthorDataModel
            (
                ValidId,
                string.Empty,
                ValidFio,
                ValidEmail,
                ValidBithDayDate,
                ValidWorkPlace
            );

        var ex = Assert.Throws<ValidationException>(() => model.Validation());
        Assert.That(ex.Message, Is.EqualTo("Field ArticleId is empty"));
    }
    [Test]
    public void Validation_EmptyFIO_ThrowValidationException()
    {
        // Arrange
        var model = new AuthorDataModel
            (
                ValidId,
                ValidArticleId,
                string.Empty,
                ValidEmail,
                ValidBithDayDate,
                ValidWorkPlace
            );

        var ex = Assert.Throws<ValidationException>(() => model.Validation());
        Assert.That(ex.Message, Is.EqualTo("Field FIO is empty"));
    }
    [Test]
    public void Validation_EmptyEmail_ThrowValidationException()
    {
        // Arrange
        var model = new AuthorDataModel
            (
                ValidId,
                ValidArticleId,
                ValidFio,
                string.Empty,
                ValidBithDayDate,
                ValidWorkPlace
            );

        var ex = Assert.Throws<ValidationException>(() => model.Validation());
        Assert.That(ex.Message, Is.EqualTo("Field Email is empty"));
    }

    [Test]
    public void Validation_EmptyWorkPlace_ThrowValidationException()
    {
        // Arrange
        var model = new AuthorDataModel
            (
                ValidId,
                ValidArticleId,
                ValidFio,
                ValidEmail,
                ValidBithDayDate,
                string.Empty
            );

        var ex = Assert.Throws<ValidationException>(() => model.Validation());
        Assert.That(ex.Message, Is.EqualTo("Field WorkPlace is empty"));
    }

    [Test]
    public void Validation_WithNotValidEmail_ThrowValidationException()
    {
        // Arrange
        var model = new AuthorDataModel
            (
                ValidId,
                ValidArticleId,
                ValidFio,
                "ValidEmail",
                ValidBithDayDate,
                ValidWorkPlace
            );

        var ex = Assert.Throws<ValidationException>(() => model.Validation());
        Assert.That(ex.Message, Is.EqualTo("Field Email is not email"));
    }

    [Test]
    
    public void Validation_WithFutureDate_ThrowValidationException()
    {
        // Arrange
        var model = new AuthorDataModel
            (
                ValidId,
                ValidArticleId,
                ValidFio,
                ValidEmail,
                DateTime.UtcNow.AddDays(5),
                ValidWorkPlace
            );

        // Act & Assert
        var ex = Assert.Throws<ValidationException>(() => model.Validation());
        Assert.That(ex.Message, Is.EqualTo("BirthDay is invalid"));

    }
    [Test]

    public void Validation_WithSmallAuthor_ThrowValidationException()
    {
        // Arrange
        var model = new AuthorDataModel
            (
                ValidId,
                ValidArticleId,
                ValidFio,
                ValidEmail,
                DateTime.UtcNow.AddDays(-16),
                ValidWorkPlace
            );

        // Act & Assert
        var ex = Assert.Throws<ValidationException>(() => model.Validation());
        Assert.That(ex.Message, Is.EqualTo("Author is too young"));

    }
}
