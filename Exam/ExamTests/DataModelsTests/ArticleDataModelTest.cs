using ExamContractModel.DataModels;
using ExamContractModel.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamTests.DataModelsTests;

[TestFixture]
public class ArticleDataModelTest
{
    private static readonly string ValidId = Guid.NewGuid().ToString();
    private const string ValidName = "Какое-то";
    private const string ValidThematic = "Какая-то";
    private readonly DateTime ValidDate = DateTime.UtcNow.AddDays(-1);

    [Test]
    public void Validation_WithValidData_NotThrowValidationException()
    {
        // Arrange
        var model = new ArticleDataModel
            (
                ValidId,
                ValidName,
                ValidThematic,
                ValidDate
            );

        // Act & Assert
        Assert.DoesNotThrow(() => model.Validation());

    }

    [Test]
    public void Validation_NotGuidId_ThrowValidationException()
    {
        // Arrange
        var model = new ArticleDataModel
            (
                "sadasd1",
                ValidName,
                ValidThematic,
                ValidDate
            );

        // Act & Assert
        var ex = Assert.Throws<ValidationException>(() => model.Validation());
        Assert.That(ex.Message, Is.EqualTo("Field Id is not id"));

    }

    [Test]
    public void Validation_EmptyId_ThrowValidationException()
    {
        // Arrange
        var model = new ArticleDataModel
            (
                string.Empty,
                ValidName,
                ValidThematic,
                ValidDate
            );

        // Act & Assert
        var ex = Assert.Throws<ValidationException>(() => model.Validation());
        Assert.That(ex.Message, Is.EqualTo("Field Id is empty"));
    }

    [Test]
    public void Validation_EmptyName_ThrowValidationException()
    {
        // Arrange
        var model = new ArticleDataModel
            (
                ValidId,
                string.Empty,
                ValidThematic,
                ValidDate
            );

        // Act & Assert
        var ex = Assert.Throws<ValidationException>(() => model.Validation());
        Assert.That(ex.Message, Is.EqualTo("Field Name is empty"));
    }

    [Test]
    public void Validation_EmptyThematic_ThrowValidationException()
    {
        // Arrange
        var model = new ArticleDataModel
            (
                ValidId,
                ValidName,
                string.Empty,
                ValidDate
            );

        // Act & Assert
        var ex = Assert.Throws<ValidationException>(() => model.Validation());
        Assert.That(ex.Message, Is.EqualTo("Field Thematic is empty"));
    }
    [Test]
    public void Validation_WithFutureDate_ThrowValidationException()
    {
        // Arrange
        var model = new ArticleDataModel
            (
                ValidId,
                ValidName,
                ValidThematic,
                DateTime.UtcNow.AddDays(1)
            );
        
        // Act & Assert
        var ex = Assert.Throws<ValidationException>(() => model.Validation());
        Assert.That(ex.Message, Is.EqualTo("Date is invalid"));

    }
}
