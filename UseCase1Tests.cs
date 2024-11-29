using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExampleProject;
using System.ComponentModel;

namespace ExampleProject.Tests
{
    [TestClass]
    public class UseCase1Tests
    {
        [TestMethod]
        public void DoSomething_v1_ReturnsCorrectStringBuilder()
        {
            // Arrange
            var listManager1 = new ExampleListManager();
            var listManager2 = new ExampleListManager();
            var useCase = new UseCase1();
            
            // let's check access to the list
            // a) with casting here (needed in some variations of code)
            var newList = listManager1.Records as BindingList<ExampleListEntry>;
            Assert.AreEqual(3, newList.Count, "Cast Records count failed");

            // b) withouot casting (if cast is provided in the ListManagerWFO abstract class)
            Assert.AreEqual(3, listManager1.Records?.Count, "Original object's Records count failed");


            int cnt = 0;
            // if not cast exlicitly this may fail
            foreach (var entry in listManager1.Records)
            {
                entry.Id = cnt++.ToString();
            }

            foreach (var entry in listManager2.Records)
            {
                entry.Id = cnt++.ToString();
            }

            // Act
            StringBuilder result = useCase.DoSomething_v1(listManager1, listManager2);

            // Assert
            string expected = 
                    "my0, Name1 (Id overriden/IsMy: True), UNDEF" +
                    "my1, Name2 (Id overriden/IsMy: True), UNDEF" +
                    "my2, Name3 (Id overriden/IsMy: True), UNDEF" +
                    "my3, Name1 (Id overriden/IsMy: True), UNDEF" +
                    "my4, Name2 (Id overriden/IsMy: True), UNDEF" +
                    "my5, Name3 (Id overriden/IsMy: True), UNDEF";

            Assert.AreEqual(expected, result.ToString());
        }

        [TestMethod]
        public void DoSomething_v2_ReturnsCorrectCount()
        {
            // Arrange
            var listManager1 = new ExampleListManager();
            var listManager2 = new ExampleListManager();
            var useCase = new UseCase1();

            // let's check access to the list
            // a) with casting here (needed in some variations of code)
            var newList = listManager1.Records as BindingList<ExampleListEntry>;
            Assert.AreEqual(3, newList.Count, "Cast Records count failed");

            // b) withouot casting (if cast is provided in the ListManagerWFO abstract class)
            Assert.AreEqual(3, listManager1.Records?.Count, "Original object's Records count failed");
           
            // Act
            int result = useCase.DoSomething_v2(listManager1, listManager2);

            // Assert
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void TestDoSomehting_v1_DoesNotThrowException()
        {
            // Arrange
            var useCase = new UseCase1();

            // Act & Assert
            useCase.TestDoSomehting_v1();
        }
    }
}
