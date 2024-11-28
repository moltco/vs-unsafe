using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExampleProject;

namespace ExampleProject.Tests
{
    [TestClass]
    public class ListManagerTests
    {
        [TestMethod]
        public async Task ListManagerObject_Clear_ClearsRecords()
        {
            // Arrange
            var manager = new TestListManagerObject();
            manager.Records.Add(new ExampleListEntry());

            // Act
            await manager.Clear();

            // Assert
            Assert.AreEqual(0, manager.Records.Count);
        }

        [TestMethod]
        public void ListManagerObject_FiltersApply_ReturnsFilteredRecords()
        {
            // Arrange
            var manager = new TestListManagerObject();

            // FiltersApply will filter by ItemName == "filterMe"
            manager.Records.Add(new ExampleListEntry { Id="my", ItemName = "filterMe" });
            manager.Records.Add(new ExampleListEntry { Id="other", ItemName = "otherItem" });

            // Act
            // FiltersApply will filter by ItemName == "filterMe"
            var result = manager.FiltersApply();

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("filterMe", result.First().ItemName);
        }

        [TestMethod]
        public void ListManagerObject_SelectRecord_SetsItemStatusToSelected()
        {
            // Arrange
            var manager = new TestListManagerObject();
            var entry = new ExampleListEntry();

            // Act
            manager.SelectRecord(entry);

            // Assert
            Assert.AreEqual(ExampleItemStatus.SELECTED, entry.ItemStatus);
        }

        [TestMethod]
        public void ListManagerObject_GetBindingSource_ReturnsBindingSource()
        {
            // Arrange
            var manager = new TestListManagerObject();

            // Act
            var bindingSource = manager.GetBindingSource();

            // Assert
            Assert.IsNotNull(bindingSource);
            Assert.AreEqual(manager.Records, bindingSource.DataSource);
        }

        [TestMethod]
        public void ListManagerObject_IsCurrentRecordSelected_ReturnsTrue()
        {
            // Arrange
            var manager = new TestListManagerObject();
            var entry = new ExampleListEntry { ItemStatus = ExampleItemStatus.SELECTED };
            manager.Records.Add(entry);

            // Act
            var result = manager.IsCurrentRecordSelected();

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task ListManagerObject_UndoPop_ReturnsTrue()
        {
            // Arrange
            var manager = new TestListManagerObject();
            var entry = new ExampleListEntry();

            // Act
            var result = await manager.UndoPop(entry);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ListManagerObject_UndoPush_DoesNotThrowException()
        {
            // Arrange
            var manager = new TestListManagerObject();
            var entry = new ExampleListEntry();

            // Act & Assert
            manager.UndoPush(entry);
        }

        [TestMethod]
        public void ExampleListManager_RecordsProperty_ReturnsBindingList()
        {
            // Arrange
            var manager = new ExampleListManager();

            // Act
            var records = manager.Records;

            // Assert
            Assert.IsNotNull(records);
            Assert.IsInstanceOfType(records, typeof(BindingList<ExampleListEntry>));
        }

        // Helper class for testing ListManagerObject
        private class TestListManagerObject : ListManagerObject<ExampleListEntry>
        {
            public override BindingList<ExampleListEntry> Records { get; set; } = new BindingList<ExampleListEntry>();
        }
    }
}
