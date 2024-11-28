using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExampleProject;

namespace ExampleProject.Tests
{
    [TestClass]
    public class ListItemTests
    {
        [TestMethod]
        public void BaseListEntry_IsNamed_ReturnsTrue()
        {
            // Arrange
            var entry = new BaseListEntry();

            // Act
            // default name is "!"
            var result = entry.IsNamed("!"); 
            var resultCorrect = entry.IsNamed("default name!");
            // Assert
            Assert.IsFalse(result);
            Assert.IsTrue(resultCorrect);
        }

        [TestMethod]
        public void BaseListEntry_IsMy_ReturnsTrueForMatchingName()
        {
            // Arrange
            var entry = new BaseListEntry { ItemName = "myItem" };

            // Act
            var result = entry.IsMy();

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void BaseListEntry_DefaultConstructor_SetsDefaultValues()
        {
            // Arrange
            var entry = new BaseListEntry();

            // Act & Assert
            Assert.AreEqual("default name!", entry.ItemName);
            Assert.AreEqual("default Id", entry.Id);
            Assert.AreEqual(ExampleItemStatus.UNDEF, entry.ItemStatus);
        }

        [TestMethod]
        public async Task BaseListEntry_Create_ReturnsList()
        {
            // Arrange
            var entry = new BaseListEntry();

            // Act
            var result = await entry.Create();

            // Assert
            Assert.IsNotNull(result);
            CollectionAssert.Contains(result, "base");
        }

        [TestMethod]
        public void ExampleListEntry_IdProperty_PrependsMy()
        {
            // Arrange
            var entry = new ExampleListEntry { Id = "123" };

            // Act
            var result = entry.Id;

            // Assert
            Assert.AreEqual("my123", result);
        }

        [TestMethod]
        public void ExampleListEntry_DefaultConstructor_SetsDefaultValues()
        {
            // Arrange
            var entry = new ExampleListEntry();

            // Act & Assert
            Assert.AreEqual("default name!", entry.ItemName);
            Assert.AreEqual("my", entry.Id);
            Assert.AreEqual(ExampleItemStatus.UNDEF, entry.ItemStatus);
        }

        [TestMethod]
        public void ExampleListEntry_SomethingElseProperty_SetAndGet()
        {
            // Arrange
            var entry = new ExampleListEntry { SomethingElse = "TestValue" };

            // Act
            var result = entry.SomethingElse;

            // Assert
            Assert.AreEqual("TestValue", result);
        }

        [TestMethod]
        public void ExampleListEntry_ToString_ReturnsExpectedString()
        {
            // Arrange
            var entry = new ExampleListEntry { Id = "123", ItemName = "TestItem", ItemStatus = ExampleItemStatus.SELECTED };

            // Act
            var result = entry.ToString();

            // Assert
            Assert.AreEqual("my123, TestItem (Id overriden/IsMy: True), SELECTED", result);
        }
    }
}
