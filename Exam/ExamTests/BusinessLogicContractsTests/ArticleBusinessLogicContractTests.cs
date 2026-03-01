using BusinessLogicContracts.Implementation;
using ExamContractModel.DataModels;
using ExamContractModel.Exceptions;
using ExamContractModel.StorageContract;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamTests.BusinessLogicContractsTests;

public class ArticleBusinessLogicContractTests
{
    private Mock<IArticleStorageContract> _articleStorageMock;
    private ArticleBusinessLogicContract _articleBusinessLogic;
    private ArticleDataModel _testArticle;

    [SetUp]
    public void SetUp()
    {
        _articleStorageMock = new Mock<IArticleStorageContract>();
        _articleBusinessLogic = new ArticleBusinessLogicContract(_articleStorageMock.Object);
        _testArticle = new ArticleDataModel(
            Guid.NewGuid().ToString(),
            "Name",
            "Какая-то",
            DateTime.UtcNow.AddDays(-1));
    }

    [Test]
    public void GetAll_ReturnsListFromStorage()
    {
        // Arrange
        var list = new List<ArticleDataModel> { _testArticle };

        _articleStorageMock.Setup(x => x.GetList()).Returns(list);

        // Act

        var result = _articleBusinessLogic.GetAll();

        // Assert
        Assert.That(result, Is.EqualTo(list));
        _articleStorageMock.Verify(x => x.GetList(), Times.Once);
    }

    [Test]
    public void GetElementByData_WithGuid_CallsGetByIdAndReturnsResult()
    {
        // Arrange
        var guid = _testArticle.Id;
        _articleStorageMock.Setup(x => x.GetById(guid)).Returns(_testArticle);

        // Act
        var result = _articleBusinessLogic.GetElementByData(guid);

        // Assert
        Assert.That(result, Is.EqualTo(_testArticle));
        _articleStorageMock.Verify(x => x.GetById(guid), Times.Once);
        _articleStorageMock.Verify(x => x.GetByName(It.IsAny<string>()), Times.Never);
    }

    [Test]
    
    public void GetElementByData_WithGuid_CallsGetByNameAndReturnsResult()
    {
        // Arrange
        var name = _testArticle.Name;
        _articleStorageMock.Setup(x => x.GetByName(name)).Returns(_testArticle);

        // Act
        var result = _articleBusinessLogic.GetElementByData(name);

        // Assert
        Assert.That(result, Is.EqualTo(_testArticle));
        _articleStorageMock.Verify(x => x.GetByName(name), Times.Once);
        _articleStorageMock.Verify(x => x.GetById(It.IsAny<string>()), Times.Never);
    }

    [Test]
    public void GetElementByData_WithEmptyData_ThrowsArgumentNullException()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() =>
            _articleBusinessLogic.GetElementByData(string.Empty));
    }

    [Test]
    public void GetElementByData_WithNonExistentGuid_ThrowsElementNotFoundException()
    {
        // Arrange
        var guid = Guid.NewGuid().ToString();
        _articleStorageMock.Setup(x => x.GetById(guid)).Returns(((ArticleDataModel?)null)!);

        // Act & Assert
        Assert.Throws<ElementNotFoundException>(() =>
            _articleBusinessLogic.GetElementByData(guid));
    }
    [Test]
    public void GetElementByData_WithNonExistentName_ThrowsElementNotFoundException()
    {
        // Arrange
        var name = "name";
        _articleStorageMock.Setup(x => x.GetByName(name)).Returns(((ArticleDataModel?)null)!);

        // Act & Assert
        Assert.Throws<ElementNotFoundException>(() =>
            _articleBusinessLogic.GetElementByData(name));
    }

    [Test]
    public void InsertElement_WithValidData_CallsCreateOnStorage()
    {
        // Act
        _articleBusinessLogic.InsertElement(_testArticle);

        // Assert
        _articleStorageMock.Verify(x => x.Create(_testArticle), Times.Once);
    }

    [Test]
    public void InsertElement_WithNullData_ThrowsArgumentNullException()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() =>
            _articleBusinessLogic.InsertElement(null!));
    }

    [Test]
    public void InsertElement_WithInvalidData_ThrowsValidationException()
    {
        // Arrange
        var invalidArticle = new ArticleDataModel(
            string.Empty,
            "WarehouseName",
            "Иванов Иван Иванович",
            DateTime.UtcNow.AddDays(-1));

        // Act & Assert
        Assert.Throws<ValidationException>(() =>
            _articleBusinessLogic.InsertElement(invalidArticle));

        _articleStorageMock.Verify(x => x.Create(It.IsAny<ArticleDataModel>()), Times.Never);
    }

    [Test]
    public void UpdateElement_WithValidData_CallsUpdateOnStorage()
    {
        // Act
        _articleBusinessLogic.UpdateElement(_testArticle);

        // Assert
        _articleStorageMock.Verify(x => x.Update(_testArticle), Times.Once);
    }

    [Test]
    public void UpdateElement_WithNullData_ThrowsArgumentNullException()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() =>
            _articleBusinessLogic.UpdateElement(null!));
    }

    [Test]
    public void UpdateElement_WithInvalidData_ThrowsValidationException()
    {
        // Arrange
        var invalidWarehouse = new ArticleDataModel(
            "id",
            "Test Warehouse",
            "Иванов Иван Иванович",
            DateTime.UtcNow.AddDays(-1));

        // Act & Assert
        Assert.Throws<ValidationException>(() =>
            _articleBusinessLogic.UpdateElement(invalidWarehouse));

        _articleStorageMock.Verify(x => x.Create(It.IsAny<ArticleDataModel>()), Times.Never);
    }

    [Test]
    public void DeleteElement_WithValidGuid_CallsDeleteOnStorage()
    {
        // Arrange
        var guid = Guid.NewGuid().ToString();

        // Act
        _articleBusinessLogic.DeleteElement(guid);

        // Assert
        _articleStorageMock.Verify(x => x.Delete(guid), Times.Once);
    }

    [Test]
    public void DeleteElement_WithEmptyId_ThrowsArgumentNullException()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() =>
            _articleBusinessLogic.DeleteElement(string.Empty));
    }

    [Test]
    public void DeleteElement_WithInvalidGuid_ThrowsArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            _articleBusinessLogic.DeleteElement("id"));

        _articleStorageMock.Verify(x => x.Delete(It.IsAny<string>()), Times.Never);
    }
}
