# vs-unsafe: sample code for Entry Point Exception troubleshooting

This solution/project is based on .NET481 and WinForms to mimic the 
environment where problem occurs - `Entry Point Not Found` Exception. 
Program.cs runs Form1 that onClick runs `UseCase1.DoSomething_v1()` 
where the example use case is illustrated.

In the `vs-unsafe` project there are 3 files, each containing multiple
interfaces and classes inside + 3 files with tests.

# ListItem.cs 
Interfaces and classes representing objects that are ultimately 
stored in a IListManager object. 

**ExampleListEntry** is the 'final' implementation in the chain of 
classes and interfaces that represent a list item.

Tests associated with these classes are in ListItemTests.cs

# ListManager.cs
Interfaces and classes representing a utility class that stores a 
BindingList of objects that implement the IListEntry interface and some
utility methods associated with this list.

The base classes aim to implement default functionality that can be overriden
by derived classes.

**ExampleListManager** is the 'final' implementation.

Tests for these classes are in ListManagerTests.cs

# UseCase1.cs

This demonstrates typical use of ExampleListManager and ExampleListEntry.

The `UseCase1.DoSomething_v1()` demonstrates where 
`Entry Point Not Found` exception occurs.