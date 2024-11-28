using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExampleProject
{

    #region Interfaces
    // Enums
    public enum ExampleItemStatus { UNDEF, SELECTED }

    // Interfaces
    public interface IListEntry: IFilterable, ISelectable
    {
        string Id { get; set; }
        ExampleItemStatus ItemStatus { get; set; }
        string ItemName { get; set; }
        bool IsNamed(string name);
        bool IsMy();
        Task<List<string>> Create();
    }

  

    public interface IFilterable { }
    public interface ISelectable { }

    public interface ISomethingElse { } 
    public interface ISomethingElse2 { 
        string SomethingElse { get; set; }
    }
    #endregion

    #region Classes
    // Classess

    /// <summary>
    /// Provides default implementation for a list entry (record)
    /// </summary>
    public class BaseListEntry: IListEntry, IFilterable, ISelectable
    {
        public BaseListEntry() { ItemName = ItemName + "!"; }
        public virtual string Id { get; set; } = "default Id";
        public virtual string ItemName { get; set; } = "default name";
        public virtual ExampleItemStatus ItemStatus { get; set;  }

        public virtual bool IsNamed(string name) => ItemName == name; 
        public virtual bool IsMy() => Id.StartsWith("my");
        public virtual async Task<List<string>> Create() { await Task.CompletedTask; return new List<string>() { "base" }; }
    }


    /// <summary>
    /// Extends BaseRecord and it's interfaces but also some additional 
    /// interfaces 'SomethingElse' and 'SomethingElse2'.
    /// Overrides Id property to prepend 'my' to the value.
    /// </summary>
    public class ExampleListEntry :
        BaseListEntry,
        IListEntry,
        IFilterable, ISelectable, 
        ISomethingElse, ISomethingElse2
    {
        public ExampleListEntry() : base() { }
        string _id = "";
        override public string Id { get => "my" + _id; set => _id = value; }
        //public ExampleItemStatus ItemStatus { get; set; }
        //public string ItemName { get; set; }

        public string SomethingElse { get; set; }

        public override string ToString() => Id + ", " + ItemName + $" (Id overriden/IsMy: {IsMy()}), " + ItemStatus;
    }
    #endregion
}
